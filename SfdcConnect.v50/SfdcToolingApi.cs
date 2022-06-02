/****************************************************************************
*
*   File name: SfdcToolingApi.cs
*   Author: Sean Fife
*   Create date: 5/27/2022
*   Solution: SfdcConnect
*   Project: SfdcConnect
*   Description: Includes the class for Salesforce Tooling API Calls
*
****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Sfdc.Soap.Tooling;

namespace SfdcConnect
{
    public class SfdcToolingApi : System.ServiceModel.ClientBase<SforceServicePortType>
    {
        public SfdcToolingApi(EndpointConfiguration endpointConfiguration, string remoteAddress) :
               base(GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress))
        {
            EndpointsList.Add(endpointConfiguration, remoteAddress);

            Endpoint.Name = endpointConfiguration.ToString();

            Channel = base.ChannelFactory.CreateChannel();

            DataProtector = new SfdcDataProtection();

            SetXmlSerializerFlag(1);
        }
        public SfdcToolingApi(Environment env, int apiversion, string refreshToken = "") :
                base(GetDefaultBinding(),
                    new System.ServiceModel.EndpointAddress(string.Format("https://{0}.salesforce.com/services/Soap/T/{1}.0", env == Environment.Sandbox ? "test" : "login", apiversion)))
        {
            EndpointsList.Add(EndpointConfiguration.Login, string.Format("https://{0}.salesforce.com/services/Soap/T/{1}.0", env == Environment.Sandbox ? "test" : "login", apiversion));

            Endpoint.Name = EndpointConfiguration.Login.ToString();

            Channel = base.ChannelFactory.CreateChannel();

            DataProtector = new SfdcDataProtection();

            SetXmlSerializerFlag(1);
        }

        public void ChangeEndpoint(EndpointConfiguration endpointConfiguration)
        {
            Endpoint.Address = new System.ServiceModel.EndpointAddress(EndpointsList[endpointConfiguration]);
            Endpoint.Name = endpointConfiguration.ToString();

            SetXmlSerializerFlag(1);
        }

        protected new SforceServicePortType Channel { get; set; }

        internal SfdcDataProtection DataProtector;
        protected SfdcSession InternalConnection;

        public CallOptions CallOptions { get; set; }
        public PackageVersion[] PackageVersionHeader { get; set; }
        public PackageVersion1[] PackageVersionExecAnonHeader { get; set; }
        public AllowFieldTruncationHeader AllowFieldTruncationHeader { get; set; }
        public DisableFeedTrackingHeader DisableFeedTrackingHeader { get; set; }
        public AllOrNoneHeader AllOrNoneHeader { get; set; }
        public DebuggingHeader DebuggingHeader { get; set; }
        public MetadataWarningsHeader MetadataWarningsHeader { get; set; }
        public MetadataVersionCheck MetadataVersionCheck { get; set; }

        public DebuggingInfo DebuggingInfo { get; protected set; }
        public APIPerformanceInfo APIPerformanceInfo { get; protected set; }

        /// <summary>
        /// Sets the Xml Serializer Flag. Fixes a bug in the way the serialization happens.
        /// </summary>
        /// <param name="flag">1 = On, 0 = Off</param>
        public static void SetXmlSerializerFlag(int flag = 1)
        {
            MethodInfo method = typeof(XmlSerializer).GetMethod("set_Mode", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
            method.Invoke(null, new object[] { flag });
        }

        private SessionHeader GetSessionHeader()
        {
            SessionHeader sessionHeader = new SessionHeader();
            sessionHeader.sessionId = InternalConnection.SessionId;
            return sessionHeader;
        }

        public void Open(SfdcSession conn)
        {
            InternalConnection = conn;

            if (EndpointsList.ContainsKey(EndpointConfiguration.Active))
            {
                EndpointsList[EndpointConfiguration.Active] = conn.Endpoint.Address.Uri.AbsoluteUri.Replace("/u/", "/T/");
            }
            else
            {
                EndpointsList.Add(EndpointConfiguration.Active, conn.Endpoint.Address.Uri.AbsoluteUri.Replace("/u/", "/T/"));
            }

            ChangeEndpoint(EndpointConfiguration.Active);

            ((IClientChannel)Channel).Close();

            Channel = base.ChannelFactory.CreateChannel();

            ((IClientChannel)Channel).Open();
        }

        public new void Close()
        {
            InternalConnection.Close();

            ((IClientChannel)Channel).Close();
        }

        #region Methods
        public ChangeOwnPasswordResult changeOwnPassword(string oldPassword, string newPassword)
        {
            changeOwnPasswordRequest inValue = new changeOwnPasswordRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.oldPassword = oldPassword;
            inValue.newPassword = newPassword;
            changeOwnPasswordResponse retVal = Channel.changeOwnPassword(inValue);
            return retVal.result;
        }

        public async Task<ChangeOwnPasswordResult> changeOwnPasswordAsync(string oldPassword, string newPassword)
        {
            changeOwnPasswordRequest inValue = new changeOwnPasswordRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.oldPassword = oldPassword;
            inValue.newPassword = newPassword;
            changeOwnPasswordResponse t = await Channel.changeOwnPasswordAsync(inValue);
            return t.result;
        }

        public SaveResult[] create(sObject[] sObjects)
        {
            createRequest inValue = new createRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.MetadataWarningsHeader = MetadataWarningsHeader;
            inValue.AllOrNoneHeader = AllOrNoneHeader;
            inValue.CallOptions = CallOptions;
            inValue.sObjects = sObjects;
            createResponse retVal = Channel.create(inValue);
            APIPerformanceInfo = retVal.APIPerformanceInfo;
            return retVal.result;
        }

        public async Task<SaveResult[]> createAsync(sObject[] sObjects)
        {
            createRequest inValue = new createRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.MetadataWarningsHeader = MetadataWarningsHeader;
            inValue.AllOrNoneHeader = AllOrNoneHeader;
            inValue.CallOptions = CallOptions;
            inValue.sObjects = sObjects;
            createResponse t = await Channel.createAsync(inValue);
            APIPerformanceInfo = t.APIPerformanceInfo;
            return t.result;
        }

        public DeleteResult[] delete(string[] ids)
        {
            deleteRequest inValue = new deleteRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.MetadataWarningsHeader = MetadataWarningsHeader;
            inValue.AllOrNoneHeader = AllOrNoneHeader;
            inValue.CallOptions = CallOptions;
            inValue.ids = ids;
            deleteResponse retVal = Channel.delete(inValue);
            APIPerformanceInfo = retVal.APIPerformanceInfo;
            return retVal.result;
        }

        public async Task<DeleteResult[]> deleteAsync(string[] ids)
        {
            deleteRequest inValue = new deleteRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.MetadataWarningsHeader = MetadataWarningsHeader;
            inValue.AllOrNoneHeader = AllOrNoneHeader;
            inValue.CallOptions = CallOptions;
            inValue.ids = ids;
            deleteResponse t = await Channel.deleteAsync(inValue);
            APIPerformanceInfo = t.APIPerformanceInfo;
            return t.result;
        }

        public DescribeGlobalResult describeGlobal()
        {
            describeGlobalRequest inValue = new describeGlobalRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            describeGlobalResponse retVal = Channel.describeGlobal(inValue);
            APIPerformanceInfo = retVal.APIPerformanceInfo;
            return retVal.result;
        }

        public async Task<DescribeGlobalResult> describeGlobalAsync()
        {
            describeGlobalRequest inValue = new describeGlobalRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            describeGlobalResponse t = await Channel.describeGlobalAsync(inValue);
            APIPerformanceInfo = t.APIPerformanceInfo;
            return t.result;
        }

        public DescribeLayout[] describeLayout(string type, string layoutName, string[] recordTypeIds)
        {
            describeLayoutRequest inValue = new describeLayoutRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.type = type;
            inValue.layoutName = layoutName;
            inValue.recordTypeIds = recordTypeIds;
            describeLayoutResponse retVal = Channel.describeLayout(inValue);
            return retVal.result;
        }

        public async Task<DescribeLayout[]> describeLayoutAsync(string type, string layoutName, string[] recordTypeIds)
        {
            describeLayoutRequest inValue = new describeLayoutRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.type = type;
            inValue.layoutName = layoutName;
            inValue.recordTypeIds = recordTypeIds;
            describeLayoutResponse t = await Channel.describeLayoutAsync(inValue);
            return t.result;
        }

        public DescribeSObjectResult describeSObject(string type)
        {
            describeSObjectRequest inValue = new describeSObjectRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.type = type;
            describeSObjectResponse retVal = Channel.describeSObject(inValue);
            APIPerformanceInfo = retVal.APIPerformanceInfo;
            return retVal.result;
        }

        public async Task<DescribeSObjectResult> describeSObjectAsync(string type)
        {
            describeSObjectRequest inValue = new describeSObjectRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.type = type;
            describeSObjectResponse t = await Channel.describeSObjectAsync(inValue);
            APIPerformanceInfo = t.APIPerformanceInfo;
            return t.result;
        }

        public DescribeSObjectResult[] describeSObjects(string[] types)
        {
            describeSObjectsRequest inValue = new describeSObjectsRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.types = types;
            describeSObjectsResponse retVal = Channel.describeSObjects(inValue);
            APIPerformanceInfo = retVal.APIPerformanceInfo;
            return retVal.result;
        }

        public async Task<DescribeSObjectResult[]> describeSObjectsAsync(string[] types)
        {
            describeSObjectsRequest inValue = new describeSObjectsRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.types = types;
            describeSObjectsResponse t = await Channel.describeSObjectsAsync(inValue);
            APIPerformanceInfo = t.APIPerformanceInfo;
            return t.result;
        }

        public DescribeSoqlListView[] describeSoqlListViews(DescribeSoqlListViewParams[] request)
        {
            describeSoqlListViewsRequest inValue = new describeSoqlListViewsRequest();
            inValue.request = request;
            describeSoqlListViewsResponse retVal = Channel.describeSoqlListViews(inValue);
            return retVal.result;
        }

        public async Task<DescribeSoqlListView[]> describeSoqlListViewsAsync(DescribeSoqlListViewParams[] request)
        {
            describeSoqlListViewsRequest inValue = new describeSoqlListViewsRequest();
            inValue.request = request;
            describeSoqlListViewsResponse t = await Channel.describeSoqlListViewsAsync(inValue);
            return t.result;
        }

        public DescribeValueTypeResult describeValueType(string type)
        {
            describeValueTypeRequest inValue = new describeValueTypeRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.type = type;
            describeValueTypeResponse retVal = Channel.describeValueType(inValue);
            return retVal.result;
        }

        public async Task<DescribeValueTypeResult> describeValueTypeAsync(string type)
        {
            describeValueTypeRequest inValue = new describeValueTypeRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.type = type;
            describeValueTypeResponse t = await Channel.describeValueTypeAsync(inValue);
            return t.result;
        }

        public DescribeWorkitemActionResult[] describeWorkitemActions(string[] workitemIds)
        {
            describeWorkitemActionsRequest inValue = new describeWorkitemActionsRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.workitemIds = workitemIds;
            describeWorkitemActionsResponse retVal = Channel.describeWorkitemActions(inValue);
            return retVal.result;
        }

        public async Task<DescribeWorkitemActionResult[]> describeWorkitemActionsAsync(string[] workitemIds)
        {
            describeWorkitemActionsRequest inValue = new describeWorkitemActionsRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.workitemIds = workitemIds;
            describeWorkitemActionsResponse t = await Channel.describeWorkitemActionsAsync(inValue);
            return t.result;
        }

        public ExecuteAnonymousResult executeAnonymous(string apexCode)
        {
            executeAnonymousRequest inValue = new executeAnonymousRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.DebuggingHeader = DebuggingHeader;
            inValue.PackageVersionHeader = PackageVersionExecAnonHeader;
            inValue.CallOptions = CallOptions;
            inValue.AllowFieldTruncationHeader = AllowFieldTruncationHeader;
            inValue.DisableFeedTrackingHeader = DisableFeedTrackingHeader;
            inValue.String = apexCode;
            executeAnonymousResponse retVal = Channel.executeAnonymous(inValue);
            DebuggingInfo = retVal.DebuggingInfo;
            return retVal.result;
        }

        public async Task<ExecuteAnonymousResult> executeAnonymousAsync(string apexCode)
        {
            executeAnonymousRequest inValue = new executeAnonymousRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.DebuggingHeader = DebuggingHeader;
            inValue.PackageVersionHeader = PackageVersionExecAnonHeader;
            inValue.CallOptions = CallOptions;
            inValue.AllowFieldTruncationHeader = AllowFieldTruncationHeader;
            inValue.DisableFeedTrackingHeader = DisableFeedTrackingHeader;
            inValue.String = apexCode;
            executeAnonymousResponse t = await Channel.executeAnonymousAsync(inValue);
            DebuggingInfo = t.DebuggingInfo;
            return t.result;
        }

        public GetDeletedResult getDeleted(string sObjectType, System.DateTime start, System.DateTime end)
        {
            getDeletedRequest inValue = new getDeletedRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.sObjectType = sObjectType;
            inValue.start = start;
            inValue.end = end;
            getDeletedResponse retVal = Channel.getDeleted(inValue);
            return retVal.result;
        }

        public async Task<GetDeletedResult> getDeletedAsync(string sObjectType, System.DateTime start, System.DateTime end)
        {
            getDeletedRequest inValue = new getDeletedRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.sObjectType = sObjectType;
            inValue.start = start;
            inValue.end = end;
            getDeletedResponse t = await Channel.getDeletedAsync(inValue);
            return t.result;
        }

        public GetServerTimestampResult getServerTimestamp()
        {
            getServerTimestampRequest inValue = new getServerTimestampRequest();
            inValue.SessionHeader = GetSessionHeader();
            getServerTimestampResponse retVal = Channel.getServerTimestamp(inValue);
            return retVal.result;
        }

        public async Task<GetServerTimestampResult> getServerTimestampAsync()
        {
            getServerTimestampRequest inValue = new getServerTimestampRequest();
            inValue.SessionHeader = GetSessionHeader();
            getServerTimestampResponse t = await Channel.getServerTimestampAsync(inValue);
            return t.result;
        }

        public GetUpdatedResult getUpdated(string sObjectType, System.DateTime start, System.DateTime end)
        {
            getUpdatedRequest inValue = new getUpdatedRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.sObjectType = sObjectType;
            inValue.start = start;
            inValue.end = end;
            getUpdatedResponse retVal = Channel.getUpdated(inValue);
            return retVal.result;
        }

        public async Task<GetUpdatedResult> getUpdatedAsync(string sObjectType, System.DateTime start, System.DateTime end)
        {
            getUpdatedRequest inValue = new getUpdatedRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.sObjectType = sObjectType;
            inValue.start = start;
            inValue.end = end;
            getUpdatedResponse t = await Channel.getUpdatedAsync(inValue);
            return t.result;
        }

        public GetUserInfoResult getUserInfo()
        {
            getUserInfoRequest inValue = new getUserInfoRequest();
            inValue.SessionHeader = GetSessionHeader();
            getUserInfoResponse retVal = Channel.getUserInfo(inValue);
            return retVal.result;
        }

        public async Task<GetUserInfoResult> getUserInfoAsync()
        {
            getUserInfoRequest inValue = new getUserInfoRequest();
            inValue.SessionHeader = GetSessionHeader();
            getUserInfoResponse t = await Channel.getUserInfoAsync(inValue);
            return t.result;
        }

        public InvalidateSessionsResult[] invalidateSessions(string[] ArrayList)
        {
            invalidateSessionsRequest inValue = new invalidateSessionsRequest();
            inValue.ArrayList = ArrayList;
            invalidateSessionsResponse retVal = Channel.invalidateSessions(inValue);
            return retVal.result;
        }

        public async Task<InvalidateSessionsResult[]> invalidateSessionsAsync(string[] ArrayList)
        {
            invalidateSessionsRequest inValue = new invalidateSessionsRequest();
            inValue.ArrayList = ArrayList;
            invalidateSessionsResponse t = await Channel.invalidateSessionsAsync(inValue);
            return t.result;
        }

        /// <summary>
        /// Do not use, Use Open()
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public LoginResult login(string username, string password)
        {
            return null;// Channel.login(username, password);
        }

        /// <summary>
        /// Do not use, Use Open()
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<LoginResult> loginAsync(string username, string password)
        {
            return null;// Channel.loginAsync(username, password);
        }

        //public void logout(SessionHeader SessionHeader)
        //{
        //    logoutRequest inValue = new logoutRequest();
        //    inValue.SessionHeader = GetSessionHeader();
        //    logoutResponse retVal = Channel.logout(inValue);
        //}

        //public async Task<logoutResponse> logoutAsync(SessionHeader SessionHeader)
        //{
        //    logoutRequest inValue = new logoutRequest();
        //    inValue.SessionHeader = GetSessionHeader();
        //    XXX t = await Channel.logoutAsync(inValue);
        //}

        public QueryResult query(string queryString)
        {
            queryRequest inValue = new queryRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.MetadataVersionCheck = MetadataVersionCheck;
            inValue.queryString = queryString;
            queryResponse retVal = Channel.query(inValue);
            MetadataVersionCheck = retVal.MetadataVersionCheck;
            APIPerformanceInfo = retVal.APIPerformanceInfo;
            return retVal.result;
        }

        public async Task<QueryResult> queryAsync(string queryString)
        {
            queryRequest inValue = new queryRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.MetadataVersionCheck = MetadataVersionCheck;
            inValue.queryString = queryString;
            queryResponse t = await Channel.queryAsync(inValue);
            APIPerformanceInfo = t.APIPerformanceInfo;
            return t.result;
        }

        public QueryResult queryAll(string queryString)
        {
            queryAllRequest inValue = new queryAllRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.queryString = queryString;
            queryAllResponse retVal = Channel.queryAll(inValue);
            APIPerformanceInfo = retVal.APIPerformanceInfo;
            return retVal.result;
        }

        public async Task<QueryResult> queryAllAsync(string queryString)
        {
            queryAllRequest inValue = new queryAllRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.queryString = queryString;
            queryAllResponse t = await Channel.queryAllAsync(inValue);
            APIPerformanceInfo = t.APIPerformanceInfo;
            return t.result;
        }

        public QueryResult queryMore(string queryLocator)
        {
            queryMoreRequest inValue = new queryMoreRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.queryLocator = queryLocator;
            queryMoreResponse retVal = Channel.queryMore(inValue);
            APIPerformanceInfo = retVal.APIPerformanceInfo;
            return retVal.result;
        }

        public async Task<QueryResult> queryMoreAsync(string queryLocator)
        {
            queryMoreRequest inValue = new queryMoreRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.queryLocator = queryLocator;
            queryMoreResponse t = await Channel.queryMoreAsync(inValue);
            APIPerformanceInfo = t.APIPerformanceInfo;
            return t.result;
        }

        public sObject[] retrieve(string select, string type, string[] ids)
        {
            retrieveRequest inValue = new retrieveRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.select = select;
            inValue.type = type;
            inValue.ids = ids;
            retrieveResponse retVal = Channel.retrieve(inValue);
            APIPerformanceInfo = retVal.APIPerformanceInfo;
            return retVal.result;
        }

        public async Task<sObject[]> retrieveAsync(string select, string type, string[] ids)
        {
            retrieveRequest inValue = new retrieveRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.select = select;
            inValue.type = type;
            inValue.ids = ids;
            retrieveResponse t = await Channel.retrieveAsync(inValue);
            APIPerformanceInfo = t.APIPerformanceInfo;
            return t.result;
        }

        public RunTestsResult runTests(RunTestsRequest RunTestsRequest)
        {
            runTestsRequest1 inValue = new runTestsRequest1();
            inValue.SessionHeader = GetSessionHeader();
            inValue.DebuggingHeader = DebuggingHeader;
            inValue.CallOptions = CallOptions;
            inValue.RunTestsRequest = RunTestsRequest;
            runTestsResponse retVal = Channel.runTests(inValue);
            DebuggingInfo = retVal.DebuggingInfo;
            return retVal.result;
        }

        public async Task<RunTestsResult> runTestsAsync(RunTestsRequest RunTestsRequest)
        {
            runTestsRequest1 inValue = new runTestsRequest1();
            inValue.SessionHeader = GetSessionHeader();
            inValue.DebuggingHeader = DebuggingHeader;
            inValue.CallOptions = CallOptions;
            inValue.RunTestsRequest = RunTestsRequest;
            runTestsResponse t = await Channel.runTestsAsync(inValue);
            DebuggingInfo = t.DebuggingInfo;
            return t.result;
        }

        public string runTestsAsynchronous(string classids, string suiteids, int maxFailedTests, TestLevel testLevel, string classNames, string suiteNames, TestsNode[] tests, bool skipCodeCoverage)
        {
            runTestsAsynchronousRequest inValue = new runTestsAsynchronousRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.DebuggingHeader = DebuggingHeader;
            inValue.CallOptions = CallOptions;
            inValue.classids = classids;
            inValue.suiteids = suiteids;
            inValue.maxFailedTests = maxFailedTests;
            inValue.testLevel = testLevel;
            inValue.classNames = classNames;
            inValue.suiteNames = suiteNames;
            inValue.tests = tests;
            inValue.skipCodeCoverage = skipCodeCoverage;
            runTestsAsynchronousResponse retVal = Channel.runTestsAsynchronous(inValue);
            DebuggingInfo = retVal.DebuggingInfo;
            return retVal.result;
        }

        public async Task<string> runTestsAsynchronousAsync(string classids, string suiteids, int maxFailedTests, TestLevel testLevel, string classNames, string suiteNames, TestsNode[] tests, bool skipCodeCoverage)
        {
            runTestsAsynchronousRequest inValue = new runTestsAsynchronousRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.DebuggingHeader = DebuggingHeader;
            inValue.CallOptions = CallOptions;
            inValue.classids = classids;
            inValue.suiteids = suiteids;
            inValue.maxFailedTests = maxFailedTests;
            inValue.testLevel = testLevel;
            inValue.classNames = classNames;
            inValue.suiteNames = suiteNames;
            inValue.tests = tests;
            inValue.skipCodeCoverage = skipCodeCoverage;
            runTestsAsynchronousResponse t = await Channel.runTestsAsynchronousAsync(inValue);
            DebuggingInfo = t.DebuggingInfo;
            return t.result;
        }

        public SearchResult search(string searchString)
        {
            searchRequest inValue = new searchRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.searchString = searchString;
            searchResponse retVal = Channel.search(inValue);
            APIPerformanceInfo = retVal.APIPerformanceInfo;
            return retVal.result;
        }

        public async Task<SearchResult> searchAsync(string searchString)
        {
            searchRequest inValue = new searchRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.searchString = searchString;
            searchResponse t = await Channel.searchAsync(inValue);
            APIPerformanceInfo = t.APIPerformanceInfo;
            return t.result;
        }

        public SetPasswordResult setPassword(string userId, string password)
        {
            setPasswordRequest inValue = new setPasswordRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.userId = userId;
            inValue.password = password;
            setPasswordResponse retVal = Channel.setPassword(inValue);
            return retVal.result;
        }

        public async Task<SetPasswordResult> setPasswordAsync(string userId, string password)
        {
            setPasswordRequest inValue = new setPasswordRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.userId = userId;
            inValue.password = password;
            setPasswordResponse t = await Channel.setPasswordAsync(inValue);
            return t.result;
        }

        public SaveResult[] update(sObject[] sObjects)
        {
            updateRequest inValue = new updateRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.MetadataWarningsHeader = MetadataWarningsHeader;
            inValue.AllOrNoneHeader = AllOrNoneHeader;
            inValue.CallOptions = CallOptions;
            inValue.MetadataVersionCheck = MetadataVersionCheck;
            inValue.sObjects = sObjects;
            updateResponse retVal = Channel.update(inValue);
            MetadataVersionCheck = retVal.MetadataVersionCheck;
            APIPerformanceInfo = retVal.APIPerformanceInfo;
            return retVal.result;
        }

        public async Task<SaveResult[]> updateAsync(sObject[] sObjects)
        {
            updateRequest inValue = new updateRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.MetadataWarningsHeader = MetadataWarningsHeader;
            inValue.AllOrNoneHeader = AllOrNoneHeader;
            inValue.CallOptions = CallOptions;
            inValue.MetadataVersionCheck = MetadataVersionCheck;
            inValue.sObjects = sObjects;
            updateResponse t = await Channel.updateAsync(inValue);
            APIPerformanceInfo = t.APIPerformanceInfo;
            return t.result;
        }

        public UpsertResult[] upsert(string fieldName, sObject[] entities)
        {
            upsertRequest inValue = new upsertRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.MetadataWarningsHeader = MetadataWarningsHeader;
            inValue.MetadataVersionCheck = MetadataVersionCheck;
            inValue.AllOrNoneHeader = AllOrNoneHeader;
            inValue.CallOptions = CallOptions;
            inValue.fieldName = fieldName;
            inValue.entities = entities;
            upsertResponse retVal = Channel.upsert(inValue);
            MetadataVersionCheck = retVal.MetadataVersionCheck;
            APIPerformanceInfo = retVal.APIPerformanceInfo;
            return retVal.result;
        }

        public async Task<UpsertResult[]> upsertAsync(string fieldName, sObject[] entities)
        {
            upsertRequest inValue = new upsertRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.MetadataWarningsHeader = MetadataWarningsHeader;
            inValue.MetadataVersionCheck = MetadataVersionCheck;
            inValue.AllOrNoneHeader = AllOrNoneHeader;
            inValue.CallOptions = CallOptions;
            inValue.fieldName = fieldName;
            inValue.entities = entities;
            upsertResponse t = await Channel.upsertAsync(inValue);
            APIPerformanceInfo = t.APIPerformanceInfo;
            return t.result;
        }
        #endregion

        #region Endpoint Management
        public static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration)
        {
            //if ((endpointConfiguration == EndpointConfiguration.Soap))
            //{
            System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
            result.MaxBufferSize = int.MaxValue;
            result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
            result.MaxReceivedMessageSize = int.MaxValue;
            result.AllowCookies = true;
            result.Security.Mode = System.ServiceModel.BasicHttpSecurityMode.Transport;
            return result;
            //}
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }

        public static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration)
        {
            if (EndpointsList.ContainsKey(endpointConfiguration))
            {
                return new System.ServiceModel.EndpointAddress(EndpointsList[endpointConfiguration]);
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }

        public static System.ServiceModel.Channels.Binding GetDefaultBinding()
        {
            return GetBindingForEndpoint(EndpointConfiguration.Login);
        }

        public static System.ServiceModel.EndpointAddress GetDefaultEndpointAddress()
        {
            return GetEndpointAddress(EndpointConfiguration.Login);
        }

        public static System.Collections.Generic.Dictionary<EndpointConfiguration, string> EndpointsList = new System.Collections.Generic.Dictionary<EndpointConfiguration, string>();

        public enum EndpointConfiguration
        {
            Login,
            Active
        }
        #endregion
    }
}
