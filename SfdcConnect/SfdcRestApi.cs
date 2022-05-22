/****************************************************************************
*
*   File name: SfdcRestApi.cs
*   Author: Sean Fife
*   Create date: 4/20/2016
*   Solution: SfdcConnect
*   Project: SfdcConnect
*   Description: Includes the SfdcRestApi class for Salesforce REST Api Connections
*
****************************************************************************/
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SfdcConnect.Objects;
using SfdcConnect.SoapObjects;
using System.Web;
using System.Net.Sockets;

namespace SfdcConnect
{
    public enum Modified { ModifiedSince, UnmodifiedSince };
    public enum Match { IfMatch, IfNoneMatch };

    /// <summary>
    /// Salesforce REST Api connection class that uses SOAP to authenticate
    /// </summary>
    public class SfdcRestApi : SfdcConnection
    {
        public SfdcRestApi(string refreshToken = "")
            : base()
        {
            RefreshToken = refreshToken;
            lastStatusCode = HttpStatusCode.OK;
        }
        public SfdcRestApi(string uri, string refreshToken = "")
            : base(uri)
        {

            RefreshToken = refreshToken;
            lastStatusCode = HttpStatusCode.OK;
        }
        public SfdcRestApi(bool isTest, int apiversion, string refreshToken = "")
            : base(isTest, apiversion)
        {
            RefreshToken = refreshToken;
            lastStatusCode = HttpStatusCode.OK;
            Version = apiversion.ToString() + ".0";
        }

        #region Variables
        string baseUrl = "";
        HttpStatusCode lastStatusCode;
        ApiLimits apiLimits = null;

        /// <summary>
        /// OAuth Refresh Token
        /// </summary>
        public string RefreshToken { get; protected set; }
        /// <summary>
        /// Callback Endpoint for OAuth. If unset, it will generate an endpoint on http://127.0.0.1 for a random port.
        /// </summary>
        protected string CallbackEndpoint;
        /// <summary>
        /// Determines which OAuth flow is being used
        /// </summary>
        protected LoginFlow Flow;
        /// <summary>
        /// OAuth ClientId
        /// </summary>
        public string ClientId { get; set; }
        /// <summary>
        /// OAuth ClientSecret
        /// </summary>
        public string ClientSecret { get; set; }
        /// <summary>
        /// List of available endpoints for the REST API
        /// </summary>
        public AvailableResources AvailableResources { get; private set; }
        #endregion

        public override void Open()
        {
            Open(LoginFlow.SOAP);
        }
        /// <summary>
        /// Open a connection to Salesforce.  Requires Username and Password to be filled in.
        /// </summary>
        public void Open(LoginFlow flowType = LoginFlow.SOAP, string state = null)
        {
            Flow = flowType;
            if (flowType == LoginFlow.SOAP)
            {
                base.Open();
                baseUrl = "https://" + ApiEndPoint.Host;
            }
            else if (flowType == LoginFlow.OAuthPassword)
            {
                OAuthPassword(state);
            }
            else
            {
                OAuthAuthorize(state);
            }

        }
        /// <summary>
        /// Open a connection to Salesforce asynchronously.  Requires Usernamd and Password to be filled in.
        /// </summary>
        public override void OpenAsync()
        {
            if (state == ConnectionState.Open) return;
            state = ConnectionState.Connecting;

            Flow = LoginFlow.SOAP;
            loginCompleted += ls_restloginCompleted;
            LoginAsync(Username, Password + Token);

            //return default(Task);
        }
        /// <summary>
        /// Open a connection to Salesforce asynchronously.  Requires Usernamd and Password to be filled in.
        /// </summary>
        public override async Task OpenAsync(CancellationToken cancellationToken)
        {
            if (state == ConnectionState.Open) return;
            state = ConnectionState.Connecting;

            Flow = LoginFlow.SOAP;
            loginCompleted += ls_restloginCompleted;
            await LoginAsync(Username, Password + Token, cancellationToken);
        }

        /// <summary>
        /// Callback for OpenAsync calls
        /// </summary>
        /// <param name="sender">should be this object</param>
        /// <param name="e">Event Args holding the login information from Salesforce</param>
        private void ls_restloginCompleted(object sender, loginCompletedEventArgs e)
        {
            LoginTime = DateTime.Now;
            Url = e.Result.serverUrl;
            SessionHeaderValue = new SessionHeader();
            SessionHeaderValue.sessionId = e.Result.sessionId;

            SessionId = e.Result.sessionId;
            ServerUrl = e.Result.serverUrl;

            string[] pieces = ServerUrl.Split('/');

            Version = pieces[pieces.Length - 2];

            ApiEndPoint = new Uri(e.Result.serverUrl);

            baseUrl = ApiEndPoint.Scheme + "://" + ApiEndPoint.Host;

            state = ConnectionState.Open;
        }

        /// <summary>
        /// Closes the connection based on the Flow used
        /// </summary>
        public override void Close()
        {
            if (Flow == LoginFlow.SOAP)
            {
                base.Close();
            }
            else
            {
                OAuthRequest request = new OAuthRequest();
                request.token = SessionId;

                string req = request.ToString();
                HttpWebRequest webrequest = (HttpWebRequest)HttpWebRequest.Create(string.Format("{0}/services/oauth2/revoke", baseUrl));
                webrequest.Method = "POST";
                webrequest.ContentType = "application/x-www-form-urlencoded";
                webrequest.Accept = "application/json";//text/html,application/xhtml+xml,application/xml,

                byte[] buffer = Encoding.UTF8.GetBytes(req);

                Stream postStream = webrequest.GetRequestStream();
                postStream.Write(buffer, 0, buffer.Length);

                OAuthResponse returnValue = null;
                using (HttpWebResponse response = (HttpWebResponse)webrequest.GetResponse())
                {
                    lastStatusCode = response.StatusCode;
                    using (StreamReader resp = new StreamReader(response.GetResponseStream()))
                    {
                        returnValue = JsonConvert.DeserializeObject<OAuthResponse>(resp.ReadToEnd());

                        this.state = ConnectionState.Closed;
                    }
                }

                this.state = ConnectionState.Closed;
            }
        }

