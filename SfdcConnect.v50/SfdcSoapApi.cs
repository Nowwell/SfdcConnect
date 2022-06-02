/****************************************************************************
*
*   File name: SfdcSoapApi.cs
*   Author: Sean Fife
*   Create date: 5/27/2022
*   Solution: SfdcConnect
*   Project: SfdcConnect
*   Description: Includes the SfdcSoapApi class for Salesforce Soap Api Connections
*
****************************************************************************/
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Xml;
using System.Xml.Serialization;

using Sfdc.Soap.Partner;
using System.Reflection;
//using SfdcConnect.OAuth;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;


namespace SfdcConnect
{
    /// <summary>
    /// Salesforce SOAP Api connection class
    /// </summary>
    public partial class SfdcSoapApi : System.ServiceModel.ClientBase<Soap>
    {
        public SfdcSoapApi(EndpointConfiguration endpointConfiguration, string remoteAddress) :
                base(GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress))
        {
            EndpointsList.Add(endpointConfiguration, remoteAddress);

            Endpoint.Name = endpointConfiguration.ToString();

            DataProtector = new SfdcDataProtection();

            SetXmlSerializerFlag(1);
        }
        public SfdcSoapApi(Environment env, int apiversion, string refreshToken = "") :
                base(GetDefaultBinding(),
                    new System.ServiceModel.EndpointAddress(string.Format("https://{0}.salesforce.com/services/Soap/u/{1}.0", env == Environment.Sandbox ? "test" : "login", apiversion)))
        {
            EndpointsList.Add(EndpointConfiguration.Login, string.Format("https://{0}.salesforce.com/services/Soap/u/{1}.0", env == Environment.Sandbox ? "test" : "login", apiversion));

            Endpoint.Name = EndpointConfiguration.Login.ToString();

            DataProtector = new SfdcDataProtection();

            SetXmlSerializerFlag(1);
        }

        protected new Soap Channel { get; set; }

        internal SfdcDataProtection DataProtector;
        protected string sessionId;
        protected loginResponse LastLoginResponse;
        protected loginRequest LastLoginRequest;
        protected SfdcSession InternalConnection;

        private MyMessageInspector MessageInspector = new MyMessageInspector();


        public CallOptions CallOptions { get; set; }
        public LocaleOptions LocaleOptions { get; set; }
        public PackageVersion[] PackageVersionHeader { get; set; }
        public AssignmentRuleHeader AssignmentRuleHeader { get; set; }
        public MruHeader MruHeader { get; set; }
        public AllowFieldTruncationHeader AllowFieldTruncationHeader { get; set; }
        public DisableFeedTrackingHeader DisableFeedTrackingHeader { get; set; }
        public StreamingEnabledHeader StreamingEnabledHeader { get; set; }
        public AllOrNoneHeader AllOrNoneHeader { get; set; }
        public DuplicateRuleHeader DuplicateRuleHeader { get; set; }
        public DebuggingHeader DebuggingHeader { get; set; }
        public EmailHeader EmailHeader { get; set; }
        public LimitInfo[] LimitInfo { get; protected set; }

        public DebuggingInfo DebuggingInfo { get; protected set; }

        /// <summary>
        /// Sets the Xml Serializer Flag. Fixes a bug in the way the serialization happens.
        /// </summary>
        /// <param name="flag">1 = On, 0 = Off</param>
        public static void SetXmlSerializerFlag(int flag = 1)
        {
            MethodInfo method = typeof(XmlSerializer).GetMethod("set_Mode", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
            method.Invoke(null, new object[] { flag });
        }

        public void ChangeEndpoint(EndpointConfiguration endpointConfiguration)
        {
            Endpoint.Address = new System.ServiceModel.EndpointAddress(EndpointsList[endpointConfiguration]);
            Endpoint.Name = endpointConfiguration.ToString();
        }

        private SessionHeader GetSessionHeader()
        {
            SessionHeader sessionHeader = new SessionHeader();
            sessionHeader.sessionId = DataProtector.Decrypt(sessionId);
            return sessionHeader;
        }

        public loginResponse Open(loginRequest request = null)
        {
            MessageInspector.message = "login";
            if (Channel == null)
            {
                InspectorBehavior ib = new InspectorBehavior();
                ib.MessageInspector = MessageInspector;
                Endpoint.EndpointBehaviors.Add(ib);
                Channel = CreateChannel();
            }

            try
            {
                if (LastLoginRequest != null && request == null)
                {
                    LastLoginRequest.username = DataProtector.Decrypt(LastLoginRequest.username);
                    LastLoginRequest.password = DataProtector.Decrypt(LastLoginRequest.password);

                    request = LastLoginRequest;
                }

                LastLoginResponse = Channel.login(request);
            }
            catch (FaultException<ApiFault>)
            {
                ((IClientChannel)Channel).Close();

                Channel = CreateChannel();

                LastLoginResponse = Channel.login(request);
            }
            catch (ObjectDisposedException)
            {
                Channel = CreateChannel();

                LastLoginResponse = Channel.login(request);
            }

            LastLoginRequest = request;
            LastLoginRequest.username = DataProtector.Encrypt(LastLoginRequest.username);
            LastLoginRequest.password = DataProtector.Encrypt(LastLoginRequest.password);

            if (EndpointsList.ContainsKey(EndpointConfiguration.Active))
            {
                EndpointsList[EndpointConfiguration.Active] = LastLoginResponse.result.serverUrl;
            }
            else
            {
                EndpointsList.Add(EndpointConfiguration.Active, LastLoginResponse.result.serverUrl);
            }

            ChangeEndpoint(EndpointConfiguration.Active);

            sessionId = DataProtector.Encrypt(LastLoginResponse.result.sessionId);
            LastLoginResponse.result.sessionId = "";

            ((IClientChannel)Channel).Close();
            ((IClientChannel)Channel).Dispose();

            Channel = CreateChannel();

            ((IClientChannel)Channel).Open();


            return LastLoginResponse;
        }

        public loginResponse Open(SfdcSession conn)
        {
            MessageInspector.message = "login";
            if (Channel == null) Channel = CreateChannel();

            InternalConnection = conn;

            if (EndpointsList.ContainsKey(EndpointConfiguration.Active))
            {
                EndpointsList[EndpointConfiguration.Active] = conn.Endpoint.Address.Uri.AbsoluteUri;
            }
            else
            {
                EndpointsList.Add(EndpointConfiguration.Active, conn.Endpoint.Address.Uri.AbsoluteUri);
            }

            ChangeEndpoint(EndpointConfiguration.Active);

            sessionId = DataProtector.Encrypt(conn.SessionId);

            ((IClientChannel)Channel).Close();

            Endpoint.EndpointBehaviors.Add(new InspectorBehavior());

            Channel = CreateChannel();

            ((IClientChannel)Channel).Open();

            return LastLoginResponse;
        }

        public new void Close()
        {
            MessageInspector.message = "logout";
            logoutRequest request = new logoutRequest();
            request.SessionHeader = GetSessionHeader();
            Channel.logout(request);
            sessionId = "";
            LastLoginResponse = null;

            ((IClientChannel)Channel).Close();
        }

        #region Methods
        //public LoginResult login(LoginScopeHeader LoginScopeHeader, string username, string password)
        //{
        //    loginRequest inValue = new loginRequest();
        //    inValue.LoginScopeHeader = LoginScopeHeader;
        //    inValue.CallOptions = CallOptions;
        //    inValue.username = username;
        //    inValue.password = password;
        //    loginResponse retVal = Channel.login(inValue);
        //    return retVal.result;
        //}

        //public async Task<loginResponse> loginAsync(LoginScopeHeader LoginScopeHeader, string username, string password)
        //{
        //    loginRequest inValue = new loginRequest();
        //    inValue.LoginScopeHeader = LoginScopeHeader;
        //    inValue.CallOptions = CallOptions;
        //    inValue.username = username;
        //    inValue.password = password;
        //    XXX t = await Channel.loginAsync(inValue);
        //}

        public DescribeSObjectResult describeSObject(string sObjectType)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            describeSObjectRequest inValue = new describeSObjectRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.PackageVersionHeader = PackageVersionHeader;
            inValue.LocaleOptions = LocaleOptions;
            inValue.sObjectType = sObjectType;
            describeSObjectResponse retVal = Channel.describeSObject(inValue);
            LimitInfo = retVal.LimitInfoHeader;
            return retVal.result;
        }

