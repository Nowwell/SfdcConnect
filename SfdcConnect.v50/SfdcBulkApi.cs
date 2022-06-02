/****************************************************************************
*
*   File name: SfdcBulkApi.cs
*   Author: Sean Fife
*   Create date: 5/27/2022
*   Solution: SfdcConnect
*   Project: SfdcConnect
*   Description: Includes the SfdcBulkApi class for Salesforce Bulk Api Connections.
*                   Modified from a BulkApiClient implementation found on the net.
*
****************************************************************************/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

using SfdcConnect.Bulk;

namespace SfdcConnect
{
    public enum ContentType { None, CSV, XML, ZIP_CSV, ZIP_XML };
    public enum Operations { update, insert, upsert, delete, hardDelete, query };
    public enum ConcurrencyMode { Parallel, Serial };
    public enum WebMethod { GET, POST, HEAD, DELETE };
    public enum JobOperation { Query, Insert, Update, Delete, HardDelete, Upsert }
    public enum JobContentType { CSV, XML }

    /// <summary>
    /// Salesforce Bulk Api connection class
    /// </summary>
    public class SfdcBulkApi
    {
        //const string jobRequestXMLTemplate = "<?xml version=\"1.0\" encoding=\"UTF-8\"?> <jobInfo xmlns=\"http://www.force.com/2009/06/asyncapi/dataload\"> <operation>{0}</operation> <object>{1}</object> {4} {3} {2} </jobInfo>";

        public SfdcBulkApi()
        {
            version = 54;
            DataProtector = new SfdcDataProtection();
            EndPoint = new Uri("https://login.salesforce.com");
        }
        public SfdcBulkApi(string uri)
        {
            version = 54;
            DataProtector = new SfdcDataProtection();
            EndPoint = new Uri("https://login.salesforce.com");
        }
        public SfdcBulkApi(Environment env, int apiversion)
        {
            version = apiversion;
            DataProtector = new SfdcDataProtection();
            EndPoint = new Uri("https://login.salesforce.com");
        }

        internal SfdcDataProtection DataProtector;
        protected SfdcSession InternalConnection;
        public Uri EndPoint { get; set; }
        protected int version;

        public void Open(SfdcSession conn)
        {
            InternalConnection = conn;
            EndPoint = new Uri(conn.Endpoint.Address.Uri.AbsoluteUri);

            //string[] pieces = conn.Endpoint.Address.Uri.AbsoluteUri.Split('.');
            //pieces[0] += "-api";

            //EndPoint = new Uri(string.Join(".", pieces));
        }

        public void Close()
        {
            InternalConnection.Close();
        }