        /// <summary>
        /// Initiates an OAuth Password flow
        /// </summary>
        /// <param name="state">State to be passed to the OAuth endpoint</param>
        /// <returns>token</returns>
        private string OAuthPassword(string state = null)
        {
            OAuthRequest request = new OAuthRequest();
            request.client_id = ClientId;
            request.client_secret = ClientSecret;
            request.username = Username;
            request.password = Password + Token;
            request.grant_type = "password";
            request.state = state;

            string req = request.ToString();
            Uri endpoint = new Uri(Url);
            HttpWebRequest webrequest = (HttpWebRequest)HttpWebRequest.Create(string.Format("{0}://{1}/services/oauth2/token", endpoint.Scheme, endpoint.Host));
            webrequest.Method = "POST";
            webrequest.ContentType = "application/x-www-form-urlencoded";
            webrequest.Accept = "application/json";//text/html,application/xhtml+xml,application/xml,

            byte[] buffer = Encoding.UTF8.GetBytes(req);

            Stream postStream = webrequest.GetRequestStream();
            postStream.Write(buffer, 0, buffer.Length);

            OAuthResponse returnValue = null;
            using (HttpWebResponse response = (HttpWebResponse)webrequest.GetResponse())
            {
                lastStatusCode = response.StatusCode;
                using (StreamReader resp = new StreamReader(response.GetResponseStream()))
                {
                    returnValue = JsonConvert.DeserializeObject<OAuthResponse>(resp.ReadToEnd());
                    this.state = ConnectionState.Open;
                    //TODO check signature, check that the response was good or bad


                    SessionId = returnValue.access_token;
                    ServerUrl = returnValue.instance_url;
                    ApiEndPoint = new Uri(returnValue.instance_url);
                    baseUrl = "https://" + ApiEndPoint.Host;
                    RefreshToken = returnValue.refresh_token;
                    this.state = ConnectionState.Open;
                    LoginTime = DateTime.Now;
                }
            }
            return SessionId;
        }

        /// <summary>
        /// Initiates an OAuth authorize flow
        /// </summary>
        /// <param name="originalstate">State to be passed to the OAuth endpoint</param>
        /// <returns>token</returns>
        private string OAuthAuthorize(string originalstate = null)
        {
            if (!string.IsNullOrEmpty(RefreshToken))
            {
                OAuthRefreshToken();
                return SessionId;
            }
            else
            {
                //then this call should work. and this should open a browser
                OAuthRequest request = new OAuthRequest();
                request.client_id = ClientId;
                request.client_secret = ClientSecret;
                if (string.IsNullOrEmpty(CallbackEndpoint))
                {
                    request.redirect_uri = string.Format("http://{0}:{1}/", IPAddress.Loopback.ToString(), FindAvailablePort());
                }
                else
                {
                    request.redirect_uri = CallbackEndpoint;
                }
                request.response_type = "code";
                request.state = originalstate;

                OAuthAuthorize(request);
                return SessionId;
            }
        }

        /// <summary>
        /// Initiates an OAuth authorize flow
        /// </summary>
        /// <param name="request">Class that contains the appropriate vlaues in the OAuthRequest parameter</param>
        /// <returns>token</returns>
        private string OAuthAuthorize(OAuthRequest request)
        {
            if (request.response_type != "code")
            {
                return "Invalid response type: " + request.response_type + ", should be 'code'";
            }

            if (!HttpListener.IsSupported)
            {
                return "HttpListener is not supported";
            }

            HttpListener server = new HttpListener();
            server.Prefixes.Add(request.redirect_uri);
            server.Start();

            //Open the browser to the Auth endpoint
            //TODO Make this process start async
            Uri endpoint = new Uri(Url);
            System.Diagnostics.Process.Start(string.Format("{0}://{1}/services/oauth2/authorize?{2}", endpoint.Scheme, endpoint.Host, request.ToString()));

            // Waits for the the user to go through and approve the app
            HttpListenerContext context = server.GetContext();

            HttpListenerResponse response = context.Response;
            string basicPage = "<html><head></head><body>{0}</body></html>";
            string responsePage = "";

            // get the code and save the state, if it exists
            string code = context.Request.QueryString.Get("code");
            string state = context.Request.QueryString.Get("state");

            responsePage = string.Format(basicPage, "You can close this browser and return to the app");
            // Checks for errors.
            string error = "";
            if (context.Request.QueryString.Get("error") != null)
            {
                responsePage = string.Format(basicPage, "There was an error with your request.");
                error = context.Request.QueryString.Get("error");
            }
            if (code == null || state == null)
            {
                responsePage = string.Format(basicPage, "We were unable to authorize you at this time.");
                error = "Error: Bad code or state";
            }
            if (state != request.state)
            {
                responsePage = string.Format(basicPage, "There was a problem with your request.");
                error = "Bad state";
            }

            //Show the page to the user and shut down the temporary http server
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responsePage);
            response.ContentLength64 = buffer.Length;
            response.OutputStream.Write(buffer, 0, buffer.Length);
            response.OutputStream.Flush();
            response.Close();
            server.Stop();