        public async Task<DescribeSObjectResult> describeSObjectAsync(string sObjectType)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            describeSObjectRequest inValue = new describeSObjectRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.PackageVersionHeader = PackageVersionHeader;
            inValue.LocaleOptions = LocaleOptions;
            inValue.sObjectType = sObjectType;
            describeSObjectResponse t = await Channel.describeSObjectAsync(inValue);
            LimitInfo = t.LimitInfoHeader;
            return t.result;
        }

        public DescribeSObjectResult[] describeSObjects(string[] sObjectType)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            describeSObjectsRequest inValue = new describeSObjectsRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.PackageVersionHeader = PackageVersionHeader;
            inValue.LocaleOptions = LocaleOptions;
            inValue.sObjectType = sObjectType;
            describeSObjectsResponse retVal = Channel.describeSObjects(inValue);
            LimitInfo = retVal.LimitInfoHeader;
            return retVal.result;
        }

        public async Task<DescribeSObjectResult[]> describeSObjectsAsync(string[] sObjectType)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            describeSObjectsRequest inValue = new describeSObjectsRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.PackageVersionHeader = PackageVersionHeader;
            inValue.LocaleOptions = LocaleOptions;
            inValue.sObjectType = sObjectType;
            describeSObjectsResponse t = await Channel.describeSObjectsAsync(inValue);
            LimitInfo = t.LimitInfoHeader;
            return t.result;
        }

        public DescribeGlobalResult describeGlobal()
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            describeGlobalRequest inValue = new describeGlobalRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.PackageVersionHeader = PackageVersionHeader;
            describeGlobalResponse retVal = Channel.describeGlobal(inValue);
            LimitInfo = retVal.LimitInfoHeader;
            return retVal.result;
        }

        public async Task<DescribeGlobalResult> describeGlobalAsync()
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            describeGlobalRequest inValue = new describeGlobalRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.PackageVersionHeader = PackageVersionHeader;
            describeGlobalResponse t = await Channel.describeGlobalAsync(inValue);
            LimitInfo = t.LimitInfoHeader;
            return t.result;
        }

        public DescribeDataCategoryGroupResult[] describeDataCategoryGroups(string[] sObjectType)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            describeDataCategoryGroupsRequest inValue = new describeDataCategoryGroupsRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.PackageVersionHeader = PackageVersionHeader;
            inValue.LocaleOptions = LocaleOptions;
            inValue.sObjectType = sObjectType;
            describeDataCategoryGroupsResponse retVal = Channel.describeDataCategoryGroups(inValue);
            LimitInfo = retVal.LimitInfoHeader;
            return retVal.result;
        }

        public async Task<DescribeDataCategoryGroupResult[]> describeDataCategoryGroupsAsync(string[] sObjectType)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            describeDataCategoryGroupsRequest inValue = new describeDataCategoryGroupsRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.PackageVersionHeader = PackageVersionHeader;
            inValue.LocaleOptions = LocaleOptions;
            inValue.sObjectType = sObjectType;
            describeDataCategoryGroupsResponse t = await Channel.describeDataCategoryGroupsAsync(inValue);
            LimitInfo = t.LimitInfoHeader;
            return t.result;
        }

        public DescribeDataCategoryGroupStructureResult[] describeDataCategoryGroupStructures(DataCategoryGroupSobjectTypePair[] pairs, bool topCategoriesOnly)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            describeDataCategoryGroupStructuresRequest inValue = new describeDataCategoryGroupStructuresRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.PackageVersionHeader = PackageVersionHeader;
            inValue.LocaleOptions = LocaleOptions;
            inValue.pairs = pairs;
            inValue.topCategoriesOnly = topCategoriesOnly;
            describeDataCategoryGroupStructuresResponse retVal = Channel.describeDataCategoryGroupStructures(inValue);
            LimitInfo = retVal.LimitInfoHeader;
            return retVal.result;

        }

        public async Task<DescribeDataCategoryGroupStructureResult[]> describeDataCategoryGroupStructuresAsync(DataCategoryGroupSobjectTypePair[] pairs, bool topCategoriesOnly)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            describeDataCategoryGroupStructuresRequest inValue = new describeDataCategoryGroupStructuresRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.PackageVersionHeader = PackageVersionHeader;
            inValue.LocaleOptions = LocaleOptions;
            inValue.pairs = pairs;
            inValue.topCategoriesOnly = topCategoriesOnly;
            describeDataCategoryGroupStructuresResponse t = await Channel.describeDataCategoryGroupStructuresAsync(inValue);
            LimitInfo = t.LimitInfoHeader;
            return t.result;
        }

        public DescribeDataCategoryMappingResult[] describeDataCategoryMappings()
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            describeDataCategoryMappingsRequest inValue = new describeDataCategoryMappingsRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.PackageVersionHeader = PackageVersionHeader;
            inValue.LocaleOptions = LocaleOptions;
            describeDataCategoryMappingsResponse retVal = Channel.describeDataCategoryMappings(inValue);
            LimitInfo = retVal.LimitInfoHeader;
            return retVal.result;

        }

        public async Task<DescribeDataCategoryMappingResult[]> describeDataCategoryMappingsAsync()
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            describeDataCategoryMappingsRequest inValue = new describeDataCategoryMappingsRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.PackageVersionHeader = PackageVersionHeader;
            inValue.LocaleOptions = LocaleOptions;
            describeDataCategoryMappingsResponse t = await Channel.describeDataCategoryMappingsAsync(inValue);
            LimitInfo = t.LimitInfoHeader;
            return t.result;
        }

        public KnowledgeSettings describeKnowledgeSettings()
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            describeKnowledgeSettingsRequest inValue = new describeKnowledgeSettingsRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.PackageVersionHeader = PackageVersionHeader;
            inValue.LocaleOptions = LocaleOptions;
            describeKnowledgeSettingsResponse retVal = Channel.describeKnowledgeSettings(inValue);
            LimitInfo = retVal.LimitInfoHeader;
            return retVal.result;

        }

        public async Task<KnowledgeSettings> describeKnowledgeSettingsAsync()
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            describeKnowledgeSettingsRequest inValue = new describeKnowledgeSettingsRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.PackageVersionHeader = PackageVersionHeader;
            inValue.LocaleOptions = LocaleOptions;
            describeKnowledgeSettingsResponse t = await Channel.describeKnowledgeSettingsAsync(inValue);
            LimitInfo = t.LimitInfoHeader;
            return t.result;
        }

        public DescribeAppMenuItem[] describeAppMenu(AppMenuType appMenuType, string networkId)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            describeAppMenuRequest inValue = new describeAppMenuRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.PackageVersionHeader = PackageVersionHeader;
            inValue.appMenuType = appMenuType;
            inValue.networkId = networkId;
            describeAppMenuResponse retVal = Channel.describeAppMenu(inValue);
            LimitInfo = retVal.LimitInfoHeader;
            return retVal.result;

        }

        public async Task<DescribeAppMenuItem[]> describeAppMenuAsync(AppMenuType appMenuType, string networkId)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            describeAppMenuRequest inValue = new describeAppMenuRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.PackageVersionHeader = PackageVersionHeader;
            inValue.appMenuType = appMenuType;
            inValue.networkId = networkId;
            describeAppMenuResponse t = await Channel.describeAppMenuAsync(inValue);
            LimitInfo = t.LimitInfoHeader;
            return t.result;
        }

        public DescribeGlobalTheme describeGlobalTheme()
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            describeGlobalThemeRequest inValue = new describeGlobalThemeRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.PackageVersionHeader = PackageVersionHeader;
            describeGlobalThemeResponse retVal = Channel.describeGlobalTheme(inValue);
            LimitInfo = retVal.LimitInfoHeader;
            return retVal.result;

        }

        public async Task<DescribeGlobalTheme> describeGlobalThemeAsync()
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            describeGlobalThemeRequest inValue = new describeGlobalThemeRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.PackageVersionHeader = PackageVersionHeader;
            describeGlobalThemeResponse t = await Channel.describeGlobalThemeAsync(inValue);
            LimitInfo = t.LimitInfoHeader;
            return t.result;
        }

        public DescribeThemeItem[] describeTheme(string[] sobjectType)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            describeThemeRequest inValue = new describeThemeRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.PackageVersionHeader = PackageVersionHeader;
            inValue.sobjectType = sobjectType;
            describeThemeResponse retVal = Channel.describeTheme(inValue);
            LimitInfo = retVal.LimitInfoHeader;
            return retVal.result;

        }

        public async Task<DescribeThemeItem[]> describeThemeAsync(string[] sobjectType)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            describeThemeRequest inValue = new describeThemeRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.PackageVersionHeader = PackageVersionHeader;
            inValue.sobjectType = sobjectType;
            describeThemeResponse t = await Channel.describeThemeAsync(inValue);
            LimitInfo = t.LimitInfoHeader;
            return t.result;
        }

        public DescribeLayoutResult describeLayout(string sObjectType, string layoutName, string[] recordTypeIds)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            describeLayoutRequest inValue = new describeLayoutRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.PackageVersionHeader = PackageVersionHeader;
            inValue.sObjectType = sObjectType;
            inValue.layoutName = layoutName;
            inValue.recordTypeIds = recordTypeIds;
            describeLayoutResponse retVal = Channel.describeLayout(inValue);
            LimitInfo = retVal.LimitInfoHeader;
            return retVal.result;

        }

        public async Task<DescribeLayoutResult> describeLayoutAsync(string sObjectType, string layoutName, string[] recordTypeIds)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            describeLayoutRequest inValue = new describeLayoutRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.PackageVersionHeader = PackageVersionHeader;
            inValue.sObjectType = sObjectType;
            inValue.layoutName = layoutName;
            inValue.recordTypeIds = recordTypeIds;
            describeLayoutResponse t = await Channel.describeLayoutAsync(inValue);
            LimitInfo = t.LimitInfoHeader;
            return t.result;
        }

        public DescribeSoftphoneLayoutResult describeSoftphoneLayout()
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            describeSoftphoneLayoutRequest inValue = new describeSoftphoneLayoutRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.PackageVersionHeader = PackageVersionHeader;
            describeSoftphoneLayoutResponse retVal = Channel.describeSoftphoneLayout(inValue);
            LimitInfo = retVal.LimitInfoHeader;
            return retVal.result;

        }

        public async Task<DescribeSoftphoneLayoutResult> describeSoftphoneLayoutAsync()
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            describeSoftphoneLayoutRequest inValue = new describeSoftphoneLayoutRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.PackageVersionHeader = PackageVersionHeader;
            describeSoftphoneLayoutResponse t = await Channel.describeSoftphoneLayoutAsync(inValue);
            LimitInfo = t.LimitInfoHeader;
            return t.result;
        }

        public DescribeSearchLayoutResult[] describeSearchLayouts(string[] sObjectType)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            describeSearchLayoutsRequest inValue = new describeSearchLayoutsRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.PackageVersionHeader = PackageVersionHeader;
            inValue.sObjectType = sObjectType;
            describeSearchLayoutsResponse retVal = Channel.describeSearchLayouts(inValue);
            LimitInfo = retVal.LimitInfoHeader;
            return retVal.result;

        }

        public async Task<DescribeSearchLayoutResult[]> describeSearchLayoutsAsync(string[] sObjectType)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            describeSearchLayoutsRequest inValue = new describeSearchLayoutsRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.PackageVersionHeader = PackageVersionHeader;
            inValue.sObjectType = sObjectType;
            describeSearchLayoutsResponse t = await Channel.describeSearchLayoutsAsync(inValue);
            LimitInfo = t.LimitInfoHeader;
            return t.result;
        }

        public DescribeSearchableEntityResult[] describeSearchableEntities(bool includeOnlyEntitiesWithTabs)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            describeSearchableEntitiesRequest inValue = new describeSearchableEntitiesRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.PackageVersionHeader = PackageVersionHeader;
            inValue.includeOnlyEntitiesWithTabs = includeOnlyEntitiesWithTabs;
            describeSearchableEntitiesResponse retVal = Channel.describeSearchableEntities(inValue);
            LimitInfo = retVal.LimitInfoHeader;
            return retVal.result;

        }

        public async Task<DescribeSearchableEntityResult[]> describeSearchableEntitiesAsync(bool includeOnlyEntitiesWithTabs)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            describeSearchableEntitiesRequest inValue = new describeSearchableEntitiesRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.PackageVersionHeader = PackageVersionHeader;
            inValue.includeOnlyEntitiesWithTabs = includeOnlyEntitiesWithTabs;
            describeSearchableEntitiesResponse t = await Channel.describeSearchableEntitiesAsync(inValue);
            LimitInfo = t.LimitInfoHeader;
            return t.result;
        }

        public DescribeSearchScopeOrderResult[] describeSearchScopeOrder(bool includeRealTimeEntities)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            describeSearchScopeOrderRequest inValue = new describeSearchScopeOrderRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.PackageVersionHeader = PackageVersionHeader;
            inValue.includeRealTimeEntities = includeRealTimeEntities;
            describeSearchScopeOrderResponse retVal = Channel.describeSearchScopeOrder(inValue);
            LimitInfo = retVal.LimitInfoHeader;
            return retVal.result;

        }

        public async Task<DescribeSearchScopeOrderResult[]> describeSearchScopeOrderAsync(bool includeRealTimeEntities)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            describeSearchScopeOrderRequest inValue = new describeSearchScopeOrderRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.PackageVersionHeader = PackageVersionHeader;
            inValue.includeRealTimeEntities = includeRealTimeEntities;
            describeSearchScopeOrderResponse t = await Channel.describeSearchScopeOrderAsync(inValue);
            LimitInfo = t.LimitInfoHeader;
            return t.result;
        }

        public DescribeCompactLayoutsResult describeCompactLayouts(string sObjectType, string[] recordTypeIds)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            describeCompactLayoutsRequest inValue = new describeCompactLayoutsRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.PackageVersionHeader = PackageVersionHeader;
            inValue.sObjectType = sObjectType;
            inValue.recordTypeIds = recordTypeIds;
            describeCompactLayoutsResponse retVal = Channel.describeCompactLayouts(inValue);
            LimitInfo = retVal.LimitInfoHeader;
            return retVal.result;

        }

        public async Task<DescribeCompactLayoutsResult> describeCompactLayoutsAsync(string sObjectType, string[] recordTypeIds)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            describeCompactLayoutsRequest inValue = new describeCompactLayoutsRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.PackageVersionHeader = PackageVersionHeader;
            inValue.sObjectType = sObjectType;
            inValue.recordTypeIds = recordTypeIds;
            describeCompactLayoutsResponse t = await Channel.describeCompactLayoutsAsync(inValue);
            LimitInfo = t.LimitInfoHeader;
            return t.result;
        }

        public DescribePathAssistant[] describePathAssistants(string sObjectType, string picklistValue, string[] recordTypeIds)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            describePathAssistantsRequest inValue = new describePathAssistantsRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.PackageVersionHeader = PackageVersionHeader;
            inValue.sObjectType = sObjectType;
            inValue.picklistValue = picklistValue;
            inValue.recordTypeIds = recordTypeIds;
            describePathAssistantsResponse retVal = Channel.describePathAssistants(inValue);
            LimitInfo = retVal.LimitInfoHeader;
            return retVal.result;

        }

        public async Task<DescribePathAssistant[]> describePathAssistantsAsync(string sObjectType, string picklistValue, string[] recordTypeIds)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            describePathAssistantsRequest inValue = new describePathAssistantsRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.PackageVersionHeader = PackageVersionHeader;
            inValue.sObjectType = sObjectType;
            inValue.picklistValue = picklistValue;
            inValue.recordTypeIds = recordTypeIds;
            describePathAssistantsResponse t = await Channel.describePathAssistantsAsync(inValue);
            LimitInfo = t.LimitInfoHeader;
            return t.result;
        }

        public DescribeApprovalLayout[] describeApprovalLayout(string sObjectType, string[] approvalProcessNames)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            describeApprovalLayoutRequest inValue = new describeApprovalLayoutRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.PackageVersionHeader = PackageVersionHeader;
            inValue.sObjectType = sObjectType;
            inValue.approvalProcessNames = approvalProcessNames;
            describeApprovalLayoutResponse retVal = Channel.describeApprovalLayout(inValue);
            LimitInfo = retVal.LimitInfoHeader;
            return retVal.result;

        }

        public async Task<DescribeApprovalLayout[]> describeApprovalLayoutAsync(string sObjectType, string[] approvalProcessNames)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            describeApprovalLayoutRequest inValue = new describeApprovalLayoutRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.PackageVersionHeader = PackageVersionHeader;
            inValue.sObjectType = sObjectType;
            inValue.approvalProcessNames = approvalProcessNames;
            describeApprovalLayoutResponse t = await Channel.describeApprovalLayoutAsync(inValue);
            LimitInfo = t.LimitInfoHeader;
            return t.result;
        }

        public DescribeSoqlListView[] describeSoqlListViews(DescribeSoqlListViewParams[] request)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            describeSoqlListViewsRequest inValue = new describeSoqlListViewsRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.PackageVersionHeader = PackageVersionHeader;
            inValue.request = request;
            describeSoqlListViewsResponse retVal = Channel.describeSoqlListViews(inValue);
            LimitInfo = retVal.LimitInfoHeader;
            return retVal.result;

        }

        public async Task<DescribeSoqlListView[]> describeSoqlListViewsAsync(DescribeSoqlListViewParams[] request)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            describeSoqlListViewsRequest inValue = new describeSoqlListViewsRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.PackageVersionHeader = PackageVersionHeader;
            inValue.request = request;
            describeSoqlListViewsResponse t = await Channel.describeSoqlListViewsAsync(inValue);
            LimitInfo = t.LimitInfoHeader;
            return t.result;
        }

        public ExecuteListViewResult executeListView(ExecuteListViewRequest request)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            executeListViewRequest1 inValue = new executeListViewRequest1();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.MruHeader = MruHeader;
            inValue.request = request;
            executeListViewResponse retVal = Channel.executeListView(inValue);
            LimitInfo = retVal.LimitInfoHeader;
            return retVal.result;

        }

        public async Task<ExecuteListViewResult> executeListViewAsync(ExecuteListViewRequest request)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            executeListViewRequest1 inValue = new executeListViewRequest1();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.MruHeader = MruHeader;
            inValue.request = request;
            executeListViewResponse t = await Channel.executeListViewAsync(inValue);
            LimitInfo = t.LimitInfoHeader;
            return t.result;
        }

        public DescribeSoqlListView[] describeSObjectListViews(string sObjectType, bool recentsOnly, listViewIsSoqlCompatible isSoqlCompatible, int limit, int offset)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            describeSObjectListViewsRequest inValue = new describeSObjectListViewsRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.PackageVersionHeader = PackageVersionHeader;
            inValue.sObjectType = sObjectType;
            inValue.recentsOnly = recentsOnly;
            inValue.isSoqlCompatible = isSoqlCompatible;
            inValue.limit = limit;
            inValue.offset = offset;
            describeSObjectListViewsResponse retVal = Channel.describeSObjectListViews(inValue);
            LimitInfo = retVal.LimitInfoHeader;
            return retVal.result;

        }

        public async Task<DescribeSoqlListView[]> describeSObjectListViewsAsync(string sObjectType, bool recentsOnly, listViewIsSoqlCompatible isSoqlCompatible, int limit, int offset)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            describeSObjectListViewsRequest inValue = new describeSObjectListViewsRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.PackageVersionHeader = PackageVersionHeader;
            inValue.sObjectType = sObjectType;
            inValue.recentsOnly = recentsOnly;
            inValue.isSoqlCompatible = isSoqlCompatible;
            inValue.limit = limit;
            inValue.offset = offset;
            describeSObjectListViewsResponse t = await Channel.describeSObjectListViewsAsync(inValue);
            LimitInfo = t.LimitInfoHeader;
            return t.result;
        }

        public DescribeTabSetResult[] describeTabs()
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            describeTabsRequest inValue = new describeTabsRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.PackageVersionHeader = PackageVersionHeader;
            describeTabsResponse retVal = Channel.describeTabs(inValue);
            LimitInfo = retVal.LimitInfoHeader;
            return retVal.result;

        }

        public async Task<DescribeTabSetResult[]> describeTabsAsync()
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            describeTabsRequest inValue = new describeTabsRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.PackageVersionHeader = PackageVersionHeader;
            describeTabsResponse t = await Channel.describeTabsAsync(inValue);
            LimitInfo = t.LimitInfoHeader;
            return t.result;
        }

        public DescribeTab[] describeAllTabs()
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            describeAllTabsRequest inValue = new describeAllTabsRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.PackageVersionHeader = PackageVersionHeader;
            describeAllTabsResponse retVal = Channel.describeAllTabs(inValue);
            LimitInfo = retVal.LimitInfoHeader;
            return retVal.result;

        }

        public async Task<DescribeTab[]> describeAllTabsAsync()
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            describeAllTabsRequest inValue = new describeAllTabsRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.PackageVersionHeader = PackageVersionHeader;
            describeAllTabsResponse t = await Channel.describeAllTabsAsync(inValue);
            LimitInfo = t.LimitInfoHeader;
            return t.result;
        }

        public DescribeCompactLayout[] describePrimaryCompactLayouts(string[] sObjectTypes)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            describePrimaryCompactLayoutsRequest inValue = new describePrimaryCompactLayoutsRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.PackageVersionHeader = PackageVersionHeader;
            inValue.sObjectTypes = sObjectTypes;
            describePrimaryCompactLayoutsResponse retVal = Channel.describePrimaryCompactLayouts(inValue);
            LimitInfo = retVal.LimitInfoHeader;
            return retVal.result;

        }

        public async Task<DescribeCompactLayout[]> describePrimaryCompactLayoutsAsync(string[] sObjectTypes)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            describePrimaryCompactLayoutsRequest inValue = new describePrimaryCompactLayoutsRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.PackageVersionHeader = PackageVersionHeader;
            inValue.sObjectTypes = sObjectTypes;
            describePrimaryCompactLayoutsResponse t = await Channel.describePrimaryCompactLayoutsAsync(inValue);
            LimitInfo = t.LimitInfoHeader;
            return t.result;
        }

        public SaveResult[] create(sObject[] sObjects)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            createRequest inValue = new createRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.AssignmentRuleHeader = AssignmentRuleHeader;
            inValue.MruHeader = MruHeader;
            inValue.AllowFieldTruncationHeader = AllowFieldTruncationHeader;
            inValue.DisableFeedTrackingHeader = DisableFeedTrackingHeader;
            inValue.StreamingEnabledHeader = StreamingEnabledHeader;
            inValue.AllOrNoneHeader = AllOrNoneHeader;
            inValue.DuplicateRuleHeader = DuplicateRuleHeader;
            inValue.LocaleOptions = LocaleOptions;
            inValue.DebuggingHeader = DebuggingHeader;
            inValue.PackageVersionHeader = PackageVersionHeader;
            inValue.EmailHeader = EmailHeader;
            inValue.sObjects = sObjects;
            createResponse retVal = Channel.create(inValue);
            LimitInfo = retVal.LimitInfoHeader;
            DebuggingInfo = retVal.DebuggingInfo;
            return retVal.result;
        }

        public async Task<SaveResult[]> createAsync(sObject[] sObjects)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            createRequest inValue = new createRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.AssignmentRuleHeader = AssignmentRuleHeader;
            inValue.MruHeader = MruHeader;
            inValue.AllowFieldTruncationHeader = AllowFieldTruncationHeader;
            inValue.DisableFeedTrackingHeader = DisableFeedTrackingHeader;
            inValue.StreamingEnabledHeader = StreamingEnabledHeader;
            inValue.AllOrNoneHeader = AllOrNoneHeader;
            inValue.DuplicateRuleHeader = DuplicateRuleHeader;
            inValue.LocaleOptions = LocaleOptions;
            inValue.DebuggingHeader = DebuggingHeader;
            inValue.PackageVersionHeader = PackageVersionHeader;
            inValue.EmailHeader = EmailHeader;
            inValue.sObjects = sObjects;
            createResponse t = await Channel.createAsync(inValue);
            LimitInfo = t.LimitInfoHeader;
            return t.result;
        }

        public SaveResult[] update(sObject[] sObjects, OwnerChangeOption[] OwnerChangeOptions = null)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            updateRequest inValue = new updateRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.AssignmentRuleHeader = AssignmentRuleHeader;
            inValue.MruHeader = MruHeader;
            inValue.AllowFieldTruncationHeader = AllowFieldTruncationHeader;
            inValue.DisableFeedTrackingHeader = DisableFeedTrackingHeader;
            inValue.StreamingEnabledHeader = StreamingEnabledHeader;
            inValue.AllOrNoneHeader = AllOrNoneHeader;
            inValue.DuplicateRuleHeader = DuplicateRuleHeader;
            inValue.LocaleOptions = LocaleOptions;
            inValue.DebuggingHeader = DebuggingHeader;
            inValue.PackageVersionHeader = PackageVersionHeader;
            inValue.EmailHeader = EmailHeader;
            inValue.OwnerChangeOptions = OwnerChangeOptions;
            inValue.sObjects = sObjects;
            updateResponse retVal = Channel.update(inValue);
            LimitInfo = retVal.LimitInfoHeader;
            DebuggingInfo = retVal.DebuggingInfo;
            return retVal.result;
        }

        public async Task<SaveResult[]> updateAsync(sObject[] sObjects, OwnerChangeOption[] OwnerChangeOptions = null)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            updateRequest inValue = new updateRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.AssignmentRuleHeader = AssignmentRuleHeader;
            inValue.MruHeader = MruHeader;
            inValue.AllowFieldTruncationHeader = AllowFieldTruncationHeader;
            inValue.DisableFeedTrackingHeader = DisableFeedTrackingHeader;
            inValue.StreamingEnabledHeader = StreamingEnabledHeader;
            inValue.AllOrNoneHeader = AllOrNoneHeader;
            inValue.DuplicateRuleHeader = DuplicateRuleHeader;
            inValue.LocaleOptions = LocaleOptions;
            inValue.DebuggingHeader = DebuggingHeader;
            inValue.PackageVersionHeader = PackageVersionHeader;
            inValue.EmailHeader = EmailHeader;
            inValue.OwnerChangeOptions = OwnerChangeOptions;
            inValue.sObjects = sObjects;
            updateResponse t = await Channel.updateAsync(inValue);
            LimitInfo = t.LimitInfoHeader;
            return t.result;
        }

        public UpsertResult[] upsert(sObject[] sObjects, string externalIDFieldName, OwnerChangeOption[] OwnerChangeOptions = null)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            upsertRequest inValue = new upsertRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.AssignmentRuleHeader = AssignmentRuleHeader;
            inValue.MruHeader = MruHeader;
            inValue.AllowFieldTruncationHeader = AllowFieldTruncationHeader;
            inValue.DisableFeedTrackingHeader = DisableFeedTrackingHeader;
            inValue.StreamingEnabledHeader = StreamingEnabledHeader;
            inValue.AllOrNoneHeader = AllOrNoneHeader;
            inValue.DuplicateRuleHeader = DuplicateRuleHeader;
            inValue.LocaleOptions = LocaleOptions;
            inValue.DebuggingHeader = DebuggingHeader;
            inValue.PackageVersionHeader = PackageVersionHeader;
            inValue.EmailHeader = EmailHeader;
            inValue.OwnerChangeOptions = OwnerChangeOptions;
            inValue.externalIDFieldName = externalIDFieldName;
            inValue.sObjects = sObjects;
            upsertResponse retVal = Channel.upsert(inValue);
            DebuggingInfo = retVal.DebuggingInfo;
            LimitInfo = retVal.LimitInfoHeader;
            return retVal.result;

        }

        public async Task<UpsertResult[]> upsertAsync(sObject[] sObjects, string externalIDFieldName, OwnerChangeOption[] OwnerChangeOptions = null)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            upsertRequest inValue = new upsertRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.AssignmentRuleHeader = AssignmentRuleHeader;
            inValue.MruHeader = MruHeader;
            inValue.AllowFieldTruncationHeader = AllowFieldTruncationHeader;
            inValue.DisableFeedTrackingHeader = DisableFeedTrackingHeader;
            inValue.StreamingEnabledHeader = StreamingEnabledHeader;
            inValue.AllOrNoneHeader = AllOrNoneHeader;
            inValue.DuplicateRuleHeader = DuplicateRuleHeader;
            inValue.LocaleOptions = LocaleOptions;
            inValue.DebuggingHeader = DebuggingHeader;
            inValue.PackageVersionHeader = PackageVersionHeader;
            inValue.EmailHeader = EmailHeader;
            inValue.OwnerChangeOptions = OwnerChangeOptions;
            inValue.externalIDFieldName = externalIDFieldName;
            inValue.sObjects = sObjects;
            upsertResponse t = await Channel.upsertAsync(inValue);
            LimitInfo = t.LimitInfoHeader;
            return t.result;
        }

        public MergeResult[] merge(MergeRequest[] request)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            mergeRequest1 inValue = new mergeRequest1();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.AssignmentRuleHeader = AssignmentRuleHeader;
            inValue.MruHeader = MruHeader;
            inValue.AllowFieldTruncationHeader = AllowFieldTruncationHeader;
            inValue.DisableFeedTrackingHeader = DisableFeedTrackingHeader;
            inValue.StreamingEnabledHeader = StreamingEnabledHeader;
            inValue.DuplicateRuleHeader = DuplicateRuleHeader;
            inValue.LocaleOptions = LocaleOptions;
            inValue.DebuggingHeader = DebuggingHeader;
            inValue.PackageVersionHeader = PackageVersionHeader;
            inValue.EmailHeader = EmailHeader;
            inValue.request = request;
            mergeResponse retVal = Channel.merge(inValue);
            LimitInfo = retVal.LimitInfoHeader;
            DebuggingInfo = retVal.DebuggingInfo;
            return retVal.result;
        }

        public async Task<MergeResult[]> mergeAsync(MergeRequest[] request)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            mergeRequest1 inValue = new mergeRequest1();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.AssignmentRuleHeader = AssignmentRuleHeader;
            inValue.MruHeader = MruHeader;
            inValue.AllowFieldTruncationHeader = AllowFieldTruncationHeader;
            inValue.DisableFeedTrackingHeader = DisableFeedTrackingHeader;
            inValue.StreamingEnabledHeader = StreamingEnabledHeader;
            inValue.DuplicateRuleHeader = DuplicateRuleHeader;
            inValue.LocaleOptions = LocaleOptions;
            inValue.DebuggingHeader = DebuggingHeader;
            inValue.PackageVersionHeader = PackageVersionHeader;
            inValue.EmailHeader = EmailHeader;
            inValue.request = request;
            mergeResponse t = await Channel.mergeAsync(inValue);
            LimitInfo = t.LimitInfoHeader;
            return t.result;
        }

        public DeleteResult[] delete(string[] ids, UserTerritoryDeleteHeader UserTerritoryDeleteHeader = null)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            deleteRequest inValue = new deleteRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.PackageVersionHeader = PackageVersionHeader;
            inValue.UserTerritoryDeleteHeader = UserTerritoryDeleteHeader;
            inValue.EmailHeader = EmailHeader;
            inValue.AllowFieldTruncationHeader = AllowFieldTruncationHeader;
            inValue.DisableFeedTrackingHeader = DisableFeedTrackingHeader;
            inValue.StreamingEnabledHeader = StreamingEnabledHeader;
            inValue.AllOrNoneHeader = AllOrNoneHeader;
            inValue.DuplicateRuleHeader = DuplicateRuleHeader;
            inValue.LocaleOptions = LocaleOptions;
            inValue.DebuggingHeader = DebuggingHeader;
            inValue.ids = ids;
            deleteResponse retVal = Channel.delete(inValue);
            DebuggingInfo = retVal.DebuggingInfo;
            LimitInfo = retVal.LimitInfoHeader;
            return retVal.result;
        }

        public async Task<DeleteResult[]> deleteAsync(string[] ids, UserTerritoryDeleteHeader UserTerritoryDeleteHeader = null)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            deleteRequest inValue = new deleteRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.PackageVersionHeader = PackageVersionHeader;
            inValue.UserTerritoryDeleteHeader = UserTerritoryDeleteHeader;
            inValue.EmailHeader = EmailHeader;
            inValue.AllowFieldTruncationHeader = AllowFieldTruncationHeader;
            inValue.DisableFeedTrackingHeader = DisableFeedTrackingHeader;
            inValue.StreamingEnabledHeader = StreamingEnabledHeader;
            inValue.AllOrNoneHeader = AllOrNoneHeader;
            inValue.DuplicateRuleHeader = DuplicateRuleHeader;
            inValue.LocaleOptions = LocaleOptions;
            inValue.DebuggingHeader = DebuggingHeader;
            inValue.ids = ids;
            deleteResponse t = await Channel.deleteAsync(inValue);
            LimitInfo = t.LimitInfoHeader;
            return t.result;
        }

        public UndeleteResult[] undelete(string[] ids)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            undeleteRequest inValue = new undeleteRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.AllowFieldTruncationHeader = AllowFieldTruncationHeader;
            inValue.DisableFeedTrackingHeader = DisableFeedTrackingHeader;
            inValue.StreamingEnabledHeader = StreamingEnabledHeader;
            inValue.AllOrNoneHeader = AllOrNoneHeader;
            inValue.DuplicateRuleHeader = DuplicateRuleHeader;
            inValue.LocaleOptions = LocaleOptions;
            inValue.DebuggingHeader = DebuggingHeader;
            inValue.PackageVersionHeader = PackageVersionHeader;
            inValue.ids = ids;
            undeleteResponse retVal = Channel.undelete(inValue);
            DebuggingInfo = retVal.DebuggingInfo;
            LimitInfo = retVal.LimitInfoHeader;
            return retVal.result;
        }

        public async Task<UndeleteResult[]> undeleteAsync(string[] ids)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            undeleteRequest inValue = new undeleteRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.AllowFieldTruncationHeader = AllowFieldTruncationHeader;
            inValue.DisableFeedTrackingHeader = DisableFeedTrackingHeader;
            inValue.StreamingEnabledHeader = StreamingEnabledHeader;
            inValue.AllOrNoneHeader = AllOrNoneHeader;
            inValue.DuplicateRuleHeader = DuplicateRuleHeader;
            inValue.LocaleOptions = LocaleOptions;
            inValue.DebuggingHeader = DebuggingHeader;
            inValue.PackageVersionHeader = PackageVersionHeader;
            inValue.ids = ids;
            undeleteResponse t = await Channel.undeleteAsync(inValue);
            LimitInfo = t.LimitInfoHeader;
            return t.result;
        }

        public EmptyRecycleBinResult[] emptyRecycleBin(string[] ids)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            emptyRecycleBinRequest inValue = new emptyRecycleBinRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.ids = ids;
            emptyRecycleBinResponse retVal = Channel.emptyRecycleBin(inValue);
            LimitInfo = retVal.LimitInfoHeader;
            return retVal.result;
        }

        public async Task<EmptyRecycleBinResult[]> emptyRecycleBinAsync(string[] ids)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            emptyRecycleBinRequest inValue = new emptyRecycleBinRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.ids = ids;
            emptyRecycleBinResponse t = await Channel.emptyRecycleBinAsync(inValue);
            LimitInfo = t.LimitInfoHeader;
            return t.result;
        }

        public sObject[] retrieve(string fieldList, string sObjectType, string[] ids, QueryOptions QueryOptions = null)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            retrieveRequest inValue = new retrieveRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.QueryOptions = QueryOptions;
            inValue.MruHeader = MruHeader;
            inValue.PackageVersionHeader = PackageVersionHeader;
            inValue.fieldList = fieldList;
            inValue.sObjectType = sObjectType;
            inValue.ids = ids;
            retrieveResponse retVal = Channel.retrieve(inValue);
            LimitInfo = retVal.LimitInfoHeader;
            return retVal.result;
        }

        public async Task<sObject[]> retrieveAsync(string fieldList, string sObjectType, string[] ids, QueryOptions QueryOptions = null)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            retrieveRequest inValue = new retrieveRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.QueryOptions = QueryOptions;
            inValue.MruHeader = MruHeader;
            inValue.PackageVersionHeader = PackageVersionHeader;
            inValue.fieldList = fieldList;
            inValue.sObjectType = sObjectType;
            inValue.ids = ids;
            retrieveResponse t = await Channel.retrieveAsync(inValue);
            LimitInfo = t.LimitInfoHeader;
            return t.result;
        }

        public ProcessResult[] process(ProcessRequest[] actions)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            processRequest1 inValue = new processRequest1();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.AllowFieldTruncationHeader = AllowFieldTruncationHeader;
            inValue.DisableFeedTrackingHeader = DisableFeedTrackingHeader;
            inValue.StreamingEnabledHeader = StreamingEnabledHeader;
            inValue.DuplicateRuleHeader = DuplicateRuleHeader;
            inValue.LocaleOptions = LocaleOptions;
            inValue.DebuggingHeader = DebuggingHeader;
            inValue.PackageVersionHeader = PackageVersionHeader;
            inValue.actions = actions;
            processResponse retVal = Channel.process(inValue);
            DebuggingInfo = retVal.DebuggingInfo;
            LimitInfo = retVal.LimitInfoHeader;
            return retVal.result;
        }

        public async Task<ProcessResult[]> processAsync(ProcessRequest[] actions)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            processRequest1 inValue = new processRequest1();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.AllowFieldTruncationHeader = AllowFieldTruncationHeader;
            inValue.DisableFeedTrackingHeader = DisableFeedTrackingHeader;
            inValue.StreamingEnabledHeader = StreamingEnabledHeader;
            inValue.DuplicateRuleHeader = DuplicateRuleHeader;
            inValue.LocaleOptions = LocaleOptions;
            inValue.DebuggingHeader = DebuggingHeader;
            inValue.PackageVersionHeader = PackageVersionHeader;
            inValue.actions = actions;
            processResponse t = await Channel.processAsync(inValue);
            LimitInfo = t.LimitInfoHeader;
            return t.result;
        }

        public LeadConvertResult[] convertLead(LeadConvert[] leadConverts)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            convertLeadRequest inValue = new convertLeadRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.AllowFieldTruncationHeader = AllowFieldTruncationHeader;
            inValue.DisableFeedTrackingHeader = DisableFeedTrackingHeader;
            inValue.StreamingEnabledHeader = StreamingEnabledHeader;
            inValue.DuplicateRuleHeader = DuplicateRuleHeader;
            inValue.LocaleOptions = LocaleOptions;
            inValue.DebuggingHeader = DebuggingHeader;
            inValue.PackageVersionHeader = PackageVersionHeader;
            inValue.leadConverts = leadConverts;
            convertLeadResponse retVal = Channel.convertLead(inValue);
            DebuggingInfo = retVal.DebuggingInfo;
            LimitInfo = retVal.LimitInfoHeader;
            return retVal.result;
        }

        public async Task<LeadConvertResult[]> convertLeadAsync(LeadConvert[] leadConverts)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            convertLeadRequest inValue = new convertLeadRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.AllowFieldTruncationHeader = AllowFieldTruncationHeader;
            inValue.DisableFeedTrackingHeader = DisableFeedTrackingHeader;
            inValue.StreamingEnabledHeader = StreamingEnabledHeader;
            inValue.DuplicateRuleHeader = DuplicateRuleHeader;
            inValue.LocaleOptions = LocaleOptions;
            inValue.DebuggingHeader = DebuggingHeader;
            inValue.PackageVersionHeader = PackageVersionHeader;
            inValue.leadConverts = leadConverts;
            convertLeadResponse t = await Channel.convertLeadAsync(inValue);
            LimitInfo = t.LimitInfoHeader;
            return t.result;
        }

        //public LimitInfo[] logout(CallOptions CallOptions)
        //{
        //    logoutRequest inValue = new logoutRequest();
        //    inValue.SessionHeader = GetSessionHeader();
        //    inValue.CallOptions = CallOptions;
        //    logoutResponse retVal = Channel.logout(inValue);
        //    
        //}

        //public async Task<logoutResponse> logoutAsync(CallOptions CallOptions)
        //{
        //    logoutRequest inValue = new logoutRequest();
        //    inValue.SessionHeader = GetSessionHeader();
        //    inValue.CallOptions = CallOptions;
        //    XXX t = await Channel.logoutAsync(inValue);
        //}

        public InvalidateSessionsResult[] invalidateSessions(string[] sessionIds)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            invalidateSessionsRequest inValue = new invalidateSessionsRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.sessionIds = sessionIds;
            invalidateSessionsResponse retVal = Channel.invalidateSessions(inValue);
            LimitInfo = retVal.LimitInfoHeader;
            return retVal.result;
        }

        public async Task<InvalidateSessionsResult[]> invalidateSessionsAsync(string[] sessionIds)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            invalidateSessionsRequest inValue = new invalidateSessionsRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.sessionIds = sessionIds;
            invalidateSessionsResponse t = await Channel.invalidateSessionsAsync(inValue);
            LimitInfo = t.LimitInfoHeader;
            return t.result;
        }

        public GetDeletedResult getDeleted(string sObjectType, System.DateTime startDate, System.DateTime endDate)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            getDeletedRequest inValue = new getDeletedRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.sObjectType = sObjectType;
            inValue.startDate = startDate;
            inValue.endDate = endDate;
            getDeletedResponse retVal = Channel.getDeleted(inValue);
            LimitInfo = retVal.LimitInfoHeader;
            return retVal.result;

        }

        public async Task<GetDeletedResult> getDeletedAsync(string sObjectType, System.DateTime startDate, System.DateTime endDate)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            getDeletedRequest inValue = new getDeletedRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.sObjectType = sObjectType;
            inValue.startDate = startDate;
            inValue.endDate = endDate;
            getDeletedResponse t = await Channel.getDeletedAsync(inValue);
            LimitInfo = t.LimitInfoHeader;
            return t.result;
        }

        public GetUpdatedResult getUpdated(string sObjectType, System.DateTime startDate, System.DateTime endDate)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            getUpdatedRequest inValue = new getUpdatedRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.sObjectType = sObjectType;
            inValue.startDate = startDate;
            inValue.endDate = endDate;
            getUpdatedResponse retVal = Channel.getUpdated(inValue);
            LimitInfo = retVal.LimitInfoHeader;
            return retVal.result;

        }

        public async Task<GetUpdatedResult> getUpdatedAsync(string sObjectType, System.DateTime startDate, System.DateTime endDate)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            getUpdatedRequest inValue = new getUpdatedRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.sObjectType = sObjectType;
            inValue.startDate = startDate;
            inValue.endDate = endDate;
            getUpdatedResponse t = await Channel.getUpdatedAsync(inValue);
            LimitInfo = t.LimitInfoHeader;
            return t.result;
        }

        public QueryResult query(string queryString, QueryOptions QueryOptions = null)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            queryRequest inValue = new queryRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.QueryOptions = QueryOptions;
            inValue.MruHeader = MruHeader;
            inValue.PackageVersionHeader = PackageVersionHeader;
            inValue.queryString = queryString;
            queryResponse retVal = Channel.query(inValue);
            LimitInfo = retVal.LimitInfoHeader;
            return retVal.result;
        }

        public async Task<QueryResult> queryAsync(string queryString, QueryOptions QueryOptions = null)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            queryRequest inValue = new queryRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.QueryOptions = QueryOptions;
            inValue.MruHeader = MruHeader;
            inValue.PackageVersionHeader = PackageVersionHeader;
            inValue.queryString = queryString;

            queryResponse t = await Channel.queryAsync(inValue);
            LimitInfo = t.LimitInfoHeader;
            return t.result;
        }

        public QueryResult queryAll(string queryString, QueryOptions QueryOptions = null)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            queryAllRequest inValue = new queryAllRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.QueryOptions = QueryOptions;
            inValue.queryString = queryString;
            queryAllResponse retVal = Channel.queryAll(inValue);
            LimitInfo = retVal.LimitInfoHeader;
            return retVal.result;
        }

        public async Task<QueryResult> queryAllAsync(string queryString, QueryOptions QueryOptions = null)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            queryAllRequest inValue = new queryAllRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.QueryOptions = QueryOptions;
            inValue.queryString = queryString;
            queryAllResponse t = await Channel.queryAllAsync(inValue);
            LimitInfo = t.LimitInfoHeader;
            return t.result;
        }

        public QueryResult queryMore(string queryLocator, QueryOptions QueryOptions = null)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            queryMoreRequest inValue = new queryMoreRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.QueryOptions = QueryOptions;
            inValue.queryLocator = queryLocator;
            queryMoreResponse retVal = Channel.queryMore(inValue);
            LimitInfo = retVal.LimitInfoHeader;
            return retVal.result;
        }

        public async Task<QueryResult> queryMoreAsync(string queryLocator, QueryOptions QueryOptions = null)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            queryMoreRequest inValue = new queryMoreRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.QueryOptions = QueryOptions;
            inValue.queryLocator = queryLocator;
            queryMoreResponse t = await Channel.queryMoreAsync(inValue);
            LimitInfo = t.LimitInfoHeader;
            return t.result;
        }

        public SearchResult search(string searchString)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            searchRequest inValue = new searchRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.PackageVersionHeader = PackageVersionHeader;
            inValue.searchString = searchString;
            searchResponse retVal = Channel.search(inValue);
            LimitInfo = retVal.LimitInfoHeader;
            return retVal.result;
        }

        public async Task<SearchResult> searchAsync(string searchString)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            searchRequest inValue = new searchRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.PackageVersionHeader = PackageVersionHeader;
            inValue.searchString = searchString;
            searchResponse t = await Channel.searchAsync(inValue);
            LimitInfo = t.LimitInfoHeader;
            return t.result;
        }

        public GetServerTimestampResult getServerTimestamp()
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            getServerTimestampRequest inValue = new getServerTimestampRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            getServerTimestampResponse retVal = Channel.getServerTimestamp(inValue);
            LimitInfo = retVal.LimitInfoHeader;
            return retVal.result;
        }

        public async Task<GetServerTimestampResult> getServerTimestampAsync(CallOptions CallOptions)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            getServerTimestampRequest inValue = new getServerTimestampRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            getServerTimestampResponse t = await Channel.getServerTimestampAsync(inValue);
            LimitInfo = t.LimitInfoHeader;
            return t.result;
        }

        public SetPasswordResult setPassword(string userId, string password)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            setPasswordRequest inValue = new setPasswordRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.userId = userId;
            inValue.password = password;
            setPasswordResponse retVal = Channel.setPassword(inValue);
            LimitInfo = retVal.LimitInfoHeader;
            return retVal.result;
        }

        public async Task<SetPasswordResult> setPasswordAsync(string userId, string password)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            setPasswordRequest inValue = new setPasswordRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.userId = userId;
            inValue.password = password;
            setPasswordResponse t = await Channel.setPasswordAsync(inValue);
            LimitInfo = t.LimitInfoHeader;
            return t.result;
        }

        public ChangeOwnPasswordResult changeOwnPassword(string oldPassword, string newPassword)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            changeOwnPasswordRequest inValue = new changeOwnPasswordRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.oldPassword = oldPassword;
            inValue.newPassword = newPassword;
            changeOwnPasswordResponse retVal = Channel.changeOwnPassword(inValue);
            LimitInfo = retVal.LimitInfoHeader;
            return retVal.result;
        }

        public async Task<ChangeOwnPasswordResult> changeOwnPasswordAsync(string oldPassword, string newPassword)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            changeOwnPasswordRequest inValue = new changeOwnPasswordRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.oldPassword = oldPassword;
            inValue.newPassword = newPassword;
            changeOwnPasswordResponse t = await Channel.changeOwnPasswordAsync(inValue);
            LimitInfo = t.LimitInfoHeader;
            return t.result;
        }

        public ResetPasswordResult resetPassword(string userId)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            resetPasswordRequest inValue = new resetPasswordRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.EmailHeader = EmailHeader;
            inValue.userId = userId;
            resetPasswordResponse retVal = Channel.resetPassword(inValue);
            LimitInfo = retVal.LimitInfoHeader;
            return retVal.result;
        }

        public async Task<ResetPasswordResult> resetPasswordAsync(string userId)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            resetPasswordRequest inValue = new resetPasswordRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.EmailHeader = EmailHeader;
            inValue.userId = userId;
            resetPasswordResponse t = await Channel.resetPasswordAsync(inValue);
            LimitInfo = t.LimitInfoHeader;
            return t.result;
        }

        public GetUserInfoResult getUserInfo()
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            getUserInfoRequest inValue = new getUserInfoRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            getUserInfoResponse retVal = Channel.getUserInfo(inValue);
            LimitInfo = retVal.LimitInfoHeader;
            return retVal.result;
        }

        public async Task<GetUserInfoResult> getUserInfoAsync(CallOptions CallOptions)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            getUserInfoRequest inValue = new getUserInfoRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            getUserInfoResponse t = await Channel.getUserInfoAsync(inValue);
            LimitInfo = t.LimitInfoHeader;
            return t.result;
        }

        public DeleteByExampleResult[] deleteByExample(sObject[] sObjects, UserTerritoryDeleteHeader UserTerritoryDeleteHeader = null)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            deleteByExampleRequest inValue = new deleteByExampleRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.PackageVersionHeader = PackageVersionHeader;
            inValue.UserTerritoryDeleteHeader = UserTerritoryDeleteHeader;
            inValue.EmailHeader = EmailHeader;
            inValue.AllowFieldTruncationHeader = AllowFieldTruncationHeader;
            inValue.DisableFeedTrackingHeader = DisableFeedTrackingHeader;
            inValue.StreamingEnabledHeader = StreamingEnabledHeader;
            inValue.AllOrNoneHeader = AllOrNoneHeader;
            inValue.DuplicateRuleHeader = DuplicateRuleHeader;
            inValue.LocaleOptions = LocaleOptions;
            inValue.DebuggingHeader = DebuggingHeader;
            inValue.sObjects = sObjects;
            deleteByExampleResponse retVal = Channel.deleteByExample(inValue);
            DebuggingInfo = retVal.DebuggingInfo;
            LimitInfo = retVal.LimitInfoHeader;
            return retVal.result;
        }

        public async Task<DeleteByExampleResult[]> deleteByExampleAsync(sObject[] sObjects, UserTerritoryDeleteHeader UserTerritoryDeleteHeader = null)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            deleteByExampleRequest inValue = new deleteByExampleRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.PackageVersionHeader = PackageVersionHeader;
            inValue.UserTerritoryDeleteHeader = UserTerritoryDeleteHeader;
            inValue.EmailHeader = EmailHeader;
            inValue.AllowFieldTruncationHeader = AllowFieldTruncationHeader;
            inValue.DisableFeedTrackingHeader = DisableFeedTrackingHeader;
            inValue.StreamingEnabledHeader = StreamingEnabledHeader;
            inValue.AllOrNoneHeader = AllOrNoneHeader;
            inValue.DuplicateRuleHeader = DuplicateRuleHeader;
            inValue.LocaleOptions = LocaleOptions;
            inValue.DebuggingHeader = DebuggingHeader;
            inValue.sObjects = sObjects;
            deleteByExampleResponse t = await Channel.deleteByExampleAsync(inValue);
            LimitInfo = t.LimitInfoHeader;
            return t.result;
        }

        public SendEmailResult[] sendEmailMessage(string[] ids)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            sendEmailMessageRequest inValue = new sendEmailMessageRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.ids = ids;
            sendEmailMessageResponse retVal = Channel.sendEmailMessage(inValue);
            LimitInfo = retVal.LimitInfoHeader;
            return retVal.result;
        }

        public async Task<SendEmailResult[]> sendEmailMessageAsync(string[] ids)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            sendEmailMessageRequest inValue = new sendEmailMessageRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.ids = ids;
            sendEmailMessageResponse t = await Channel.sendEmailMessageAsync(inValue);
            LimitInfo = t.LimitInfoHeader;
            return t.result;
        }

        public SendEmailResult[] sendEmail(Email[] messages)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            sendEmailRequest inValue = new sendEmailRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.messages = messages;
            sendEmailResponse retVal = Channel.sendEmail(inValue);
            LimitInfo = retVal.LimitInfoHeader;
            return retVal.result;
        }

        public async Task<SendEmailResult[]> sendEmailAsync(Email[] messages)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            sendEmailRequest inValue = new sendEmailRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.messages = messages;
            sendEmailResponse t = await Channel.sendEmailAsync(inValue);
            LimitInfo = t.LimitInfoHeader;
            return t.result;
        }

        public RenderEmailTemplateResult[] renderEmailTemplate(RenderEmailTemplateRequest[] renderRequests)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            renderEmailTemplateRequest1 inValue = new renderEmailTemplateRequest1();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.renderRequests = renderRequests;
            renderEmailTemplateResponse retVal = Channel.renderEmailTemplate(inValue);
            LimitInfo = retVal.LimitInfoHeader;
            return retVal.result;
        }

        public async Task<RenderEmailTemplateResult[]> renderEmailTemplateAsync(RenderEmailTemplateRequest[] renderRequests)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            renderEmailTemplateRequest1 inValue = new renderEmailTemplateRequest1();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.renderRequests = renderRequests;
            renderEmailTemplateResponse t = await Channel.renderEmailTemplateAsync(inValue);
            LimitInfo = t.LimitInfoHeader;
            return t.result;
        }

        public RenderStoredEmailTemplateResult renderStoredEmailTemplate(RenderStoredEmailTemplateRequest request)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            renderStoredEmailTemplateRequest1 inValue = new renderStoredEmailTemplateRequest1();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.request = request;
            renderStoredEmailTemplateResponse retVal = Channel.renderStoredEmailTemplate(inValue);
            LimitInfo = retVal.LimitInfoHeader;
            return retVal.result;
        }

        public async Task<RenderStoredEmailTemplateResult> renderStoredEmailTemplateAsync(RenderStoredEmailTemplateRequest request)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            renderStoredEmailTemplateRequest1 inValue = new renderStoredEmailTemplateRequest1();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.request = request;
            renderStoredEmailTemplateResponse t = await Channel.renderStoredEmailTemplateAsync(inValue);
            LimitInfo = t.LimitInfoHeader;
            return t.result;
        }

        public PerformQuickActionResult[] performQuickActions(PerformQuickActionRequest[] quickActions, OwnerChangeOption[] OwnerChangeOptions = null)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            performQuickActionsRequest inValue = new performQuickActionsRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.AssignmentRuleHeader = AssignmentRuleHeader;
            inValue.MruHeader = MruHeader;
            inValue.AllowFieldTruncationHeader = AllowFieldTruncationHeader;
            inValue.DisableFeedTrackingHeader = DisableFeedTrackingHeader;
            inValue.StreamingEnabledHeader = StreamingEnabledHeader;
            inValue.AllOrNoneHeader = AllOrNoneHeader;
            inValue.DuplicateRuleHeader = DuplicateRuleHeader;
            inValue.LocaleOptions = LocaleOptions;
            inValue.DebuggingHeader = DebuggingHeader;
            inValue.PackageVersionHeader = PackageVersionHeader;
            inValue.EmailHeader = EmailHeader;
            inValue.OwnerChangeOptions = OwnerChangeOptions;
            inValue.quickActions = quickActions;
            performQuickActionsResponse retVal = Channel.performQuickActions(inValue);
            LimitInfo = retVal.LimitInfoHeader;
            return retVal.result;
        }

        public async Task<PerformQuickActionResult[]> performQuickActionsAsync(PerformQuickActionRequest[] quickActions, OwnerChangeOption[] OwnerChangeOptions = null)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            performQuickActionsRequest inValue = new performQuickActionsRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.AssignmentRuleHeader = AssignmentRuleHeader;
            inValue.MruHeader = MruHeader;
            inValue.AllowFieldTruncationHeader = AllowFieldTruncationHeader;
            inValue.DisableFeedTrackingHeader = DisableFeedTrackingHeader;
            inValue.StreamingEnabledHeader = StreamingEnabledHeader;
            inValue.AllOrNoneHeader = AllOrNoneHeader;
            inValue.DuplicateRuleHeader = DuplicateRuleHeader;
            inValue.LocaleOptions = LocaleOptions;
            inValue.DebuggingHeader = DebuggingHeader;
            inValue.PackageVersionHeader = PackageVersionHeader;
            inValue.EmailHeader = EmailHeader;
            inValue.OwnerChangeOptions = OwnerChangeOptions;
            inValue.quickActions = quickActions;
            performQuickActionsResponse t = await Channel.performQuickActionsAsync(inValue);
            LimitInfo = t.LimitInfoHeader;
            return t.result;
        }

        public DescribeQuickActionResult[] describeQuickActions(string[] quickActions)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            describeQuickActionsRequest inValue = new describeQuickActionsRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.PackageVersionHeader = PackageVersionHeader;
            inValue.LocaleOptions = LocaleOptions;
            inValue.quickActions = quickActions;
            describeQuickActionsResponse retVal = Channel.describeQuickActions(inValue);
            LimitInfo = retVal.LimitInfoHeader;
            return retVal.result;
        }

        public async Task<DescribeQuickActionResult[]> describeQuickActionsAsync(string[] quickActions)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            describeQuickActionsRequest inValue = new describeQuickActionsRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.PackageVersionHeader = PackageVersionHeader;
            inValue.LocaleOptions = LocaleOptions;
            inValue.quickActions = quickActions;
            describeQuickActionsResponse t = await Channel.describeQuickActionsAsync(inValue);
            LimitInfo = t.LimitInfoHeader;
            return t.result;
        }

        public DescribeQuickActionResult[] describeQuickActionsForRecordType(string recordTypeId, string[] quickActions)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            describeQuickActionsForRecordTypeRequest inValue = new describeQuickActionsForRecordTypeRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.PackageVersionHeader = PackageVersionHeader;
            inValue.LocaleOptions = LocaleOptions;
            inValue.quickActions = quickActions;
            inValue.recordTypeId = recordTypeId;
            describeQuickActionsForRecordTypeResponse retVal = Channel.describeQuickActionsForRecordType(inValue);
            LimitInfo = retVal.LimitInfoHeader;
            return retVal.result;
        }

        public async Task<DescribeQuickActionResult[]> describeQuickActionsForRecordTypeAsync(string recordTypeId, string[] quickActions)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            describeQuickActionsForRecordTypeRequest inValue = new describeQuickActionsForRecordTypeRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.PackageVersionHeader = PackageVersionHeader;
            inValue.LocaleOptions = LocaleOptions;
            inValue.quickActions = quickActions;
            inValue.recordTypeId = recordTypeId;
            describeQuickActionsForRecordTypeResponse t = await Channel.describeQuickActionsForRecordTypeAsync(inValue);
            LimitInfo = t.LimitInfoHeader;
            return t.result;
        }

        public DescribeAvailableQuickActionResult[] describeAvailableQuickActions(string contextType)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            describeAvailableQuickActionsRequest inValue = new describeAvailableQuickActionsRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.PackageVersionHeader = PackageVersionHeader;
            inValue.LocaleOptions = LocaleOptions;
            inValue.contextType = contextType;
            describeAvailableQuickActionsResponse retVal = Channel.describeAvailableQuickActions(inValue);
            LimitInfo = retVal.LimitInfoHeader;
            return retVal.result;
        }

        public async Task<DescribeAvailableQuickActionResult[]> describeAvailableQuickActionsAsync(string contextType)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            describeAvailableQuickActionsRequest inValue = new describeAvailableQuickActionsRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.PackageVersionHeader = PackageVersionHeader;
            inValue.LocaleOptions = LocaleOptions;
            inValue.contextType = contextType;
            describeAvailableQuickActionsResponse t = await Channel.describeAvailableQuickActionsAsync(inValue);
            LimitInfo = t.LimitInfoHeader;
            return t.result;
        }

        public QuickActionTemplateResult[] retrieveQuickActionTemplates(string contextId, string[] quickActionNames)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            retrieveQuickActionTemplatesRequest inValue = new retrieveQuickActionTemplatesRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.PackageVersionHeader = PackageVersionHeader;
            inValue.LocaleOptions = LocaleOptions;
            inValue.quickActionNames = quickActionNames;
            inValue.contextId = contextId;
            retrieveQuickActionTemplatesResponse retVal = Channel.retrieveQuickActionTemplates(inValue);
            LimitInfo = retVal.LimitInfoHeader;
            return retVal.result;
        }

        public async Task<QuickActionTemplateResult[]> retrieveQuickActionTemplatesAsync(string contextId, string[] quickActionNames)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            retrieveQuickActionTemplatesRequest inValue = new retrieveQuickActionTemplatesRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.PackageVersionHeader = PackageVersionHeader;
            inValue.LocaleOptions = LocaleOptions;
            inValue.quickActionNames = quickActionNames;
            inValue.contextId = contextId;
            retrieveQuickActionTemplatesResponse t = await Channel.retrieveQuickActionTemplatesAsync(inValue);
            LimitInfo = t.LimitInfoHeader;
            return t.result;
        }

        public QuickActionTemplateResult[] retrieveMassQuickActionTemplates(string[] contextIds, string quickActionName)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            retrieveMassQuickActionTemplatesRequest inValue = new retrieveMassQuickActionTemplatesRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.PackageVersionHeader = PackageVersionHeader;
            inValue.LocaleOptions = LocaleOptions;
            inValue.quickActionName = quickActionName;
            inValue.contextIds = contextIds;
            retrieveMassQuickActionTemplatesResponse retVal = Channel.retrieveMassQuickActionTemplates(inValue);
            LimitInfo = retVal.LimitInfoHeader;
            return retVal.result;
        }

        public async Task<QuickActionTemplateResult[]> retrieveMassQuickActionTemplatesAsync(string[] contextIds, string quickActionName)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            retrieveMassQuickActionTemplatesRequest inValue = new retrieveMassQuickActionTemplatesRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.PackageVersionHeader = PackageVersionHeader;
            inValue.LocaleOptions = LocaleOptions;
            inValue.quickActionName = quickActionName;
            inValue.contextIds = contextIds;
            retrieveMassQuickActionTemplatesResponse t = await Channel.retrieveMassQuickActionTemplatesAsync(inValue);
            LimitInfo = t.LimitInfoHeader;
            return t.result;
        }

        public DescribeVisualForceResult describeVisualForce(string namespacePrefix, bool includeAllDetails)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            describeVisualForceRequest inValue = new describeVisualForceRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.PackageVersionHeader = PackageVersionHeader;
            inValue.includeAllDetails = includeAllDetails;
            inValue.namespacePrefix = namespacePrefix;
            describeVisualForceResponse retVal = Channel.describeVisualForce(inValue);
            LimitInfo = retVal.LimitInfoHeader;
            return retVal.result;
        }

        public async Task<DescribeVisualForceResult> describeVisualForceAsync(string namespacePrefix, bool includeAllDetails)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            describeVisualForceRequest inValue = new describeVisualForceRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.PackageVersionHeader = PackageVersionHeader;
            inValue.includeAllDetails = includeAllDetails;
            inValue.namespacePrefix = namespacePrefix;
            describeVisualForceResponse t = await Channel.describeVisualForceAsync(inValue);
            LimitInfo = t.LimitInfoHeader;
            return t.result;
        }

        public FindDuplicatesResult[] findDuplicates(sObject[] sObjects)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            findDuplicatesRequest inValue = new findDuplicatesRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.PackageVersionHeader = PackageVersionHeader;
            inValue.DuplicateRuleHeader = DuplicateRuleHeader;
            inValue.sObjects = sObjects;
            findDuplicatesResponse retVal = Channel.findDuplicates(inValue);
            LimitInfo = retVal.LimitInfoHeader;
            return retVal.result;
        }

        public async Task<FindDuplicatesResult[]> findDuplicatesAsync(sObject[] sObjects)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            findDuplicatesRequest inValue = new findDuplicatesRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.PackageVersionHeader = PackageVersionHeader;
            inValue.DuplicateRuleHeader = DuplicateRuleHeader;
            inValue.sObjects = sObjects;
            findDuplicatesResponse t = await Channel.findDuplicatesAsync(inValue);
            LimitInfo = t.LimitInfoHeader;
            return t.result;
        }

        public FindDuplicatesResult[] findDuplicatesByIds(string[] ids)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            findDuplicatesByIdsRequest inValue = new findDuplicatesByIdsRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.PackageVersionHeader = PackageVersionHeader;
            inValue.DuplicateRuleHeader = DuplicateRuleHeader;
            inValue.ids = ids;
            findDuplicatesByIdsResponse retVal = Channel.findDuplicatesByIds(inValue);
            LimitInfo = retVal.LimitInfoHeader;
            return retVal.result;
        }

        public async Task<FindDuplicatesResult[]> findDuplicatesByIdsAsync(string[] ids)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            findDuplicatesByIdsRequest inValue = new findDuplicatesByIdsRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.PackageVersionHeader = PackageVersionHeader;
            inValue.DuplicateRuleHeader = DuplicateRuleHeader;
            inValue.ids = ids;
            findDuplicatesByIdsResponse t = await Channel.findDuplicatesByIdsAsync(inValue);
            LimitInfo = t.LimitInfoHeader;
            return t.result;
        }

        public DescribeNounResult[] describeNouns(string[] nouns, bool onlyRenamed, bool includeFields)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            describeNounsRequest inValue = new describeNounsRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.PackageVersionHeader = PackageVersionHeader;
            inValue.LocaleOptions = LocaleOptions;
            inValue.nouns = nouns;
            inValue.onlyRenamed = onlyRenamed;
            inValue.includeFields = includeFields;
            describeNounsResponse retVal = Channel.describeNouns(inValue);
            LimitInfo = retVal.LimitInfoHeader;
            return retVal.result;
        }

        public async Task<DescribeNounResult[]> describeNounsAsync(string[] nouns, bool onlyRenamed, bool includeFields)
        {
            MessageInspector.message = MethodBase.GetCurrentMethod().Name;
            describeNounsRequest inValue = new describeNounsRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.PackageVersionHeader = PackageVersionHeader;
            inValue.LocaleOptions = LocaleOptions;
            inValue.nouns = nouns;
            inValue.onlyRenamed = onlyRenamed;
            inValue.includeFields = includeFields;
            describeNounsResponse t = await Channel.describeNounsAsync(inValue);
            LimitInfo = t.LimitInfoHeader;
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

    public class MyMessageInspector : IClientMessageInspector
    {
        public string message = "";

        void IClientMessageInspector.AfterReceiveReply(ref Message reply, object correlationState)
        {
            if (message != "query")
                return;

            MemoryStream ms = new MemoryStream();
            XmlWriter writer = XmlWriter.Create(ms);
            reply.WriteMessage(writer);
            writer.Flush();
            ms.Position = 0;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.PreserveWhitespace = true;
            xmlDoc.Load(ms);

            //Queries come back with '<records xsi:type="sf:sObject">', but the generated objects can't handle it
            // and there doesn't seem to be a way to handle the deserialization properly by configuration. The problem is 
            // the <records> element is in one namespace and "sf:sObjects" is in another, but the deserializer thinks they
            // are both in the same namespace.
            XmlNodeList records = xmlDoc.GetElementsByTagName("records");
            foreach (XmlNode node in records)
            {
                if (node.Attributes["xsi:type"] != null && node.Attributes["xsi:type"].Value == "sf:sObject")
                {
                    node.Attributes["xsi:type"].Value = "sObject";
                }
            }

            ms = new MemoryStream();
            xmlDoc.Save(ms);
            ms.Position = 0;
            XmlReader reader = XmlReader.Create(ms);
            Message newMessage = Message.CreateMessage(reader, int.MaxValue, reply.Version);
            newMessage.Properties.CopyProperties(reply.Properties);
            reply = newMessage;
        }

        object IClientMessageInspector.BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            return null;
        }
    }

    //public class MyParameterInspector : IParameterInspector
    //{
    //    void IParameterInspector.AfterCall(string operationName, object[] outputs, object returnValue, object correlationState)
    //    {
    //    }

    //    object IParameterInspector.BeforeCall(string operationName, object[] inputs)
    //    {
    //        return null;
    //    }
    //}

    public class InspectorBehavior : System.ServiceModel.Description.IEndpointBehavior
    {
        public MyMessageInspector MessageInspector;

        void IEndpointBehavior.AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
        }

        void IEndpointBehavior.ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            clientRuntime.ClientMessageInspectors.Add(MessageInspector);
            //foreach (ClientOperation op in clientRuntime.ClientOperations)
            //op.ClientParameterInspectors.Add(new MyParameterInspector());
        }

        void IEndpointBehavior.ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
        }

        void IEndpointBehavior.Validate(ServiceEndpoint endpoint)
        {
        }
    }

}