        #region Jobs
        public Job CreateJob(string SfObject, ContentType contentType, Operations operation, ConcurrencyMode mode, string ExternalIdFieldName = null)
        {
            string createJobUrl = BuildBatchUrl();// "https://" + EndPoint.Host + "/services/async/" + version.ToString() + "/job";

            XmlDocument jobDocument = new XmlDocument();
            XmlDeclaration decl = jobDocument.CreateXmlDeclaration("1.0", "UTF-8", null);
            XmlElement jobinfo = jobDocument.CreateElement("jobInfo");
            jobDocument.AppendChild(jobinfo);
            jobDocument.InsertBefore(decl, jobinfo);

            jobinfo.SetAttribute("xmlns", "http://www.force.com/2009/06/asyncapi/dataload");

            XmlElement op = jobDocument.CreateElement("operation");
            op.InnerText = operation.ToString();
            jobinfo.AppendChild(op);

            XmlElement objele = jobDocument.CreateElement("object");
            objele.InnerText = SfObject;
            jobinfo.AppendChild(objele);

            if (contentType != ContentType.None)
            {
                XmlElement ct = jobDocument.CreateElement("contentType");
                ct.InnerText = contentType.ToString();
                jobinfo.AppendChild(ct);
            }

            if (!string.IsNullOrEmpty(ExternalIdFieldName))
            {
                XmlElement ef = jobDocument.CreateElement("externalField");
                ef.InnerText = ExternalIdFieldName;
                jobinfo.AppendChild(ef);
            }

            if (operation == Operations.upsert)
            {
                XmlElement concurrencyMode = jobDocument.CreateElement("concurrencyMode");
                concurrencyMode.InnerText = mode.ToString();
                jobinfo.AppendChild(concurrencyMode);
            }

            string resultXML = InvokeAPI(createJobUrl, jobDocument.OuterXml, WebMethod.POST, string.Empty); //invokeRestAPI(createJobUrl, jobDocument.InnerXml);

            return Job.Create(resultXML);
        }
        public Job CloseJob(Job job)
        {
            string closeJobUrl = BuildBatchUrl(job.Id);

            XmlDocument jobDocument = new XmlDocument();
            XmlDeclaration decl = jobDocument.CreateXmlDeclaration("1.0", "UTF-8", null);
            XmlElement jobinfo = jobDocument.CreateElement("jobInfo");
            jobDocument.AppendChild(jobinfo);
            jobDocument.InsertBefore(decl, jobinfo);

            jobinfo.SetAttribute("xmlns", "http://www.force.com/2009/06/asyncapi/dataload");

            XmlElement state = jobDocument.CreateElement("state");
            state.InnerText = "Closed";
            jobinfo.AppendChild(state);

            string resultXML = InvokeAPI(closeJobUrl, jobDocument.OuterXml, WebMethod.POST, string.Empty);

            return Job.Create(resultXML);
        }
        public Job GetJob(Job job)
        {
            string getJobUrl = BuildBatchUrl(job.Id);

            string resultXML = InvokeAPI(getJobUrl, string.Empty, WebMethod.GET, string.Empty);

            return Job.Create(resultXML);
        }
        #endregion

        #region Batches
        public Batch CreateBatch(Job job, string batchContents, string batchContentType, string batchContentHeader)
        {
            string requestUrl = BuildBatchUrl(job.Id, "+/");

            string contentType = batchContentType;

            if (batchContentType != "")
            {
                contentType = batchContentHeader;
            }

            string resultXML = InvokeAPI(requestUrl, batchContents, WebMethod.POST, contentType);

            return Batch.CreateBatch(resultXML);
        }
        public Batch GetBatch(Job job, Batch batch)
        {
            string requestUrl = BuildBatchUrl(job.Id, batch.Id);

            string resultXML = InvokeAPI(requestUrl, string.Empty, WebMethod.GET, string.Empty);

            return Batch.CreateBatch(resultXML);
        }
        public List<Batch> GetBatches(string jobId)
        {
            string requestUrl = BuildBatchUrl(jobId, "?/");

            string resultXML = InvokeAPI(requestUrl, string.Empty, WebMethod.GET, string.Empty);

            return Batch.CreateBatches(resultXML);
        }
        public string GetBatchRequest(string jobId, string batchId)
        {
            string requestUrl = BuildBatchUrl(jobId, batchId, "request"); //"https://" + EndPoint.Host + "/services/async/" + version.ToString() + "/job/" + jobId + "/batch/" + batchId + "/request";

            string resultXML = InvokeAPI(requestUrl, string.Empty, WebMethod.GET, string.Empty);

            return resultXML;
        }
        #endregion