            if (error != "")
            {
                this.state = ConnectionState.Closed;
                return error;
            }

            //do the code exchange: code -> token
            OAuthResponse resp = OAuthCodeToToken(code, request.redirect_uri);
            SessionId = resp.access_token;
            ServerUrl = resp.instance_url;
            ApiEndPoint = new Uri(resp.instance_url);
            baseUrl = "https://" + ApiEndPoint.Host;
            RefreshToken = resp.refresh_token;
            this.state = ConnectionState.Open;
            LoginTime = DateTime.Now;

            return SessionId;
        }

        /// <summary>
        /// Converts and OAuth code to a token. This is part of the OAuth Authorize flow
        /// </summary>
        /// <param name="code">Code from OAuth Authorize call</param>
        /// <param name="oauthCallback">Callback URL used from the OAuth Authorize call</param>
        /// <returns>OAuthResponse of the converted code to token</returns>
        private OAuthResponse OAuthCodeToToken(string code, string oauthCallback)
        {
            OAuthRequest request = new OAuthRequest();
            request.grant_type = "authorization_code";
            request.client_id = ClientId;
            request.code = code;
            request.redirect_uri = oauthCallback;

            Uri endpoint = new Uri(Url);
            HttpWebRequest webrequest = (HttpWebRequest)HttpWebRequest.Create(string.Format("{0}://{1}/services/oauth2/token", endpoint.Scheme, endpoint.Host));
            webrequest.Method = "POST";
            webrequest.ContentType = "application/x-www-form-urlencoded";
            webrequest.Accept = "application/json";//text/html,application/xhtml+xml,application/xml,

            byte[] buffer = Encoding.UTF8.GetBytes(request.ToString());

            Stream postStream = webrequest.GetRequestStream();
            postStream.Write(buffer, 0, buffer.Length);

            OAuthResponse returnValue = null;
            using (HttpWebResponse response = (HttpWebResponse)webrequest.GetResponse())
            {
                lastStatusCode = response.StatusCode;
                using (StreamReader resp = new StreamReader(response.GetResponseStream()))
                {
                    returnValue = JsonConvert.DeserializeObject<OAuthResponse>(resp.ReadToEnd());

                    //TODO check signature

                }
            }
            return returnValue;
        }

        /// <summary>
        /// Gets a new access token from an OAuth refresh token
        /// </summary>
        /// <returns>OAuth refresh response</returns>
        private OAuthResponse OAuthRefreshToken()
        {
            OAuthRequest request = new OAuthRequest();
            request.grant_type = "refresh_token";
            request.client_id = ClientId;
            request.client_secret = ClientSecret;
            request.refresh_token = RefreshToken;

            Uri endpoint = new Uri(Url);
            HttpWebRequest webrequest = (HttpWebRequest)HttpWebRequest.Create(string.Format("{0}://{1}/services/oauth2/token", endpoint.Scheme, endpoint.Host));
            webrequest.Method = "POST";
            webrequest.ContentType = "application/x-www-form-urlencoded";
            webrequest.Accept = "application/json";//text/html,application/xhtml+xml,application/xml,

            byte[] buffer = Encoding.UTF8.GetBytes(request.ToString());

            Stream postStream = webrequest.GetRequestStream();
            postStream.Write(buffer, 0, buffer.Length);

            OAuthResponse returnValue = null;
            using (HttpWebResponse response = (HttpWebResponse)webrequest.GetResponse())
            {
                lastStatusCode = response.StatusCode;
                using (StreamReader resp = new StreamReader(response.GetResponseStream()))
                {
                    returnValue = JsonConvert.DeserializeObject<OAuthResponse>(resp.ReadToEnd());
                    this.state = ConnectionState.Open;
                    //TODO check signature, check that the response was good or bad
                    SessionId = returnValue.access_token;
                    ServerUrl = returnValue.instance_url;
                    ApiEndPoint = new Uri(returnValue.instance_url);
                    baseUrl = "https://" + ApiEndPoint.Host;
                    LoginTime = DateTime.Now;
                }
            }
            return returnValue;
        }

        /// <summary>
        /// Finds an open port on the local computer
        /// </summary>
        /// <returns>Port number</returns>
        private int FindAvailablePort()
        {
            //There has to be a better way to do this...
            TcpListener server = new TcpListener(IPAddress.Parse("127.0.0.1"), 0);

            server.Start();

            int port = ((IPEndPoint)server.LocalEndpoint).Port;

            server.Stop();

            return port;
        }









        public string GenerateMatchHeader(string[] etag, Match matchType = Match.IfMatch)
        {
            string header = (matchType == Match.IfMatch ? "If-Match: " : "If-None-Match: ");
            List<string> fullEtags = new List<string>();
            foreach(string s in etag)
            {
                fullEtags.Add($"\"{s}--gzip\"");
            }
            return header + string.Join(",", fullEtags);
        }

        public string GenerateModifiedHeader(DateTime date, Modified modifiedtype = Modified.ModifiedSince)
        {
            return (modifiedtype == Modified.ModifiedSince ? "If-Modified-Since: " : "If-Unmodified-Since: ") + ToSalesforceAPIHeaderDateTimeString(date);//.ToString("ddd, dd MMM yyyy HH:mm:ss z");
        }

