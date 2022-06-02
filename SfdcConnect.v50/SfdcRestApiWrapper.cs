using SfdcConnect.Rest;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;

namespace SfdcConnect
{
    public class SfdcRestApiWrapper
    {
        public SfdcRestApiWrapper(SfdcRestApi RestApi= null)
        {
            this.RestApi = RestApi;
        }
        ~SfdcRestApiWrapper()
        {
            if(RestApi != null && RestApi.State == ConnectionState.Open)
            {
                RestApi.Close();
            }
        }

        public SfdcRestApi RestApi { get; set; }

        public DescribeGlobalResult GetGlobalDescribeByDate(DateTime date, string[] additionalHeaders = null)
        {
            string whatsChanged = StandardAPICallout("sobjects", "GET", string.Empty, additionalHeaders);

            if (RestApi.LastStatusCode == HttpStatusCode.NotModified)
            {
                //nothing changed
                return null;
            }
            else
            {
                //something changed
                //deserialize response.
                return JsonSerializer.Deserialize<DescribeGlobalResult>(whatsChanged);
            }
        }

        public GetSObjectResult GetSObject(string objectApiName)
        {
            string url = string.Format("sobjects/{0}", objectApiName);

            return StandardAPICallout<GetSObjectResult>(url, "GET", string.Empty);
        }

        /// <summary>
        /// Gets the description of an Salesforce Object
        /// </summary>
        /// <param name="objectApiName">API name of the object to describe</param>
        /// <param name="additionalHeaders">(optional) If-Modified-Since or If-Unmodified-Since header</param>
        /// <returns>DescribeSObjectResult for the object or null depending on the additional header (e.g. null if no modifications since when using If-Modified-since)</returns>
        public DescribeSObjectResult DescribeSObject(string objectApiName, string[] additionalHeaders = null)
        {
            string url = string.Format("sobjects/{0}/describe", objectApiName);

            string whatsChanged = StandardAPICallout(url, "GET", string.Empty, additionalHeaders);

            if (RestApi.LastStatusCode == HttpStatusCode.NotModified)
            {
                //nothing changed
                return null;
            }
            else
            {
                //something changed
                //deserialize response.
                return JsonSerializer.Deserialize<DescribeSObjectResult>(whatsChanged);
            }
        }

        public GetDeletedResult GetDeleted(string objectApiName, DateTime startDate, DateTime endDate)
        {
            string url = string.Format("sobjects/{0}/deleted?start={1}&end={2}", objectApiName, HttpUtility.UrlEncode(SfdcRestApi.ToSalesforceAPIQueryDateTimeString(startDate)), HttpUtility.UrlEncode(SfdcRestApi.ToSalesforceAPIQueryDateTimeString(endDate)));

            return StandardAPICallout<GetDeletedResult>(url, "GET", string.Empty);
        }

        public GetUpdatedResult GetUpdated(string objectApiName, DateTime startDate, DateTime endDate)
        {
            string url = string.Format("sobjects/{0}/updated?start={1}&end={2}", objectApiName, HttpUtility.UrlEncode(SfdcRestApi.ToSalesforceAPIQueryDateTimeString(startDate)), HttpUtility.UrlEncode(SfdcRestApi.ToSalesforceAPIQueryDateTimeString(endDate)));

            return StandardAPICallout<GetUpdatedResult>(url, "GET", string.Empty);
        }

        public SaveResult CreateRecord(string objectApiName, string recordJson)
        {
            string url = string.Format("sobjects/{0}", objectApiName);

            return StandardAPICallout<SaveResult>(url, "POST", recordJson);
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
        public sObject GetRecord(string objectApiName, string id, string[] fields = null, string fieldApiName = null, string[] additionalHeaders = null)
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
            if (fields != null && fields.Length > 0)
            {
                url += "?fields=" + string.Join(",", fields);
            }

            string result = StandardAPICallout(url, "GET", string.Empty, additionalHeaders);
            sObject r = JsonSerializer.Deserialize<sObject>(result);

            return r;
        }

        /// <summary>
        /// Updates a record in Salesforce
        /// </summary>
        /// <param name="objectApiName">API name of the Salesforce object</param>
        /// <param name="id">Id of the record. Either Salesforce Id or external Id</param>
        /// <param name="recordJson">Json of the fields to update</param>
        /// <param name="additionalHeaders">(optional) If-Modified-Since or If-Unmodified-Since header and/or, if an Account, If-Match or If-None-Match headers</param>
        /// <returns>SaveResult</returns>
        public SaveResult UpdateRecord(string objectApiName, string id, string recordJson, string[] additionalHeaders = null)
        {
            //TODO external field update
            string url = string.Format("sobjects/{0}/{1}", objectApiName, id);

            return StandardAPICallout<SaveResult>(url, "PATCH", recordJson, additionalHeaders);
        }

