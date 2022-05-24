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
            : base(refreshToken)
        {
        }
        public SfdcRestApi(string uri, string refreshToken = "")
            : base(uri, refreshToken)
        {
        }
        public SfdcRestApi(bool isTest, int apiversion, string refreshToken = "")
            : base(isTest, apiversion, refreshToken)
        {
            Version = apiversion.ToString() + ".0";
        }

        #region Variables
        /// <summary>
        /// API Limits state
        /// </summary>
        public ApiLimits ApiLimits { get; private set; }
        /// <summary>
        /// List of available endpoints for the REST API
        /// </summary>
        public AvailableResources AvailableResources { get; private set; }
        /// <summary>
        /// Identity object of the logged in user from the available resources 'identity' endpoint or the oauth 'id' endpoint
        /// </summary>
        public Identity Identity { get; private set; }
        /// <summary>
        /// Exposes the host
        /// </summary>
        public string BaseUrl
        {
            get
            {
                return this.baseUrl;
            }
        }
        #endregion

        /// <summary>
        /// Generates a header for the If-Match or If-None-Match ETags
        /// </summary>
        /// <param name="etag">ETag</param>
        /// <param name="matchType">Is-Math or If-None-Match</param>
        /// <returns>Header for the supplied etags</returns>
        public static string GenerateMatchHeader(string[] etag, Match matchType = Match.IfMatch)
        {
            string header = (matchType == Match.IfMatch ? "If-Match: " : "If-None-Match: ");
            List<string> fullEtags = new List<string>();
            foreach (string s in etag)
            {
                fullEtags.Add($"\"{s}--gzip\"");
            }
            return header + string.Join(",", fullEtags);
        }

        /// <summary>
        /// Generates a header for the If-Modified-Since or If-Unmodified-Since headers
        /// </summary>
        /// <param name="date">Datetime to measure modification against</param>
        /// <param name="modifiedtype">ModifiedSicne or UnmodifiedSince</param>
        /// <returns>Header for the supplied DateTime</returns>
        public static string GenerateModifiedHeader(DateTime date, Modified modifiedtype = Modified.ModifiedSince)
        {
            return (modifiedtype == Modified.ModifiedSince ? "If-Modified-Since: " : "If-Unmodified-Since: ") + ToSalesforceAPIHeaderDateTimeString(date);//.ToString("ddd, dd MMM yyyy HH:mm:ss z");
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
                LastStatusCode = response.StatusCode;
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
        /// <param name="getFromServer">If true, it will do the callout, if false it will return the results from the last call</param>
        /// <returns>List of available resources in a Dictionary</returns>
        public AvailableResources GetAvilableResources(bool getFromServer = false)
        {
            if (getFromServer)
            {
                string url = baseUrl + string.Format("/services/data/v{0}", Version);
                AvailableResources returnValue = null;

                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                request.Method = "GET";
                request.Accept = "application/json";

                request.Headers.Add("Authorization: Bearer " + SessionId);

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    LastStatusCode = response.StatusCode;
                    using (StreamReader resp = new StreamReader(response.GetResponseStream()))
                    {
                        AvailableResources = JsonConvert.DeserializeObject<AvailableResources>(resp.ReadToEnd());
                        this.MetadataEndPoint = new Uri(baseUrl + AvailableResources["metadata"]);
                        returnValue = AvailableResources;
                    }
                }
            }
            return AvailableResources;
        }

        /// <summary>
        /// Gets the Identity of the user.
        /// </summary>
        /// <param name="getFromServer">If true, it will do the callout, if false it will return the results from the last call</param>
        /// <returns>Identity object of the user</returns>
        public Identity GetIdentity(bool getFromServer = false)
        {
            if (getFromServer)
            {
                if (string.IsNullOrEmpty(identityEndpoint))
                {
                    Identity = Callout<Identity>(BuildUrlFromAvailableResources("identity"), "GET", string.Empty, "application/json", "application/json");
                }
                Identity = Callout<Identity>(identityEndpoint, "GET", string.Empty, "application/json", "application/json");
            }
            return Identity;
        }

        /// <summary>
        /// Gets the current API limts
        /// </summary>
        /// <param name="getFromServer">If true, it will do the callout, if false it will return the results from the last call</param>
        /// <returns>ApiLimits object containing the API limits</returns>
        public ApiLimits GetLimits(bool getFromServer = true)
        {
            if (getFromServer)
            {
                ApiLimits = Callout<ApiLimits>(BuildStandardUrl("limits"), "GET", string.Empty);
            }

            return ApiLimits;
        }

        /// <summary>
        /// Gets a list of all the Objects in the org
        /// </summary>
        /// <param name="getFromServer">If true, it will do the callout, if false it will return the results from the last call</param>
        /// <returns>DescribeGlobalResult</returns>
        public Objects.DescribeGlobalResult GetGlobalDescribe()
        {
            return Callout<Objects.DescribeGlobalResult>(BuildStandardUrl("sobjects"), "GET", string.Empty);
        }

        /// <summary>
        /// Gets the value of a Blobl. Blob fields can only be retrieved by specifically calling for them.
        /// </summary>
        /// <param name="objectApiName">API name of the Salesforce object</param>
        /// <param name="id">Id of the record. Either Salesforce Id or external Id</param>
        /// <param name="blobFieldName">Name of the Blob field to query</param>
        /// <returns>Binary blob data</returns>
        public byte[] GetBlobField(string objectApiName, string id, string blobFieldName)
        {
            string url = baseUrl + string.Format("/services/data/v{0}/sobjects/{1}/{2}/{3}", Version, objectApiName, id, blobFieldName);

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "";
            request.Accept = "application/octet-stream";

            request.Headers.Add("Authorization: Bearer " + SessionId);

            byte[] returnValue = null;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                LastStatusCode = response.StatusCode;
                using (BinaryReader resp = new BinaryReader(response.GetResponseStream()))
                {
                    //createa  1 MB buffer
                    returnValue = new byte[1048576];
                    int totalBytes = 0;
                    int bytesRead = 4096;
                    for (int x = 0; resp.BaseStream.CanRead && bytesRead > 0; x += bytesRead)
                    {
                        bytesRead = resp.Read(returnValue, x, 4096);
                        totalBytes += bytesRead;

                        if(totalBytes + 4096 > returnValue.Length)
                        {
                            // Add 1MB to the buffer
                            Array.Resize(ref returnValue, returnValue.Length + 1048576);
                        }
                    }

                    if (totalBytes != returnValue.Length)
                    {
                        Array.Resize(ref returnValue, totalBytes);
                    }
                }
            }

            return returnValue;
        }

        /// <summary>
        /// Builds a standard REST call endpoint from the AvailableResource object
        /// </summary>
        /// <param name="endPointName"></param>
        /// <returns>Url for REST callout</returns>
        public string BuildUrlFromAvailableResources(string endPointName)
        {
            UriBuilder uri = new UriBuilder();
            uri.Host = baseUrl;
            uri.Path = AvailableResources[endPointName];
            return uri.Uri.AbsoluteUri;
        }

        /// <summary>
        /// Builds a standard REST call endpoint
        /// </summary>
        /// <param name="endPoint">The end of the URL after the version</param>
        /// <returns>Url for REST callout</returns>
        public string BuildStandardUrl(string endPoint)
        {
            if (endPoint.Length > 0 && endPoint[0] != '/') endPoint = '/' + endPoint;
            return baseUrl + string.Format("/services/data/v{0}{1}", Version, endPoint);
        }

        /// <summary>
        /// Builds a custom REST call endpoint
        /// </summary>
        /// <param name="endPoint">The end of the URL after apexrest</param>
        /// <returns>Url for REST callout</returns>
        public string BuildCustomUrl(string endPoint)
        {
            if (endPoint.Length > 0 && endPoint[0] != '/') endPoint = '/' + endPoint;
            return baseUrl + string.Format("/services/apexrest{0}", endPoint);
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
                LastStatusCode = response.StatusCode;
                using (StreamReader resp = new StreamReader(response.GetResponseStream()))
                {
                    returnValue = resp.ReadToEnd();
                }
            }

            return returnValue;
        }

        /// <summary>
        /// Typed callout method. Use this if you can assume Content-Type and assume are both "application/json"
        /// </summary>
        /// <typeparam name="T">Type to deserialize the response to</typeparam>
        /// <param name="endPoint">The full endpoint that you intent to call</param>
        /// <param name="method">POST, GET, PATCH, or DELETE</param>
        /// <param name="package">Payload for POST and PATCH requests, pass string.Empty if not needed</param>
        /// <param name="additionalHeaders">(optional) Additional parameters for the callout</param>
        /// <returns>Response from Salesforce</returns>
        public T Callout<T>(string endPoint, string method, string package, string[] additionalHeaders = null)
        {
            return JsonConvert.DeserializeObject<T>(Callout(endPoint, method, package, "application/json", "application/json", additionalHeaders));
        }

        /// <summary>
        /// Typed callout method. Use this if you need to change the Content-Type or Accept headers
        /// </summary>
        /// <typeparam name="T">Type to deserialize the response to</typeparam>
        /// <param name="endPoint">The full endpoint that you intent to call</param>
        /// <param name="method">POST, GET, PATCH, or DELETE</param>
        /// <param name="package">Payload for POST and PATCH requests, pass string.Empty if not needed</param>
        /// <param name="contentType">Content-Type header</param>
        /// <param name="accept">Accept header</param>
        /// <param name="additionalHeaders">(optional) Additional parameters for the callout</param>
        /// <returns>Response from Salesforce</returns>
        public T Callout<T>(string endPoint, string method, string package, string contentType, string accept, string[] additionalHeaders = null)
        {
            return JsonConvert.DeserializeObject<T>(Callout(endPoint, method, package, contentType, accept, additionalHeaders));
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