        /// <summary>
        /// Gets a list of the available services for a Salesforce Org. Essentialy the API version numbers
        /// </summary>
        /// <param name="automaticallySetToMostRecentVersion">If true, will automatically set the Version parameter to be the most recent API version available</param>
        /// <returns>List of available services</returns>
        public List<RestServices> GetServices(bool automaticallySetToMostRecentVersion = true)
        {
            string url = baseUrl + string.Format("/services/data/");
            List<RestServices> returnValue = null;

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Method = "GET";
            request.Accept = "application/json";

            request.Headers.Add("Authorization: Bearer " + SessionId);

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                lastStatusCode = response.StatusCode;
                using (StreamReader resp = new StreamReader(response.GetResponseStream()))
                {
                    returnValue = JsonConvert.DeserializeObject<List<RestServices>>(resp.ReadToEnd());
                    if (automaticallySetToMostRecentVersion) Version = returnValue[returnValue.Count - 1].version;
                }
            }

            return returnValue;
        }

        /// <summary>
        /// Gets a list of available endpoints for a given API Version (in the Version variable)
        /// </summary>
        /// <returns>List of available resources in a Dictionary</returns>
        public AvailableResources GetAvilableResources()
        {
            string url = baseUrl + string.Format("/services/data/v{0}", Version);
            AvailableResources returnValue = null;

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Method = "GET";
            request.Accept = "application/json";

            request.Headers.Add("Authorization: Bearer " + SessionId);

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                lastStatusCode = response.StatusCode;
                using (StreamReader resp = new StreamReader(response.GetResponseStream()))
                {
                    AvailableResources = JsonConvert.DeserializeObject<AvailableResources>(resp.ReadToEnd());
                    this.MetadataEndPoint = new Uri(AvailableResources["metadata"]);
                    returnValue = AvailableResources;
                }
            }

            return returnValue;// JsonConvert.DeserializeObject<AvailableResources>(StandardAPICallout(url, "GET", string.Empty));
        }

        /// <summary>
        /// Gets the current API limts
        /// </summary>
        /// <param name="refresh">If true, it will do the callout, if false it will return the results from the last call</param>
        /// <returns>ApiLimits object containing the API limits</returns>
        public ApiLimits GetLimits(bool refresh = true)
        {
            if (refresh)
            {
                string url = baseUrl + string.Format("/limits/", Version);

                apiLimits = StandardAPICallout<ApiLimits>(url, "GET", string.Empty);
            }

            return apiLimits;
        }

        public Objects.DescribeGlobalResult GetSObjects(DateTime date, Modified modifiedtype = Modified.ModifiedSince)
        {
            string header = (modifiedtype == Modified.ModifiedSince?"If-Modified-Since: " : "If-Unmodified-Since: ") + ToSalesforceAPIHeaderDateTimeString(date);//.ToString("ddd, dd MMM yyyy HH:mm:ss z");

            string whatsChanged = StandardAPICallout("sobjects", "GET", string.Empty, new string[] { header });

            if (lastStatusCode == HttpStatusCode.NotModified)
            {
                //nothing changed
                return null;
            }
            else
            {
                //something changed
                //deserialize response.
                return JsonConvert.DeserializeObject<Objects.DescribeGlobalResult>(whatsChanged);
            }
        }

        public Objects.DescribeGlobalResult GetSObjects()
        {
            return StandardAPICallout<Objects.DescribeGlobalResult>("sobjects", "GET", string.Empty);
        }

        public Objects.GetSObjectResult GetSObject(string objectApiName)
        {
            string url = string.Format("sobjects/{0}", objectApiName);

            return StandardAPICallout<Objects.GetSObjectResult>(url, "GET", string.Empty);
        }

        /// <summary>
        /// Gets the description of an Salesforce Object
        /// </summary>
        /// <param name="objectApiName">API name of the object to describe</param>
        /// <param name="additionalHeaders">(optional) If-Modified-Since or If-Unmodified-Since header</param>
        /// <returns>DescribeSObjectResult for the object or null depending on the additional header (e.g. null if no modifications since when using If-Modified-since)</returns>
        public Objects.DescribeSObjectResult DescribeSObject(string objectApiName, string[] additionalHeaders = null)
        {
            string url = string.Format("sobjects/{0}/describe", objectApiName);

            string whatsChanged = StandardAPICallout(url, "GET", string.Empty, additionalHeaders);

            if (lastStatusCode == HttpStatusCode.NotModified)
            {
                //nothing changed
                return null;
            }
            else
            {
                //something changed
                //deserialize response.
                return JsonConvert.DeserializeObject<Objects.DescribeSObjectResult>(whatsChanged);
            }
        }

        //public Objects.DescribeSObjectResult DescribeSObject(string objectApiName, DateTime date, Modified modifiedtype = Modified.ModifiedSince)
        //{
        //    string header = (modifiedtype == Modified.ModifiedSince ? "If-Modified-Since: " : "If-Unmodified-Since: ") + ToSalesforceAPIHeaderDateTimeString(date);//.ToString("ddd, dd MMM yyyy HH:mm:ss z");
        //    string url = string.Format("sobjects/{0}/describe", objectApiName);

        //    string whatsChanged = StandardAPICallout(url, "GET", string.Empty, new string[] { header });

        //    if (lastStatusCode == HttpStatusCode.NotModified)
        //    {
        //        //nothing changed
        //        return null;
        //    }
        //    else
        //    {
        //        //something changed
        //        //deserialize response.
        //        return JsonConvert.DeserializeObject<Objects.DescribeSObjectResult>(whatsChanged);
        //    }
        //}

        public Objects.GetDeletedResult GetDeleted(string objectApiName, DateTime startDate, DateTime endDate)
        {
            string url = string.Format("sobjects/{0}/deleted?start={1}&end={2}", objectApiName, HttpUtility.UrlEncode(ToSalesforceAPIQueryDateTimeString(startDate)), HttpUtility.UrlEncode(ToSalesforceAPIQueryDateTimeString(endDate)));

            return StandardAPICallout<Objects.GetDeletedResult>(url, "GET", string.Empty);
        }

        public Objects.GetUpdatedResult GetUpdated(string objectApiName, DateTime startDate, DateTime endDate)
        {
            string url = string.Format("sobjects/{0}/updated?start={1}&end={2}", objectApiName, HttpUtility.UrlEncode(ToSalesforceAPIQueryDateTimeString(startDate)), HttpUtility.UrlEncode(ToSalesforceAPIQueryDateTimeString(endDate)));

            return StandardAPICallout<Objects.GetUpdatedResult>(url, "GET", string.Empty);
        }

        public Objects.SaveResult CreateRecord(string objectApiName, string recordJson)
        {
            string url = string.Format("sobjects/{0}", objectApiName);

            return StandardAPICallout<Objects.SaveResult>(url, "POST", recordJson);
        }

        /// <summary>
        /// Gets a record from Salesforce
        /// </summary>
        /// <param name="objectApiName">API name of the Salesforce object</param>
        /// <param name="id">Id of the record. Either Salesforce Id or external Id</param>
        /// <param name="fields">(optional) list of fields to retreive, pass null if not needed</param>
        /// <param name="fieldApiName">(optional) API name of the external id field to serch with, pass string.Empty or null if not needed</param>
        /// <param name="additionalHeaders">(optional) If-Modified-Since or If-Unmodified-Since header and/or, if an Account, If-Match or If-None-Match headers</param>
        /// <returns>sObject</returns>
        public Objects.sObject GetRecord(string objectApiName, string id, string[] fields = null, string fieldApiName = null, string[] additionalHeaders = null)
        {
            string url = "";
            if (string.IsNullOrEmpty(fieldApiName))
            {
               url = string.Format("sobjects/{0}/{1}", objectApiName, id);
            }
            else
            {
                url = string.Format("sobjects/{0}/{1}/{2}", objectApiName, fieldApiName, id);
            }
            if(fields != null && fields.Length > 0)
            {
                url += "?fields=" + string.Join(",", fields);
            }

            string result = StandardAPICallout(url, "GET", string.Empty, additionalHeaders);
            Objects.sObject r = JsonConvert.DeserializeObject<Objects.sObject>(result);

            return r;
        }

        /// <summary>
        /// Gets the value of a Blobl. Blob fields can only be retrieved by specifically calling for them.
        /// </summary>
        /// <param name="objectApiName">API name of the Salesforce object</param>
        /// <param name="id">Id of the record. Either Salesforce Id or external Id</param>
        /// <param name="blobFieldName">Name of the Blob field to query</param>
        /// <returns>Binary blob data</returns>
        public string GetBlobField(string objectApiName, string id, string blobFieldName)
        {
            string url = string.Format("sobjects/{0}/{1}/{2}", objectApiName, id, blobFieldName);

            return StandardAPICallout(url, "GET", string.Empty);
        }

        /// <summary>
        /// Updates a record in Salesforce
        /// </summary>
        /// <param name="objectApiName">API name of the Salesforce object</param>
        /// <param name="id">Id of the record. Either Salesforce Id or external Id</param>
        /// <param name="recordJson">Json of the fields to update</param>
        /// <param name="additionalHeaders">(optional) If-Modified-Since or If-Unmodified-Since header and/or, if an Account, If-Match or If-None-Match headers</param>
        /// <returns>SaveResult</returns>
        public Objects.SaveResult UpdateRecord(string objectApiName, string id, string recordJson, string[] additionalHeaders = null)
        {
            //TODO external field update
            string url = string.Format("sobjects/{0}/{1}", objectApiName, id);

            return StandardAPICallout<Objects.SaveResult>(url, "PATCH", recordJson, additionalHeaders);
        }

        /// <summary>
        /// Updates a record in Salesforce
        /// </summary>
        /// <param name="objectApiName">API name of the Salesforce object</param>
        /// <param name="id">Id of the record. Either Salesforce Id or external Id</param>
        /// <param name="fieldApiName">API name of the field to upsert on</param>
        /// <param name="recordJson">Json of the fields to update/insert</param>
        /// <returns>UpsertResult</returns>
        public Objects.UpsertResult UpsertRecord(string objectApiName, string id, string fieldApiName, string recordJson)
        {
            string url = string.Format("sobjects/{0}/{1}/{2}", objectApiName, fieldApiName, id);

            return StandardAPICallout<Objects.UpsertResult>(url, "PATCH", recordJson);
        }

        /// <summary>
        /// Deletes a record in Salesforce
        /// </summary>
        /// <param name="objectApiName">API name of the Salesforce object</param>
        /// <param name="id">Id of the record. Either Salesforce Id or external Id</param>
        /// <param name="additionalHeaders">(optional) If-Modified-Since or If-Unmodified-Since header and/or, if an Account, If-Match or If-None-Match headers</param>
        /// <returns>DeleteResult</returns>
        public Objects.DeleteResult DeleteRecord(string objectApiName, string id, string[] additionalHeaders = null)
        {
            string url = string.Format("sobjects/{0}/{1}", objectApiName, id);

            return StandardAPICallout<Objects.DeleteResult>(url, "DELETE", string.Empty, additionalHeaders);
        }

        public Objects.QueryResult Query(string query)
        {
            string url = string.Format("query?q={0}", HttpUtility.UrlEncode(query));

            return StandardAPICallout<Objects.QueryResult>(url, "GET", string.Empty);
        }

        public Objects.QueryResult QueryAll(string query)
        {
            string url = string.Format("queryAll?q={0}", HttpUtility.UrlEncode(query));

            return StandardAPICallout<Objects.QueryResult>(url, "GET", string.Empty);
        }

        public Objects.QueryResult QueryMore(string query)
        {
            return StandardAPICallout<Objects.QueryResult>(query, "GET", string.Empty, string.Empty, null);
        }



        public void AsyncQuery()
        {
            throw new NotImplementedException();
            //must use big (__b) objects
            //https://developer.salesforce.com/docs/atlas.en-us.bigobjects.meta/bigobjects/async_query_running_queries.htm
            //https://developer.salesforce.com/docs/atlas.en-us.bigobjects.meta/bigobjects/async_query_reference.htm
        }

        public void AsyncQueryCancel(string jobId)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Gets the valid format for header datetimes for the REST API
        /// </summary>
        /// <param name="dt">DateTime value</param>
        /// <returns>Valud API header DateTime string</returns>
        public static string ToSalesforceAPIHeaderDateTimeString(DateTime dt)
        {
            return dt.ToString("ddd, dd MMM yyyy HH:mm:ss z");
        }

        /// <summary>
        /// Gets the valid format for query parameter datetimes for the REST API
        /// </summary>
        /// <param name="dt">DateTime value</param>
        /// <returns>Valud API query parameter DateTime string</returns>
        public static string ToSalesforceAPIQueryDateTimeString(DateTime dt)
        {
            return dt.ToString("yyyy-MM-ddTHH:mm:sszzz");
        }

        /// <summary>
        /// Calls into Saleesforce to make a call into one of the standard REST API endpoints
        /// </summary>
        /// <param name="endPoint">Endpoint from AvailableResources</param>
        /// <param name="method">POST, GET, PATCH, or DELETE</param>
        /// <param name="queryParameters">Full query parameters, everything that goes after the '?'. Shoud be url encoded. If not necessary pass string.Empty</param>
        /// <param name="package">Payload for POST and PATCH requests, pass string.Empty if not needed</param>
        /// <param name="additionalHeaders">(optional) Additional parameters for the callout</param>
        /// <returns>JSON response from Salesforce</returns>
        public string StandardAPICallout(string endPoint, string method, string queryParameters, string package, string[] additionalHeaders = null)
        {
            if (this.state != ConnectionState.Open)
            {
                return "Connection closed";
            }
            string url = baseUrl + endPoint;
            if(string.IsNullOrEmpty(queryParameters))
            {
                url += queryParameters;
            }

            string returnValue = "";

            try
            {
                returnValue = Callout(url, method, package, "application/json", "application/json", additionalHeaders);
            }
            catch (WebException ex)
            {
                if (((HttpWebResponse)ex.Response).StatusCode == HttpStatusCode.Unauthorized)
                {
                    this.state = ConnectionState.Closed;
                    Open(Flow);
                    if (this.state == ConnectionState.Open)
                    {
                        returnValue = Callout(url, method, package, "application/json", "application/json", additionalHeaders);
                    }
                }
                else
                {
                    throw ex;
                }

            }
            return returnValue;
        }

        /// <summary>
        /// Calls into Saleesforce to make a call into one of the standard REST API endpoints
        /// </summary>
        /// <typeparam name="T">Type to deserialize the Json respose into</typeparam>
        /// <param name="endPoint">Endpoint from AvailableResources</param>
        /// <param name="method">POST, GET, PATCH, or DELETE</param>
        /// <param name="queryParameters">Full query parameters, everything that goes after the '?'. Shoud be url encoded. If not necessary pass string.Empty</param>
        /// <param name="package">Payload for POST and PATCH requests, pass string.Empty if not needed</param>
        /// <param name="additionalHeaders">(optional) Additional parameters for the callout</param>
        /// <returns>JSON response from Salesforce</returns>
        public T StandardAPICallout<T>(string endPoint, string method, string queryParameters, string package, string[] additionalHeaders = null)
        {
            return JsonConvert.DeserializeObject<T>(StandardAPICallout(endPoint, method, queryParameters, package, additionalHeaders));
        }

        /// <summary>
        /// Calls into Salesforce to make a call into one of the standard REST API endpoints
        /// </summary>
        /// <param name="endPoint">The additinoal part of the endpoint that goes after /services/data/vXX.X/, include query parameters</param>
        /// <param name="method">POST, GET, PATCH, or DELETE</param>
        /// <param name="package">Payload for POST and PATCH requests, pass string.Empty if not needed</param>
        /// <param name="additionalHeaders">(optional) Additional parameters for the callout</param>
        /// <returns>JSON response from Salesforce</returns>
        public string StandardAPICallout(string endPoint, string method, string package, string[] additionalHeaders = null)
    {
        if (this.state != ConnectionState.Open)
        {
            return "Connection closed";
        }
        //should this endPoint just be the entire uri? or perhaps just the location?
        if (endPoint.Length > 0 && endPoint[0] != '/') endPoint = '/' + endPoint;
        string url = baseUrl + string.Format("/services/data/v{0}{1}", Version, endPoint);

        string returnValue = "";

        try
        {
            returnValue = Callout(url, method, package, "application/json", "application/json", additionalHeaders);
        }
        catch (WebException ex)
        {
            if (((HttpWebResponse)ex.Response).StatusCode == HttpStatusCode.Unauthorized)
            {
                this.state = ConnectionState.Closed;
                Open(Flow);
                if (this.state == ConnectionState.Open)
                {
                    returnValue = Callout(url, method, package, "application/json", "application/json", additionalHeaders);
                }
            }
            else
            {
                throw ex;
            }

        }
        return returnValue;
    }

        /// <summary>
        /// Calls into Salesforce to make a call into one of the standard REST API endpoints
        /// </summary>
        /// <typeparam name="T">Type to deserialize the Json respose into</typeparam>
        /// <param name="endPoint">The additinoal part of the endpoint that goes after /services/data/vXX.X/, include query parameters</param>
        /// <param name="method">POST, GET, PATCH, or DELETE</param>
        /// <param name="package">Payload for POST and PATCH requests, pass string.Empty if not needed</param>
        /// <param name="additionalHeaders">(optional) Additional parameters for the callout</param>
        /// <returns>JSON response from Salesforce</returns>
        public T StandardAPICallout<T>(string endPoint, string method, string package, string[] additionalHeaders = null)
        {
            return JsonConvert.DeserializeObject<T>(StandardAPICallout(endPoint, method, package, additionalHeaders));
        }

        /// <summary>
        /// Calls into Salesforce to make a call into one of the custom REST API endpoints
        /// </summary>
        /// <param name="endPoint">The additinoal part of the endpoint that goes after /services/data/vXX.X/, include query parameters</param>
        /// <param name="method">POST, GET, PATCH, or DELETE</param>
        /// <param name="package">Payload for POST and PATCH requests, pass string.Empty if not needed</param>
        /// <param name="additionalHeaders">(optional) Additional parameters for the callout</param>
        /// <returns>JSON response from Salesforce</returns>
        public string CustomAPICallout(string endPoint, string method, string package, string[] additionalHeaders = null)
        {
            if (this.state != ConnectionState.Open)
            {
                return "Connection closed";
            }
            if (endPoint.Length > 0 && endPoint[0] != '/') endPoint = '/' + endPoint;
            string url = baseUrl + string.Format("/services/apexrest/{0}", endPoint);
            string returnValue = "";

            try
            {
                returnValue = Callout(url, method, package, "application/json", "application/json", additionalHeaders);
            }
            catch (WebException ex)
            {
                if (((HttpWebResponse)ex.Response).StatusCode == HttpStatusCode.Unauthorized)
                {
                    this.state = ConnectionState.Closed;
                    Open(Flow);
                    if (this.state == ConnectionState.Open)
                    {
                        returnValue = Callout(url, method, package, "application/json", "application/json", additionalHeaders);
                    }
                }
                else
                {
                    throw ex;
                }
            }
            return returnValue;
        }

        /// <summary>
        /// Calls into Salesforce to make a call into one of the custom REST API endpoints
        /// </summary>
        /// <typeparam name="T">Type to deserialize the Json respose into</typeparam>
        /// <param name="endPoint">The additinoal part of the endpoint that goes after /services/data/vXX.X/, include query parameters</param>
        /// <param name="method">POST, GET, PATCH, or DELETE</param>
        /// <param name="package">Payload for POST and PATCH requests, pass string.Empty if not needed</param>
        /// <param name="additionalHeaders">(optional) Additional parameters for the callout</param>
        /// <returns>JSON response from Salesforce</returns>
        public T CustomAPICallout<T>(string endPoint, string method, string package, string[] additionalHeaders = null)
        {
            return JsonConvert.DeserializeObject<T>(CustomAPICallout(endPoint, method, package, additionalHeaders));
        }

        /// <summary>
        /// Base callout method for the other callout methods. Use this if you need to change the Content-Type or Accept headers
        /// </summary>
        /// <param name="endPoint">The full endpoint that you intent to call</param>
        /// <param name="method">POST, GET, PATCH, or DELETE</param>
        /// <param name="package">Payload for POST and PATCH requests, pass string.Empty if not needed</param>
        /// <param name="contentType">Content-Type header</param>
        /// <param name="accept">Accept header</param>
        /// <param name="additionalHeaders">(optional) Additional parameters for the callout</param>
        /// <returns>Response from Salesforce</returns>
        public string Callout(string endPoint, string method, string package, string contentType, string accept, string[] additionalHeaders = null)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(endPoint);
            request.Method = method;
            request.ContentType = contentType;
            request.Accept = accept;

            request.Headers.Add("Authorization: Bearer " + SessionId);

            if (additionalHeaders != null)
            {
                foreach (string s in additionalHeaders)
                {
                    request.Headers.Add(s);
                }
            }

            //everything but get can have data to send
            if (method.ToUpper() != "GET" && package.Trim().Length > 0)
            {
                byte[] buffer = null;
                buffer = System.Text.Encoding.UTF8.GetBytes(package);

                Stream postStream = request.GetRequestStream();
                postStream.Write(buffer, 0, buffer.Length);
                postStream.Flush();
            }

            string returnValue = "";

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                lastStatusCode = response.StatusCode;
                using (StreamReader resp = new StreamReader(response.GetResponseStream()))
                {
                    returnValue = resp.ReadToEnd();
                }
            }

            return returnValue;
        }

    }
}


