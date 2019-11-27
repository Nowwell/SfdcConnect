/****************************************************************************
*
*   File name: SfdcToolingApi.cs
*   Author: Sean Fife
*   Create date: 5/23/2017
*   Solution: SfdcConnect
*   Project: SfdcConnect
*   Description: Includes the class for Salesforce Tooling API Calls
*
****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SfdcConnect.Tooling;
using System.Data;

namespace SfdcConnect
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name = "SforceServiceBinding", Namespace = "urn:tooling.soap.sforce.com")]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(ApiFault))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(SubscriberPackageProfiles))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(ChannelLayoutItem))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(Metadata))]
    public class SfdcToolingApi : SfdcLoginBase
    {
        public SfdcToolingApi()
        {
        }
        public SfdcToolingApi(string uri)
            : base(uri)
        {
        }
        public SfdcToolingApi(bool isTest, int apiversion)
        {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            state = ConnectionState.Closed;

            if (isTest)
            {
                Url = string.Format("https://test.salesforce.com/services/Soap/T/{0}.0", apiversion);
            }
            else
            {
                Url = string.Format("https://login.salesforce.com/services/Soap/T/{0}.0", apiversion);
            }

            if ((this.IsLocalFileSystemWebService(this.Url) == true))
            {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else
            {
                this.useDefaultCredentialsSetExplicitly = true;
            }

            Version = apiversion.ToString();
        }

        /// <summary>
        /// Open a connection to Salesforce.  Requires Usernamd and Password to be filled in.
        /// </summary>
        public override void Open()
        {
            if (state == ConnectionState.Open) return;
            state = ConnectionState.Connecting;

            if (!string.IsNullOrEmpty(ServerUrl))
            {
                Url = ServerUrl;
            }

            LoginResult loginResult = login(Username, Password + Token);

            LoginTime = DateTime.Now;
            Url = loginResult.serverUrl;
            SessionHeaderValue = new SessionHeader();
            SessionHeaderValue.sessionId = loginResult.sessionId;

            CallOptionsValue = new CallOptions();

            SessionId = loginResult.sessionId;
            ServerUrl = loginResult.serverUrl;

            string[] pieces = ServerUrl.Split('/');

            Version = pieces[pieces.Length - 2];

            ApiEndPoint = new Uri(loginResult.serverUrl);
            MetadataEndPoint = new Uri(loginResult.metadataServerUrl);

            //Uri url = new Uri(ServerUrl);
            //this.Url = ServerUrl.Replace("Soap/u/", "Soap/T/");// + this.ApiEndPoint.Segments[this.ApiEndPoint.Segments.Length - 1];


            state = ConnectionState.Open;
        }
        /// <summary>
        /// Close the Salesforce connection
        /// </summary>
        public override void Close()
        {
            logout();

            state = ConnectionState.Closed;
        }

        #region Sfdc - From wsdl
        public SfdcConnect.Tooling.SessionHeader SessionHeaderValue { get; set; }
        public SfdcConnect.Tooling.CallOptions CallOptionsValue { get; set; }

        public DebuggingHeader DebuggingHeaderValue { get; set; }
        public PackageVersionHeader PackageVersionHeaderValue { get; set; }
        public DebuggingInfo DebuggingInfoValue { get; set; }
        public AllowFieldTruncationHeader AllowFieldTruncationHeaderValue { get; set; }
        public DisableFeedTrackingHeader DisableFeedTrackingHeaderValue { get; set; }
        public MetadataWarningsHeader MetadataWarningsHeaderValue { get; set; }
        public AllOrNoneHeader AllOrNoneHeaderValue { get; set; }
        public APIPerformanceInfo APIPerformanceInfoValue { get; set; }
        public MetadataVersionCheck MetadataVersionCheckValue { get; set; }

        private System.Threading.SendOrPostCallback createOperationCompleted;
        private System.Threading.SendOrPostCallback deleteOperationCompleted;
        private System.Threading.SendOrPostCallback describeGlobalOperationCompleted;
        private System.Threading.SendOrPostCallback describeLayoutOperationCompleted;
        private System.Threading.SendOrPostCallback describeSObjectOperationCompleted;
        private System.Threading.SendOrPostCallback describeSObjectsOperationCompleted;
        private System.Threading.SendOrPostCallback describeSoqlListViewsOperationCompleted;
        private System.Threading.SendOrPostCallback describeValueTypeOperationCompleted;
        private System.Threading.SendOrPostCallback describeWorkitemActionsOperationCompleted;
        private System.Threading.SendOrPostCallback executeAnonymousOperationCompleted;
        private System.Threading.SendOrPostCallback getDeletedOperationCompleted;
        private System.Threading.SendOrPostCallback getServerTimestampOperationCompleted;
        private System.Threading.SendOrPostCallback getUpdatedOperationCompleted;
        private System.Threading.SendOrPostCallback getUserInfoOperationCompleted;
        private System.Threading.SendOrPostCallback invalidateSessionsOperationCompleted;
        private System.Threading.SendOrPostCallback loginOperationCompleted;
        private System.Threading.SendOrPostCallback logoutOperationCompleted;
        private System.Threading.SendOrPostCallback queryOperationCompleted;
        private System.Threading.SendOrPostCallback queryAllOperationCompleted;
        private System.Threading.SendOrPostCallback queryMoreOperationCompleted;
        private System.Threading.SendOrPostCallback retrieveOperationCompleted;
        private System.Threading.SendOrPostCallback runTestsOperationCompleted;
        private System.Threading.SendOrPostCallback runTestsAsynchronousOperationCompleted;
        private System.Threading.SendOrPostCallback searchOperationCompleted;
        private System.Threading.SendOrPostCallback setPasswordOperationCompleted;
        private System.Threading.SendOrPostCallback updateOperationCompleted;
        private System.Threading.SendOrPostCallback upsertOperationCompleted;

        /// <remarks/>
        public new event SfdcConnect.Tooling.loginCompletedEventHandler loginCompleted;

        /// <remarks/>
        public new event SfdcConnect.Tooling.logoutCompletedEventHandler logoutCompleted;

        /// <remarks/>
        public event createCompletedEventHandler createCompleted;

        /// <remarks/>
        public event deleteCompletedEventHandler deleteCompleted;

        /// <remarks/>
        public event describeGlobalCompletedEventHandler describeGlobalCompleted;

        /// <remarks/>
        public event describeLayoutCompletedEventHandler describeLayoutCompleted;

        /// <remarks/>
        public event describeSObjectCompletedEventHandler describeSObjectCompleted;

        /// <remarks/>
        public event describeSObjectsCompletedEventHandler describeSObjectsCompleted;

        /// <remarks/>
        public event describeSoqlListViewsCompletedEventHandler describeSoqlListViewsCompleted;

        /// <remarks/>
        public event describeValueTypeCompletedEventHandler describeValueTypeCompleted;

        /// <remarks/>
        public event describeWorkitemActionsCompletedEventHandler describeWorkitemActionsCompleted;

        /// <remarks/>
        public event executeAnonymousCompletedEventHandler executeAnonymousCompleted;

        /// <remarks/>
        public event getDeletedCompletedEventHandler getDeletedCompleted;

        /// <remarks/>
        public event getServerTimestampCompletedEventHandler getServerTimestampCompleted;

        /// <remarks/>
        public event getUpdatedCompletedEventHandler getUpdatedCompleted;

        /// <remarks/>
        public event getUserInfoCompletedEventHandler getUserInfoCompleted;

        /// <remarks/>
        public event invalidateSessionsCompletedEventHandler invalidateSessionsCompleted;

        /// <remarks/>
        public event queryCompletedEventHandler queryCompleted;

        /// <remarks/>
        public event queryAllCompletedEventHandler queryAllCompleted;

        /// <remarks/>
        public event queryMoreCompletedEventHandler queryMoreCompleted;

        /// <remarks/>
        public event retrieveCompletedEventHandler retrieveCompleted;

        /// <remarks/>
        public event runTestsCompletedEventHandler runTestsCompleted;

        /// <remarks/>
        public event runTestsAsynchronousCompletedEventHandler runTestsAsynchronousCompleted;

        /// <remarks/>
        public event searchCompletedEventHandler searchCompleted;

        /// <remarks/>
        public event setPasswordCompletedEventHandler setPasswordCompleted;

        /// <remarks/>
        public event updateCompletedEventHandler updateCompleted;

        /// <remarks/>
        public event upsertCompletedEventHandler upsertCompleted;
        #endregion

        #region Synchronous Calls
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "urn:tooling.soap.sforce.com", ResponseNamespace = "urn:tooling.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("result")]
        public new LoginResult login(string username, string password)
        {
            object[] results = this.Invoke("login", new object[] {
                        username,
                        password});
            return ((LoginResult)(results[0]));
        }
        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("SessionHeaderValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "urn:tooling.soap.sforce.com", ResponseNamespace = "urn:tooling.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public new void logout()
        {
            this.Invoke("logout", new object[0]);
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("APIPerformanceInfoValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
        [System.Web.Services.Protocols.SoapHeaderAttribute("CallOptionsValue")]
        [System.Web.Services.Protocols.SoapHeaderAttribute("AllOrNoneHeaderValue")]
        [System.Web.Services.Protocols.SoapHeaderAttribute("SessionHeaderValue")]
        [System.Web.Services.Protocols.SoapHeaderAttribute("MetadataWarningsHeaderValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "urn:tooling.soap.sforce.com", ResponseNamespace = "urn:tooling.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("result")]
        public SaveResult[] create([System.Xml.Serialization.XmlElementAttribute("sObjects")] sObject[] sObjects)
        {
            object[] results = this.Invoke("create", new object[] {
                        sObjects});
            return ((SaveResult[])(results[0]));
        }
        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("APIPerformanceInfoValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
        [System.Web.Services.Protocols.SoapHeaderAttribute("CallOptionsValue")]
        [System.Web.Services.Protocols.SoapHeaderAttribute("AllOrNoneHeaderValue")]
        [System.Web.Services.Protocols.SoapHeaderAttribute("SessionHeaderValue")]
        [System.Web.Services.Protocols.SoapHeaderAttribute("MetadataWarningsHeaderValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "urn:tooling.soap.sforce.com", ResponseNamespace = "urn:tooling.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("result")]
        public DeleteResult[] delete([System.Xml.Serialization.XmlElementAttribute("ids")] string[] ids)
        {
            object[] results = this.Invoke("delete", new object[] {
                        ids});
            return ((DeleteResult[])(results[0]));
        }
        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("APIPerformanceInfoValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
        [System.Web.Services.Protocols.SoapHeaderAttribute("CallOptionsValue")]
        [System.Web.Services.Protocols.SoapHeaderAttribute("SessionHeaderValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "urn:tooling.soap.sforce.com", ResponseNamespace = "urn:tooling.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("result")]
        public DescribeGlobalResult describeGlobal()
        {
            object[] results = this.Invoke("describeGlobal", new object[0]);
            return ((DescribeGlobalResult)(results[0]));
        }
        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("SessionHeaderValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "urn:tooling.soap.sforce.com", ResponseNamespace = "urn:tooling.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlArrayAttribute("result")]
        [return: System.Xml.Serialization.XmlArrayItemAttribute("layouts", IsNullable = false)]
        public DescribeLayout[] describeLayout(string type, string layoutName, [System.Xml.Serialization.XmlElementAttribute("recordTypeIds")] string[] recordTypeIds)
        {
            object[] results = this.Invoke("describeLayout", new object[] {
                        type,
                        layoutName,
                        recordTypeIds});
            return ((DescribeLayout[])(results[0]));
        }
        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("APIPerformanceInfoValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
        [System.Web.Services.Protocols.SoapHeaderAttribute("CallOptionsValue")]
        [System.Web.Services.Protocols.SoapHeaderAttribute("SessionHeaderValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "urn:tooling.soap.sforce.com", ResponseNamespace = "urn:tooling.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("result")]
        public DescribeSObjectResult describeSObject(string type)
        {
            object[] results = this.Invoke("describeSObject", new object[] {
                        type});
            return ((DescribeSObjectResult)(results[0]));
        }
        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("APIPerformanceInfoValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
        [System.Web.Services.Protocols.SoapHeaderAttribute("CallOptionsValue")]
        [System.Web.Services.Protocols.SoapHeaderAttribute("SessionHeaderValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "urn:tooling.soap.sforce.com", ResponseNamespace = "urn:tooling.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("result")]
        public DescribeSObjectResult[] describeSObjects([System.Xml.Serialization.XmlElementAttribute("types")] string[] types)
        {
            object[] results = this.Invoke("describeSObjects", new object[] {
                        types});
            return ((DescribeSObjectResult[])(results[0]));
        }
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "urn:tooling.soap.sforce.com", ResponseNamespace = "urn:tooling.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlArrayAttribute("result")]
        [return: System.Xml.Serialization.XmlArrayItemAttribute("describeSoqlListViews", IsNullable = false)]
        public DescribeSoqlListView[] describeSoqlListViews([System.Xml.Serialization.XmlArrayItemAttribute("listViewParams", IsNullable = false)] DescribeSoqlListViewParams[] request)
        {
            object[] results = this.Invoke("describeSoqlListViews", new object[] {
                        request});
            return ((DescribeSoqlListView[])(results[0]));
        }
        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("SessionHeaderValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "urn:tooling.soap.sforce.com", ResponseNamespace = "urn:tooling.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("result")]
        public DescribeValueTypeResult describeValueType(string type)
        {
            object[] results = this.Invoke("describeValueType", new object[] {
                        type});
            return ((DescribeValueTypeResult)(results[0]));
        }
        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("SessionHeaderValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "urn:tooling.soap.sforce.com", ResponseNamespace = "urn:tooling.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("result")]
        public DescribeWorkitemActionResult[] describeWorkitemActions([System.Xml.Serialization.XmlElementAttribute("workitemIds")] string[] workitemIds)
        {
            object[] results = this.Invoke("describeWorkitemActions", new object[] {
                        workitemIds});
            return ((DescribeWorkitemActionResult[])(results[0]));
        }
        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("DebuggingInfoValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
        [System.Web.Services.Protocols.SoapHeaderAttribute("CallOptionsValue")]
        [System.Web.Services.Protocols.SoapHeaderAttribute("PackageVersionHeaderValue")]
        [System.Web.Services.Protocols.SoapHeaderAttribute("AllowFieldTruncationHeaderValue")]
        [System.Web.Services.Protocols.SoapHeaderAttribute("SessionHeaderValue")]
        [System.Web.Services.Protocols.SoapHeaderAttribute("DebuggingHeaderValue")]
        [System.Web.Services.Protocols.SoapHeaderAttribute("DisableFeedTrackingHeaderValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "urn:tooling.soap.sforce.com", ResponseNamespace = "urn:tooling.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("result")]
        public ExecuteAnonymousResult executeAnonymous(string String)
        {
            object[] results = this.Invoke("executeAnonymous", new object[] {
                        String});
            return ((ExecuteAnonymousResult)(results[0]));
        }
        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("SessionHeaderValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "urn:tooling.soap.sforce.com", ResponseNamespace = "urn:tooling.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("result")]
        public GetDeletedResult getDeleted(string sObjectType, System.DateTime start, System.DateTime end)
        {
            object[] results = this.Invoke("getDeleted", new object[] {
                        sObjectType,
                        start,
                        end});
            return ((GetDeletedResult)(results[0]));
        }
        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("SessionHeaderValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "urn:tooling.soap.sforce.com", ResponseNamespace = "urn:tooling.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("result")]
        public GetServerTimestampResult getServerTimestamp()
        {
            object[] results = this.Invoke("getServerTimestamp", new object[0]);
            return ((GetServerTimestampResult)(results[0]));
        }
        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("SessionHeaderValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "urn:tooling.soap.sforce.com", ResponseNamespace = "urn:tooling.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("result")]
        public GetUpdatedResult getUpdated(string sObjectType, System.DateTime start, System.DateTime end)
        {
            object[] results = this.Invoke("getUpdated", new object[] {
                        sObjectType,
                        start,
                        end});
            return ((GetUpdatedResult)(results[0]));
        }
        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("SessionHeaderValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "urn:tooling.soap.sforce.com", ResponseNamespace = "urn:tooling.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("result")]
        public GetUserInfoResult getUserInfo()
        {
            object[] results = this.Invoke("getUserInfo", new object[0]);
            return ((GetUserInfoResult)(results[0]));
        }
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "urn:tooling.soap.sforce.com", ResponseNamespace = "urn:tooling.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("result")]
        public InvalidateSessionsResult[] invalidateSessions([System.Xml.Serialization.XmlElementAttribute("ArrayList")] string[] ArrayList)
        {
            object[] results = this.Invoke("invalidateSessions", new object[] {
                        ArrayList});
            return ((InvalidateSessionsResult[])(results[0]));
        }
        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("MetadataVersionCheckValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.InOut)]
        [System.Web.Services.Protocols.SoapHeaderAttribute("APIPerformanceInfoValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
        [System.Web.Services.Protocols.SoapHeaderAttribute("CallOptionsValue")]
        [System.Web.Services.Protocols.SoapHeaderAttribute("SessionHeaderValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "urn:tooling.soap.sforce.com", ResponseNamespace = "urn:tooling.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("result")]
        public QueryResult query(string queryString)
        {
            object[] results = this.Invoke("query", new object[] {
                        queryString});
            return ((QueryResult)(results[0]));
        }
        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("APIPerformanceInfoValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
        [System.Web.Services.Protocols.SoapHeaderAttribute("CallOptionsValue")]
        [System.Web.Services.Protocols.SoapHeaderAttribute("SessionHeaderValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "urn:tooling.soap.sforce.com", ResponseNamespace = "urn:tooling.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("result")]
        public QueryResult queryAll(string queryString)
        {
            object[] results = this.Invoke("queryAll", new object[] {
                        queryString});
            return ((QueryResult)(results[0]));
        }
        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("APIPerformanceInfoValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
        [System.Web.Services.Protocols.SoapHeaderAttribute("CallOptionsValue")]
        [System.Web.Services.Protocols.SoapHeaderAttribute("SessionHeaderValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "urn:tooling.soap.sforce.com", ResponseNamespace = "urn:tooling.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("result")]
        public QueryResult queryMore(string queryLocator)
        {
            object[] results = this.Invoke("queryMore", new object[] {
                        queryLocator});
            return ((QueryResult)(results[0]));
        }
        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("APIPerformanceInfoValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
        [System.Web.Services.Protocols.SoapHeaderAttribute("CallOptionsValue")]
        [System.Web.Services.Protocols.SoapHeaderAttribute("SessionHeaderValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "urn:tooling.soap.sforce.com", ResponseNamespace = "urn:tooling.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("result")]
        public sObject[] retrieve(string select, string type, [System.Xml.Serialization.XmlElementAttribute("ids")] string[] ids)
        {
            object[] results = this.Invoke("retrieve", new object[] {
                        select,
                        type,
                        ids});
            return ((sObject[])(results[0]));
        }
        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("DebuggingInfoValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
        [System.Web.Services.Protocols.SoapHeaderAttribute("CallOptionsValue")]
        [System.Web.Services.Protocols.SoapHeaderAttribute("DebuggingHeaderValue")]
        [System.Web.Services.Protocols.SoapHeaderAttribute("SessionHeaderValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "urn:tooling.soap.sforce.com", ResponseNamespace = "urn:tooling.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("result")]
        public RunTestsResult runTests(RunTestsRequest RunTestsRequest)
        {
            object[] results = this.Invoke("runTests", new object[] {
                        RunTestsRequest});
            return ((RunTestsResult)(results[0]));
        }
        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("DebuggingInfoValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
        [System.Web.Services.Protocols.SoapHeaderAttribute("CallOptionsValue")]
        [System.Web.Services.Protocols.SoapHeaderAttribute("DebuggingHeaderValue")]
        [System.Web.Services.Protocols.SoapHeaderAttribute("SessionHeaderValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "urn:tooling.soap.sforce.com", ResponseNamespace = "urn:tooling.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("result")]
        public string runTestsAsynchronous(string classids, string suiteids, int maxFailedTests, TestLevel testLevel, string classNames, string suiteNames)
        {
            object[] results = this.Invoke("runTestsAsynchronous", new object[] {
                        classids,
                        suiteids,
                        maxFailedTests,
                        testLevel,
                        classNames,
                        suiteNames});
            return ((string)(results[0]));
        }
        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("APIPerformanceInfoValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
        [System.Web.Services.Protocols.SoapHeaderAttribute("CallOptionsValue")]
        [System.Web.Services.Protocols.SoapHeaderAttribute("SessionHeaderValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "urn:tooling.soap.sforce.com", ResponseNamespace = "urn:tooling.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("result")]
        public SearchResult search(string searchString)
        {
            object[] results = this.Invoke("search", new object[] {
                        searchString});
            return ((SearchResult)(results[0]));
        }
        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("SessionHeaderValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "urn:tooling.soap.sforce.com", ResponseNamespace = "urn:tooling.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("result")]
        public SetPasswordResult setPassword(string userId, string password)
        {
            object[] results = this.Invoke("setPassword", new object[] {
                        userId,
                        password});
            return ((SetPasswordResult)(results[0]));
        }
        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("MetadataWarningsHeaderValue")]
        [System.Web.Services.Protocols.SoapHeaderAttribute("APIPerformanceInfoValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
        [System.Web.Services.Protocols.SoapHeaderAttribute("MetadataVersionCheckValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.InOut)]
        [System.Web.Services.Protocols.SoapHeaderAttribute("AllOrNoneHeaderValue")]
        [System.Web.Services.Protocols.SoapHeaderAttribute("SessionHeaderValue")]
        [System.Web.Services.Protocols.SoapHeaderAttribute("CallOptionsValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "urn:tooling.soap.sforce.com", ResponseNamespace = "urn:tooling.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("result")]
        public SaveResult[] update([System.Xml.Serialization.XmlElementAttribute("sObjects")] sObject[] sObjects)
        {
            object[] results = this.Invoke("update", new object[] {
                        sObjects});
            return ((SaveResult[])(results[0]));
        }
        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("MetadataWarningsHeaderValue")]
        [System.Web.Services.Protocols.SoapHeaderAttribute("APIPerformanceInfoValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
        [System.Web.Services.Protocols.SoapHeaderAttribute("MetadataVersionCheckValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.InOut)]
        [System.Web.Services.Protocols.SoapHeaderAttribute("AllOrNoneHeaderValue")]
        [System.Web.Services.Protocols.SoapHeaderAttribute("SessionHeaderValue")]
        [System.Web.Services.Protocols.SoapHeaderAttribute("CallOptionsValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "urn:tooling.soap.sforce.com", ResponseNamespace = "urn:tooling.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("result")]
        public UpsertResult[] upsert(string fieldName, [System.Xml.Serialization.XmlElementAttribute("entities")] sObject[] entities)
        {
            object[] results = this.Invoke("upsert", new object[] {
                        fieldName,
                        entities});
            return ((UpsertResult[])(results[0]));
        }
        #endregion

        #region Asynchronous Calls

        /// <remarks/>
        public void createAsync(sObject[] sObjects)
        {
            this.createAsync(sObjects, null);
        }
        /// <remarks/>
        public void createAsync(sObject[] sObjects, object userState)
        {
            if ((this.createOperationCompleted == null))
            {
                this.createOperationCompleted = new System.Threading.SendOrPostCallback(this.OncreateOperationCompleted);
            }
            this.InvokeAsync("create", new object[] {
                        sObjects}, this.createOperationCompleted, userState);
        }
        /// <remarks/>
        public void deleteAsync(string[] ids)
        {
            this.deleteAsync(ids, null);
        }
        /// <remarks/>
        public void deleteAsync(string[] ids, object userState)
        {
            if ((this.deleteOperationCompleted == null))
            {
                this.deleteOperationCompleted = new System.Threading.SendOrPostCallback(this.OndeleteOperationCompleted);
            }
            this.InvokeAsync("delete", new object[] {
                        ids}, this.deleteOperationCompleted, userState);
        }
        /// <remarks/>
        public void describeGlobalAsync()
        {
            this.describeGlobalAsync(null);
        }
        /// <remarks/>
        public void describeGlobalAsync(object userState)
        {
            if ((this.describeGlobalOperationCompleted == null))
            {
                this.describeGlobalOperationCompleted = new System.Threading.SendOrPostCallback(this.OndescribeGlobalOperationCompleted);
            }
            this.InvokeAsync("describeGlobal", new object[0], this.describeGlobalOperationCompleted, userState);
        }
        /// <remarks/>
        public void describeLayoutAsync(string type, string layoutName, string[] recordTypeIds)
        {
            this.describeLayoutAsync(type, layoutName, recordTypeIds, null);
        }
        /// <remarks/>
        public void describeLayoutAsync(string type, string layoutName, string[] recordTypeIds, object userState)
        {
            if ((this.describeLayoutOperationCompleted == null))
            {
                this.describeLayoutOperationCompleted = new System.Threading.SendOrPostCallback(this.OndescribeLayoutOperationCompleted);
            }
            this.InvokeAsync("describeLayout", new object[] {
                        type,
                        layoutName,
                        recordTypeIds}, this.describeLayoutOperationCompleted, userState);
        }
        /// <remarks/>
        public void describeSObjectAsync(string type)
        {
            this.describeSObjectAsync(type, null);
        }
        /// <remarks/>
        public void describeSObjectAsync(string type, object userState)
        {
            if ((this.describeSObjectOperationCompleted == null))
            {
                this.describeSObjectOperationCompleted = new System.Threading.SendOrPostCallback(this.OndescribeSObjectOperationCompleted);
            }
            this.InvokeAsync("describeSObject", new object[] {
                        type}, this.describeSObjectOperationCompleted, userState);
        }
        /// <remarks/>
        public void describeSObjectsAsync(string[] types)
        {
            this.describeSObjectsAsync(types, null);
        }
        /// <remarks/>
        public void describeSObjectsAsync(string[] types, object userState)
        {
            if ((this.describeSObjectsOperationCompleted == null))
            {
                this.describeSObjectsOperationCompleted = new System.Threading.SendOrPostCallback(this.OndescribeSObjectsOperationCompleted);
            }
            this.InvokeAsync("describeSObjects", new object[] {
                        types}, this.describeSObjectsOperationCompleted, userState);
        }
        /// <remarks/>
        public void describeSoqlListViewsAsync(DescribeSoqlListViewParams[] request)
        {
            this.describeSoqlListViewsAsync(request, null);
        }
        /// <remarks/>
        public void describeSoqlListViewsAsync(DescribeSoqlListViewParams[] request, object userState)
        {
            if ((this.describeSoqlListViewsOperationCompleted == null))
            {
                this.describeSoqlListViewsOperationCompleted = new System.Threading.SendOrPostCallback(this.OndescribeSoqlListViewsOperationCompleted);
            }
            this.InvokeAsync("describeSoqlListViews", new object[] {
                        request}, this.describeSoqlListViewsOperationCompleted, userState);
        }
        /// <remarks/>
        public void describeValueTypeAsync(string type)
        {
            this.describeValueTypeAsync(type, null);
        }
        /// <remarks/>
        public void describeValueTypeAsync(string type, object userState)
        {
            if ((this.describeValueTypeOperationCompleted == null))
            {
                this.describeValueTypeOperationCompleted = new System.Threading.SendOrPostCallback(this.OndescribeValueTypeOperationCompleted);
            }
            this.InvokeAsync("describeValueType", new object[] {
                        type}, this.describeValueTypeOperationCompleted, userState);
        }
        /// <remarks/>
        public void describeWorkitemActionsAsync(string[] workitemIds)
        {
            this.describeWorkitemActionsAsync(workitemIds, null);
        }
        /// <remarks/>
        public void describeWorkitemActionsAsync(string[] workitemIds, object userState)
        {
            if ((this.describeWorkitemActionsOperationCompleted == null))
            {
                this.describeWorkitemActionsOperationCompleted = new System.Threading.SendOrPostCallback(this.OndescribeWorkitemActionsOperationCompleted);
            }
            this.InvokeAsync("describeWorkitemActions", new object[] {
                        workitemIds}, this.describeWorkitemActionsOperationCompleted, userState);
        }
        /// <remarks/>
        public void executeAnonymousAsync(string String)
        {
            this.executeAnonymousAsync(String, null);
        }
        /// <remarks/>
        public void executeAnonymousAsync(string String, object userState)
        {
            if ((this.executeAnonymousOperationCompleted == null))
            {
                this.executeAnonymousOperationCompleted = new System.Threading.SendOrPostCallback(this.OnexecuteAnonymousOperationCompleted);
            }
            this.InvokeAsync("executeAnonymous", new object[] {
                        String}, this.executeAnonymousOperationCompleted, userState);
        }
        /// <remarks/>
        public void getDeletedAsync(string sObjectType, System.DateTime start, System.DateTime end)
        {
            this.getDeletedAsync(sObjectType, start, end, null);
        }
        /// <remarks/>
        public void getDeletedAsync(string sObjectType, System.DateTime start, System.DateTime end, object userState)
        {
            if ((this.getDeletedOperationCompleted == null))
            {
                this.getDeletedOperationCompleted = new System.Threading.SendOrPostCallback(this.OngetDeletedOperationCompleted);
            }
            this.InvokeAsync("getDeleted", new object[] {
                        sObjectType,
                        start,
                        end}, this.getDeletedOperationCompleted, userState);
        }
        /// <remarks/>
        public void getServerTimestampAsync()
        {
            this.getServerTimestampAsync(null);
        }
        /// <remarks/>
        public void getServerTimestampAsync(object userState)
        {
            if ((this.getServerTimestampOperationCompleted == null))
            {
                this.getServerTimestampOperationCompleted = new System.Threading.SendOrPostCallback(this.OngetServerTimestampOperationCompleted);
            }
            this.InvokeAsync("getServerTimestamp", new object[0], this.getServerTimestampOperationCompleted, userState);
        }
        /// <remarks/>
        public void getUpdatedAsync(string sObjectType, System.DateTime start, System.DateTime end)
        {
            this.getUpdatedAsync(sObjectType, start, end, null);
        }
        /// <remarks/>
        public void getUpdatedAsync(string sObjectType, System.DateTime start, System.DateTime end, object userState)
        {
            if ((this.getUpdatedOperationCompleted == null))
            {
                this.getUpdatedOperationCompleted = new System.Threading.SendOrPostCallback(this.OngetUpdatedOperationCompleted);
            }
            this.InvokeAsync("getUpdated", new object[] {
                        sObjectType,
                        start,
                        end}, this.getUpdatedOperationCompleted, userState);
        }
        /// <remarks/>
        public void getUserInfoAsync()
        {
            this.getUserInfoAsync(null);
        }
        /// <remarks/>
        public void getUserInfoAsync(object userState)
        {
            if ((this.getUserInfoOperationCompleted == null))
            {
                this.getUserInfoOperationCompleted = new System.Threading.SendOrPostCallback(this.OngetUserInfoOperationCompleted);
            }
            this.InvokeAsync("getUserInfo", new object[0], this.getUserInfoOperationCompleted, userState);
        }
        /// <remarks/>
        public void invalidateSessionsAsync(string[] ArrayList)
        {
            this.invalidateSessionsAsync(ArrayList, null);
        }
        /// <remarks/>
        public void invalidateSessionsAsync(string[] ArrayList, object userState)
        {
            if ((this.invalidateSessionsOperationCompleted == null))
            {
                this.invalidateSessionsOperationCompleted = new System.Threading.SendOrPostCallback(this.OninvalidateSessionsOperationCompleted);
            }
            this.InvokeAsync("invalidateSessions", new object[] {
                        ArrayList}, this.invalidateSessionsOperationCompleted, userState);
        }
        /// <remarks/>
        public void loginAsync(string username, string password)
        {
            this.loginAsync(username, password, null);
        }
        /// <remarks/>
        public void loginAsync(string username, string password, object userState)
        {
            if ((this.loginOperationCompleted == null))
            {
                this.loginOperationCompleted = new System.Threading.SendOrPostCallback(this.OnloginOperationCompleted);
            }
            this.InvokeAsync("login", new object[] {
                        username,
                        password}, this.loginOperationCompleted, userState);
        }
        /// <remarks/>
        public void logoutAsync()
        {
            this.logoutAsync(null);
        }
        /// <remarks/>
        public void logoutAsync(object userState)
        {
            if ((this.logoutOperationCompleted == null))
            {
                this.logoutOperationCompleted = new System.Threading.SendOrPostCallback(this.OnlogoutOperationCompleted);
            }
            this.InvokeAsync("logout", new object[0], this.logoutOperationCompleted, userState);
        }
        /// <remarks/>
        public void queryAsync(string queryString)
        {
            this.queryAsync(queryString, null);
        }
        /// <remarks/>
        public void queryAsync(string queryString, object userState)
        {
            if ((this.queryOperationCompleted == null))
            {
                this.queryOperationCompleted = new System.Threading.SendOrPostCallback(this.OnqueryOperationCompleted);
            }
            this.InvokeAsync("query", new object[] {
                        queryString}, this.queryOperationCompleted, userState);
        }
        /// <remarks/>
        public void queryAllAsync(string queryString)
        {
            this.queryAllAsync(queryString, null);
        }
        /// <remarks/>
        public void queryAllAsync(string queryString, object userState)
        {
            if ((this.queryAllOperationCompleted == null))
            {
                this.queryAllOperationCompleted = new System.Threading.SendOrPostCallback(this.OnqueryAllOperationCompleted);
            }
            this.InvokeAsync("queryAll", new object[] {
                        queryString}, this.queryAllOperationCompleted, userState);
        }
        /// <remarks/>
        public void queryMoreAsync(string queryLocator)
        {
            this.queryMoreAsync(queryLocator, null);
        }
        /// <remarks/>
        public void queryMoreAsync(string queryLocator, object userState)
        {
            if ((this.queryMoreOperationCompleted == null))
            {
                this.queryMoreOperationCompleted = new System.Threading.SendOrPostCallback(this.OnqueryMoreOperationCompleted);
            }
            this.InvokeAsync("queryMore", new object[] {
                        queryLocator}, this.queryMoreOperationCompleted, userState);
        }
        /// <remarks/>
        public void retrieveAsync(string select, string type, string[] ids)
        {
            this.retrieveAsync(select, type, ids, null);
        }
        /// <remarks/>
        public void retrieveAsync(string select, string type, string[] ids, object userState)
        {
            if ((this.retrieveOperationCompleted == null))
            {
                this.retrieveOperationCompleted = new System.Threading.SendOrPostCallback(this.OnretrieveOperationCompleted);
            }
            this.InvokeAsync("retrieve", new object[] {
                        select,
                        type,
                        ids}, this.retrieveOperationCompleted, userState);
        }
        /// <remarks/>
        public void runTestsAsync(RunTestsRequest RunTestsRequest)
        {
            this.runTestsAsync(RunTestsRequest, null);
        }
        /// <remarks/>
        public void runTestsAsync(RunTestsRequest RunTestsRequest, object userState)
        {
            if ((this.runTestsOperationCompleted == null))
            {
                this.runTestsOperationCompleted = new System.Threading.SendOrPostCallback(this.OnrunTestsOperationCompleted);
            }
            this.InvokeAsync("runTests", new object[] {
                        RunTestsRequest}, this.runTestsOperationCompleted, userState);
        }
        /// <remarks/>
        public void runTestsAsynchronousAsync(string classids, string suiteids, int maxFailedTests, TestLevel testLevel, string classNames, string suiteNames)
        {
            this.runTestsAsynchronousAsync(classids, suiteids, maxFailedTests, testLevel, classNames, suiteNames, null);
        }
        /// <remarks/>
        public void runTestsAsynchronousAsync(string classids, string suiteids, int maxFailedTests, TestLevel testLevel, string classNames, string suiteNames, object userState)
        {
            if ((this.runTestsAsynchronousOperationCompleted == null))
            {
                this.runTestsAsynchronousOperationCompleted = new System.Threading.SendOrPostCallback(this.OnrunTestsAsynchronousOperationCompleted);
            }
            this.InvokeAsync("runTestsAsynchronous", new object[] {
                        classids,
                        suiteids,
                        maxFailedTests,
                        testLevel,
                        classNames,
                        suiteNames}, this.runTestsAsynchronousOperationCompleted, userState);
        }
        /// <remarks/>
        public void searchAsync(string searchString)
        {
            this.searchAsync(searchString, null);
        }
        /// <remarks/>
        public void searchAsync(string searchString, object userState)
        {
            if ((this.searchOperationCompleted == null))
            {
                this.searchOperationCompleted = new System.Threading.SendOrPostCallback(this.OnsearchOperationCompleted);
            }
            this.InvokeAsync("search", new object[] {
                        searchString}, this.searchOperationCompleted, userState);
        }
        /// <remarks/>
        public void setPasswordAsync(string userId, string password)
        {
            this.setPasswordAsync(userId, password, null);
        }
        /// <remarks/>
        public void setPasswordAsync(string userId, string password, object userState)
        {
            if ((this.setPasswordOperationCompleted == null))
            {
                this.setPasswordOperationCompleted = new System.Threading.SendOrPostCallback(this.OnsetPasswordOperationCompleted);
            }
            this.InvokeAsync("setPassword", new object[] {
                        userId,
                        password}, this.setPasswordOperationCompleted, userState);
        }
        /// <remarks/>
        public void updateAsync(sObject[] sObjects)
        {
            this.updateAsync(sObjects, null);
        }
        /// <remarks/>
        public void updateAsync(sObject[] sObjects, object userState)
        {
            if ((this.updateOperationCompleted == null))
            {
                this.updateOperationCompleted = new System.Threading.SendOrPostCallback(this.OnupdateOperationCompleted);
            }
            this.InvokeAsync("update", new object[] {
                        sObjects}, this.updateOperationCompleted, userState);
        }
        /// <remarks/>
        public void upsertAsync(string fieldName, sObject[] entities)
        {
            this.upsertAsync(fieldName, entities, null);
        }
        /// <remarks/>
        public void upsertAsync(string fieldName, sObject[] entities, object userState)
        {
            if ((this.upsertOperationCompleted == null))
            {
                this.upsertOperationCompleted = new System.Threading.SendOrPostCallback(this.OnupsertOperationCompleted);
            }
            this.InvokeAsync("upsert", new object[] {
                        fieldName,
                        entities}, this.upsertOperationCompleted, userState);
        }
        /// <remarks/>
        public new void CancelAsync(object userState)
        {
            base.CancelAsync(userState);
        }
        #endregion

        #region Callbacks
        private void OndeleteOperationCompleted(object arg)
        {
            if ((this.deleteCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.deleteCompleted(this, new deleteCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        private void OncreateOperationCompleted(object arg)
        {
            if ((this.createCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.createCompleted(this, new createCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        private void OndescribeGlobalOperationCompleted(object arg)
        {
            if ((this.describeGlobalCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.describeGlobalCompleted(this, new describeGlobalCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        private void OndescribeLayoutOperationCompleted(object arg)
        {
            if ((this.describeLayoutCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.describeLayoutCompleted(this, new describeLayoutCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        private void OndescribeSObjectOperationCompleted(object arg)
        {
            if ((this.describeSObjectCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.describeSObjectCompleted(this, new describeSObjectCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        private void OndescribeSObjectsOperationCompleted(object arg)
        {
            if ((this.describeSObjectsCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.describeSObjectsCompleted(this, new describeSObjectsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        private void OndescribeSoqlListViewsOperationCompleted(object arg)
        {
            if ((this.describeSoqlListViewsCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.describeSoqlListViewsCompleted(this, new describeSoqlListViewsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        private void OndescribeValueTypeOperationCompleted(object arg)
        {
            if ((this.describeValueTypeCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.describeValueTypeCompleted(this, new describeValueTypeCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        private void OndescribeWorkitemActionsOperationCompleted(object arg)
        {
            if ((this.describeWorkitemActionsCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.describeWorkitemActionsCompleted(this, new describeWorkitemActionsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        private void OnexecuteAnonymousOperationCompleted(object arg)
        {
            if ((this.executeAnonymousCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.executeAnonymousCompleted(this, new executeAnonymousCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        private void OngetDeletedOperationCompleted(object arg)
        {
            if ((this.getDeletedCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.getDeletedCompleted(this, new getDeletedCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        private void OngetServerTimestampOperationCompleted(object arg)
        {
            if ((this.getServerTimestampCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.getServerTimestampCompleted(this, new getServerTimestampCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        private void OngetUpdatedOperationCompleted(object arg)
        {
            if ((this.getUpdatedCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.getUpdatedCompleted(this, new getUpdatedCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        private void OngetUserInfoOperationCompleted(object arg)
        {
            if ((this.getUserInfoCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.getUserInfoCompleted(this, new getUserInfoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        private void OninvalidateSessionsOperationCompleted(object arg)
        {
            if ((this.invalidateSessionsCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.invalidateSessionsCompleted(this, new invalidateSessionsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        private void OnloginOperationCompleted(object arg)
        {
            if ((this.loginCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.loginCompleted(this, new loginCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        private void OnlogoutOperationCompleted(object arg)
        {
            if ((this.logoutCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.logoutCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        private void OnqueryOperationCompleted(object arg)
        {
            if ((this.queryCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.queryCompleted(this, new queryCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        private void OnqueryAllOperationCompleted(object arg)
        {
            if ((this.queryAllCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.queryAllCompleted(this, new queryAllCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        private void OnqueryMoreOperationCompleted(object arg)
        {
            if ((this.queryMoreCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.queryMoreCompleted(this, new queryMoreCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        private void OnretrieveOperationCompleted(object arg)
        {
            if ((this.retrieveCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.retrieveCompleted(this, new retrieveCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        private void OnrunTestsOperationCompleted(object arg)
        {
            if ((this.runTestsCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.runTestsCompleted(this, new runTestsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        private void OnrunTestsAsynchronousOperationCompleted(object arg)
        {
            if ((this.runTestsAsynchronousCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.runTestsAsynchronousCompleted(this, new runTestsAsynchronousCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        private void OnsearchOperationCompleted(object arg)
        {
            if ((this.searchCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.searchCompleted(this, new searchCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        private void OnsetPasswordOperationCompleted(object arg)
        {
            if ((this.setPasswordCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.setPasswordCompleted(this, new setPasswordCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        private void OnupdateOperationCompleted(object arg)
        {
            if ((this.updateCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.updateCompleted(this, new updateCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        private void OnupsertOperationCompleted(object arg)
        {
            if ((this.upsertCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.upsertCompleted(this, new upsertCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        #endregion
    }
}