        #region Results
        public string GetBatchResults(Job job, Batch batch)
        {
            string requestUrl = BuildBatchUrl(job.Id, batch.Id, "result");// "https://" + EndPoint.Host + "/services/async/" + version.ToString() + "/job/" + job.Id + "/batch/" + batch.Id + "/result";

            string resultXML = InvokeAPI(requestUrl, string.Empty, WebMethod.GET, string.Empty);

            return resultXML;
        }
        public bool GetQueryBatchResults(Job job, Batch batch, string filename, bool truncateFile)
        {
            string requestUrl = "https://" + EndPoint.Host + "/services/async/" + version.ToString() + "/job/" + job.Id + "/batch/" + batch.Id + "/result";

            string resultXML = InvokeAPI(requestUrl, string.Empty, WebMethod.GET, string.Empty);

            List<string> resultIds = GetResultIds(resultXML);

            if (resultIds.Count > 0)
            {
                FileStream file = null;
                FileInfo fi = new FileInfo(filename);
                if (truncateFile && fi.Exists)
                {
                    file = new FileStream(filename, FileMode.OpenOrCreate | FileMode.Truncate, FileAccess.ReadWrite);
                }
                else
                {
                    file = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                }
                using (StreamWriter output = new StreamWriter(file, System.Text.Encoding.UTF8))
                {
                    foreach (string id in resultIds)
                    {
                        string url = requestUrl + "/" + id;

                        HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                        //request.SendChunked = true;
                        //request.TransferEncoding = "UTF-8";
                        request.AllowReadStreamBuffering = false;
                        request.Method = "GET";
                        request.Headers.Add("X-SFDC-Session: " + EndPoint.Host + "/services/async/" + InternalConnection.SessionId);

                        using (HttpWebResponse response = (HttpWebResponse)(request.GetResponse()))
                        {
                            Stream data = response.GetResponseStream();

                            byte[] xfer = new byte[16 * 1024];
                            char[] chars = null;
                            int readBytes = 0;
                            while ((readBytes = data.Read(xfer, 0, xfer.Length)) > 0)
                            {
                                chars = System.Text.Encoding.UTF8.GetChars(xfer);

                                output.Write(chars, 0, readBytes);
                            }

                            output.Flush();

                            response.Close();
                        }
                    }
                    output.Close();
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool GetQueryBatchResults(Job job, Batch batch, string filename, bool truncateFile, Action<string, int> callback)
        {
            string requestUrl = "https://" + EndPoint.Host + "/services/async/" + version.ToString() + "/job/" + job.Id + "/batch/" + batch.Id + "/result";

            string resultXML = InvokeAPI(requestUrl, string.Empty, WebMethod.GET, string.Empty);

            List<string> resultIds = GetResultIds(resultXML);

            int bytesDownloaded = 0;
            if (resultIds.Count > 0)
            {
                FileStream file = null;
                FileInfo fi = new FileInfo(filename);
                if (truncateFile && fi.Exists)
                {
                    file = new FileStream(filename, FileMode.OpenOrCreate | FileMode.Truncate, FileAccess.ReadWrite);
                }
                else
                {
                    file = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                }
                //FileStream file = new FileStream(filename, FileMode.OpenOrCreate | FileMode.Truncate, FileAccess.ReadWrite);
                StreamWriter output = new StreamWriter(file, System.Text.Encoding.UTF8);
                foreach (string id in resultIds)
                {
                    string url = requestUrl + "/" + id;

                    HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                    //request.SendChunked = true;
                    //request.TransferEncoding = "UTF-8";
                    request.AllowReadStreamBuffering = false;
                    request.Method = "GET";
                    request.Headers.Add("X-SFDC-Session: " + EndPoint.Host + "/services/async/" + InternalConnection.SessionId);

                    using (HttpWebResponse response = (HttpWebResponse)(request.GetResponse()))
                    {
                        Stream data = response.GetResponseStream();

                        byte[] xfer = new byte[16 * 1024];
                        char[] chars = null;
                        int readBytes = 0;
                        while ((readBytes = data.Read(xfer, 0, xfer.Length)) > 0)
                        {
                            chars = System.Text.Encoding.UTF8.GetChars(xfer);

                            output.Write(chars, 0, readBytes);

                            bytesDownloaded += readBytes;
                            callback(string.Format("{0:N0} bytes downloaded", bytesDownloaded), 0);
                        }

                        output.Flush();

                        response.Close();
                    }
                }
                output.Close();
                output.Dispose();
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool GetQueryBatchResults(Job job, Batch batch, string filename, bool truncateFile, Action<string, bool> callback)
        {
            string requestUrl = "https://" + EndPoint.Host + "/services/async/" + version.ToString() + "/job/" + job.Id + "/batch/" + batch.Id + "/result";

            string resultXML = InvokeAPI(requestUrl, string.Empty, WebMethod.GET, string.Empty);

            List<string> resultIds = GetResultIds(resultXML);

            int bytesDownloaded = 0;
            if (resultIds.Count > 0)
            {
                FileStream file = null;
                FileInfo fi = new FileInfo(filename);
                if (truncateFile && fi.Exists)
                {
                    file = new FileStream(filename, FileMode.OpenOrCreate | FileMode.Truncate, FileAccess.ReadWrite);
                }
                else
                {
                    file = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                }
                //FileStream file = new FileStream(filename, FileMode.OpenOrCreate | FileMode.Truncate, FileAccess.ReadWrite);
                StreamWriter output = new StreamWriter(file, System.Text.Encoding.UTF8);
                foreach (string id in resultIds)
                {
                    string url = requestUrl + "/" + id;

                    HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                    //request.SendChunked = true;
                    //request.TransferEncoding = "UTF-8";
                    //request.AllowReadStreamBuffering = false;
                    request.Method = "GET";
                    request.Headers.Add("X-SFDC-Session: " + EndPoint.Host + "/services/async/" + InternalConnection.SessionId);

                    using (HttpWebResponse response = (HttpWebResponse)(request.GetResponse()))
                    {
                        Stream data = response.GetResponseStream();

                        byte[] xfer = new byte[32 * 1024];
                        char[] chars = null;
                        int readBytes = 0;
                        while ((readBytes = data.Read(xfer, 0, xfer.Length)) > 0)
                        {
                            chars = System.Text.Encoding.UTF8.GetChars(xfer);

                            output.Write(chars, 0, readBytes);

                            bytesDownloaded += readBytes;
                            callback(string.Format("{0:N0} bytes downloaded", bytesDownloaded), true);

                            Array.Clear(xfer, 0, xfer.Length);
                        }

                        output.Flush();

                        response.Close();
                    }
                }
                output.Close();
                output.Dispose();
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<bool> GetQueryBatchResultsAsync(Job job, Batch batch, string filename)
        {
            string requestUrl = "https://" + EndPoint.Host + "/services/async/" + version.ToString() + "/job/" + job.Id + "/batch/" + batch.Id + "/result";

            string resultXML = InvokeAPI(requestUrl, string.Empty, WebMethod.GET, string.Empty);

            List<string> resultIds = GetResultIds(resultXML);

            if (resultIds.Count > 0)
            {
                FileStream file = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                StreamWriter output = new StreamWriter(file, System.Text.Encoding.UTF8);
                foreach (string id in resultIds)
                {
                    string url = requestUrl + "/" + id;

                    HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                    //request.SendChunked = true;
                    //request.TransferEncoding = "UTF-8";
                    request.AllowReadStreamBuffering = false;
                    request.Method = "GET";
                    request.Headers.Add("X-SFDC-Session: " + EndPoint.Host + "/services/async/" + InternalConnection.SessionId);

                    using (HttpWebResponse response = (HttpWebResponse)(await request.GetResponseAsync()))
                    {
                        Stream data = response.GetResponseStream();

                        byte[] xfer = new byte[16 * 1024];
                        char[] chars = null;
                        int readBytes = 0;
                        while ((readBytes = await data.ReadAsync(xfer, 0, xfer.Length)) > 0)
                        {
                            chars = System.Text.Encoding.UTF8.GetChars(xfer);

                            await output.WriteAsync(chars, 0, readBytes);

                            //await output.WriteAsync(xfer, 0, readBytes);

                        }

                        output.Flush();

                        response.Close();
                    }
                }
                output.Close();
                output.Dispose();
                return true;
            }
            else
            {
                return false;
            }
        }
        public List<string> GetResultIds(string queryBatchResultListXML)
        {
            //<result-list xmlns="http://www.force.com/2009/06/asyncapi/dataload"><result>752x000000000F1</result></result-list>

            XDocument doc = XDocument.Parse(queryBatchResultListXML);
            List<string> resultIds = new List<string>();

            XElement resultListElement = doc.Root;

            foreach (XElement resultElement in resultListElement.Elements())
            {
                string resultId = resultElement.Value;
                resultIds.Add(resultId);
            }

            return resultIds;
        }
        public string GetBatchResult(string jobId, string batchId, string resultId)
        {
            string requestUrl = BuildBatchUrl(jobId, batchId, resultId);// "https://" + EndPoint.Host + "/services/async/" + version.ToString() + "/job/" + jobId + "/batch/" + batchId + "/result/" + resultId;

            string resultXML = InvokeAPI(requestUrl, string.Empty, WebMethod.GET, string.Empty);

            return resultXML;
        }
        #endregion

        /// <summary>
        /// Builds the url for a bulk api action
        /// </summary>
        /// <param name="jobId">Blank to create a job, JobId to manage a job</param>
        /// <param name="batchId">"+/" to create a batch, "?/" to query batches, BatchId to manage a batch</param>
        /// <param name="resultId">"request" for request query, "result" for request query, resultId for results</param>
        /// <returns>Bulk API endpoint</returns>
        private string BuildBatchUrl(string jobId = "", string batchId = "", string resultId = "")
        {
            StringBuilder endpoint = new StringBuilder();
            endpoint.Append("https://");
            endpoint.Append(EndPoint.Host);
            endpoint.Append("/services/async/");
            endpoint.Append(version);
            endpoint.Append("/job");

            if (!string.IsNullOrEmpty(jobId))
            {
                endpoint.Append("/");
                endpoint.Append(jobId);

                if (string.IsNullOrEmpty(batchId))
                {
                    //do nothing?
                }
                else
                {
                    if (batchId == "+/")
                    {
                        endpoint.Append("/batch");
                    }
                    else if (batchId != "?/")
                    {
                        endpoint.Append("/batch/");
                    }
                    else
                    {
                        endpoint.Append("/batch/");
                        endpoint.Append("/");
                        endpoint.Append(batchId);

                        if (string.IsNullOrEmpty(resultId))
                        {
                        }
                        else if(resultId == "request")
                        {
                            endpoint.Append("/request");
                        }
                        else if (resultId == "result")
                        {
                            endpoint.Append("/result");
                        }
                        else
                        {
                            endpoint.Append("/result/");
                            endpoint.Append(resultId);
                        }
                    }
                }
            }

            return endpoint.ToString();// "https://" + EndPoint.Host + "/services/async/" + version.ToString() + "/job/" + jobId;
        }
        private string InvokeAPI(string endpointURL, string postData, WebMethod httpVerb, string contentType)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(endpointURL);
            request.Method = httpVerb.ToString();
            request.Headers.Add("X-SFDC-Session: " + InternalConnection.SessionId);
            if (!string.IsNullOrWhiteSpace(contentType))
            {
                request.ContentType = contentType;
            }

            try
            {
                byte[] buffer = null;
                if(httpVerb != WebMethod.GET && httpVerb != WebMethod.HEAD)
                {
                    buffer = System.Text.Encoding.ASCII.GetBytes(postData);
                }

                Stream postStream = request.GetRequestStream();
                postStream.Write(buffer, 0, buffer.Length);

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                StreamReader resp = new StreamReader(response.GetResponseStream());

                return resp.ReadToEnd();
            }
            catch (WebException webEx)
            {
                string error = string.Empty;

                if (webEx.Response != null)
                {
                    using (var errorResponse = (HttpWebResponse)webEx.Response)
                    {
                        using (var reader = new StreamReader(errorResponse.GetResponseStream()))
                        {
                            error = reader.ReadToEnd();
                        }
                    }
                }

                throw new Exception(error);
            }
            catch (Exception genEx)
            {
                throw new Exception("API Call Failed", genEx);
            }
        }
    }
}