//public string SendRequest(string uri, string method, string payload = null, bool isJson = false, string[] additionalHeaders = null)
//{
//    switch (method)
//    {
//        case "GET":
//            return DoGet(uri, additionalHeaders);
//        case "POST":
//            return DoPost(uri, payload, isJson, additionalHeaders);
//        case "DELETE":
//            return DoDelete(uri, payload, additionalHeaders);
//        case "PUT":
//            return DoPut(uri, payload, additionalHeaders);
//        case "PATCH":
//            return DoPatch(uri, payload, additionalHeaders);
//        default:
//            throw new Exception("Invalid web request method: " + method);
//    }
//}
//private string DoGet(string uri, string[] additionalHeaders = null)
//{
//    HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uri);
//    request.AllowReadStreamBuffering = false;
//    request.Method = "GET";
//    request.ContentType = "application/json";
//    request.Headers.Add("Authorization: Bearer " + SessionId);
//    if (additionalHeaders != null)
//    {
//        foreach (string s in additionalHeaders)
//        {
//            request.Headers.Add(s);
//        }
//    }

//    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
//    {
//        lastStatusCode = response.StatusCode;
//        using (StreamReader resp = new StreamReader(response.GetResponseStream()))
//        {
//            return resp.ReadToEnd();
//        }
//    }
//}
//private string DoPost(string uri, string postData, bool isJson = false, string[] additionalHeaders = null)
//{
//    HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uri);
//    request.Method = "POST";
//    if (isJson)
//    {
//        request.ContentType = "application/json; charset=UTF-8";
//        request.Accept = "application/json";
//    }
//    else
//    {
//        request.ContentType = "application/x-www-form-urlencoded";//"multipart/form-data"
//    }
//    request.Headers.Add("Authorization: Bearer " + SessionId);
//    if (additionalHeaders != null)
//    {
//        foreach (string s in additionalHeaders)
//        {
//            request.Headers.Add(s);
//        }
//    }