        /// <summary>
        /// Updates a record in Salesforce
        /// </summary>
        /// <param name="objectApiName">API name of the Salesforce object</param>
        /// <param name="id">Id of the record. Either Salesforce Id or external Id</param>
        /// <param name="fieldApiName">API name of the field to upsert on</param>
        /// <param name="recordJson">Json of the fields to update/insert</param>
        /// <returns>UpsertResult</returns>
        public UpsertResult UpsertRecord(string objectApiName, string id, string fieldApiName, string recordJson)
        {
            string url = string.Format("sobjects/{0}/{1}/{2}", objectApiName, fieldApiName, id);

            return StandardAPICallout<UpsertResult>(url, "PATCH", recordJson);
        }

        /// <summary>
        /// Deletes a record in Salesforce
        /// </summary>
        /// <param name="objectApiName">API name of the Salesforce object</param>
        /// <param name="id">Id of the record. Either Salesforce Id or external Id</param>
        /// <param name="additionalHeaders">(optional) If-Modified-Since or If-Unmodified-Since header and/or, if an Account, If-Match or If-None-Match headers</param>
        /// <returns>DeleteResult</returns>
        public DeleteResult DeleteRecord(string objectApiName, string id, string[] additionalHeaders = null)
        {
            string url = string.Format("sobjects/{0}/{1}", objectApiName, id);

            return StandardAPICallout<DeleteResult>(url, "DELETE", string.Empty, additionalHeaders);
        }

        /// <summary>
        /// Queries Salesforce using SOQL
        /// </summary>
        /// <param name="query">SOQL Query</param>
        /// <returns>Query Results</returns>
        public QueryResult Query(string query)
        {
            string url = string.Format("query?q={0}", HttpUtility.UrlEncode(query));

            return StandardAPICallout<QueryResult>(url, "GET", string.Empty);
        }

        /// <summary>
        /// Queries Salesforce using SOQL icluding deleted records
        /// </summary>
        /// <param name="query">SOQL Query</param>
        /// <returns>Query Results</returns>
        public QueryResult QueryAll(string query)
        {
            string url = string.Format("queryAll?q={0}", HttpUtility.UrlEncode(query));

            return StandardAPICallout<QueryResult>(url, "GET", string.Empty);
        }

        /// <summary>
        /// Gets the additinal results from a Query or QueryAll cal
        /// </summary>
        /// <param name="queryMore">Url provided by Salesforce to get more records in a SOQL query</param>
        /// <returns>Query Results</returns>
        public QueryResult QueryMore(string queryMore)
        {
            return StandardAPICallout<QueryResult>(queryMore, "GET", string.Empty, string.Empty, null);
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
            if (RestApi.State != ConnectionState.Open)
            {
                return "Connection closed";
            }
            string url = RestApi.BaseUrl + endPoint;
            if (string.IsNullOrEmpty(queryParameters))
            {
                url += queryParameters;
            }

            string returnValue = "";

            try
            {
                returnValue = RestApi.Callout(url, method, package, "application/json", "application/json", additionalHeaders);
            }
            catch (WebException ex)
            {
                if (((HttpWebResponse)ex.Response).StatusCode == HttpStatusCode.Unauthorized)
                {
                    RestApi.Open(RestApi.Flow);
                    if (RestApi.State == ConnectionState.Open)
                    {
                        returnValue = RestApi.Callout(url, method, package, "application/json", "application/json", additionalHeaders);
                    }
                    else
                    {
                        returnValue = "Unauthorized";
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
            return JsonSerializer.Deserialize<T>(StandardAPICallout(endPoint, method, queryParameters, package, additionalHeaders));
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
            if (RestApi.State != ConnectionState.Open)
            {
                return "Connection closed";
            }
            string url = RestApi.BuildStandardUrl(endPoint);

            string returnValue = "";

            try
            {
                returnValue = RestApi.Callout(url, method, package, "application/json", "application/json", additionalHeaders);
            }
            catch (WebException ex)
            {
                if (((HttpWebResponse)ex.Response).StatusCode == HttpStatusCode.Unauthorized)
                {
                    RestApi.Open(RestApi.Flow);
                    if (RestApi.State == ConnectionState.Open)
                    {
                        returnValue = RestApi.Callout(url, method, package, "application/json", "application/json", additionalHeaders);
                    }
                    else
                    {
                        returnValue = "Unauthorized";
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
            return JsonSerializer.Deserialize<T>(StandardAPICallout(endPoint, method, package, additionalHeaders));
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
            if (RestApi.State != ConnectionState.Open)
            {
                return "Connection closed";
            }
            string url = RestApi.BuildCustomUrl(endPoint);

            string returnValue = "";

            try
            {
                returnValue = RestApi.Callout(url, method, package, "application/json", "application/json", additionalHeaders);
            }
            catch (WebException ex)
            {
                if (((HttpWebResponse)ex.Response).StatusCode == HttpStatusCode.Unauthorized)
                {
                    RestApi.Open(RestApi.Flow);
                    if (RestApi.State == ConnectionState.Open)
                    {
                        returnValue = RestApi.Callout(url, method, package, "application/json", "application/json", additionalHeaders);
                    }
                    else
                    {
                        returnValue = "Unauthorized";
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
            return JsonSerializer.Deserialize<T>(CustomAPICallout(endPoint, method, package, additionalHeaders));
        }

    }
}