//    byte[] buffer = null;
//    buffer = System.Text.Encoding.UTF8.GetBytes(postData);

//    Stream postStream = request.GetRequestStream();
//    postStream.Write(buffer, 0, buffer.Length);

//    try
//    {
//        using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
//        {
//            lastStatusCode = response.StatusCode;
//            using (StreamReader resp = new StreamReader(response.GetResponseStream()))
//            {
//                return resp.ReadToEnd();
//            }
//        }
//    }
//    catch (WebException ex)
//    {
//        string msg = "";

//        if (ex.Status == WebExceptionStatus.ProtocolError)
//        {
//            WebResponse resp = ex.Response;
//            msg = new System.IO.StreamReader(resp.GetResponseStream()).ReadToEnd().Trim();
//        }

//        return msg;
//        //throw new SalesforceException(msg, ex);
//    }
//}
//private string DoDelete(string uri, string additionalData, string[] additionalHeaders = null)
//{
//    return default(string);
//}
//private string DoPut(string uri, string additionalData, string[] additionalHeaders = null)
//{
//    return default(string);
//}
//private string DoPatch(string uri, string additionalData, string[] additionalHeaders = null)
//{
//    return default(string);
//}


//public string StandardAPICall(string endPoint, string method, string package, string contentType, string accept, string[] additionalHeaders = null)
//{
//    //should this endPoint just be the entire uri? or perhaps just the location?
//    if (endPoint[0] != '/') endPoint = '/' + endPoint;
//    string url = baseUrl + string.Format("/services/data/v{0}{1}", Version, endPoint);
//    string returnValue = "";

//    HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
//    request.Method = method;
//    request.ContentType = contentType;
//    request.Accept = accept;

//    request.Headers.Add("Authorization: Bearer " + SessionId);


//    if (additionalHeaders != null)
//    {
//        foreach (string s in additionalHeaders)
//        {
//            request.Headers.Add(s);
//        }
//    }

//    //everything but get can have data to send
//    if (method.ToUpper() != "GET" && package.Trim().Length > 0)
//    {
//        byte[] buffer = null;
//        buffer = System.Text.Encoding.UTF8.GetBytes(package);

//        Stream postStream = request.GetRequestStream();
//        postStream.Write(buffer, 0, buffer.Length);
//    }

//    try
//    {
//        using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
//        {
//            lastStatusCode = response.StatusCode;
//            using (StreamReader resp = new StreamReader(response.GetResponseStream()))
//            {
//                returnValue = resp.ReadToEnd();
//            }
//        }
//    }
//    catch (WebException ex)
//    {
//        string msg = "";

//        if (ex.Status == WebExceptionStatus.ProtocolError)
//        {
//            WebResponse resp = ex.Response;
//            msg = new System.IO.StreamReader(resp.GetResponseStream()).ReadToEnd().Trim();
//        }

//        //throw new SalesforceException(msg, ex);
//        returnValue = msg;
//    }

//    return returnValue;
//}
//public string CustomAPICall(string endPoint, string method, string package, string contentType, string accept, string[] additionalHeaders = null)
//{
//    string url = baseUrl + string.Format("/services/apexrest/{0}", endPoint);
//    string returnValue = "";

//    HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
//    request.Method = method;
//    request.ContentType = contentType;
//    request.Accept = accept;

//    request.Headers.Add("Authorization: Bearer " + SessionId);


//    if (additionalHeaders != null)
//    {
//        foreach (string s in additionalHeaders)
//        {
//            request.Headers.Add(s);
//        }
//    }

//    //everything but get can have data to send
//    if (method.ToUpper() != "GET" && package.Trim().Length > 0)
//    {
//        byte[] buffer = null;
//        buffer = System.Text.Encoding.UTF8.GetBytes(package);

//        Stream postStream = request.GetRequestStream();
//        postStream.Write(buffer, 0, buffer.Length);
//    }

//    try
//    {
//        using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
//        {
//            lastStatusCode = response.StatusCode;
//            using (StreamReader resp = new StreamReader(response.GetResponseStream()))
//            {
//                returnValue = resp.ReadToEnd();
//            }
//        }
//    }
//    catch (WebException ex)
//    {
//        string msg = "";

//        if (ex.Status == WebExceptionStatus.ProtocolError)
//        {
//            WebResponse resp = ex.Response;
//            msg = new System.IO.StreamReader(resp.GetResponseStream()).ReadToEnd().Trim();
//        }

//        //throw new SalesforceException(msg, ex);
//        returnValue = msg;
//    }

//    return returnValue;
//}
