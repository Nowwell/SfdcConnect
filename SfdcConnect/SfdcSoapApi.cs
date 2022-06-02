/****************************************************************************
*
*   File name: SfdcSoapApi.cs
*   Author: Sean Fife
*   Create date: 4/20/2016
*   Solution: SfdcConnect
*   Project: SfdcConnect
*   Description: Includes the SfdcSoapApi class for Salesforce Soap Api Connections
*
****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Web.Services.Protocols;
using System.Xml.Serialization;
using SfdcConnect.SoapObjects;

namespace SfdcConnect
{
    /// <summary>
    /// Salesforce SOAP Api connection class
    /// </summary>
    public class SfdcSoapApi : SfdcConnection
    {
        public SfdcSoapApi()
            : base()
        {
        }
        public SfdcSoapApi(string uri)
            : base(uri)
        {
        }
        public SfdcSoapApi(bool isTest, int apiversion)
            : base(isTest, apiversion)
        {
        }

        //#region Callbacks
        //private SendOrPostCallback loginOperationCompleted;
        //private SendOrPostCallback describeSObjectOperationCompleted;
        //private SendOrPostCallback describeSObjectsOperationCompleted;
        //private SendOrPostCallback describeGlobalOperationCompleted;
        //private SendOrPostCallback describeDataCategoryGroupsOperationCompleted;
        //private SendOrPostCallback describeDataCategoryGroupStructuresOperationCompleted;
        //private SendOrPostCallback describeKnowledgeSettingsOperationCompleted;
        //private SendOrPostCallback describeFlexiPagesOperationCompleted;
        //private SendOrPostCallback describeAppMenuOperationCompleted;
        //private SendOrPostCallback describeGlobalThemeOperationCompleted;
        //private SendOrPostCallback describeThemeOperationCompleted;
        //private SendOrPostCallback describeLayoutOperationCompleted;
        //private SendOrPostCallback describeSoftphoneLayoutOperationCompleted;
        //private SendOrPostCallback describeSearchLayoutsOperationCompleted;
        //private SendOrPostCallback describeSearchScopeOrderOperationCompleted;
        //private SendOrPostCallback describeCompactLayoutsOperationCompleted;
        //private SendOrPostCallback describePathAssistantsOperationCompleted;
        //private SendOrPostCallback describeApprovalLayoutOperationCompleted;
        //private SendOrPostCallback describeSoqlListViewsOperationCompleted;
        //private SendOrPostCallback executeListViewOperationCompleted;
        //private SendOrPostCallback describeSObjectListViewsOperationCompleted;
        //private SendOrPostCallback describeTabsOperationCompleted;
        //private SendOrPostCallback describeAllTabsOperationCompleted;
        //private SendOrPostCallback describePrimaryCompactLayoutsOperationCompleted;
        //private SendOrPostCallback createOperationCompleted;
        //private SendOrPostCallback updateOperationCompleted;
        //private SendOrPostCallback upsertOperationCompleted;
        //private SendOrPostCallback mergeOperationCompleted;
        //private SendOrPostCallback deleteOperationCompleted;
        //private SendOrPostCallback undeleteOperationCompleted;
        //private SendOrPostCallback emptyRecycleBinOperationCompleted;
        //private SendOrPostCallback retrieveOperationCompleted;
        //private SendOrPostCallback processOperationCompleted;
        //private SendOrPostCallback convertLeadOperationCompleted;
        //private SendOrPostCallback logoutOperationCompleted;
        //private SendOrPostCallback invalidateSessionsOperationCompleted;
        //private SendOrPostCallback getDeletedOperationCompleted;
        //private SendOrPostCallback getUpdatedOperationCompleted;
        //private SendOrPostCallback queryOperationCompleted;
        //private SendOrPostCallback queryAllOperationCompleted;
        //private SendOrPostCallback queryMoreOperationCompleted;
        //private SendOrPostCallback searchOperationCompleted;
        //private SendOrPostCallback getServerTimestampOperationCompleted;
        //private SendOrPostCallback setPasswordOperationCompleted;
        //private SendOrPostCallback resetPasswordOperationCompleted;
        //private SendOrPostCallback getUserInfoOperationCompleted;
        //private SendOrPostCallback sendEmailMessageOperationCompleted;
        //private SendOrPostCallback sendEmailOperationCompleted;
        //private SendOrPostCallback renderEmailTemplateOperationCompleted;
        //private SendOrPostCallback performQuickActionsOperationCompleted;
        //private SendOrPostCallback describeQuickActionsOperationCompleted;
        //private SendOrPostCallback describeAvailableQuickActionsOperationCompleted;
        //private SendOrPostCallback retrieveQuickActionTemplatesOperationCompleted;
        //private SendOrPostCallback describeNounsOperationCompleted;
        //#endregion

        //#region Properties
        //public PackageVersionHeader PackageVersionHeaderValue { get; set; }
        //public LocaleOptions LocaleOptionsValue { get; set; }
        //public MruHeader MruHeaderValue { get; set; }
        //public AssignmentRuleHeader AssignmentRuleHeaderValue { get; set; }
        //public AllowFieldTruncationHeader AllowFieldTruncationHeaderValue { get; set; }
        //public DisableFeedTrackingHeader DisableFeedTrackingHeaderValue { get; set; }
        //public StreamingEnabledHeader StreamingEnabledHeaderValue { get; set; }
        //public AllOrNoneHeader AllOrNoneHeaderValue { get; set; }
        //public DuplicateRuleHeader DuplicateRuleHeaderValue { get; set; }
        //public DebuggingHeader DebuggingHeaderValue { get; set; }
        //public EmailHeader EmailHeaderValue { get; set; }
        //public DebuggingInfo DebuggingInfoValue { get; set; }
        //public OwnerChangeOptions OwnerChangeOptionsValue { get; set; }
        //public UserTerritoryDeleteHeader UserTerritoryDeleteHeaderValue { get; set; }
        //public QueryOptions QueryOptionsValue { get; set; }

        //#endregion

        //#region Events
        ///// <remarks/>
        //public event describeSObjectCompletedEventHandler describeSObjectCompleted;
        ///// <remarks/>
        //public event describeSObjectsCompletedEventHandler describeSObjectsCompleted;
        ///// <remarks/>
        //public event describeGlobalCompletedEventHandler describeGlobalCompleted;
        ///// <remarks/>
        //public event describeDataCategoryGroupsCompletedEventHandler describeDataCategoryGroupsCompleted;
        ///// <remarks/>
        //public event describeDataCategoryGroupStructuresCompletedEventHandler describeDataCategoryGroupStructuresCompleted;
        ///// <remarks/>
        //public event describeKnowledgeSettingsCompletedEventHandler describeKnowledgeSettingsCompleted;
        ///// <remarks/>
        //public event describeFlexiPagesCompletedEventHandler describeFlexiPagesCompleted;
        ///// <remarks/>
        //public event describeAppMenuCompletedEventHandler describeAppMenuCompleted;
        ///// <remarks/>
        //public event describeGlobalThemeCompletedEventHandler describeGlobalThemeCompleted;
        ///// <remarks/>
        //public event describeThemeCompletedEventHandler describeThemeCompleted;
        ///// <remarks/>
        //public event describeLayoutCompletedEventHandler describeLayoutCompleted;
        ///// <remarks/>
        //public event describeSoftphoneLayoutCompletedEventHandler describeSoftphoneLayoutCompleted;
        ///// <remarks/>
        //public event describeSearchLayoutsCompletedEventHandler describeSearchLayoutsCompleted;
        ///// <remarks/>
        //public event describeSearchScopeOrderCompletedEventHandler describeSearchScopeOrderCompleted;
        ///// <remarks/>
        //public event describeCompactLayoutsCompletedEventHandler describeCompactLayoutsCompleted;
        ///// <remarks/>
        //public event describePathAssistantsCompletedEventHandler describePathAssistantsCompleted;
        ///// <remarks/>
        //public event describeApprovalLayoutCompletedEventHandler describeApprovalLayoutCompleted;
        ///// <remarks/>
        //public event describeSoqlListViewsCompletedEventHandler describeSoqlListViewsCompleted;
        ///// <remarks/>
        //public event executeListViewCompletedEventHandler executeListViewCompleted;
        ///// <remarks/>
        //public event describeSObjectListViewsCompletedEventHandler describeSObjectListViewsCompleted;
        ///// <remarks/>
        //public event describeTabsCompletedEventHandler describeTabsCompleted;
        ///// <remarks/>
        //public event describeAllTabsCompletedEventHandler describeAllTabsCompleted;
        ///// <remarks/>
        //public event describePrimaryCompactLayoutsCompletedEventHandler describePrimaryCompactLayoutsCompleted;
        ///// <remarks/>
        //public event createCompletedEventHandler createCompleted;
        ///// <remarks/>
        //public event updateCompletedEventHandler updateCompleted;
        ///// <remarks/>
        //public event upsertCompletedEventHandler upsertCompleted;
        ///// <remarks/>
        //public event mergeCompletedEventHandler mergeCompleted;
        ///// <remarks/>
        //public event deleteCompletedEventHandler deleteCompleted;
        ///// <remarks/>
        //public event undeleteCompletedEventHandler undeleteCompleted;
        ///// <remarks/>
        //public event emptyRecycleBinCompletedEventHandler emptyRecycleBinCompleted;
        ///// <remarks/>
        //public event retrieveCompletedEventHandler retrieveCompleted;
        ///// <remarks/>
        //public event processCompletedEventHandler processCompleted;
        ///// <remarks/>
        //public event convertLeadCompletedEventHandler convertLeadCompleted;
        ///// <remarks/>
        //public event invalidateSessionsCompletedEventHandler invalidateSessionsCompleted;
        ///// <remarks/>
        //public event getDeletedCompletedEventHandler getDeletedCompleted;
        ///// <remarks/>
        //public event getUpdatedCompletedEventHandler getUpdatedCompleted;
        ///// <remarks/>
        //public event queryCompletedEventHandler queryCompleted;
        ///// <remarks/>
        //public event queryAllCompletedEventHandler queryAllCompleted;
        ///// <remarks/>
        //public event queryMoreCompletedEventHandler queryMoreCompleted;
        ///// <remarks/>
        //public event searchCompletedEventHandler searchCompleted;
        ///// <remarks/>
        //public event getServerTimestampCompletedEventHandler getServerTimestampCompleted;
        ///// <remarks/>
        //public event setPasswordCompletedEventHandler setPasswordCompleted;
        ///// <remarks/>
        //public event resetPasswordCompletedEventHandler resetPasswordCompleted;
        ///// <remarks/>
        //public event getUserInfoCompletedEventHandler getUserInfoCompleted;
        ///// <remarks/>
        //public event sendEmailMessageCompletedEventHandler sendEmailMessageCompleted;
        ///// <remarks/>
        //public event sendEmailCompletedEventHandler sendEmailCompleted;
        ///// <remarks/>
        //public event renderEmailTemplateCompletedEventHandler renderEmailTemplateCompleted;
        ///// <remarks/>
        //public event performQuickActionsCompletedEventHandler performQuickActionsCompleted;
        ///// <remarks/>
        //public event describeQuickActionsCompletedEventHandler describeQuickActionsCompleted;
        ///// <remarks/>
        //public event describeAvailableQuickActionsCompletedEventHandler describeAvailableQuickActionsCompleted;
        ///// <remarks/>
        //public event retrieveQuickActionTemplatesCompletedEventHandler retrieveQuickActionTemplatesCompleted;
        ///// <remarks/>
        //public event describeNounsCompletedEventHandler describeNounsCompleted;
        //#endregion

        //#region Synchronous Calls
        ///// <remarks/>
        //[SoapHeaderAttribute("SessionHeaderValue")]
        //[SoapHeaderAttribute("LimitInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
        //[SoapHeaderAttribute("CallOptionsValue")]
        //[SoapHeaderAttribute("PackageVersionHeaderValue")]
        //[SoapHeaderAttribute("LocaleOptionsValue")]
        //[SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
        //[return: XmlElementAttribute("result", IsNullable = true)]
        //public DescribeSObjectResult describeSObject(string sObjectType)
        //{
        //    object[] results = this.Invoke("describeSObject", new object[] { sObjectType });
        //    return ((DescribeSObjectResult)(results[0]));
        //}

        ///// <remarks/>
        //[SoapHeaderAttribute("SessionHeaderValue")]
        //[SoapHeaderAttribute("LimitInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
        //[SoapHeaderAttribute("CallOptionsValue")]
        //[SoapHeaderAttribute("PackageVersionHeaderValue")]
        //[SoapHeaderAttribute("LocaleOptionsValue")]
        //[SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
        //[return: XmlElementAttribute("result", IsNullable = true)]
        //public DescribeSObjectResult[] describeSObjects([XmlElementAttribute("sObjectType")] string[] sObjectType)
        //{
        //    object[] results = this.Invoke("describeSObjects", new object[] {
        //                sObjectType});
        //    return ((DescribeSObjectResult[])(results[0]));
        //}

        ///// <remarks/>
        //[SoapHeaderAttribute("SessionHeaderValue")]
        //[SoapHeaderAttribute("LimitInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
        //[SoapHeaderAttribute("CallOptionsValue")]
        //[SoapHeaderAttribute("PackageVersionHeaderValue")]
        //[SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
        //[return: XmlElementAttribute("result")]
        //public DescribeGlobalResult describeGlobal()
        //{
        //    object[] results = this.Invoke("describeGlobal", new object[0]);
        //    return ((DescribeGlobalResult)(results[0]));
        //}

        ///// <remarks/>
        //[SoapHeaderAttribute("SessionHeaderValue")]
        //[SoapHeaderAttribute("LimitInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
        //[SoapHeaderAttribute("CallOptionsValue")]
        //[SoapHeaderAttribute("PackageVersionHeaderValue")]
        //[SoapHeaderAttribute("LocaleOptionsValue")]
        //[SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
        //[return: XmlElementAttribute("result")]
        //public DescribeDataCategoryGroupResult[] describeDataCategoryGroups([XmlElementAttribute("sObjectType")] string[] sObjectType)
        //{
        //    object[] results = this.Invoke("describeDataCategoryGroups", new object[] {
        //                sObjectType});
        //    return ((DescribeDataCategoryGroupResult[])(results[0]));
        //}

        ///// <remarks/>
        //[SoapHeaderAttribute("SessionHeaderValue")]
        //[SoapHeaderAttribute("LimitInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
        //[SoapHeaderAttribute("CallOptionsValue")]
        //[SoapHeaderAttribute("PackageVersionHeaderValue")]
        //[SoapHeaderAttribute("LocaleOptionsValue")]
        //[SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
        //[return: XmlElementAttribute("result")]
        //public DescribeDataCategoryGroupStructureResult[] describeDataCategoryGroupStructures([XmlElementAttribute("pairs")] DataCategoryGroupSobjectTypePair[] pairs, bool topCategoriesOnly)
        //{
        //    object[] results = this.Invoke("describeDataCategoryGroupStructures", new object[] {
        //                pairs,
        //                topCategoriesOnly});
        //    return ((DescribeDataCategoryGroupStructureResult[])(results[0]));
        //}

        ///// <remarks/>
        //[SoapHeaderAttribute("SessionHeaderValue")]
        //[SoapHeaderAttribute("LimitInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
        //[SoapHeaderAttribute("CallOptionsValue")]
        //[SoapHeaderAttribute("PackageVersionHeaderValue")]
        //[SoapHeaderAttribute("LocaleOptionsValue")]
        //[SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
        //[return: XmlElementAttribute("result")]
        //public KnowledgeSettings describeKnowledgeSettings()
        //{
        //    object[] results = this.Invoke("describeKnowledgeSettings", new object[0]);
        //    return ((KnowledgeSettings)(results[0]));
        //}

        ///// <remarks/>
        //[SoapHeaderAttribute("SessionHeaderValue")]
        //[SoapHeaderAttribute("LimitInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
        //[SoapHeaderAttribute("CallOptionsValue")]
        //[SoapHeaderAttribute("PackageVersionHeaderValue")]
        //[SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
        //[return: XmlElementAttribute("result")]
        //public DescribeFlexiPageResult[] describeFlexiPages([XmlElementAttribute("flexiPages")] string[] flexiPages, [XmlElementAttribute("contexts")] FlexipageContext[] contexts)
        //{
        //    object[] results = this.Invoke("describeFlexiPages", new object[] {
        //                flexiPages,
        //                contexts});
        //    return ((DescribeFlexiPageResult[])(results[0]));
        //}

        ///// <remarks/>
        //[SoapHeaderAttribute("SessionHeaderValue")]
        //[SoapHeaderAttribute("LimitInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
        //[SoapHeaderAttribute("CallOptionsValue")]
        //[SoapHeaderAttribute("PackageVersionHeaderValue")]
        //[SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
        //[return: XmlArrayAttribute("result", IsNullable = true)]
        //[return: XmlArrayItemAttribute("appMenuItems", IsNullable = false)]
        //public DescribeAppMenuItem[] describeAppMenu(AppMenuType appMenuType, [XmlElementAttribute(IsNullable = true)] string networkId)
        //{
        //    object[] results = this.Invoke("describeAppMenu", new object[] {
        //                appMenuType,
        //                networkId});
        //    return ((DescribeAppMenuItem[])(results[0]));
        //}

        ///// <remarks/>
        //[SoapHeaderAttribute("SessionHeaderValue")]
        //[SoapHeaderAttribute("LimitInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
        //[SoapHeaderAttribute("CallOptionsValue")]
        //[SoapHeaderAttribute("PackageVersionHeaderValue")]
        //[SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
        //[return: XmlElementAttribute("result")]
        //public DescribeGlobalTheme describeGlobalTheme()
        //{
        //    object[] results = this.Invoke("describeGlobalTheme", new object[0]);
        //    return ((DescribeGlobalTheme)(results[0]));
        //}

        ///// <remarks/>
        //[SoapHeaderAttribute("SessionHeaderValue")]
        //[SoapHeaderAttribute("LimitInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
        //[SoapHeaderAttribute("CallOptionsValue")]
        //[SoapHeaderAttribute("PackageVersionHeaderValue")]
        //[SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
        //[return: XmlArrayAttribute("result")]
        //[return: XmlArrayItemAttribute("themeItems", IsNullable = false)]
        //public DescribeThemeItem[] describeTheme([XmlElementAttribute("sobjectType")] string[] sobjectType)
        //{
        //    object[] results = this.Invoke("describeTheme", new object[] {
        //                sobjectType});
        //    return ((DescribeThemeItem[])(results[0]));
        //}

        ///// <remarks/>
        //[SoapHeaderAttribute("SessionHeaderValue")]
        //[SoapHeaderAttribute("LimitInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
        //[SoapHeaderAttribute("CallOptionsValue")]
        //[SoapHeaderAttribute("PackageVersionHeaderValue")]
        //[SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
        //[return: XmlElementAttribute("result", IsNullable = true)]
        //public DescribeLayoutResult describeLayout(string sObjectType, [XmlElementAttribute(IsNullable = true)] string layoutName, [XmlElementAttribute("recordTypeIds")] string[] recordTypeIds)
        //{
        //    object[] results = this.Invoke("describeLayout", new object[] {
        //                sObjectType,
        //                layoutName,
        //                recordTypeIds});
        //    return ((DescribeLayoutResult)(results[0]));
        //}

        ///// <remarks/>
        //[SoapHeaderAttribute("SessionHeaderValue")]
        //[SoapHeaderAttribute("LimitInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
        //[SoapHeaderAttribute("CallOptionsValue")]
        //[SoapHeaderAttribute("PackageVersionHeaderValue")]
        //[SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
        //[return: XmlElementAttribute("result", IsNullable = true)]
        //public DescribeSoftphoneLayoutResult describeSoftphoneLayout()
        //{
        //    object[] results = this.Invoke("describeSoftphoneLayout", new object[0]);
        //    return ((DescribeSoftphoneLayoutResult)(results[0]));
        //}

        ///// <remarks/>
        //[SoapHeaderAttribute("SessionHeaderValue")]
        //[SoapHeaderAttribute("LimitInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
        //[SoapHeaderAttribute("CallOptionsValue")]
        //[SoapHeaderAttribute("PackageVersionHeaderValue")]
        //[SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
        //[return: XmlElementAttribute("result", IsNullable = true)]
        //public DescribeSearchLayoutResult[] describeSearchLayouts([XmlElementAttribute("sObjectType")] string[] sObjectType)
        //{
        //    object[] results = this.Invoke("describeSearchLayouts", new object[] {
        //                sObjectType});
        //    return ((DescribeSearchLayoutResult[])(results[0]));
        //}

        ///// <remarks/>
        //[SoapHeaderAttribute("SessionHeaderValue")]
        //[SoapHeaderAttribute("LimitInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
        //[SoapHeaderAttribute("CallOptionsValue")]
        //[SoapHeaderAttribute("PackageVersionHeaderValue")]
        //[SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
        //[return: XmlElementAttribute("result")]
        //public DescribeSearchScopeOrderResult[] describeSearchScopeOrder()
        //{
        //    object[] results = this.Invoke("describeSearchScopeOrder", new object[0]);
        //    return ((DescribeSearchScopeOrderResult[])(results[0]));
        //}

        ///// <remarks/>
        //[SoapHeaderAttribute("SessionHeaderValue")]
        //[SoapHeaderAttribute("LimitInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
        //[SoapHeaderAttribute("CallOptionsValue")]
        //[SoapHeaderAttribute("PackageVersionHeaderValue")]
        //[SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
        //[return: XmlElementAttribute("result", IsNullable = true)]
        //public DescribeCompactLayoutsResult describeCompactLayouts(string sObjectType, [XmlElementAttribute("recordTypeIds")] string[] recordTypeIds)
        //{
        //    object[] results = this.Invoke("describeCompactLayouts", new object[] {
        //                sObjectType,
        //                recordTypeIds});
        //    return ((DescribeCompactLayoutsResult)(results[0]));
        //}

        ///// <remarks/>
        //[SoapHeaderAttribute("SessionHeaderValue")]
        //[SoapHeaderAttribute("LimitInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
        //[SoapHeaderAttribute("CallOptionsValue")]
        //[SoapHeaderAttribute("PackageVersionHeaderValue")]
        //[SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
        //[return: XmlArrayAttribute("result", IsNullable = true)]
        //[return: XmlArrayItemAttribute("pathAssistants", IsNullable = false)]
        //public DescribePathAssistant[] describePathAssistants(string sObjectType, [XmlElementAttribute(IsNullable = true)] string picklistValue, [XmlElementAttribute("recordTypeIds")] string[] recordTypeIds)
        //{
        //    object[] results = this.Invoke("describePathAssistants", new object[] {
        //                sObjectType,
        //                picklistValue,
        //                recordTypeIds});
        //    return ((DescribePathAssistant[])(results[0]));
        //}

        ///// <remarks/>
        //[SoapHeaderAttribute("SessionHeaderValue")]
        //[SoapHeaderAttribute("LimitInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
        //[SoapHeaderAttribute("CallOptionsValue")]
        //[SoapHeaderAttribute("PackageVersionHeaderValue")]
        //[SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
        //[return: XmlArrayAttribute("result", IsNullable = true)]
        //[return: XmlArrayItemAttribute("approvalLayouts", IsNullable = false)]
        //public DescribeApprovalLayout[] describeApprovalLayout(string sObjectType, [XmlElementAttribute("approvalProcessNames")] string[] approvalProcessNames)
        //{
        //    object[] results = this.Invoke("describeApprovalLayout", new object[] {
        //                sObjectType,
        //                approvalProcessNames});
        //    return ((DescribeApprovalLayout[])(results[0]));
        //}

        ///// <remarks/>
        //[SoapHeaderAttribute("SessionHeaderValue")]
        //[SoapHeaderAttribute("LimitInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
        //[SoapHeaderAttribute("CallOptionsValue")]
        //[SoapHeaderAttribute("PackageVersionHeaderValue")]
        //[SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
        //[return: XmlArrayAttribute("result")]
        //[return: XmlArrayItemAttribute("describeSoqlListViews", IsNullable = false)]
        //public DescribeSoqlListView[] describeSoqlListViews([XmlArrayItemAttribute("listViewParams", IsNullable = false)] DescribeSoqlListViewParams[] request)
        //{
        //    object[] results = this.Invoke("describeSoqlListViews", new object[] {
        //                request});
        //    return ((DescribeSoqlListView[])(results[0]));
        //}

        ///// <remarks/>
        //[SoapHeaderAttribute("SessionHeaderValue")]
        //[SoapHeaderAttribute("CallOptionsValue")]
        //[SoapHeaderAttribute("LimitInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
        //[SoapHeaderAttribute("MruHeaderValue")]
        //[SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
        //[return: XmlElementAttribute("result")]
        //public ExecuteListViewResult executeListView(ExecuteListViewRequest request)
        //{
        //    object[] results = this.Invoke("executeListView", new object[] {
        //                request});
        //    return ((ExecuteListViewResult)(results[0]));
        //}

        ///// <remarks/>
        //[SoapHeaderAttribute("SessionHeaderValue")]
        //[SoapHeaderAttribute("LimitInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
        //[SoapHeaderAttribute("CallOptionsValue")]
        //[SoapHeaderAttribute("PackageVersionHeaderValue")]
        //[SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
        //[return: XmlArrayAttribute("result")]
        //[return: XmlArrayItemAttribute("describeSoqlListViews", IsNullable = false)]
        //public DescribeSoqlListView[] describeSObjectListViews(string sObjectType, bool recentsOnly, listViewIsSoqlCompatible isSoqlCompatible, int limit, int offset)
        //{
        //    object[] results = this.Invoke("describeSObjectListViews", new object[] {
        //                sObjectType,
        //                recentsOnly,
        //                isSoqlCompatible,
        //                limit,
        //                offset});
        //    return ((DescribeSoqlListView[])(results[0]));
        //}

        ///// <remarks/>
        //[SoapHeaderAttribute("SessionHeaderValue")]
        //[SoapHeaderAttribute("LimitInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
        //[SoapHeaderAttribute("CallOptionsValue")]
        //[SoapHeaderAttribute("PackageVersionHeaderValue")]
        //[SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
        //[return: XmlElementAttribute("result", IsNullable = true)]
        //public DescribeTabSetResult[] describeTabs()
        //{
        //    object[] results = this.Invoke("describeTabs", new object[0]);
        //    return ((DescribeTabSetResult[])(results[0]));
        //}

        ///// <remarks/>
        //[SoapHeaderAttribute("SessionHeaderValue")]
        //[SoapHeaderAttribute("LimitInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
        //[SoapHeaderAttribute("CallOptionsValue")]
        //[SoapHeaderAttribute("PackageVersionHeaderValue")]
        //[SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
        //[return: XmlElementAttribute("result", IsNullable = true)]
        //public DescribeTab[] describeAllTabs()
        //{
        //    object[] results = this.Invoke("describeAllTabs", new object[0]);
        //    return ((DescribeTab[])(results[0]));
        //}

        ///// <remarks/>
        //[SoapHeaderAttribute("SessionHeaderValue")]
        //[SoapHeaderAttribute("LimitInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
        //[SoapHeaderAttribute("CallOptionsValue")]
        //[SoapHeaderAttribute("PackageVersionHeaderValue")]
        //[SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
        //[return: XmlElementAttribute("result")]
        //public DescribeCompactLayout[] describePrimaryCompactLayouts([XmlElementAttribute("sObjectTypes")] string[] sObjectTypes)
        //{
        //    object[] results = this.Invoke("describePrimaryCompactLayouts", new object[] {
        //                sObjectTypes});
        //    return ((DescribeCompactLayout[])(results[0]));
        //}

        ///// <remarks/>
        //[SoapHeaderAttribute("SessionHeaderValue")]
        //[SoapHeaderAttribute("StreamingEnabledHeaderValue")]
        //[SoapHeaderAttribute("CallOptionsValue")]
        //[SoapHeaderAttribute("AssignmentRuleHeaderValue")]
        //[SoapHeaderAttribute("LimitInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
        //[SoapHeaderAttribute("LocaleOptionsValue")]
        //[SoapHeaderAttribute("AllOrNoneHeaderValue")]
        //[SoapHeaderAttribute("EmailHeaderValue")]
        //[SoapHeaderAttribute("DisableFeedTrackingHeaderValue")]
        //[SoapHeaderAttribute("DebuggingInfoValue", Direction = SoapHeaderDirection.Out)]
        //[SoapHeaderAttribute("AllowFieldTruncationHeaderValue")]
        //[SoapHeaderAttribute("DebuggingHeaderValue")]
        //[SoapHeaderAttribute("MruHeaderValue")]
        //[SoapHeaderAttribute("PackageVersionHeaderValue")]
        //[SoapHeaderAttribute("DuplicateRuleHeaderValue")]
        //[SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
        //[return: XmlElementAttribute("result")]
        //public SaveResult[] create([XmlElementAttribute("sObjects")] sObject[] sObjects)
        //{
        //    object[] results = this.Invoke("create", new object[] {
        //                sObjects});
        //    return ((SaveResult[])(results[0]));
        //}

        ///// <remarks/>
        //[SoapHeaderAttribute("SessionHeaderValue")]
        //[SoapHeaderAttribute("OwnerChangeOptionsValue")]
        //[SoapHeaderAttribute("StreamingEnabledHeaderValue")]
        //[SoapHeaderAttribute("CallOptionsValue")]
        //[SoapHeaderAttribute("AssignmentRuleHeaderValue")]
        //[SoapHeaderAttribute("LimitInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
        //[SoapHeaderAttribute("LocaleOptionsValue")]
        //[SoapHeaderAttribute("AllOrNoneHeaderValue")]
        //[SoapHeaderAttribute("EmailHeaderValue")]
        //[SoapHeaderAttribute("DisableFeedTrackingHeaderValue")]
        //[SoapHeaderAttribute("DebuggingInfoValue", Direction = SoapHeaderDirection.Out)]
        //[SoapHeaderAttribute("AllowFieldTruncationHeaderValue")]
        //[SoapHeaderAttribute("DebuggingHeaderValue")]
        //[SoapHeaderAttribute("MruHeaderValue")]
        //[SoapHeaderAttribute("PackageVersionHeaderValue")]
        //[SoapHeaderAttribute("DuplicateRuleHeaderValue")]
        //[SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
        //[return: XmlElementAttribute("result")]
        //public SaveResult[] update([XmlElementAttribute("sObjects")] sObject[] sObjects)
        //{
        //    object[] results = this.Invoke("update", new object[] {
        //                sObjects});
        //    return ((SaveResult[])(results[0]));
        //}

        ///// <remarks/>
        //[SoapHeaderAttribute("SessionHeaderValue")]
        //[SoapHeaderAttribute("OwnerChangeOptionsValue")]
        //[SoapHeaderAttribute("StreamingEnabledHeaderValue")]
        //[SoapHeaderAttribute("CallOptionsValue")]
        //[SoapHeaderAttribute("AssignmentRuleHeaderValue")]
        //[SoapHeaderAttribute("LimitInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
        //[SoapHeaderAttribute("LocaleOptionsValue")]
        //[SoapHeaderAttribute("AllOrNoneHeaderValue")]
        //[SoapHeaderAttribute("EmailHeaderValue")]
        //[SoapHeaderAttribute("DisableFeedTrackingHeaderValue")]
        //[SoapHeaderAttribute("DebuggingInfoValue", Direction = SoapHeaderDirection.Out)]
        //[SoapHeaderAttribute("AllowFieldTruncationHeaderValue")]
        //[SoapHeaderAttribute("DebuggingHeaderValue")]
        //[SoapHeaderAttribute("MruHeaderValue")]
        //[SoapHeaderAttribute("PackageVersionHeaderValue")]
        //[SoapHeaderAttribute("DuplicateRuleHeaderValue")]
        //[SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
        //[return: XmlElementAttribute("result")]
        //public UpsertResult[] upsert(string externalIDFieldName, [XmlElementAttribute("sObjects")] sObject[] sObjects)
        //{
        //    object[] results = this.Invoke("upsert", new object[] {
        //                externalIDFieldName,
        //                sObjects});
        //    return ((UpsertResult[])(results[0]));
        //}

        ///// <remarks/>
        //[SoapHeaderAttribute("SessionHeaderValue")]
        //[SoapHeaderAttribute("DebuggingHeaderValue")]
        //[SoapHeaderAttribute("CallOptionsValue")]
        //[SoapHeaderAttribute("AssignmentRuleHeaderValue")]
        //[SoapHeaderAttribute("LimitInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
        //[SoapHeaderAttribute("LocaleOptionsValue")]
        //[SoapHeaderAttribute("StreamingEnabledHeaderValue")]
        //[SoapHeaderAttribute("DisableFeedTrackingHeaderValue")]
        //[SoapHeaderAttribute("DebuggingInfoValue", Direction = SoapHeaderDirection.Out)]
        //[SoapHeaderAttribute("AllowFieldTruncationHeaderValue")]
        //[SoapHeaderAttribute("EmailHeaderValue")]
        //[SoapHeaderAttribute("MruHeaderValue")]
        //[SoapHeaderAttribute("PackageVersionHeaderValue")]
        //[SoapHeaderAttribute("DuplicateRuleHeaderValue")]
        //[SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
        //[return: XmlElementAttribute("result")]
        //public MergeResult[] merge([XmlElementAttribute("request")] MergeRequest[] request)
        //{
        //    object[] results = this.Invoke("merge", new object[] {
        //                request});
        //    return ((MergeResult[])(results[0]));
        //}

        ///// <remarks/>
        //[SoapHeaderAttribute("SessionHeaderValue")]
        //[SoapHeaderAttribute("DebuggingHeaderValue")]
        //[SoapHeaderAttribute("CallOptionsValue")]
        //[SoapHeaderAttribute("LimitInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
        //[SoapHeaderAttribute("LocaleOptionsValue")]
        //[SoapHeaderAttribute("UserTerritoryDeleteHeaderValue")]
        //[SoapHeaderAttribute("AllOrNoneHeaderValue")]
        //[SoapHeaderAttribute("StreamingEnabledHeaderValue")]
        //[SoapHeaderAttribute("DisableFeedTrackingHeaderValue")]
        //[SoapHeaderAttribute("DebuggingInfoValue", Direction = SoapHeaderDirection.Out)]
        //[SoapHeaderAttribute("AllowFieldTruncationHeaderValue")]
        //[SoapHeaderAttribute("EmailHeaderValue")]
        //[SoapHeaderAttribute("PackageVersionHeaderValue")]
        //[SoapHeaderAttribute("DuplicateRuleHeaderValue")]
        //[SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
        //[return: XmlElementAttribute("result")]
        //public DeleteResult[] delete([XmlElementAttribute("ids")] string[] ids)
        //{
        //    object[] results = this.Invoke("delete", new object[] {
        //                ids});
        //    return ((DeleteResult[])(results[0]));
        //}

        ///// <remarks/>
        //[SoapHeaderAttribute("CallOptionsValue")]
        //[SoapHeaderAttribute("SessionHeaderValue")]
        //[SoapHeaderAttribute("StreamingEnabledHeaderValue")]
        //[SoapHeaderAttribute("AllOrNoneHeaderValue")]
        //[SoapHeaderAttribute("DebuggingHeaderValue")]
        //[SoapHeaderAttribute("DebuggingInfoValue", Direction = SoapHeaderDirection.Out)]
        //[SoapHeaderAttribute("DuplicateRuleHeaderValue")]
        //[SoapHeaderAttribute("LimitInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
        //[SoapHeaderAttribute("LocaleOptionsValue")]
        //[SoapHeaderAttribute("PackageVersionHeaderValue")]
        //[SoapHeaderAttribute("DisableFeedTrackingHeaderValue")]
        //[SoapHeaderAttribute("AllowFieldTruncationHeaderValue")]
        //[SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
        //[return: XmlElementAttribute("result")]
        //public UndeleteResult[] undelete([XmlElementAttribute("ids")] string[] ids)
        //{
        //    object[] results = this.Invoke("undelete", new object[] {
        //                ids});
        //    return ((UndeleteResult[])(results[0]));
        //}

        ///// <remarks/>
        //[SoapHeaderAttribute("SessionHeaderValue")]
        //[SoapHeaderAttribute("CallOptionsValue")]
        //[SoapHeaderAttribute("LimitInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
        //[SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
        //[return: XmlElementAttribute("result")]
        //public EmptyRecycleBinResult[] emptyRecycleBin([XmlElementAttribute("ids")] string[] ids)
        //{
        //    object[] results = this.Invoke("emptyRecycleBin", new object[] {
        //                ids});
        //    return ((EmptyRecycleBinResult[])(results[0]));
        //}

        ///// <remarks/>
        //[SoapHeaderAttribute("CallOptionsValue")]
        //[SoapHeaderAttribute("SessionHeaderValue")]
        //[SoapHeaderAttribute("LimitInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
        //[SoapHeaderAttribute("QueryOptionsValue")]
        //[SoapHeaderAttribute("MruHeaderValue")]
        //[SoapHeaderAttribute("PackageVersionHeaderValue")]
        //[SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
        //[return: XmlElementAttribute("result", IsNullable = true)]
        //public sObject[] retrieve(string fieldList, string sObjectType, [XmlElementAttribute("ids")] string[] ids)
        //{
        //    object[] results = this.Invoke("retrieve", new object[] {
        //                fieldList,
        //                sObjectType,
        //                ids});
        //    return ((sObject[])(results[0]));
        //}

        ///// <remarks/>
        //[SoapHeaderAttribute("CallOptionsValue")]
        //[SoapHeaderAttribute("SessionHeaderValue")]
        //[SoapHeaderAttribute("StreamingEnabledHeaderValue")]
        //[SoapHeaderAttribute("DebuggingHeaderValue")]
        //[SoapHeaderAttribute("DebuggingInfoValue", Direction = SoapHeaderDirection.Out)]
        //[SoapHeaderAttribute("DuplicateRuleHeaderValue")]
        //[SoapHeaderAttribute("LimitInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
        //[SoapHeaderAttribute("LocaleOptionsValue")]
        //[SoapHeaderAttribute("PackageVersionHeaderValue")]
        //[SoapHeaderAttribute("DisableFeedTrackingHeaderValue")]
        //[SoapHeaderAttribute("AllowFieldTruncationHeaderValue")]
        //[SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
        //[return: XmlElementAttribute("result")]
        //public ProcessResult[] process([XmlElementAttribute("actions")] ProcessRequest[] actions)
        //{
        //    object[] results = this.Invoke("process", new object[] {
        //                actions});
        //    return ((ProcessResult[])(results[0]));
        //}

        ///// <remarks/>
        //[SoapHeaderAttribute("CallOptionsValue")]
        //[SoapHeaderAttribute("SessionHeaderValue")]
        //[SoapHeaderAttribute("StreamingEnabledHeaderValue")]
        //[SoapHeaderAttribute("DebuggingHeaderValue")]
        //[SoapHeaderAttribute("DebuggingInfoValue", Direction = SoapHeaderDirection.Out)]
        //[SoapHeaderAttribute("DuplicateRuleHeaderValue")]
        //[SoapHeaderAttribute("LimitInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
        //[SoapHeaderAttribute("LocaleOptionsValue")]
        //[SoapHeaderAttribute("PackageVersionHeaderValue")]
        //[SoapHeaderAttribute("DisableFeedTrackingHeaderValue")]
        //[SoapHeaderAttribute("AllowFieldTruncationHeaderValue")]
        //[SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
        //[return: XmlElementAttribute("result")]
        //public LeadConvertResult[] convertLead([XmlElementAttribute("leadConverts")] LeadConvert[] leadConverts)
        //{
        //    object[] results = this.Invoke("convertLead", new object[] {
        //                leadConverts});
        //    return ((LeadConvertResult[])(results[0]));
        //}

        ///// <remarks/>
        //[SoapHeaderAttribute("SessionHeaderValue")]
        //[SoapHeaderAttribute("CallOptionsValue")]
        //[SoapHeaderAttribute("LimitInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
        //[SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
        //[return: XmlElementAttribute("result")]
        //public InvalidateSessionsResult[] invalidateSessions([XmlElementAttribute("sessionIds")] string[] sessionIds)
        //{
        //    object[] results = this.Invoke("invalidateSessions", new object[] {
        //                sessionIds});
        //    return ((InvalidateSessionsResult[])(results[0]));
        //}

        ///// <remarks/>
        //[SoapHeaderAttribute("SessionHeaderValue")]
        //[SoapHeaderAttribute("CallOptionsValue")]
        //[SoapHeaderAttribute("LimitInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
        //[SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
        //[return: XmlElementAttribute("result")]
        //public GetDeletedResult getDeleted(string sObjectType, System.DateTime startDate, System.DateTime endDate)
        //{
        //    object[] results = this.Invoke("getDeleted", new object[] {
        //                sObjectType,
        //                startDate,
        //                endDate});
        //    return ((GetDeletedResult)(results[0]));
        //}

        ///// <remarks/>
        //[SoapHeaderAttribute("SessionHeaderValue")]
        //[SoapHeaderAttribute("CallOptionsValue")]
        //[SoapHeaderAttribute("LimitInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
        //[SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
        //[return: XmlElementAttribute("result")]
        //public GetUpdatedResult getUpdated(string sObjectType, System.DateTime startDate, System.DateTime endDate)
        //{
        //    object[] results = this.Invoke("getUpdated", new object[] {
        //                sObjectType,
        //                startDate,
        //                endDate});
        //    return ((GetUpdatedResult)(results[0]));
        //}

        ///// <remarks/>
        //[SoapHeaderAttribute("CallOptionsValue")]
        //[SoapHeaderAttribute("SessionHeaderValue")]
        //[SoapHeaderAttribute("LimitInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
        //[SoapHeaderAttribute("QueryOptionsValue")]
        //[SoapHeaderAttribute("MruHeaderValue")]
        //[SoapHeaderAttribute("PackageVersionHeaderValue")]
        //[SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
        //[return: XmlElementAttribute("result")]
        //public QueryResult query(string queryString)
        //{
        //    object[] results = this.Invoke("query", new object[] {
        //                queryString});
        //    return ((QueryResult)(results[0]));
        //}

        ///// <remarks/>
        //[SoapHeaderAttribute("SessionHeaderValue")]
        //[SoapHeaderAttribute("QueryOptionsValue")]
        //[SoapHeaderAttribute("CallOptionsValue")]
        //[SoapHeaderAttribute("LimitInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
        //[SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
        //[return: XmlElementAttribute("result")]
        //public QueryResult queryAll(string queryString)
        //{
        //    object[] results = this.Invoke("queryAll", new object[] {
        //                queryString});
        //    return ((QueryResult)(results[0]));
        //}

        ///// <remarks/>
        //[SoapHeaderAttribute("SessionHeaderValue")]
        //[SoapHeaderAttribute("QueryOptionsValue")]
        //[SoapHeaderAttribute("CallOptionsValue")]
        //[SoapHeaderAttribute("LimitInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
        //[SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
        //[return: XmlElementAttribute("result")]
        //public QueryResult queryMore(string queryLocator)
        //{
        //    object[] results = this.Invoke("queryMore", new object[] {
        //                queryLocator});
        //    return ((QueryResult)(results[0]));
        //}

        ///// <remarks/>
        //[SoapHeaderAttribute("SessionHeaderValue")]
        //[SoapHeaderAttribute("LimitInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
        //[SoapHeaderAttribute("CallOptionsValue")]
        //[SoapHeaderAttribute("PackageVersionHeaderValue")]
        //[SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
        //[return: XmlElementAttribute("result")]
        //public SearchResult search(string searchString)
        //{
        //    object[] results = this.Invoke("search", new object[] {
        //                searchString});
        //    return ((SearchResult)(results[0]));
        //}

        ///// <remarks/>
        //[SoapHeaderAttribute("SessionHeaderValue")]
        //[SoapHeaderAttribute("CallOptionsValue")]
        //[SoapHeaderAttribute("LimitInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
        //[SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
        //[return: XmlElementAttribute("result")]
        //public GetServerTimestampResult getServerTimestamp()
        //{
        //    object[] results = this.Invoke("getServerTimestamp", new object[0]);
        //    return ((GetServerTimestampResult)(results[0]));
        //}

        ///// <remarks/>
        //[SoapHeaderAttribute("SessionHeaderValue")]
        //[SoapHeaderAttribute("CallOptionsValue")]
        //[SoapHeaderAttribute("LimitInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
        //[SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
        //[return: XmlElementAttribute("result")]
        //public SetPasswordResult setPassword(string userId, string password)
        //{
        //    object[] results = this.Invoke("setPassword", new object[] {
        //                userId,
        //                password});
        //    return ((SetPasswordResult)(results[0]));
        //}

        ///// <remarks/>
        //[SoapHeaderAttribute("SessionHeaderValue")]
        //[SoapHeaderAttribute("LimitInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
        //[SoapHeaderAttribute("CallOptionsValue")]
        //[SoapHeaderAttribute("EmailHeaderValue")]
        //[SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
        //[return: XmlElementAttribute("result")]
        //public ResetPasswordResult resetPassword(string userId)
        //{
        //    object[] results = this.Invoke("resetPassword", new object[] {
        //                userId});
        //    return ((ResetPasswordResult)(results[0]));
        //}

        ///// <remarks/>
        //[SoapHeaderAttribute("SessionHeaderValue")]
        //[SoapHeaderAttribute("CallOptionsValue")]
        //[SoapHeaderAttribute("LimitInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
        //[SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
        //[return: XmlElementAttribute("result")]
        //public GetUserInfoResult getUserInfo()
        //{
        //    object[] results = this.Invoke("getUserInfo", new object[0]);
        //    return ((GetUserInfoResult)(results[0]));
        //}

        ///// <remarks/>
        //[SoapHeaderAttribute("SessionHeaderValue")]
        //[SoapHeaderAttribute("CallOptionsValue")]
        //[SoapHeaderAttribute("LimitInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
        //[SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
        //[return: XmlElementAttribute("result")]
        //public SendEmailResult[] sendEmailMessage([XmlElementAttribute("ids")] string[] ids)
        //{
        //    object[] results = this.Invoke("sendEmailMessage", new object[] {
        //                ids});
        //    return ((SendEmailResult[])(results[0]));
        //}

        ///// <remarks/>
        //[SoapHeaderAttribute("SessionHeaderValue")]
        //[SoapHeaderAttribute("CallOptionsValue")]
        //[SoapHeaderAttribute("LimitInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
        //[SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
        //[return: XmlElementAttribute("result")]
        //public SendEmailResult[] sendEmail([XmlElementAttribute("messages")] Email[] messages)
        //{
        //    object[] results = this.Invoke("sendEmail", new object[] {
        //                messages});
        //    return ((SendEmailResult[])(results[0]));
        //}

        ///// <remarks/>
        //[SoapHeaderAttribute("SessionHeaderValue")]
        //[SoapHeaderAttribute("CallOptionsValue")]
        //[SoapHeaderAttribute("LimitInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
        //[SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
        //[return: XmlElementAttribute("result")]
        //public RenderEmailTemplateResult[] renderEmailTemplate([XmlElementAttribute("renderRequests")] RenderEmailTemplateRequest[] renderRequests)
        //{
        //    object[] results = this.Invoke("renderEmailTemplate", new object[] {
        //                renderRequests});
        //    return ((RenderEmailTemplateResult[])(results[0]));
        //}

        ///// <remarks/>
        //[SoapHeaderAttribute("SessionHeaderValue")]
        //[SoapHeaderAttribute("OwnerChangeOptionsValue")]
        //[SoapHeaderAttribute("StreamingEnabledHeaderValue")]
        //[SoapHeaderAttribute("CallOptionsValue")]
        //[SoapHeaderAttribute("AssignmentRuleHeaderValue")]
        //[SoapHeaderAttribute("LimitInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
        //[SoapHeaderAttribute("LocaleOptionsValue")]
        //[SoapHeaderAttribute("AllOrNoneHeaderValue")]
        //[SoapHeaderAttribute("EmailHeaderValue")]
        //[SoapHeaderAttribute("DisableFeedTrackingHeaderValue")]
        //[SoapHeaderAttribute("AllowFieldTruncationHeaderValue")]
        //[SoapHeaderAttribute("DebuggingHeaderValue")]
        //[SoapHeaderAttribute("MruHeaderValue")]
        //[SoapHeaderAttribute("PackageVersionHeaderValue")]
        //[SoapHeaderAttribute("DuplicateRuleHeaderValue")]
        //[SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
        //[return: XmlElementAttribute("result")]
        //public PerformQuickActionResult[] performQuickActions([XmlElementAttribute("quickActions")] PerformQuickActionRequest[] quickActions)
        //{
        //    object[] results = this.Invoke("performQuickActions", new object[] {
        //                quickActions});
        //    return ((PerformQuickActionResult[])(results[0]));
        //}

        ///// <remarks/>
        //[SoapHeaderAttribute("SessionHeaderValue")]
        //[SoapHeaderAttribute("LimitInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
        //[SoapHeaderAttribute("CallOptionsValue")]
        //[SoapHeaderAttribute("PackageVersionHeaderValue")]
        //[SoapHeaderAttribute("LocaleOptionsValue")]
        //[SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
        //[return: XmlElementAttribute("result")]
        //public DescribeQuickActionResult[] describeQuickActions([XmlElementAttribute("quickActions")] string[] quickActions)
        //{
        //    object[] results = this.Invoke("describeQuickActions", new object[] {
        //                quickActions});
        //    return ((DescribeQuickActionResult[])(results[0]));
        //}

        ///// <remarks/>
        //[SoapHeaderAttribute("SessionHeaderValue")]
        //[SoapHeaderAttribute("LimitInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
        //[SoapHeaderAttribute("CallOptionsValue")]
        //[SoapHeaderAttribute("PackageVersionHeaderValue")]
        //[SoapHeaderAttribute("LocaleOptionsValue")]
        //[SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
        //[return: XmlElementAttribute("result")]
        //public DescribeAvailableQuickActionResult[] describeAvailableQuickActions([XmlElementAttribute(IsNullable = true)] string contextType)
        //{
        //    object[] results = this.Invoke("describeAvailableQuickActions", new object[] {
        //                contextType});
        //    return ((DescribeAvailableQuickActionResult[])(results[0]));
        //}

        ///// <remarks/>
        //[SoapHeaderAttribute("SessionHeaderValue")]
        //[SoapHeaderAttribute("LimitInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
        //[SoapHeaderAttribute("CallOptionsValue")]
        //[SoapHeaderAttribute("PackageVersionHeaderValue")]
        //[SoapHeaderAttribute("LocaleOptionsValue")]
        //[SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
        //[return: XmlElementAttribute("result")]
        //public QuickActionTemplateResult[] retrieveQuickActionTemplates([XmlElementAttribute("quickActionNames")] string[] quickActionNames, [XmlElementAttribute(IsNullable = true)] string contextId)
        //{
        //    object[] results = this.Invoke("retrieveQuickActionTemplates", new object[] {
        //                quickActionNames,
        //                contextId});
        //    return ((QuickActionTemplateResult[])(results[0]));
        //}

        ///// <remarks/>
        //[SoapHeaderAttribute("SessionHeaderValue")]
        //[SoapHeaderAttribute("LimitInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
        //[SoapHeaderAttribute("CallOptionsValue")]
        //[SoapHeaderAttribute("PackageVersionHeaderValue")]
        //[SoapHeaderAttribute("LocaleOptionsValue")]
        //[SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
        //[return: XmlElementAttribute("result", IsNullable = true)]
        //public DescribeNounResult[] describeNouns([XmlElementAttribute("nouns")] string[] nouns, bool onlyRenamed, bool includeFields)
        //{
        //    object[] results = this.Invoke("describeNouns", new object[] {
        //                nouns,
        //                onlyRenamed,
        //                includeFields});
        //    return ((DescribeNounResult[])(results[0]));
        //}
        //#endregion

        //#region Async Calls
        ///// <remarks/>
        //public void DescribeSObjectAsync(string sObjectType)
        //{
        //    this.DescribeSObjectAsync(sObjectType, null);
        //}
        ///// <remarks/>
        //public void DescribeSObjectAsync(string sObjectType, object userState)
        //{
        //    if ((this.describeSObjectOperationCompleted == null))
        //    {
        //        this.describeSObjectOperationCompleted = new SendOrPostCallback(this.OndescribeSObjectOperationCompleted);
        //    }
        //    this.InvokeAsync("describeSObject", new object[] {
        //                sObjectType}, this.describeSObjectOperationCompleted, userState);
        //}
        //private void OndescribeSObjectOperationCompleted(object arg)
        //{
        //    if ((this.describeSObjectCompleted != null))
        //    {
        //        InvokeCompletedEventArgs invokeArgs = ((InvokeCompletedEventArgs)(arg));
        //        this.describeSObjectCompleted(this, new describeSObjectCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        //    }
        //}


        ///// <remarks/>
        //public void DescribeSObjectsAsync(string[] sObjectType)
        //{
        //    this.DescribeSObjectsAsync(sObjectType, null);
        //}
        ///// <remarks/>
        //public void DescribeSObjectsAsync(string[] sObjectType, object userState)
        //{
        //    if ((this.describeSObjectsOperationCompleted == null))
        //    {
        //        this.describeSObjectsOperationCompleted = new SendOrPostCallback(this.OndescribeSObjectsOperationCompleted);
        //    }
        //    this.InvokeAsync("describeSObjects", new object[] {
        //                sObjectType}, this.describeSObjectsOperationCompleted, userState);
        //}
        //private void OndescribeSObjectsOperationCompleted(object arg)
        //{
        //    if ((this.describeSObjectsCompleted != null))
        //    {
        //        InvokeCompletedEventArgs invokeArgs = ((InvokeCompletedEventArgs)(arg));
        //        this.describeSObjectsCompleted(this, new describeSObjectsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        //    }
        //}


        ///// <remarks/>
        //public void DescribeGlobalAsync()
        //{
        //    this.DescribeGlobalAsync(null);
        //}
        ///// <remarks/>
        //public void DescribeGlobalAsync(object userState)
        //{
        //    if ((this.describeGlobalOperationCompleted == null))
        //    {
        //        this.describeGlobalOperationCompleted = new SendOrPostCallback(this.OndescribeGlobalOperationCompleted);
        //    }
        //    this.InvokeAsync("describeGlobal", new object[0], this.describeGlobalOperationCompleted, userState);
        //}
        //private void OndescribeGlobalOperationCompleted(object arg)
        //{
        //    if ((this.describeGlobalCompleted != null))
        //    {
        //        InvokeCompletedEventArgs invokeArgs = ((InvokeCompletedEventArgs)(arg));
        //        this.describeGlobalCompleted(this, new describeGlobalCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        //    }
        //}


        ///// <remarks/>
        //public void DescribeDataCategoryGroupsAsync(string[] sObjectType)
        //{
        //    this.DescribeDataCategoryGroupsAsync(sObjectType, null);
        //}
        ///// <remarks/>
        //public void DescribeDataCategoryGroupsAsync(string[] sObjectType, object userState)
        //{
        //    if ((this.describeDataCategoryGroupsOperationCompleted == null))
        //    {
        //        this.describeDataCategoryGroupsOperationCompleted = new SendOrPostCallback(this.OndescribeDataCategoryGroupsOperationCompleted);
        //    }
        //    this.InvokeAsync("describeDataCategoryGroups", new object[] {
        //                sObjectType}, this.describeDataCategoryGroupsOperationCompleted, userState);
        //}
        //private void OndescribeDataCategoryGroupsOperationCompleted(object arg)
        //{
        //    if ((this.describeDataCategoryGroupsCompleted != null))
        //    {
        //        InvokeCompletedEventArgs invokeArgs = ((InvokeCompletedEventArgs)(arg));
        //        this.describeDataCategoryGroupsCompleted(this, new describeDataCategoryGroupsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        //    }
        //}


        ///// <remarks/>
        //public void DescribeDataCategoryGroupStructuresAsync(DataCategoryGroupSobjectTypePair[] pairs, bool topCategoriesOnly)
        //{
        //    this.DescribeDataCategoryGroupStructuresAsync(pairs, topCategoriesOnly, null);
        //}
        ///// <remarks/>
        //public void DescribeDataCategoryGroupStructuresAsync(DataCategoryGroupSobjectTypePair[] pairs, bool topCategoriesOnly, object userState)
        //{
        //    if ((this.describeDataCategoryGroupStructuresOperationCompleted == null))
        //    {
        //        this.describeDataCategoryGroupStructuresOperationCompleted = new SendOrPostCallback(this.OndescribeDataCategoryGroupStructuresOperationCompleted);
        //    }
        //    this.InvokeAsync("describeDataCategoryGroupStructures", new object[] {
        //                pairs,
        //                topCategoriesOnly}, this.describeDataCategoryGroupStructuresOperationCompleted, userState);
        //}
        //private void OndescribeDataCategoryGroupStructuresOperationCompleted(object arg)
        //{
        //    if ((this.describeDataCategoryGroupStructuresCompleted != null))
        //    {
        //        InvokeCompletedEventArgs invokeArgs = ((InvokeCompletedEventArgs)(arg));
        //        this.describeDataCategoryGroupStructuresCompleted(this, new describeDataCategoryGroupStructuresCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        //    }
        //}


        ///// <remarks/>
        //public void DescribeKnowledgeSettingsAsync()
        //{
        //    this.DescribeKnowledgeSettingsAsync(null);
        //}
        ///// <remarks/>
        //public void DescribeKnowledgeSettingsAsync(object userState)
        //{
        //    if ((this.describeKnowledgeSettingsOperationCompleted == null))
        //    {
        //        this.describeKnowledgeSettingsOperationCompleted = new SendOrPostCallback(this.OndescribeKnowledgeSettingsOperationCompleted);
        //    }
        //    this.InvokeAsync("describeKnowledgeSettings", new object[0], this.describeKnowledgeSettingsOperationCompleted, userState);
        //}
        //private void OndescribeKnowledgeSettingsOperationCompleted(object arg)
        //{
        //    if ((this.describeKnowledgeSettingsCompleted != null))
        //    {
        //        InvokeCompletedEventArgs invokeArgs = ((InvokeCompletedEventArgs)(arg));
        //        this.describeKnowledgeSettingsCompleted(this, new describeKnowledgeSettingsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        //    }
        //}


        ///// <remarks/>
        //public void DescribeFlexiPagesAsync(string[] flexiPages, FlexipageContext[] contexts)
        //{
        //    this.DescribeFlexiPagesAsync(flexiPages, contexts, null);
        //}
        ///// <remarks/>
        //public void DescribeFlexiPagesAsync(string[] flexiPages, FlexipageContext[] contexts, object userState)
        //{
        //    if ((this.describeFlexiPagesOperationCompleted == null))
        //    {
        //        this.describeFlexiPagesOperationCompleted = new SendOrPostCallback(this.OndescribeFlexiPagesOperationCompleted);
        //    }
        //    this.InvokeAsync("describeFlexiPages", new object[] {
        //                flexiPages,
        //                contexts}, this.describeFlexiPagesOperationCompleted, userState);
        //}
        //private void OndescribeFlexiPagesOperationCompleted(object arg)
        //{
        //    if ((this.describeFlexiPagesCompleted != null))
        //    {
        //        InvokeCompletedEventArgs invokeArgs = ((InvokeCompletedEventArgs)(arg));
        //        this.describeFlexiPagesCompleted(this, new describeFlexiPagesCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        //    }
        //}


        ///// <remarks/>
        //public void DescribeAppMenuAsync(AppMenuType appMenuType, string networkId)
        //{
        //    this.DescribeAppMenuAsync(appMenuType, networkId, null);
        //}
        ///// <remarks/>
        //public void DescribeAppMenuAsync(AppMenuType appMenuType, string networkId, object userState)
        //{
        //    if ((this.describeAppMenuOperationCompleted == null))
        //    {
        //        this.describeAppMenuOperationCompleted = new SendOrPostCallback(this.OndescribeAppMenuOperationCompleted);
        //    }
        //    this.InvokeAsync("describeAppMenu", new object[] {
        //                appMenuType,
        //                networkId}, this.describeAppMenuOperationCompleted, userState);
        //}
        //private void OndescribeAppMenuOperationCompleted(object arg)
        //{
        //    if ((this.describeAppMenuCompleted != null))
        //    {
        //        InvokeCompletedEventArgs invokeArgs = ((InvokeCompletedEventArgs)(arg));
        //        this.describeAppMenuCompleted(this, new describeAppMenuCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        //    }
        //}


        ///// <remarks/>
        //public void DescribeGlobalThemeAsync()
        //{
        //    this.DescribeGlobalThemeAsync(null);
        //}
        ///// <remarks/>
        //public void DescribeGlobalThemeAsync(object userState)
        //{
        //    if ((this.describeGlobalThemeOperationCompleted == null))
        //    {
        //        this.describeGlobalThemeOperationCompleted = new SendOrPostCallback(this.OndescribeGlobalThemeOperationCompleted);
        //    }
        //    this.InvokeAsync("describeGlobalTheme", new object[0], this.describeGlobalThemeOperationCompleted, userState);
        //}
        //private void OndescribeGlobalThemeOperationCompleted(object arg)
        //{
        //    if ((this.describeGlobalThemeCompleted != null))
        //    {
        //        InvokeCompletedEventArgs invokeArgs = ((InvokeCompletedEventArgs)(arg));
        //        this.describeGlobalThemeCompleted(this, new describeGlobalThemeCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        //    }
        //}


        ///// <remarks/>
        //public void DescribeThemeAsync(string[] sobjectType)
        //{
        //    this.DescribeThemeAsync(sobjectType, null);
        //}
        ///// <remarks/>
        //public void DescribeThemeAsync(string[] sobjectType, object userState)
        //{
        //    if ((this.describeThemeOperationCompleted == null))
        //    {
        //        this.describeThemeOperationCompleted = new SendOrPostCallback(this.OndescribeThemeOperationCompleted);
        //    }
        //    this.InvokeAsync("describeTheme", new object[] {
        //                sobjectType}, this.describeThemeOperationCompleted, userState);
        //}
        //private void OndescribeThemeOperationCompleted(object arg)
        //{
        //    if ((this.describeThemeCompleted != null))
        //    {
        //        InvokeCompletedEventArgs invokeArgs = ((InvokeCompletedEventArgs)(arg));
        //        this.describeThemeCompleted(this, new describeThemeCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        //    }
        //}


        ///// <remarks/>
        //public void DescribeLayoutAsync(string sObjectType, string layoutName, string[] recordTypeIds)
        //{
        //    this.DescribeLayoutAsync(sObjectType, layoutName, recordTypeIds, null);
        //}
        ///// <remarks/>
        //public void DescribeLayoutAsync(string sObjectType, string layoutName, string[] recordTypeIds, object userState)
        //{
        //    if ((this.describeLayoutOperationCompleted == null))
        //    {
        //        this.describeLayoutOperationCompleted = new SendOrPostCallback(this.OndescribeLayoutOperationCompleted);
        //    }
        //    this.InvokeAsync("describeLayout", new object[] {
        //                sObjectType,
        //                layoutName,
        //                recordTypeIds}, this.describeLayoutOperationCompleted, userState);
        //}
        //private void OndescribeLayoutOperationCompleted(object arg)
        //{
        //    if ((this.describeLayoutCompleted != null))
        //    {
        //        InvokeCompletedEventArgs invokeArgs = ((InvokeCompletedEventArgs)(arg));
        //        this.describeLayoutCompleted(this, new describeLayoutCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        //    }
        //}


        ///// <remarks/>
        //public void DescribeSoftphoneLayoutAsync()
        //{
        //    this.DescribeSoftphoneLayoutAsync(null);
        //}
        ///// <remarks/>
        //public void DescribeSoftphoneLayoutAsync(object userState)
        //{
        //    if ((this.describeSoftphoneLayoutOperationCompleted == null))
        //    {
        //        this.describeSoftphoneLayoutOperationCompleted = new SendOrPostCallback(this.OndescribeSoftphoneLayoutOperationCompleted);
        //    }
        //    this.InvokeAsync("describeSoftphoneLayout", new object[0], this.describeSoftphoneLayoutOperationCompleted, userState);
        //}
        //private void OndescribeSoftphoneLayoutOperationCompleted(object arg)
        //{
        //    if ((this.describeSoftphoneLayoutCompleted != null))
        //    {
        //        InvokeCompletedEventArgs invokeArgs = ((InvokeCompletedEventArgs)(arg));
        //        this.describeSoftphoneLayoutCompleted(this, new describeSoftphoneLayoutCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        //    }
        //}


        ///// <remarks/>
        //public void DescribeSearchLayoutsAsync(string[] sObjectType)
        //{
        //    this.DescribeSearchLayoutsAsync(sObjectType, null);
        //}
        ///// <remarks/>
        //public void DescribeSearchLayoutsAsync(string[] sObjectType, object userState)
        //{
        //    if ((this.describeSearchLayoutsOperationCompleted == null))
        //    {
        //        this.describeSearchLayoutsOperationCompleted = new SendOrPostCallback(this.OndescribeSearchLayoutsOperationCompleted);
        //    }
        //    this.InvokeAsync("describeSearchLayouts", new object[] {
        //                sObjectType}, this.describeSearchLayoutsOperationCompleted, userState);
        //}
        //private void OndescribeSearchLayoutsOperationCompleted(object arg)
        //{
        //    if ((this.describeSearchLayoutsCompleted != null))
        //    {
        //        InvokeCompletedEventArgs invokeArgs = ((InvokeCompletedEventArgs)(arg));
        //        this.describeSearchLayoutsCompleted(this, new describeSearchLayoutsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        //    }
        //}


        ///// <remarks/>
        //public void DescribeSearchScopeOrderAsync()
        //{
        //    this.DescribeSearchScopeOrderAsync(null);
        //}
        ///// <remarks/>
        //public void DescribeSearchScopeOrderAsync(object userState)
        //{
        //    if ((this.describeSearchScopeOrderOperationCompleted == null))
        //    {
        //        this.describeSearchScopeOrderOperationCompleted = new SendOrPostCallback(this.OndescribeSearchScopeOrderOperationCompleted);
        //    }
        //    this.InvokeAsync("describeSearchScopeOrder", new object[0], this.describeSearchScopeOrderOperationCompleted, userState);
        //}
        //private void OndescribeSearchScopeOrderOperationCompleted(object arg)
        //{
        //    if ((this.describeSearchScopeOrderCompleted != null))
        //    {
        //        InvokeCompletedEventArgs invokeArgs = ((InvokeCompletedEventArgs)(arg));
        //        this.describeSearchScopeOrderCompleted(this, new describeSearchScopeOrderCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        //    }
        //}


        ///// <remarks/>
        //public void DescribeCompactLayoutsAsync(string sObjectType, string[] recordTypeIds)
        //{
        //    this.DescribeCompactLayoutsAsync(sObjectType, recordTypeIds, null);
        //}
        ///// <remarks/>
        //public void DescribeCompactLayoutsAsync(string sObjectType, string[] recordTypeIds, object userState)
        //{
        //    if ((this.describeCompactLayoutsOperationCompleted == null))
        //    {
        //        this.describeCompactLayoutsOperationCompleted = new SendOrPostCallback(this.OndescribeCompactLayoutsOperationCompleted);
        //    }
        //    this.InvokeAsync("describeCompactLayouts", new object[] {
        //                sObjectType,
        //                recordTypeIds}, this.describeCompactLayoutsOperationCompleted, userState);
        //}
        //private void OndescribeCompactLayoutsOperationCompleted(object arg)
        //{
        //    if ((this.describeCompactLayoutsCompleted != null))
        //    {
        //        InvokeCompletedEventArgs invokeArgs = ((InvokeCompletedEventArgs)(arg));
        //        this.describeCompactLayoutsCompleted(this, new describeCompactLayoutsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        //    }
        //}


        ///// <remarks/>
        //public void DescribePathAssistantsAsync(string sObjectType, string picklistValue, string[] recordTypeIds)
        //{
        //    this.DescribePathAssistantsAsync(sObjectType, picklistValue, recordTypeIds, null);
        //}
        ///// <remarks/>
        //public void DescribePathAssistantsAsync(string sObjectType, string picklistValue, string[] recordTypeIds, object userState)
        //{
        //    if ((this.describePathAssistantsOperationCompleted == null))
        //    {
        //        this.describePathAssistantsOperationCompleted = new SendOrPostCallback(this.OndescribePathAssistantsOperationCompleted);
        //    }
        //    this.InvokeAsync("describePathAssistants", new object[] {
        //                sObjectType,
        //                picklistValue,
        //                recordTypeIds}, this.describePathAssistantsOperationCompleted, userState);
        //}
        //private void OndescribePathAssistantsOperationCompleted(object arg)
        //{
        //    if ((this.describePathAssistantsCompleted != null))
        //    {
        //        InvokeCompletedEventArgs invokeArgs = ((InvokeCompletedEventArgs)(arg));
        //        this.describePathAssistantsCompleted(this, new describePathAssistantsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        //    }
        //}


        ///// <remarks/>
        //public void DescribeApprovalLayoutAsync(string sObjectType, string[] approvalProcessNames)
        //{
        //    this.DescribeApprovalLayoutAsync(sObjectType, approvalProcessNames, null);
        //}
        ///// <remarks/>
        //public void DescribeApprovalLayoutAsync(string sObjectType, string[] approvalProcessNames, object userState)
        //{
        //    if ((this.describeApprovalLayoutOperationCompleted == null))
        //    {
        //        this.describeApprovalLayoutOperationCompleted = new SendOrPostCallback(this.OndescribeApprovalLayoutOperationCompleted);
        //    }
        //    this.InvokeAsync("describeApprovalLayout", new object[] {
        //                sObjectType,
        //                approvalProcessNames}, this.describeApprovalLayoutOperationCompleted, userState);
        //}
        //private void OndescribeApprovalLayoutOperationCompleted(object arg)
        //{
        //    if ((this.describeApprovalLayoutCompleted != null))
        //    {
        //        InvokeCompletedEventArgs invokeArgs = ((InvokeCompletedEventArgs)(arg));
        //        this.describeApprovalLayoutCompleted(this, new describeApprovalLayoutCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        //    }
        //}


        ///// <remarks/>
        //public void DescribeSoqlListViewsAsync(DescribeSoqlListViewParams[] request)
        //{
        //    this.DescribeSoqlListViewsAsync(request, null);
        //}
        ///// <remarks/>
        //public void DescribeSoqlListViewsAsync(DescribeSoqlListViewParams[] request, object userState)
        //{
        //    if ((this.describeSoqlListViewsOperationCompleted == null))
        //    {
        //        this.describeSoqlListViewsOperationCompleted = new SendOrPostCallback(this.OndescribeSoqlListViewsOperationCompleted);
        //    }
        //    this.InvokeAsync("describeSoqlListViews", new object[] {
        //                request}, this.describeSoqlListViewsOperationCompleted, userState);
        //}
        //private void OndescribeSoqlListViewsOperationCompleted(object arg)
        //{
        //    if ((this.describeSoqlListViewsCompleted != null))
        //    {
        //        InvokeCompletedEventArgs invokeArgs = ((InvokeCompletedEventArgs)(arg));
        //        this.describeSoqlListViewsCompleted(this, new describeSoqlListViewsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        //    }
        //}


        ///// <remarks/>
        //public void DxecuteListViewAsync(ExecuteListViewRequest request)
        //{
        //    this.DxecuteListViewAsync(request, null);
        //}
        ///// <remarks/>
        //public void DxecuteListViewAsync(ExecuteListViewRequest request, object userState)
        //{
        //    if ((this.executeListViewOperationCompleted == null))
        //    {
        //        this.executeListViewOperationCompleted = new SendOrPostCallback(this.OnexecuteListViewOperationCompleted);
        //    }
        //    this.InvokeAsync("executeListView", new object[] {
        //                request}, this.executeListViewOperationCompleted, userState);
        //}
        //private void OnexecuteListViewOperationCompleted(object arg)
        //{
        //    if ((this.executeListViewCompleted != null))
        //    {
        //        InvokeCompletedEventArgs invokeArgs = ((InvokeCompletedEventArgs)(arg));
        //        this.executeListViewCompleted(this, new executeListViewCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        //    }
        //}


        ///// <remarks/>
        //public void DescribeSObjectListViewsAsync(string sObjectType, bool recentsOnly, listViewIsSoqlCompatible isSoqlCompatible, int limit, int offset)
        //{
        //    this.DescribeSObjectListViewsAsync(sObjectType, recentsOnly, isSoqlCompatible, limit, offset, null);
        //}
        ///// <remarks/>
        //public void DescribeSObjectListViewsAsync(string sObjectType, bool recentsOnly, listViewIsSoqlCompatible isSoqlCompatible, int limit, int offset, object userState)
        //{
        //    if ((this.describeSObjectListViewsOperationCompleted == null))
        //    {
        //        this.describeSObjectListViewsOperationCompleted = new SendOrPostCallback(this.OndescribeSObjectListViewsOperationCompleted);
        //    }
        //    this.InvokeAsync("describeSObjectListViews", new object[] {
        //                sObjectType,
        //                recentsOnly,
        //                isSoqlCompatible,
        //                limit,
        //                offset}, this.describeSObjectListViewsOperationCompleted, userState);
        //}
        //private void OndescribeSObjectListViewsOperationCompleted(object arg)
        //{
        //    if ((this.describeSObjectListViewsCompleted != null))
        //    {
        //        InvokeCompletedEventArgs invokeArgs = ((InvokeCompletedEventArgs)(arg));
        //        this.describeSObjectListViewsCompleted(this, new describeSObjectListViewsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        //    }
        //}


        ///// <remarks/>
        //public void DescribeTabsAsync()
        //{
        //    this.DescribeTabsAsync(null);
        //}
        ///// <remarks/>
        //public void DescribeTabsAsync(object userState)
        //{
        //    if ((this.describeTabsOperationCompleted == null))
        //    {
        //        this.describeTabsOperationCompleted = new SendOrPostCallback(this.OndescribeTabsOperationCompleted);
        //    }
        //    this.InvokeAsync("describeTabs", new object[0], this.describeTabsOperationCompleted, userState);
        //}
        //private void OndescribeTabsOperationCompleted(object arg)
        //{
        //    if ((this.describeTabsCompleted != null))
        //    {
        //        InvokeCompletedEventArgs invokeArgs = ((InvokeCompletedEventArgs)(arg));
        //        this.describeTabsCompleted(this, new describeTabsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        //    }
        //}


        ///// <remarks/>
        //public void DescribeAllTabsAsync()
        //{
        //    this.DescribeAllTabsAsync(null);
        //}
        ///// <remarks/>
        //public void DescribeAllTabsAsync(object userState)
        //{
        //    if ((this.describeAllTabsOperationCompleted == null))
        //    {
        //        this.describeAllTabsOperationCompleted = new SendOrPostCallback(this.OndescribeAllTabsOperationCompleted);
        //    }
        //    this.InvokeAsync("describeAllTabs", new object[0], this.describeAllTabsOperationCompleted, userState);
        //}
        //private void OndescribeAllTabsOperationCompleted(object arg)
        //{
        //    if ((this.describeAllTabsCompleted != null))
        //    {
        //        InvokeCompletedEventArgs invokeArgs = ((InvokeCompletedEventArgs)(arg));
        //        this.describeAllTabsCompleted(this, new describeAllTabsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        //    }
        //}


        ///// <remarks/>
        //public void DescribePrimaryCompactLayoutsAsync(string[] sObjectTypes)
        //{
        //    this.DescribePrimaryCompactLayoutsAsync(sObjectTypes, null);
        //}
        ///// <remarks/>
        //public void DescribePrimaryCompactLayoutsAsync(string[] sObjectTypes, object userState)
        //{
        //    if ((this.describePrimaryCompactLayoutsOperationCompleted == null))
        //    {
        //        this.describePrimaryCompactLayoutsOperationCompleted = new SendOrPostCallback(this.OndescribePrimaryCompactLayoutsOperationCompleted);
        //    }
        //    this.InvokeAsync("describePrimaryCompactLayouts", new object[] {
        //                sObjectTypes}, this.describePrimaryCompactLayoutsOperationCompleted, userState);
        //}
        //private void OndescribePrimaryCompactLayoutsOperationCompleted(object arg)
        //{
        //    if ((this.describePrimaryCompactLayoutsCompleted != null))
        //    {
        //        InvokeCompletedEventArgs invokeArgs = ((InvokeCompletedEventArgs)(arg));
        //        this.describePrimaryCompactLayoutsCompleted(this, new describePrimaryCompactLayoutsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        //    }
        //}


        ///// <remarks/>
        //public void CreateAsync(sObject[] sObjects)
        //{
        //    this.CreateAsync(sObjects, null);
        //}
        ///// <remarks/>
        //public void CreateAsync(sObject[] sObjects, object userState)
        //{
        //    if ((this.createOperationCompleted == null))
        //    {
        //        this.createOperationCompleted = new SendOrPostCallback(this.OncreateOperationCompleted);
        //    }
        //    this.InvokeAsync("create", new object[] {
        //                sObjects}, this.createOperationCompleted, userState);
        //}
        //private void OncreateOperationCompleted(object arg)
        //{
        //    if ((this.createCompleted != null))
        //    {
        //        InvokeCompletedEventArgs invokeArgs = ((InvokeCompletedEventArgs)(arg));
        //        this.createCompleted(this, new createCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        //    }
        //}


        ///// <remarks/>
        //public void UpdateAsync(sObject[] sObjects)
        //{
        //    this.UpdateAsync(sObjects, null);
        //}
        ///// <remarks/>
        //public void UpdateAsync(sObject[] sObjects, object userState)
        //{
        //    if ((this.updateOperationCompleted == null))
        //    {
        //        this.updateOperationCompleted = new SendOrPostCallback(this.OnupdateOperationCompleted);
        //    }
        //    this.InvokeAsync("update", new object[] {
        //                sObjects}, this.updateOperationCompleted, userState);
        //}
        //private void OnupdateOperationCompleted(object arg)
        //{
        //    if ((this.updateCompleted != null))
        //    {
        //        InvokeCompletedEventArgs invokeArgs = ((InvokeCompletedEventArgs)(arg));
        //        this.updateCompleted(this, new updateCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        //    }
        //}


        ///// <remarks/>
        //public void UpsertAsync(string externalIDFieldName, sObject[] sObjects)
        //{
        //    this.UpsertAsync(externalIDFieldName, sObjects, null);
        //}
        ///// <remarks/>
        //public void UpsertAsync(string externalIDFieldName, sObject[] sObjects, object userState)
        //{
        //    if ((this.upsertOperationCompleted == null))
        //    {
        //        this.upsertOperationCompleted = new SendOrPostCallback(this.OnupsertOperationCompleted);
        //    }
        //    this.InvokeAsync("upsert", new object[] {
        //                externalIDFieldName,
        //                sObjects}, this.upsertOperationCompleted, userState);
        //}
        //private void OnupsertOperationCompleted(object arg)
        //{
        //    if ((this.upsertCompleted != null))
        //    {
        //        InvokeCompletedEventArgs invokeArgs = ((InvokeCompletedEventArgs)(arg));
        //        this.upsertCompleted(this, new upsertCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        //    }
        //}


        ///// <remarks/>
        //public void MergeAsync(MergeRequest[] request)
        //{
        //    this.MergeAsync(request, null);
        //}
        ///// <remarks/>
        //public void MergeAsync(MergeRequest[] request, object userState)
        //{
        //    if ((this.mergeOperationCompleted == null))
        //    {
        //        this.mergeOperationCompleted = new SendOrPostCallback(this.OnmergeOperationCompleted);
        //    }
        //    this.InvokeAsync("merge", new object[] {
        //                request}, this.mergeOperationCompleted, userState);
        //}
        //private void OnmergeOperationCompleted(object arg)
        //{
        //    if ((this.mergeCompleted != null))
        //    {
        //        InvokeCompletedEventArgs invokeArgs = ((InvokeCompletedEventArgs)(arg));
        //        this.mergeCompleted(this, new mergeCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        //    }
        //}


        ///// <remarks/>
        //public void DeleteAsync(string[] ids)
        //{
        //    this.DeleteAsync(ids, null);
        //}
        ///// <remarks/>
        //public void DeleteAsync(string[] ids, object userState)
        //{
        //    if ((this.deleteOperationCompleted == null))
        //    {
        //        this.deleteOperationCompleted = new SendOrPostCallback(this.OndeleteOperationCompleted);
        //    }
        //    this.InvokeAsync("delete", new object[] {
        //                ids}, this.deleteOperationCompleted, userState);
        //}
        //private void OndeleteOperationCompleted(object arg)
        //{
        //    if ((this.deleteCompleted != null))
        //    {
        //        InvokeCompletedEventArgs invokeArgs = ((InvokeCompletedEventArgs)(arg));
        //        this.deleteCompleted(this, new deleteCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        //    }
        //}


        ///// <remarks/>
        //public void UndeleteAsync(string[] ids)
        //{
        //    this.UndeleteAsync(ids, null);
        //}
        ///// <remarks/>
        //public void UndeleteAsync(string[] ids, object userState)
        //{
        //    if ((this.undeleteOperationCompleted == null))
        //    {
        //        this.undeleteOperationCompleted = new SendOrPostCallback(this.OnundeleteOperationCompleted);
        //    }
        //    this.InvokeAsync("undelete", new object[] {
        //                ids}, this.undeleteOperationCompleted, userState);
        //}
        //private void OnundeleteOperationCompleted(object arg)
        //{
        //    if ((this.undeleteCompleted != null))
        //    {
        //        InvokeCompletedEventArgs invokeArgs = ((InvokeCompletedEventArgs)(arg));
        //        this.undeleteCompleted(this, new undeleteCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        //    }
        //}


        ///// <remarks/>
        //public void EmptyRecycleBinAsync(string[] ids)
        //{
        //    this.EmptyRecycleBinAsync(ids, null);
        //}
        ///// <remarks/>
        //public void EmptyRecycleBinAsync(string[] ids, object userState)
        //{
        //    if ((this.emptyRecycleBinOperationCompleted == null))
        //    {
        //        this.emptyRecycleBinOperationCompleted = new SendOrPostCallback(this.OnemptyRecycleBinOperationCompleted);
        //    }
        //    this.InvokeAsync("emptyRecycleBin", new object[] {
        //                ids}, this.emptyRecycleBinOperationCompleted, userState);
        //}
        //private void OnemptyRecycleBinOperationCompleted(object arg)
        //{
        //    if ((this.emptyRecycleBinCompleted != null))
        //    {
        //        InvokeCompletedEventArgs invokeArgs = ((InvokeCompletedEventArgs)(arg));
        //        this.emptyRecycleBinCompleted(this, new emptyRecycleBinCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        //    }
        //}


        ///// <remarks/>
        //public void RetrieveAsync(string fieldList, string sObjectType, string[] ids)
        //{
        //    this.RetrieveAsync(fieldList, sObjectType, ids, null);
        //}
        ///// <remarks/>
        //public void RetrieveAsync(string fieldList, string sObjectType, string[] ids, object userState)
        //{
        //    if ((this.retrieveOperationCompleted == null))
        //    {
        //        this.retrieveOperationCompleted = new SendOrPostCallback(this.OnretrieveOperationCompleted);
        //    }
        //    this.InvokeAsync("retrieve", new object[] {
        //                fieldList,
        //                sObjectType,
        //                ids}, this.retrieveOperationCompleted, userState);
        //}
        //private void OnretrieveOperationCompleted(object arg)
        //{
        //    if ((this.retrieveCompleted != null))
        //    {
        //        InvokeCompletedEventArgs invokeArgs = ((InvokeCompletedEventArgs)(arg));
        //        this.retrieveCompleted(this, new retrieveCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        //    }
        //}


        ///// <remarks/>
        //public void ProcessAsync(ProcessRequest[] actions)
        //{
        //    this.ProcessAsync(actions, null);
        //}
        ///// <remarks/>
        //public void ProcessAsync(ProcessRequest[] actions, object userState)
        //{
        //    if ((this.processOperationCompleted == null))
        //    {
        //        this.processOperationCompleted = new SendOrPostCallback(this.OnprocessOperationCompleted);
        //    }
        //    this.InvokeAsync("process", new object[] {
        //                actions}, this.processOperationCompleted, userState);
        //}
        //private void OnprocessOperationCompleted(object arg)
        //{
        //    if ((this.processCompleted != null))
        //    {
        //        InvokeCompletedEventArgs invokeArgs = ((InvokeCompletedEventArgs)(arg));
        //        this.processCompleted(this, new processCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        //    }
        //}


        ///// <remarks/>
        //public void ConvertLeadAsync(LeadConvert[] leadConverts)
        //{
        //    this.ConvertLeadAsync(leadConverts, null);
        //}
        ///// <remarks/>
        //public void ConvertLeadAsync(LeadConvert[] leadConverts, object userState)
        //{
        //    if ((this.convertLeadOperationCompleted == null))
        //    {
        //        this.convertLeadOperationCompleted = new SendOrPostCallback(this.OnconvertLeadOperationCompleted);
        //    }
        //    this.InvokeAsync("convertLead", new object[] {
        //                leadConverts}, this.convertLeadOperationCompleted, userState);
        //}
        //private void OnconvertLeadOperationCompleted(object arg)
        //{
        //    if ((this.convertLeadCompleted != null))
        //    {
        //        InvokeCompletedEventArgs invokeArgs = ((InvokeCompletedEventArgs)(arg));
        //        this.convertLeadCompleted(this, new convertLeadCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        //    }
        //}


        ///// <remarks/>
        //public void InvalidateSessionsAsync(string[] sessionIds)
        //{
        //    this.InvalidateSessionsAsync(sessionIds, null);
        //}
        ///// <remarks/>
        //public void InvalidateSessionsAsync(string[] sessionIds, object userState)
        //{
        //    if ((this.invalidateSessionsOperationCompleted == null))
        //    {
        //        this.invalidateSessionsOperationCompleted = new SendOrPostCallback(this.OninvalidateSessionsOperationCompleted);
        //    }
        //    this.InvokeAsync("invalidateSessions", new object[] {
        //                sessionIds}, this.invalidateSessionsOperationCompleted, userState);
        //}
        //private void OninvalidateSessionsOperationCompleted(object arg)
        //{
        //    if ((this.invalidateSessionsCompleted != null))
        //    {
        //        InvokeCompletedEventArgs invokeArgs = ((InvokeCompletedEventArgs)(arg));
        //        this.invalidateSessionsCompleted(this, new invalidateSessionsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        //    }
        //}


        ///// <remarks/>
        //public void GetDeletedAsync(string sObjectType, System.DateTime startDate, System.DateTime endDate)
        //{
        //    this.GetDeletedAsync(sObjectType, startDate, endDate, null);
        //}
        ///// <remarks/>
        //public void GetDeletedAsync(string sObjectType, System.DateTime startDate, System.DateTime endDate, object userState)
        //{
        //    if ((this.getDeletedOperationCompleted == null))
        //    {
        //        this.getDeletedOperationCompleted = new SendOrPostCallback(this.OngetDeletedOperationCompleted);
        //    }
        //    this.InvokeAsync("getDeleted", new object[] {
        //                sObjectType,
        //                startDate,
        //                endDate}, this.getDeletedOperationCompleted, userState);
        //}
        //private void OngetDeletedOperationCompleted(object arg)
        //{
        //    if ((this.getDeletedCompleted != null))
        //    {
        //        InvokeCompletedEventArgs invokeArgs = ((InvokeCompletedEventArgs)(arg));
        //        this.getDeletedCompleted(this, new getDeletedCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        //    }
        //}


        ///// <remarks/>
        //public void GetUpdatedAsync(string sObjectType, System.DateTime startDate, System.DateTime endDate)
        //{
        //    this.GetUpdatedAsync(sObjectType, startDate, endDate, null);
        //}
        ///// <remarks/>
        //public void GetUpdatedAsync(string sObjectType, System.DateTime startDate, System.DateTime endDate, object userState)
        //{
        //    if ((this.getUpdatedOperationCompleted == null))
        //    {
        //        this.getUpdatedOperationCompleted = new SendOrPostCallback(this.OngetUpdatedOperationCompleted);
        //    }
        //    this.InvokeAsync("getUpdated", new object[] {
        //                sObjectType,
        //                startDate,
        //                endDate}, this.getUpdatedOperationCompleted, userState);
        //}
        //private void OngetUpdatedOperationCompleted(object arg)
        //{
        //    if ((this.getUpdatedCompleted != null))
        //    {
        //        InvokeCompletedEventArgs invokeArgs = ((InvokeCompletedEventArgs)(arg));
        //        this.getUpdatedCompleted(this, new getUpdatedCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        //    }
        //}


        ///// <remarks/>
        //public void QueryAsync(string queryString)
        //{
        //    this.QueryAsync(queryString, null);
        //}
        ///// <remarks/>
        //public void QueryAsync(string queryString, object userState)
        //{
        //    if ((this.queryOperationCompleted == null))
        //    {
        //        this.queryOperationCompleted = new SendOrPostCallback(this.OnqueryOperationCompleted);
        //    }
        //    this.InvokeAsync("query", new object[] {
        //                queryString}, this.queryOperationCompleted, userState);
        //}
        //private void OnqueryOperationCompleted(object arg)
        //{
        //    if ((this.queryCompleted != null))
        //    {
        //        InvokeCompletedEventArgs invokeArgs = ((InvokeCompletedEventArgs)(arg));
        //        this.queryCompleted(this, new queryCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        //    }
        //}


        ///// <remarks/>
        //public void QueryAllAsync(string queryString)
        //{
        //    this.QueryAllAsync(queryString, null);
        //}
        ///// <remarks/>
        //public void QueryAllAsync(string queryString, object userState)
        //{
        //    if ((this.queryAllOperationCompleted == null))
        //    {
        //        this.queryAllOperationCompleted = new SendOrPostCallback(this.OnqueryAllOperationCompleted);
        //    }
        //    this.InvokeAsync("queryAll", new object[] {
        //                queryString}, this.queryAllOperationCompleted, userState);
        //}
        //private void OnqueryAllOperationCompleted(object arg)
        //{
        //    if ((this.queryAllCompleted != null))
        //    {
        //        InvokeCompletedEventArgs invokeArgs = ((InvokeCompletedEventArgs)(arg));
        //        this.queryAllCompleted(this, new queryAllCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        //    }
        //}


        ///// <remarks/>
        //public void QueryMoreAsync(string queryLocator)
        //{
        //    this.QueryMoreAsync(queryLocator, null);
        //}
        ///// <remarks/>
        //public void QueryMoreAsync(string queryLocator, object userState)
        //{
        //    if ((this.queryMoreOperationCompleted == null))
        //    {
        //        this.queryMoreOperationCompleted = new SendOrPostCallback(this.OnqueryMoreOperationCompleted);
        //    }
        //    this.InvokeAsync("queryMore", new object[] {
        //                queryLocator}, this.queryMoreOperationCompleted, userState);
        //}
        //private void OnqueryMoreOperationCompleted(object arg)
        //{
        //    if ((this.queryMoreCompleted != null))
        //    {
        //        InvokeCompletedEventArgs invokeArgs = ((InvokeCompletedEventArgs)(arg));
        //        this.queryMoreCompleted(this, new queryMoreCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        //    }
        //}


        ///// <remarks/>
        //public void SearchAsync(string searchString)
        //{
        //    this.SearchAsync(searchString, null);
        //}
        ///// <remarks/>
        //public void SearchAsync(string searchString, object userState)
        //{
        //    if ((this.searchOperationCompleted == null))
        //    {
        //        this.searchOperationCompleted = new SendOrPostCallback(this.OnsearchOperationCompleted);
        //    }
        //    this.InvokeAsync("search", new object[] {
        //                searchString}, this.searchOperationCompleted, userState);
        //}
        //private void OnsearchOperationCompleted(object arg)
        //{
        //    if ((this.searchCompleted != null))
        //    {
        //        InvokeCompletedEventArgs invokeArgs = ((InvokeCompletedEventArgs)(arg));
        //        this.searchCompleted(this, new searchCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        //    }
        //}


        ///// <remarks/>
        //public void GetServerTimestampAsync()
        //{
        //    this.GetServerTimestampAsync(null);
        //}
        ///// <remarks/>
        //public void GetServerTimestampAsync(object userState)
        //{
        //    if ((this.getServerTimestampOperationCompleted == null))
        //    {
        //        this.getServerTimestampOperationCompleted = new SendOrPostCallback(this.OngetServerTimestampOperationCompleted);
        //    }
        //    this.InvokeAsync("getServerTimestamp", new object[0], this.getServerTimestampOperationCompleted, userState);
        //}
        //private void OngetServerTimestampOperationCompleted(object arg)
        //{
        //    if ((this.getServerTimestampCompleted != null))
        //    {
        //        InvokeCompletedEventArgs invokeArgs = ((InvokeCompletedEventArgs)(arg));
        //        this.getServerTimestampCompleted(this, new getServerTimestampCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        //    }
        //}


        ///// <remarks/>
        //public void SetPasswordAsync(string userId, string password)
        //{
        //    this.SetPasswordAsync(userId, password, null);
        //}
        ///// <remarks/>
        //public void SetPasswordAsync(string userId, string password, object userState)
        //{
        //    if ((this.setPasswordOperationCompleted == null))
        //    {
        //        this.setPasswordOperationCompleted = new SendOrPostCallback(this.OnsetPasswordOperationCompleted);
        //    }
        //    this.InvokeAsync("setPassword", new object[] {
        //                userId,
        //                password}, this.setPasswordOperationCompleted, userState);
        //}
        //private void OnsetPasswordOperationCompleted(object arg)
        //{
        //    if ((this.setPasswordCompleted != null))
        //    {
        //        InvokeCompletedEventArgs invokeArgs = ((InvokeCompletedEventArgs)(arg));
        //        this.setPasswordCompleted(this, new setPasswordCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        //    }
        //}


        ///// <remarks/>
        //public void ResetPasswordAsync(string userId)
        //{
        //    this.ResetPasswordAsync(userId, null);
        //}
        ///// <remarks/>
        //public void ResetPasswordAsync(string userId, object userState)
        //{
        //    if ((this.resetPasswordOperationCompleted == null))
        //    {
        //        this.resetPasswordOperationCompleted = new SendOrPostCallback(this.OnresetPasswordOperationCompleted);
        //    }
        //    this.InvokeAsync("resetPassword", new object[] {
        //                userId}, this.resetPasswordOperationCompleted, userState);
        //}
        //private void OnresetPasswordOperationCompleted(object arg)
        //{
        //    if ((this.resetPasswordCompleted != null))
        //    {
        //        InvokeCompletedEventArgs invokeArgs = ((InvokeCompletedEventArgs)(arg));
        //        this.resetPasswordCompleted(this, new resetPasswordCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        //    }
        //}


        ///// <remarks/>
        //public void GetUserInfoAsync()
        //{
        //    this.GetUserInfoAsync(null);
        //}
        ///// <remarks/>
        //public void GetUserInfoAsync(object userState)
        //{
        //    if ((this.getUserInfoOperationCompleted == null))
        //    {
        //        this.getUserInfoOperationCompleted = new SendOrPostCallback(this.OngetUserInfoOperationCompleted);
        //    }
        //    this.InvokeAsync("getUserInfo", new object[0], this.getUserInfoOperationCompleted, userState);
        //}
        //private void OngetUserInfoOperationCompleted(object arg)
        //{
        //    if ((this.getUserInfoCompleted != null))
        //    {
        //        InvokeCompletedEventArgs invokeArgs = ((InvokeCompletedEventArgs)(arg));
        //        this.getUserInfoCompleted(this, new getUserInfoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        //    }
        //}


        ///// <remarks/>
        //public void SendEmailMessageAsync(string[] ids)
        //{
        //    this.SendEmailMessageAsync(ids, null);
        //}
        ///// <remarks/>
        //public void SendEmailMessageAsync(string[] ids, object userState)
        //{
        //    if ((this.sendEmailMessageOperationCompleted == null))
        //    {
        //        this.sendEmailMessageOperationCompleted = new SendOrPostCallback(this.OnsendEmailMessageOperationCompleted);
        //    }
        //    this.InvokeAsync("sendEmailMessage", new object[] {
        //                ids}, this.sendEmailMessageOperationCompleted, userState);
        //}
        //private void OnsendEmailMessageOperationCompleted(object arg)
        //{
        //    if ((this.sendEmailMessageCompleted != null))
        //    {
        //        InvokeCompletedEventArgs invokeArgs = ((InvokeCompletedEventArgs)(arg));
        //        this.sendEmailMessageCompleted(this, new sendEmailMessageCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        //    }
        //}


        ///// <remarks/>
        //public void SendEmailAsync(Email[] messages)
        //{
        //    this.SendEmailAsync(messages, null);
        //}
        ///// <remarks/>
        //public void SendEmailAsync(Email[] messages, object userState)
        //{
        //    if ((this.sendEmailOperationCompleted == null))
        //    {
        //        this.sendEmailOperationCompleted = new SendOrPostCallback(this.OnsendEmailOperationCompleted);
        //    }
        //    this.InvokeAsync("sendEmail", new object[] {
        //                messages}, this.sendEmailOperationCompleted, userState);
        //}
        //private void OnsendEmailOperationCompleted(object arg)
        //{
        //    if ((this.sendEmailCompleted != null))
        //    {
        //        InvokeCompletedEventArgs invokeArgs = ((InvokeCompletedEventArgs)(arg));
        //        this.sendEmailCompleted(this, new sendEmailCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        //    }
        //}


        ///// <remarks/>
        //public void RenderEmailTemplateAsync(RenderEmailTemplateRequest[] renderRequests)
        //{
        //    this.RenderEmailTemplateAsync(renderRequests, null);
        //}
        ///// <remarks/>
        //public void RenderEmailTemplateAsync(RenderEmailTemplateRequest[] renderRequests, object userState)
        //{
        //    if ((this.renderEmailTemplateOperationCompleted == null))
        //    {
        //        this.renderEmailTemplateOperationCompleted = new SendOrPostCallback(this.OnrenderEmailTemplateOperationCompleted);
        //    }
        //    this.InvokeAsync("renderEmailTemplate", new object[] {
        //                renderRequests}, this.renderEmailTemplateOperationCompleted, userState);
        //}
        //private void OnrenderEmailTemplateOperationCompleted(object arg)
        //{
        //    if ((this.renderEmailTemplateCompleted != null))
        //    {
        //        InvokeCompletedEventArgs invokeArgs = ((InvokeCompletedEventArgs)(arg));
        //        this.renderEmailTemplateCompleted(this, new renderEmailTemplateCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        //    }
        //}


        ///// <remarks/>
        //public void PerformQuickActionsAsync(PerformQuickActionRequest[] quickActions)
        //{
        //    this.PerformQuickActionsAsync(quickActions, null);
        //}
        ///// <remarks/>
        //public void PerformQuickActionsAsync(PerformQuickActionRequest[] quickActions, object userState)
        //{
        //    if ((this.performQuickActionsOperationCompleted == null))
        //    {
        //        this.performQuickActionsOperationCompleted = new SendOrPostCallback(this.OnperformQuickActionsOperationCompleted);
        //    }
        //    this.InvokeAsync("performQuickActions", new object[] {
        //                quickActions}, this.performQuickActionsOperationCompleted, userState);
        //}
        //private void OnperformQuickActionsOperationCompleted(object arg)
        //{
        //    if ((this.performQuickActionsCompleted != null))
        //    {
        //        InvokeCompletedEventArgs invokeArgs = ((InvokeCompletedEventArgs)(arg));
        //        this.performQuickActionsCompleted(this, new performQuickActionsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        //    }
        //}


        ///// <remarks/>
        //public void DescribeQuickActionsAsync(string[] quickActions)
        //{
        //    this.DescribeQuickActionsAsync(quickActions, null);
        //}
        ///// <remarks/>
        //public void DescribeQuickActionsAsync(string[] quickActions, object userState)
        //{
        //    if ((this.describeQuickActionsOperationCompleted == null))
        //    {
        //        this.describeQuickActionsOperationCompleted = new SendOrPostCallback(this.OndescribeQuickActionsOperationCompleted);
        //    }
        //    this.InvokeAsync("describeQuickActions", new object[] {
        //                quickActions}, this.describeQuickActionsOperationCompleted, userState);
        //}
        //private void OndescribeQuickActionsOperationCompleted(object arg)
        //{
        //    if ((this.describeQuickActionsCompleted != null))
        //    {
        //        InvokeCompletedEventArgs invokeArgs = ((InvokeCompletedEventArgs)(arg));
        //        this.describeQuickActionsCompleted(this, new describeQuickActionsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        //    }
        //}


        ///// <remarks/>
        //public void DescribeAvailableQuickActionsAsync(string contextType)
        //{
        //    this.DescribeAvailableQuickActionsAsync(contextType, null);
        //}
        ///// <remarks/>
        //public void DescribeAvailableQuickActionsAsync(string contextType, object userState)
        //{
        //    if ((this.describeAvailableQuickActionsOperationCompleted == null))
        //    {
        //        this.describeAvailableQuickActionsOperationCompleted = new SendOrPostCallback(this.OndescribeAvailableQuickActionsOperationCompleted);
        //    }
        //    this.InvokeAsync("describeAvailableQuickActions", new object[] {
        //                contextType}, this.describeAvailableQuickActionsOperationCompleted, userState);
        //}
        //private void OndescribeAvailableQuickActionsOperationCompleted(object arg)
        //{
        //    if ((this.describeAvailableQuickActionsCompleted != null))
        //    {
        //        InvokeCompletedEventArgs invokeArgs = ((InvokeCompletedEventArgs)(arg));
        //        this.describeAvailableQuickActionsCompleted(this, new describeAvailableQuickActionsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        //    }
        //}


        ///// <remarks/>
        //public void RetrieveQuickActionTemplatesAsync(string[] quickActionNames, string contextId)
        //{
        //    this.RetrieveQuickActionTemplatesAsync(quickActionNames, contextId, null);
        //}
        ///// <remarks/>
        //public void RetrieveQuickActionTemplatesAsync(string[] quickActionNames, string contextId, object userState)
        //{
        //    if ((this.retrieveQuickActionTemplatesOperationCompleted == null))
        //    {
        //        this.retrieveQuickActionTemplatesOperationCompleted = new SendOrPostCallback(this.OnretrieveQuickActionTemplatesOperationCompleted);
        //    }
        //    this.InvokeAsync("retrieveQuickActionTemplates", new object[] {
        //                quickActionNames,
        //                contextId}, this.retrieveQuickActionTemplatesOperationCompleted, userState);
        //}
        //private void OnretrieveQuickActionTemplatesOperationCompleted(object arg)
        //{
        //    if ((this.retrieveQuickActionTemplatesCompleted != null))
        //    {
        //        InvokeCompletedEventArgs invokeArgs = ((InvokeCompletedEventArgs)(arg));
        //        this.retrieveQuickActionTemplatesCompleted(this, new retrieveQuickActionTemplatesCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        //    }
        //}


        ///// <remarks/>
        //public void DescribeNounsAsync(string[] nouns, bool onlyRenamed, bool includeFields)
        //{
        //    this.DescribeNounsAsync(nouns, onlyRenamed, includeFields, null);
        //}
        ///// <remarks/>
        //public void DescribeNounsAsync(string[] nouns, bool onlyRenamed, bool includeFields, object userState)
        //{
        //    if ((this.describeNounsOperationCompleted == null))
        //    {
        //        this.describeNounsOperationCompleted = new SendOrPostCallback(this.OndescribeNounsOperationCompleted);
        //    }
        //    this.InvokeAsync("describeNouns", new object[] {
        //                nouns,
        //                onlyRenamed,
        //                includeFields}, this.describeNounsOperationCompleted, userState);
        //}
        //private void OndescribeNounsOperationCompleted(object arg)
        //{
        //    if ((this.describeNounsCompleted != null))
        //    {
        //        InvokeCompletedEventArgs invokeArgs = ((InvokeCompletedEventArgs)(arg));
        //        this.describeNounsCompleted(this, new describeNounsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        //    }
        //}
        //#endregion
    //}

    //[System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    //[System.Diagnostics.DebuggerStepThroughAttribute()]
    //[System.ComponentModel.DesignerCategoryAttribute("code")]
    //[System.Web.Services.WebServiceBindingAttribute(Name = "SoapBinding", Namespace = "urn:partner.soap.sforce.com")]
    //[System.Xml.Serialization.XmlIncludeAttribute(typeof(ApiFault))]
    //[System.Xml.Serialization.XmlIncludeAttribute(typeof(ChangeEventHeader))]
    //[System.Xml.Serialization.XmlIncludeAttribute(typeof(NameObjectValuePair))]
    //[System.Xml.Serialization.XmlIncludeAttribute(typeof(SearchLayoutFieldsDisplayed))]
    //[System.Xml.Serialization.XmlIncludeAttribute(typeof(SearchLayoutButtonsDisplayed))]
    //[System.Xml.Serialization.XmlIncludeAttribute(typeof(location))]
    //public partial class SforceService : System.Web.Services.Protocols.SoapHttpClientProtocol
    //{

    private LoginScopeHeader loginScopeHeaderValueField;

    private CallOptions callOptionsValueField;

    private System.Threading.SendOrPostCallback loginOperationCompleted;

    private SessionHeader sessionHeaderValueField;

    private PackageVersionHeader packageVersionHeaderValueField;

    private LocaleOptions localeOptionsValueField;

    private LimitInfoHeader limitInfoHeaderValueField;

    private System.Threading.SendOrPostCallback describeSObjectOperationCompleted;

    private System.Threading.SendOrPostCallback describeSObjectsOperationCompleted;

    private System.Threading.SendOrPostCallback describeGlobalOperationCompleted;

    private System.Threading.SendOrPostCallback describeDataCategoryGroupsOperationCompleted;

    private System.Threading.SendOrPostCallback describeDataCategoryGroupStructuresOperationCompleted;

    private System.Threading.SendOrPostCallback describeDataCategoryMappingsOperationCompleted;

    private System.Threading.SendOrPostCallback describeKnowledgeSettingsOperationCompleted;

    private System.Threading.SendOrPostCallback describeAppMenuOperationCompleted;

    private System.Threading.SendOrPostCallback describeGlobalThemeOperationCompleted;

    private System.Threading.SendOrPostCallback describeThemeOperationCompleted;

    private System.Threading.SendOrPostCallback describeLayoutOperationCompleted;

    private System.Threading.SendOrPostCallback describeSoftphoneLayoutOperationCompleted;

    private System.Threading.SendOrPostCallback describeSearchLayoutsOperationCompleted;

    private System.Threading.SendOrPostCallback describeSearchableEntitiesOperationCompleted;

    private System.Threading.SendOrPostCallback describeSearchScopeOrderOperationCompleted;

    private System.Threading.SendOrPostCallback describeCompactLayoutsOperationCompleted;

    private System.Threading.SendOrPostCallback describePathAssistantsOperationCompleted;

    private System.Threading.SendOrPostCallback describeApprovalLayoutOperationCompleted;

    private System.Threading.SendOrPostCallback describeSoqlListViewsOperationCompleted;

    private MruHeader mruHeaderValueField;

    private System.Threading.SendOrPostCallback executeListViewOperationCompleted;

    private System.Threading.SendOrPostCallback describeSObjectListViewsOperationCompleted;

    private System.Threading.SendOrPostCallback describeTabsOperationCompleted;

    private System.Threading.SendOrPostCallback describeAllTabsOperationCompleted;

    private System.Threading.SendOrPostCallback describePrimaryCompactLayoutsOperationCompleted;

    private AssignmentRuleHeader assignmentRuleHeaderValueField;

    private AllowFieldTruncationHeader allowFieldTruncationHeaderValueField;

    private DisableFeedTrackingHeader disableFeedTrackingHeaderValueField;

    private StreamingEnabledHeader streamingEnabledHeaderValueField;

    private AllOrNoneHeader allOrNoneHeaderValueField;

    private DuplicateRuleHeader duplicateRuleHeaderValueField;

    private DebuggingHeader debuggingHeaderValueField;

    private EmailHeader emailHeaderValueField;

    private DebuggingInfo debuggingInfoValueField;

    private System.Threading.SendOrPostCallback createOperationCompleted;

    private OwnerChangeOptions ownerChangeOptionsValueField;

    private System.Threading.SendOrPostCallback updateOperationCompleted;

    private System.Threading.SendOrPostCallback upsertOperationCompleted;

    private System.Threading.SendOrPostCallback mergeOperationCompleted;

    private UserTerritoryDeleteHeader userTerritoryDeleteHeaderValueField;

    private System.Threading.SendOrPostCallback deleteOperationCompleted;

    private System.Threading.SendOrPostCallback undeleteOperationCompleted;

    private System.Threading.SendOrPostCallback emptyRecycleBinOperationCompleted;

    private QueryOptions queryOptionsValueField;

    private System.Threading.SendOrPostCallback retrieveOperationCompleted;

    private System.Threading.SendOrPostCallback processOperationCompleted;

    private System.Threading.SendOrPostCallback convertLeadOperationCompleted;

    private System.Threading.SendOrPostCallback logoutOperationCompleted;

    private System.Threading.SendOrPostCallback invalidateSessionsOperationCompleted;

    private System.Threading.SendOrPostCallback getDeletedOperationCompleted;

    private System.Threading.SendOrPostCallback getUpdatedOperationCompleted;

    private System.Threading.SendOrPostCallback queryOperationCompleted;

    private System.Threading.SendOrPostCallback queryAllOperationCompleted;

    private System.Threading.SendOrPostCallback queryMoreOperationCompleted;

    private System.Threading.SendOrPostCallback searchOperationCompleted;

    private System.Threading.SendOrPostCallback getServerTimestampOperationCompleted;

    private System.Threading.SendOrPostCallback setPasswordOperationCompleted;

    private System.Threading.SendOrPostCallback changeOwnPasswordOperationCompleted;

    private System.Threading.SendOrPostCallback resetPasswordOperationCompleted;

    private System.Threading.SendOrPostCallback getUserInfoOperationCompleted;

    private System.Threading.SendOrPostCallback deleteByExampleOperationCompleted;

    private System.Threading.SendOrPostCallback sendEmailMessageOperationCompleted;

    private System.Threading.SendOrPostCallback sendEmailOperationCompleted;

    private System.Threading.SendOrPostCallback renderEmailTemplateOperationCompleted;

    private System.Threading.SendOrPostCallback renderStoredEmailTemplateOperationCompleted;

    private System.Threading.SendOrPostCallback performQuickActionsOperationCompleted;

    private System.Threading.SendOrPostCallback describeQuickActionsOperationCompleted;

    private System.Threading.SendOrPostCallback describeQuickActionsForRecordTypeOperationCompleted;

    private System.Threading.SendOrPostCallback describeAvailableQuickActionsOperationCompleted;

    private System.Threading.SendOrPostCallback retrieveQuickActionTemplatesOperationCompleted;

    private System.Threading.SendOrPostCallback retrieveMassQuickActionTemplatesOperationCompleted;

    private System.Threading.SendOrPostCallback describeVisualForceOperationCompleted;

    private System.Threading.SendOrPostCallback findDuplicatesOperationCompleted;

    private System.Threading.SendOrPostCallback findDuplicatesByIdsOperationCompleted;

    private System.Threading.SendOrPostCallback describeNounsOperationCompleted;

    private bool useDefaultCredentialsSetExplicitly;

    /// <remarks/>
    //public SforceService()
    //{
    //    this.Url = global::SfdcConnect.Properties.Settings.Default.SfdcConnect_v45_Salesforce_Partner_SforceService;
    //    if ((this.IsLocalFileSystemWebService(this.Url) == true))
    //    {
    //        this.UseDefaultCredentials = true;
    //        this.useDefaultCredentialsSetExplicitly = false;
    //    }
    //    else
    //    {
    //        this.useDefaultCredentialsSetExplicitly = true;
    //    }
    //}

    public LoginScopeHeader LoginScopeHeaderValue
    {
        get
        {
            return this.loginScopeHeaderValueField;
        }
        set
        {
            this.loginScopeHeaderValueField = value;
        }
    }

    public CallOptions CallOptionsValue
    {
        get
        {
            return this.callOptionsValueField;
        }
        set
        {
            this.callOptionsValueField = value;
        }
    }

    public SessionHeader SessionHeaderValue
    {
        get
        {
            return this.sessionHeaderValueField;
        }
        set
        {
            this.sessionHeaderValueField = value;
        }
    }

    public PackageVersionHeader PackageVersionHeaderValue
    {
        get
        {
            return this.packageVersionHeaderValueField;
        }
        set
        {
            this.packageVersionHeaderValueField = value;
        }
    }

    public LocaleOptions LocaleOptionsValue
    {
        get
        {
            return this.localeOptionsValueField;
        }
        set
        {
            this.localeOptionsValueField = value;
        }
    }

    public LimitInfoHeader LimitInfoHeaderValue
    {
        get
        {
            return this.limitInfoHeaderValueField;
        }
        set
        {
            this.limitInfoHeaderValueField = value;
        }
    }

    public MruHeader MruHeaderValue
    {
        get
        {
            return this.mruHeaderValueField;
        }
        set
        {
            this.mruHeaderValueField = value;
        }
    }

    public AssignmentRuleHeader AssignmentRuleHeaderValue
    {
        get
        {
            return this.assignmentRuleHeaderValueField;
        }
        set
        {
            this.assignmentRuleHeaderValueField = value;
        }
    }

    public AllowFieldTruncationHeader AllowFieldTruncationHeaderValue
    {
        get
        {
            return this.allowFieldTruncationHeaderValueField;
        }
        set
        {
            this.allowFieldTruncationHeaderValueField = value;
        }
    }

    public DisableFeedTrackingHeader DisableFeedTrackingHeaderValue
    {
        get
        {
            return this.disableFeedTrackingHeaderValueField;
        }
        set
        {
            this.disableFeedTrackingHeaderValueField = value;
        }
    }

    public StreamingEnabledHeader StreamingEnabledHeaderValue
    {
        get
        {
            return this.streamingEnabledHeaderValueField;
        }
        set
        {
            this.streamingEnabledHeaderValueField = value;
        }
    }

    public AllOrNoneHeader AllOrNoneHeaderValue
    {
        get
        {
            return this.allOrNoneHeaderValueField;
        }
        set
        {
            this.allOrNoneHeaderValueField = value;
        }
    }

    public DuplicateRuleHeader DuplicateRuleHeaderValue
    {
        get
        {
            return this.duplicateRuleHeaderValueField;
        }
        set
        {
            this.duplicateRuleHeaderValueField = value;
        }
    }

    public DebuggingHeader DebuggingHeaderValue
    {
        get
        {
            return this.debuggingHeaderValueField;
        }
        set
        {
            this.debuggingHeaderValueField = value;
        }
    }

    public EmailHeader EmailHeaderValue
    {
        get
        {
            return this.emailHeaderValueField;
        }
        set
        {
            this.emailHeaderValueField = value;
        }
    }

    public DebuggingInfo DebuggingInfoValue
    {
        get
        {
            return this.debuggingInfoValueField;
        }
        set
        {
            this.debuggingInfoValueField = value;
        }
    }

    public OwnerChangeOptions OwnerChangeOptionsValue
    {
        get
        {
            return this.ownerChangeOptionsValueField;
        }
        set
        {
            this.ownerChangeOptionsValueField = value;
        }
    }

    public UserTerritoryDeleteHeader UserTerritoryDeleteHeaderValue
    {
        get
        {
            return this.userTerritoryDeleteHeaderValueField;
        }
        set
        {
            this.userTerritoryDeleteHeaderValueField = value;
        }
    }

    public QueryOptions QueryOptionsValue
    {
        get
        {
            return this.queryOptionsValueField;
        }
        set
        {
            this.queryOptionsValueField = value;
        }
    }

    public new string Url
    {
        get
        {
            return base.Url;
        }
        set
        {
            if ((((this.IsLocalFileSystemWebService(base.Url) == true)
                        && (this.useDefaultCredentialsSetExplicitly == false))
                        && (this.IsLocalFileSystemWebService(value) == false)))
            {
                base.UseDefaultCredentials = false;
            }
            base.Url = value;
        }
    }

    public new bool UseDefaultCredentials
    {
        get
        {
            return base.UseDefaultCredentials;
        }
        set
        {
            base.UseDefaultCredentials = value;
            this.useDefaultCredentialsSetExplicitly = true;
        }
    }

    /// <remarks/>
    public event loginCompletedEventHandler loginCompleted;

    /// <remarks/>
    public event describeSObjectCompletedEventHandler describeSObjectCompleted;

    /// <remarks/>
    public event describeSObjectsCompletedEventHandler describeSObjectsCompleted;

    /// <remarks/>
    public event describeGlobalCompletedEventHandler describeGlobalCompleted;

    /// <remarks/>
    public event describeDataCategoryGroupsCompletedEventHandler describeDataCategoryGroupsCompleted;

    /// <remarks/>
    public event describeDataCategoryGroupStructuresCompletedEventHandler describeDataCategoryGroupStructuresCompleted;

    /// <remarks/>
    public event describeDataCategoryMappingsCompletedEventHandler describeDataCategoryMappingsCompleted;

    /// <remarks/>
    public event describeKnowledgeSettingsCompletedEventHandler describeKnowledgeSettingsCompleted;

    /// <remarks/>
    public event describeAppMenuCompletedEventHandler describeAppMenuCompleted;

    /// <remarks/>
    public event describeGlobalThemeCompletedEventHandler describeGlobalThemeCompleted;

    /// <remarks/>
    public event describeThemeCompletedEventHandler describeThemeCompleted;

    /// <remarks/>
    public event describeLayoutCompletedEventHandler describeLayoutCompleted;

    /// <remarks/>
    public event describeSoftphoneLayoutCompletedEventHandler describeSoftphoneLayoutCompleted;

    /// <remarks/>
    public event describeSearchLayoutsCompletedEventHandler describeSearchLayoutsCompleted;

    /// <remarks/>
    public event describeSearchableEntitiesCompletedEventHandler describeSearchableEntitiesCompleted;

    /// <remarks/>
    public event describeSearchScopeOrderCompletedEventHandler describeSearchScopeOrderCompleted;

    /// <remarks/>
    public event describeCompactLayoutsCompletedEventHandler describeCompactLayoutsCompleted;

    /// <remarks/>
    public event describePathAssistantsCompletedEventHandler describePathAssistantsCompleted;

    /// <remarks/>
    public event describeApprovalLayoutCompletedEventHandler describeApprovalLayoutCompleted;

    /// <remarks/>
    public event describeSoqlListViewsCompletedEventHandler describeSoqlListViewsCompleted;

    /// <remarks/>
    public event executeListViewCompletedEventHandler executeListViewCompleted;

    /// <remarks/>
    public event describeSObjectListViewsCompletedEventHandler describeSObjectListViewsCompleted;

    /// <remarks/>
    public event describeTabsCompletedEventHandler describeTabsCompleted;

    /// <remarks/>
    public event describeAllTabsCompletedEventHandler describeAllTabsCompleted;

    /// <remarks/>
    public event describePrimaryCompactLayoutsCompletedEventHandler describePrimaryCompactLayoutsCompleted;

    /// <remarks/>
    public event createCompletedEventHandler createCompleted;

    /// <remarks/>
    public event updateCompletedEventHandler updateCompleted;

    /// <remarks/>
    public event upsertCompletedEventHandler upsertCompleted;

    /// <remarks/>
    public event mergeCompletedEventHandler mergeCompleted;

    /// <remarks/>
    public event deleteCompletedEventHandler deleteCompleted;

    /// <remarks/>
    public event undeleteCompletedEventHandler undeleteCompleted;

    /// <remarks/>
    public event emptyRecycleBinCompletedEventHandler emptyRecycleBinCompleted;

    /// <remarks/>
    public event retrieveCompletedEventHandler retrieveCompleted;

    /// <remarks/>
    public event processCompletedEventHandler processCompleted;

    /// <remarks/>
    public event convertLeadCompletedEventHandler convertLeadCompleted;

    /// <remarks/>
    public event logoutCompletedEventHandler logoutCompleted;

    /// <remarks/>
    public event invalidateSessionsCompletedEventHandler invalidateSessionsCompleted;

    /// <remarks/>
    public event getDeletedCompletedEventHandler getDeletedCompleted;

    /// <remarks/>
    public event getUpdatedCompletedEventHandler getUpdatedCompleted;

    /// <remarks/>
    public event queryCompletedEventHandler queryCompleted;

    /// <remarks/>
    public event queryAllCompletedEventHandler queryAllCompleted;

    /// <remarks/>
    public event queryMoreCompletedEventHandler queryMoreCompleted;

    /// <remarks/>
    public event searchCompletedEventHandler searchCompleted;

    /// <remarks/>
    public event getServerTimestampCompletedEventHandler getServerTimestampCompleted;

    /// <remarks/>
    public event setPasswordCompletedEventHandler setPasswordCompleted;

    /// <remarks/>
    public event changeOwnPasswordCompletedEventHandler changeOwnPasswordCompleted;

    /// <remarks/>
    public event resetPasswordCompletedEventHandler resetPasswordCompleted;

    /// <remarks/>
    public event getUserInfoCompletedEventHandler getUserInfoCompleted;

    /// <remarks/>
    public event deleteByExampleCompletedEventHandler deleteByExampleCompleted;

    /// <remarks/>
    public event sendEmailMessageCompletedEventHandler sendEmailMessageCompleted;

    /// <remarks/>
    public event sendEmailCompletedEventHandler sendEmailCompleted;

    /// <remarks/>
    public event renderEmailTemplateCompletedEventHandler renderEmailTemplateCompleted;

    /// <remarks/>
    public event renderStoredEmailTemplateCompletedEventHandler renderStoredEmailTemplateCompleted;

    /// <remarks/>
    public event performQuickActionsCompletedEventHandler performQuickActionsCompleted;

    /// <remarks/>
    public event describeQuickActionsCompletedEventHandler describeQuickActionsCompleted;

    /// <remarks/>
    public event describeQuickActionsForRecordTypeCompletedEventHandler describeQuickActionsForRecordTypeCompleted;

    /// <remarks/>
    public event describeAvailableQuickActionsCompletedEventHandler describeAvailableQuickActionsCompleted;

    /// <remarks/>
    public event retrieveQuickActionTemplatesCompletedEventHandler retrieveQuickActionTemplatesCompleted;

    /// <remarks/>
    public event retrieveMassQuickActionTemplatesCompletedEventHandler retrieveMassQuickActionTemplatesCompleted;

    /// <remarks/>
    public event describeVisualForceCompletedEventHandler describeVisualForceCompleted;

    /// <remarks/>
    public event findDuplicatesCompletedEventHandler findDuplicatesCompleted;

    /// <remarks/>
    public event findDuplicatesByIdsCompletedEventHandler findDuplicatesByIdsCompleted;

    /// <remarks/>
    public event describeNounsCompletedEventHandler describeNounsCompleted;

    /// <remarks/>
    [System.Web.Services.Protocols.SoapHeaderAttribute("CallOptionsValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("LoginScopeHeaderValue")]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("result")]
    public LoginResult login(string username, string password)
    {
        object[] results = this.Invoke("login", new object[] {
                        username,
                        password});
        return ((LoginResult)(results[0]));
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

    private void OnloginOperationCompleted(object arg)
    {
        if ((this.loginCompleted != null))
        {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.loginCompleted(this, new loginCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }

    /// <remarks/>
    [System.Web.Services.Protocols.SoapHeaderAttribute("LocaleOptionsValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("PackageVersionHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("CallOptionsValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("LimitInfoHeaderValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapHeaderAttribute("SessionHeaderValue")]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("result", IsNullable = true)]
    public DescribeSObjectResult describeSObject(string sObjectType)
    {
        object[] results = this.Invoke("describeSObject", new object[] {
                        sObjectType});
        return ((DescribeSObjectResult)(results[0]));
    }

    /// <remarks/>
    public void describeSObjectAsync(string sObjectType)
    {
        this.describeSObjectAsync(sObjectType, null);
    }

    /// <remarks/>
    public void describeSObjectAsync(string sObjectType, object userState)
    {
        if ((this.describeSObjectOperationCompleted == null))
        {
            this.describeSObjectOperationCompleted = new System.Threading.SendOrPostCallback(this.OndescribeSObjectOperationCompleted);
        }
        this.InvokeAsync("describeSObject", new object[] {
                        sObjectType}, this.describeSObjectOperationCompleted, userState);
    }

    private void OndescribeSObjectOperationCompleted(object arg)
    {
        if ((this.describeSObjectCompleted != null))
        {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.describeSObjectCompleted(this, new describeSObjectCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }

    /// <remarks/>
    [System.Web.Services.Protocols.SoapHeaderAttribute("LocaleOptionsValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("PackageVersionHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("CallOptionsValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("LimitInfoHeaderValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapHeaderAttribute("SessionHeaderValue")]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("result", IsNullable = true)]
    public DescribeSObjectResult[] describeSObjects([System.Xml.Serialization.XmlElementAttribute("sObjectType")] string[] sObjectType)
    {
        object[] results = this.Invoke("describeSObjects", new object[] {
                        sObjectType});
        return ((DescribeSObjectResult[])(results[0]));
    }

    /// <remarks/>
    public void describeSObjectsAsync(string[] sObjectType)
    {
        this.describeSObjectsAsync(sObjectType, null);
    }

    /// <remarks/>
    public void describeSObjectsAsync(string[] sObjectType, object userState)
    {
        if ((this.describeSObjectsOperationCompleted == null))
        {
            this.describeSObjectsOperationCompleted = new System.Threading.SendOrPostCallback(this.OndescribeSObjectsOperationCompleted);
        }
        this.InvokeAsync("describeSObjects", new object[] {
                        sObjectType}, this.describeSObjectsOperationCompleted, userState);
    }

    private void OndescribeSObjectsOperationCompleted(object arg)
    {
        if ((this.describeSObjectsCompleted != null))
        {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.describeSObjectsCompleted(this, new describeSObjectsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }

    /// <remarks/>
    [System.Web.Services.Protocols.SoapHeaderAttribute("PackageVersionHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("CallOptionsValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("LimitInfoHeaderValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapHeaderAttribute("SessionHeaderValue")]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("result")]
    public DescribeGlobalResult describeGlobal()
    {
        object[] results = this.Invoke("describeGlobal", new object[0]);
        return ((DescribeGlobalResult)(results[0]));
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

    private void OndescribeGlobalOperationCompleted(object arg)
    {
        if ((this.describeGlobalCompleted != null))
        {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.describeGlobalCompleted(this, new describeGlobalCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }

    /// <remarks/>
    [System.Web.Services.Protocols.SoapHeaderAttribute("LocaleOptionsValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("PackageVersionHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("CallOptionsValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("LimitInfoHeaderValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapHeaderAttribute("SessionHeaderValue")]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("result")]
    public DescribeDataCategoryGroupResult[] describeDataCategoryGroups([System.Xml.Serialization.XmlElementAttribute("sObjectType")] string[] sObjectType)
    {
        object[] results = this.Invoke("describeDataCategoryGroups", new object[] {
                        sObjectType});
        return ((DescribeDataCategoryGroupResult[])(results[0]));
    }

    /// <remarks/>
    public void describeDataCategoryGroupsAsync(string[] sObjectType)
    {
        this.describeDataCategoryGroupsAsync(sObjectType, null);
    }

    /// <remarks/>
    public void describeDataCategoryGroupsAsync(string[] sObjectType, object userState)
    {
        if ((this.describeDataCategoryGroupsOperationCompleted == null))
        {
            this.describeDataCategoryGroupsOperationCompleted = new System.Threading.SendOrPostCallback(this.OndescribeDataCategoryGroupsOperationCompleted);
        }
        this.InvokeAsync("describeDataCategoryGroups", new object[] {
                        sObjectType}, this.describeDataCategoryGroupsOperationCompleted, userState);
    }

    private void OndescribeDataCategoryGroupsOperationCompleted(object arg)
    {
        if ((this.describeDataCategoryGroupsCompleted != null))
        {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.describeDataCategoryGroupsCompleted(this, new describeDataCategoryGroupsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }

    /// <remarks/>
    [System.Web.Services.Protocols.SoapHeaderAttribute("LocaleOptionsValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("PackageVersionHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("CallOptionsValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("LimitInfoHeaderValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapHeaderAttribute("SessionHeaderValue")]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("result")]
    public DescribeDataCategoryGroupStructureResult[] describeDataCategoryGroupStructures([System.Xml.Serialization.XmlElementAttribute("pairs")] DataCategoryGroupSobjectTypePair[] pairs, bool topCategoriesOnly)
    {
        object[] results = this.Invoke("describeDataCategoryGroupStructures", new object[] {
                        pairs,
                        topCategoriesOnly});
        return ((DescribeDataCategoryGroupStructureResult[])(results[0]));
    }

    /// <remarks/>
    public void describeDataCategoryGroupStructuresAsync(DataCategoryGroupSobjectTypePair[] pairs, bool topCategoriesOnly)
    {
        this.describeDataCategoryGroupStructuresAsync(pairs, topCategoriesOnly, null);
    }

    /// <remarks/>
    public void describeDataCategoryGroupStructuresAsync(DataCategoryGroupSobjectTypePair[] pairs, bool topCategoriesOnly, object userState)
    {
        if ((this.describeDataCategoryGroupStructuresOperationCompleted == null))
        {
            this.describeDataCategoryGroupStructuresOperationCompleted = new System.Threading.SendOrPostCallback(this.OndescribeDataCategoryGroupStructuresOperationCompleted);
        }
        this.InvokeAsync("describeDataCategoryGroupStructures", new object[] {
                        pairs,
                        topCategoriesOnly}, this.describeDataCategoryGroupStructuresOperationCompleted, userState);
    }

    private void OndescribeDataCategoryGroupStructuresOperationCompleted(object arg)
    {
        if ((this.describeDataCategoryGroupStructuresCompleted != null))
        {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.describeDataCategoryGroupStructuresCompleted(this, new describeDataCategoryGroupStructuresCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }

    /// <remarks/>
    [System.Web.Services.Protocols.SoapHeaderAttribute("LocaleOptionsValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("PackageVersionHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("CallOptionsValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("LimitInfoHeaderValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapHeaderAttribute("SessionHeaderValue")]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("result")]
    public DescribeDataCategoryMappingResult[] describeDataCategoryMappings()
    {
        object[] results = this.Invoke("describeDataCategoryMappings", new object[0]);
        return ((DescribeDataCategoryMappingResult[])(results[0]));
    }

    /// <remarks/>
    public void describeDataCategoryMappingsAsync()
    {
        this.describeDataCategoryMappingsAsync(null);
    }

    /// <remarks/>
    public void describeDataCategoryMappingsAsync(object userState)
    {
        if ((this.describeDataCategoryMappingsOperationCompleted == null))
        {
            this.describeDataCategoryMappingsOperationCompleted = new System.Threading.SendOrPostCallback(this.OndescribeDataCategoryMappingsOperationCompleted);
        }
        this.InvokeAsync("describeDataCategoryMappings", new object[0], this.describeDataCategoryMappingsOperationCompleted, userState);
    }

    private void OndescribeDataCategoryMappingsOperationCompleted(object arg)
    {
        if ((this.describeDataCategoryMappingsCompleted != null))
        {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.describeDataCategoryMappingsCompleted(this, new describeDataCategoryMappingsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }

    /// <remarks/>
    [System.Web.Services.Protocols.SoapHeaderAttribute("LocaleOptionsValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("PackageVersionHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("CallOptionsValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("LimitInfoHeaderValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapHeaderAttribute("SessionHeaderValue")]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("result")]
    public KnowledgeSettings describeKnowledgeSettings()
    {
        object[] results = this.Invoke("describeKnowledgeSettings", new object[0]);
        return ((KnowledgeSettings)(results[0]));
    }

    /// <remarks/>
    public void describeKnowledgeSettingsAsync()
    {
        this.describeKnowledgeSettingsAsync(null);
    }

    /// <remarks/>
    public void describeKnowledgeSettingsAsync(object userState)
    {
        if ((this.describeKnowledgeSettingsOperationCompleted == null))
        {
            this.describeKnowledgeSettingsOperationCompleted = new System.Threading.SendOrPostCallback(this.OndescribeKnowledgeSettingsOperationCompleted);
        }
        this.InvokeAsync("describeKnowledgeSettings", new object[0], this.describeKnowledgeSettingsOperationCompleted, userState);
    }

    private void OndescribeKnowledgeSettingsOperationCompleted(object arg)
    {
        if ((this.describeKnowledgeSettingsCompleted != null))
        {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.describeKnowledgeSettingsCompleted(this, new describeKnowledgeSettingsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }

    /// <remarks/>
    [System.Web.Services.Protocols.SoapHeaderAttribute("PackageVersionHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("CallOptionsValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("LimitInfoHeaderValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapHeaderAttribute("SessionHeaderValue")]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlArrayAttribute("result", IsNullable = true)]
    [return: System.Xml.Serialization.XmlArrayItemAttribute("appMenuItems", IsNullable = false)]
    public DescribeAppMenuItem[] describeAppMenu(AppMenuType appMenuType, [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)] string networkId)
    {
        object[] results = this.Invoke("describeAppMenu", new object[] {
                        appMenuType,
                        networkId});
        return ((DescribeAppMenuItem[])(results[0]));
    }

    /// <remarks/>
    public void describeAppMenuAsync(AppMenuType appMenuType, string networkId)
    {
        this.describeAppMenuAsync(appMenuType, networkId, null);
    }

    /// <remarks/>
    public void describeAppMenuAsync(AppMenuType appMenuType, string networkId, object userState)
    {
        if ((this.describeAppMenuOperationCompleted == null))
        {
            this.describeAppMenuOperationCompleted = new System.Threading.SendOrPostCallback(this.OndescribeAppMenuOperationCompleted);
        }
        this.InvokeAsync("describeAppMenu", new object[] {
                        appMenuType,
                        networkId}, this.describeAppMenuOperationCompleted, userState);
    }

    private void OndescribeAppMenuOperationCompleted(object arg)
    {
        if ((this.describeAppMenuCompleted != null))
        {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.describeAppMenuCompleted(this, new describeAppMenuCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }

    /// <remarks/>
    [System.Web.Services.Protocols.SoapHeaderAttribute("PackageVersionHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("CallOptionsValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("LimitInfoHeaderValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapHeaderAttribute("SessionHeaderValue")]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("result")]
    public DescribeGlobalTheme describeGlobalTheme()
    {
        object[] results = this.Invoke("describeGlobalTheme", new object[0]);
        return ((DescribeGlobalTheme)(results[0]));
    }

    /// <remarks/>
    public void describeGlobalThemeAsync()
    {
        this.describeGlobalThemeAsync(null);
    }

    /// <remarks/>
    public void describeGlobalThemeAsync(object userState)
    {
        if ((this.describeGlobalThemeOperationCompleted == null))
        {
            this.describeGlobalThemeOperationCompleted = new System.Threading.SendOrPostCallback(this.OndescribeGlobalThemeOperationCompleted);
        }
        this.InvokeAsync("describeGlobalTheme", new object[0], this.describeGlobalThemeOperationCompleted, userState);
    }

    private void OndescribeGlobalThemeOperationCompleted(object arg)
    {
        if ((this.describeGlobalThemeCompleted != null))
        {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.describeGlobalThemeCompleted(this, new describeGlobalThemeCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }

    /// <remarks/>
    [System.Web.Services.Protocols.SoapHeaderAttribute("PackageVersionHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("CallOptionsValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("LimitInfoHeaderValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapHeaderAttribute("SessionHeaderValue")]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlArrayAttribute("result")]
    [return: System.Xml.Serialization.XmlArrayItemAttribute("themeItems", IsNullable = false)]
    public DescribeThemeItem[] describeTheme([System.Xml.Serialization.XmlElementAttribute("sobjectType")] string[] sobjectType)
    {
        object[] results = this.Invoke("describeTheme", new object[] {
                        sobjectType});
        return ((DescribeThemeItem[])(results[0]));
    }

    /// <remarks/>
    public void describeThemeAsync(string[] sobjectType)
    {
        this.describeThemeAsync(sobjectType, null);
    }

    /// <remarks/>
    public void describeThemeAsync(string[] sobjectType, object userState)
    {
        if ((this.describeThemeOperationCompleted == null))
        {
            this.describeThemeOperationCompleted = new System.Threading.SendOrPostCallback(this.OndescribeThemeOperationCompleted);
        }
        this.InvokeAsync("describeTheme", new object[] {
                        sobjectType}, this.describeThemeOperationCompleted, userState);
    }

    private void OndescribeThemeOperationCompleted(object arg)
    {
        if ((this.describeThemeCompleted != null))
        {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.describeThemeCompleted(this, new describeThemeCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }

    /// <remarks/>
    [System.Web.Services.Protocols.SoapHeaderAttribute("PackageVersionHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("CallOptionsValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("LimitInfoHeaderValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapHeaderAttribute("SessionHeaderValue")]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("result", IsNullable = true)]
    public DescribeLayoutResult describeLayout(string sObjectType, [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)] string layoutName, [System.Xml.Serialization.XmlElementAttribute("recordTypeIds")] string[] recordTypeIds)
    {
        object[] results = this.Invoke("describeLayout", new object[] {
                        sObjectType,
                        layoutName,
                        recordTypeIds});
        return ((DescribeLayoutResult)(results[0]));
    }

    /// <remarks/>
    public void describeLayoutAsync(string sObjectType, string layoutName, string[] recordTypeIds)
    {
        this.describeLayoutAsync(sObjectType, layoutName, recordTypeIds, null);
    }

    /// <remarks/>
    public void describeLayoutAsync(string sObjectType, string layoutName, string[] recordTypeIds, object userState)
    {
        if ((this.describeLayoutOperationCompleted == null))
        {
            this.describeLayoutOperationCompleted = new System.Threading.SendOrPostCallback(this.OndescribeLayoutOperationCompleted);
        }
        this.InvokeAsync("describeLayout", new object[] {
                        sObjectType,
                        layoutName,
                        recordTypeIds}, this.describeLayoutOperationCompleted, userState);
    }

    private void OndescribeLayoutOperationCompleted(object arg)
    {
        if ((this.describeLayoutCompleted != null))
        {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.describeLayoutCompleted(this, new describeLayoutCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }

    /// <remarks/>
    [System.Web.Services.Protocols.SoapHeaderAttribute("PackageVersionHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("CallOptionsValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("LimitInfoHeaderValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapHeaderAttribute("SessionHeaderValue")]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("result", IsNullable = true)]
    public DescribeSoftphoneLayoutResult describeSoftphoneLayout()
    {
        object[] results = this.Invoke("describeSoftphoneLayout", new object[0]);
        return ((DescribeSoftphoneLayoutResult)(results[0]));
    }

    /// <remarks/>
    public void describeSoftphoneLayoutAsync()
    {
        this.describeSoftphoneLayoutAsync(null);
    }

    /// <remarks/>
    public void describeSoftphoneLayoutAsync(object userState)
    {
        if ((this.describeSoftphoneLayoutOperationCompleted == null))
        {
            this.describeSoftphoneLayoutOperationCompleted = new System.Threading.SendOrPostCallback(this.OndescribeSoftphoneLayoutOperationCompleted);
        }
        this.InvokeAsync("describeSoftphoneLayout", new object[0], this.describeSoftphoneLayoutOperationCompleted, userState);
    }

    private void OndescribeSoftphoneLayoutOperationCompleted(object arg)
    {
        if ((this.describeSoftphoneLayoutCompleted != null))
        {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.describeSoftphoneLayoutCompleted(this, new describeSoftphoneLayoutCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }

    /// <remarks/>
    [System.Web.Services.Protocols.SoapHeaderAttribute("PackageVersionHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("CallOptionsValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("LimitInfoHeaderValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapHeaderAttribute("SessionHeaderValue")]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("result", IsNullable = true)]
    public DescribeSearchLayoutResult[] describeSearchLayouts([System.Xml.Serialization.XmlElementAttribute("sObjectType")] string[] sObjectType)
    {
        object[] results = this.Invoke("describeSearchLayouts", new object[] {
                        sObjectType});
        return ((DescribeSearchLayoutResult[])(results[0]));
    }

    /// <remarks/>
    public void describeSearchLayoutsAsync(string[] sObjectType)
    {
        this.describeSearchLayoutsAsync(sObjectType, null);
    }

    /// <remarks/>
    public void describeSearchLayoutsAsync(string[] sObjectType, object userState)
    {
        if ((this.describeSearchLayoutsOperationCompleted == null))
        {
            this.describeSearchLayoutsOperationCompleted = new System.Threading.SendOrPostCallback(this.OndescribeSearchLayoutsOperationCompleted);
        }
        this.InvokeAsync("describeSearchLayouts", new object[] {
                        sObjectType}, this.describeSearchLayoutsOperationCompleted, userState);
    }

    private void OndescribeSearchLayoutsOperationCompleted(object arg)
    {
        if ((this.describeSearchLayoutsCompleted != null))
        {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.describeSearchLayoutsCompleted(this, new describeSearchLayoutsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }

    /// <remarks/>
    [System.Web.Services.Protocols.SoapHeaderAttribute("PackageVersionHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("CallOptionsValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("LimitInfoHeaderValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapHeaderAttribute("SessionHeaderValue")]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("result")]
    public DescribeSearchableEntityResult[] describeSearchableEntities(bool includeOnlyEntitiesWithTabs)
    {
        object[] results = this.Invoke("describeSearchableEntities", new object[] {
                        includeOnlyEntitiesWithTabs});
        return ((DescribeSearchableEntityResult[])(results[0]));
    }

    /// <remarks/>
    public void describeSearchableEntitiesAsync(bool includeOnlyEntitiesWithTabs)
    {
        this.describeSearchableEntitiesAsync(includeOnlyEntitiesWithTabs, null);
    }

    /// <remarks/>
    public void describeSearchableEntitiesAsync(bool includeOnlyEntitiesWithTabs, object userState)
    {
        if ((this.describeSearchableEntitiesOperationCompleted == null))
        {
            this.describeSearchableEntitiesOperationCompleted = new System.Threading.SendOrPostCallback(this.OndescribeSearchableEntitiesOperationCompleted);
        }
        this.InvokeAsync("describeSearchableEntities", new object[] {
                        includeOnlyEntitiesWithTabs}, this.describeSearchableEntitiesOperationCompleted, userState);
    }

    private void OndescribeSearchableEntitiesOperationCompleted(object arg)
    {
        if ((this.describeSearchableEntitiesCompleted != null))
        {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.describeSearchableEntitiesCompleted(this, new describeSearchableEntitiesCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }

    /// <remarks/>
    [System.Web.Services.Protocols.SoapHeaderAttribute("PackageVersionHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("CallOptionsValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("LimitInfoHeaderValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapHeaderAttribute("SessionHeaderValue")]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("result")]
    public DescribeSearchScopeOrderResult[] describeSearchScopeOrder(bool includeRealTimeEntities, [System.Xml.Serialization.XmlIgnoreAttribute()] bool includeRealTimeEntitiesSpecified)
    {
        object[] results = this.Invoke("describeSearchScopeOrder", new object[] {
                        includeRealTimeEntities,
                        includeRealTimeEntitiesSpecified});
        return ((DescribeSearchScopeOrderResult[])(results[0]));
    }

    /// <remarks/>
    public void describeSearchScopeOrderAsync(bool includeRealTimeEntities, bool includeRealTimeEntitiesSpecified)
    {
        this.describeSearchScopeOrderAsync(includeRealTimeEntities, includeRealTimeEntitiesSpecified, null);
    }

    /// <remarks/>
    public void describeSearchScopeOrderAsync(bool includeRealTimeEntities, bool includeRealTimeEntitiesSpecified, object userState)
    {
        if ((this.describeSearchScopeOrderOperationCompleted == null))
        {
            this.describeSearchScopeOrderOperationCompleted = new System.Threading.SendOrPostCallback(this.OndescribeSearchScopeOrderOperationCompleted);
        }
        this.InvokeAsync("describeSearchScopeOrder", new object[] {
                        includeRealTimeEntities,
                        includeRealTimeEntitiesSpecified}, this.describeSearchScopeOrderOperationCompleted, userState);
    }

    private void OndescribeSearchScopeOrderOperationCompleted(object arg)
    {
        if ((this.describeSearchScopeOrderCompleted != null))
        {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.describeSearchScopeOrderCompleted(this, new describeSearchScopeOrderCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }

    /// <remarks/>
    [System.Web.Services.Protocols.SoapHeaderAttribute("PackageVersionHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("CallOptionsValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("LimitInfoHeaderValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapHeaderAttribute("SessionHeaderValue")]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("result", IsNullable = true)]
    public DescribeCompactLayoutsResult describeCompactLayouts(string sObjectType, [System.Xml.Serialization.XmlElementAttribute("recordTypeIds")] string[] recordTypeIds)
    {
        object[] results = this.Invoke("describeCompactLayouts", new object[] {
                        sObjectType,
                        recordTypeIds});
        return ((DescribeCompactLayoutsResult)(results[0]));
    }

    /// <remarks/>
    public void describeCompactLayoutsAsync(string sObjectType, string[] recordTypeIds)
    {
        this.describeCompactLayoutsAsync(sObjectType, recordTypeIds, null);
    }

    /// <remarks/>
    public void describeCompactLayoutsAsync(string sObjectType, string[] recordTypeIds, object userState)
    {
        if ((this.describeCompactLayoutsOperationCompleted == null))
        {
            this.describeCompactLayoutsOperationCompleted = new System.Threading.SendOrPostCallback(this.OndescribeCompactLayoutsOperationCompleted);
        }
        this.InvokeAsync("describeCompactLayouts", new object[] {
                        sObjectType,
                        recordTypeIds}, this.describeCompactLayoutsOperationCompleted, userState);
    }

    private void OndescribeCompactLayoutsOperationCompleted(object arg)
    {
        if ((this.describeCompactLayoutsCompleted != null))
        {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.describeCompactLayoutsCompleted(this, new describeCompactLayoutsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }

    /// <remarks/>
    [System.Web.Services.Protocols.SoapHeaderAttribute("PackageVersionHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("CallOptionsValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("LimitInfoHeaderValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapHeaderAttribute("SessionHeaderValue")]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlArrayAttribute("result", IsNullable = true)]
    [return: System.Xml.Serialization.XmlArrayItemAttribute("pathAssistants", IsNullable = false)]
    public DescribePathAssistant[] describePathAssistants(string sObjectType, [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)] string picklistValue, [System.Xml.Serialization.XmlElementAttribute("recordTypeIds")] string[] recordTypeIds)
    {
        object[] results = this.Invoke("describePathAssistants", new object[] {
                        sObjectType,
                        picklistValue,
                        recordTypeIds});
        return ((DescribePathAssistant[])(results[0]));
    }

    /// <remarks/>
    public void describePathAssistantsAsync(string sObjectType, string picklistValue, string[] recordTypeIds)
    {
        this.describePathAssistantsAsync(sObjectType, picklistValue, recordTypeIds, null);
    }

    /// <remarks/>
    public void describePathAssistantsAsync(string sObjectType, string picklistValue, string[] recordTypeIds, object userState)
    {
        if ((this.describePathAssistantsOperationCompleted == null))
        {
            this.describePathAssistantsOperationCompleted = new System.Threading.SendOrPostCallback(this.OndescribePathAssistantsOperationCompleted);
        }
        this.InvokeAsync("describePathAssistants", new object[] {
                        sObjectType,
                        picklistValue,
                        recordTypeIds}, this.describePathAssistantsOperationCompleted, userState);
    }

    private void OndescribePathAssistantsOperationCompleted(object arg)
    {
        if ((this.describePathAssistantsCompleted != null))
        {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.describePathAssistantsCompleted(this, new describePathAssistantsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }

    /// <remarks/>
    [System.Web.Services.Protocols.SoapHeaderAttribute("PackageVersionHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("CallOptionsValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("LimitInfoHeaderValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapHeaderAttribute("SessionHeaderValue")]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlArrayAttribute("result", IsNullable = true)]
    [return: System.Xml.Serialization.XmlArrayItemAttribute("approvalLayouts", IsNullable = false)]
    public DescribeApprovalLayout[] describeApprovalLayout(string sObjectType, [System.Xml.Serialization.XmlElementAttribute("approvalProcessNames")] string[] approvalProcessNames)
    {
        object[] results = this.Invoke("describeApprovalLayout", new object[] {
                        sObjectType,
                        approvalProcessNames});
        return ((DescribeApprovalLayout[])(results[0]));
    }

    /// <remarks/>
    public void describeApprovalLayoutAsync(string sObjectType, string[] approvalProcessNames)
    {
        this.describeApprovalLayoutAsync(sObjectType, approvalProcessNames, null);
    }

    /// <remarks/>
    public void describeApprovalLayoutAsync(string sObjectType, string[] approvalProcessNames, object userState)
    {
        if ((this.describeApprovalLayoutOperationCompleted == null))
        {
            this.describeApprovalLayoutOperationCompleted = new System.Threading.SendOrPostCallback(this.OndescribeApprovalLayoutOperationCompleted);
        }
        this.InvokeAsync("describeApprovalLayout", new object[] {
                        sObjectType,
                        approvalProcessNames}, this.describeApprovalLayoutOperationCompleted, userState);
    }

    private void OndescribeApprovalLayoutOperationCompleted(object arg)
    {
        if ((this.describeApprovalLayoutCompleted != null))
        {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.describeApprovalLayoutCompleted(this, new describeApprovalLayoutCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }

    /// <remarks/>
    [System.Web.Services.Protocols.SoapHeaderAttribute("PackageVersionHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("CallOptionsValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("LimitInfoHeaderValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapHeaderAttribute("SessionHeaderValue")]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlArrayAttribute("result")]
    [return: System.Xml.Serialization.XmlArrayItemAttribute("describeSoqlListViews", IsNullable = false)]
    public DescribeSoqlListView[] describeSoqlListViews([System.Xml.Serialization.XmlArrayItemAttribute("listViewParams", IsNullable = false)] DescribeSoqlListViewParams[] request)
    {
        object[] results = this.Invoke("describeSoqlListViews", new object[] {
                        request});
        return ((DescribeSoqlListView[])(results[0]));
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

    private void OndescribeSoqlListViewsOperationCompleted(object arg)
    {
        if ((this.describeSoqlListViewsCompleted != null))
        {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.describeSoqlListViewsCompleted(this, new describeSoqlListViewsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }

    /// <remarks/>
    [System.Web.Services.Protocols.SoapHeaderAttribute("CallOptionsValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("MruHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("LimitInfoHeaderValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapHeaderAttribute("SessionHeaderValue")]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("result")]
    public ExecuteListViewResult executeListView(ExecuteListViewRequest request)
    {
        object[] results = this.Invoke("executeListView", new object[] {
                        request});
        return ((ExecuteListViewResult)(results[0]));
    }

    /// <remarks/>
    public void executeListViewAsync(ExecuteListViewRequest request)
    {
        this.executeListViewAsync(request, null);
    }

    /// <remarks/>
    public void executeListViewAsync(ExecuteListViewRequest request, object userState)
    {
        if ((this.executeListViewOperationCompleted == null))
        {
            this.executeListViewOperationCompleted = new System.Threading.SendOrPostCallback(this.OnexecuteListViewOperationCompleted);
        }
        this.InvokeAsync("executeListView", new object[] {
                        request}, this.executeListViewOperationCompleted, userState);
    }

    private void OnexecuteListViewOperationCompleted(object arg)
    {
        if ((this.executeListViewCompleted != null))
        {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.executeListViewCompleted(this, new executeListViewCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }

    /// <remarks/>
    [System.Web.Services.Protocols.SoapHeaderAttribute("PackageVersionHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("CallOptionsValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("LimitInfoHeaderValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapHeaderAttribute("SessionHeaderValue")]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlArrayAttribute("result")]
    [return: System.Xml.Serialization.XmlArrayItemAttribute("describeSoqlListViews", IsNullable = false)]
    public DescribeSoqlListView[] describeSObjectListViews(string sObjectType, bool recentsOnly, listViewIsSoqlCompatible isSoqlCompatible, int limit, int offset)
    {
        object[] results = this.Invoke("describeSObjectListViews", new object[] {
                        sObjectType,
                        recentsOnly,
                        isSoqlCompatible,
                        limit,
                        offset});
        return ((DescribeSoqlListView[])(results[0]));
    }

    /// <remarks/>
    public void describeSObjectListViewsAsync(string sObjectType, bool recentsOnly, listViewIsSoqlCompatible isSoqlCompatible, int limit, int offset)
    {
        this.describeSObjectListViewsAsync(sObjectType, recentsOnly, isSoqlCompatible, limit, offset, null);
    }

    /// <remarks/>
    public void describeSObjectListViewsAsync(string sObjectType, bool recentsOnly, listViewIsSoqlCompatible isSoqlCompatible, int limit, int offset, object userState)
    {
        if ((this.describeSObjectListViewsOperationCompleted == null))
        {
            this.describeSObjectListViewsOperationCompleted = new System.Threading.SendOrPostCallback(this.OndescribeSObjectListViewsOperationCompleted);
        }
        this.InvokeAsync("describeSObjectListViews", new object[] {
                        sObjectType,
                        recentsOnly,
                        isSoqlCompatible,
                        limit,
                        offset}, this.describeSObjectListViewsOperationCompleted, userState);
    }

    private void OndescribeSObjectListViewsOperationCompleted(object arg)
    {
        if ((this.describeSObjectListViewsCompleted != null))
        {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.describeSObjectListViewsCompleted(this, new describeSObjectListViewsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }

    /// <remarks/>
    [System.Web.Services.Protocols.SoapHeaderAttribute("PackageVersionHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("CallOptionsValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("LimitInfoHeaderValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapHeaderAttribute("SessionHeaderValue")]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("result", IsNullable = true)]
    public DescribeTabSetResult[] describeTabs()
    {
        object[] results = this.Invoke("describeTabs", new object[0]);
        return ((DescribeTabSetResult[])(results[0]));
    }

    /// <remarks/>
    public void describeTabsAsync()
    {
        this.describeTabsAsync(null);
    }

    /// <remarks/>
    public void describeTabsAsync(object userState)
    {
        if ((this.describeTabsOperationCompleted == null))
        {
            this.describeTabsOperationCompleted = new System.Threading.SendOrPostCallback(this.OndescribeTabsOperationCompleted);
        }
        this.InvokeAsync("describeTabs", new object[0], this.describeTabsOperationCompleted, userState);
    }

    private void OndescribeTabsOperationCompleted(object arg)
    {
        if ((this.describeTabsCompleted != null))
        {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.describeTabsCompleted(this, new describeTabsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }

    /// <remarks/>
    [System.Web.Services.Protocols.SoapHeaderAttribute("PackageVersionHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("CallOptionsValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("LimitInfoHeaderValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapHeaderAttribute("SessionHeaderValue")]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("result", IsNullable = true)]
    public DescribeTab[] describeAllTabs()
    {
        object[] results = this.Invoke("describeAllTabs", new object[0]);
        return ((DescribeTab[])(results[0]));
    }

    /// <remarks/>
    public void describeAllTabsAsync()
    {
        this.describeAllTabsAsync(null);
    }

    /// <remarks/>
    public void describeAllTabsAsync(object userState)
    {
        if ((this.describeAllTabsOperationCompleted == null))
        {
            this.describeAllTabsOperationCompleted = new System.Threading.SendOrPostCallback(this.OndescribeAllTabsOperationCompleted);
        }
        this.InvokeAsync("describeAllTabs", new object[0], this.describeAllTabsOperationCompleted, userState);
    }

    private void OndescribeAllTabsOperationCompleted(object arg)
    {
        if ((this.describeAllTabsCompleted != null))
        {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.describeAllTabsCompleted(this, new describeAllTabsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }

    /// <remarks/>
    [System.Web.Services.Protocols.SoapHeaderAttribute("PackageVersionHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("CallOptionsValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("LimitInfoHeaderValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapHeaderAttribute("SessionHeaderValue")]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("result")]
    public DescribeCompactLayout[] describePrimaryCompactLayouts([System.Xml.Serialization.XmlElementAttribute("sObjectTypes")] string[] sObjectTypes)
    {
        object[] results = this.Invoke("describePrimaryCompactLayouts", new object[] {
                        sObjectTypes});
        return ((DescribeCompactLayout[])(results[0]));
    }

    /// <remarks/>
    public void describePrimaryCompactLayoutsAsync(string[] sObjectTypes)
    {
        this.describePrimaryCompactLayoutsAsync(sObjectTypes, null);
    }

    /// <remarks/>
    public void describePrimaryCompactLayoutsAsync(string[] sObjectTypes, object userState)
    {
        if ((this.describePrimaryCompactLayoutsOperationCompleted == null))
        {
            this.describePrimaryCompactLayoutsOperationCompleted = new System.Threading.SendOrPostCallback(this.OndescribePrimaryCompactLayoutsOperationCompleted);
        }
        this.InvokeAsync("describePrimaryCompactLayouts", new object[] {
                        sObjectTypes}, this.describePrimaryCompactLayoutsOperationCompleted, userState);
    }

    private void OndescribePrimaryCompactLayoutsOperationCompleted(object arg)
    {
        if ((this.describePrimaryCompactLayoutsCompleted != null))
        {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.describePrimaryCompactLayoutsCompleted(this, new describePrimaryCompactLayoutsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }

    /// <remarks/>
    [System.Web.Services.Protocols.SoapHeaderAttribute("PackageVersionHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("LimitInfoHeaderValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapHeaderAttribute("AssignmentRuleHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("MruHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("LocaleOptionsValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("DisableFeedTrackingHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("DebuggingHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("DuplicateRuleHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("CallOptionsValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("EmailHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("AllowFieldTruncationHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("DebuggingInfoValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapHeaderAttribute("AllOrNoneHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("StreamingEnabledHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("SessionHeaderValue")]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("result")]
    public SaveResult[] create([System.Xml.Serialization.XmlElementAttribute("sObjects")] sObject[] sObjects)
    {
        object[] results = this.Invoke("create", new object[] {
                        sObjects});
        return ((SaveResult[])(results[0]));
    }

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

    private void OncreateOperationCompleted(object arg)
    {
        if ((this.createCompleted != null))
        {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.createCompleted(this, new createCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }

    /// <remarks/>
    [System.Web.Services.Protocols.SoapHeaderAttribute("PackageVersionHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("LimitInfoHeaderValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapHeaderAttribute("OwnerChangeOptionsValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("AssignmentRuleHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("MruHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("LocaleOptionsValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("DisableFeedTrackingHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("DebuggingHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("DuplicateRuleHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("CallOptionsValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("EmailHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("AllowFieldTruncationHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("DebuggingInfoValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapHeaderAttribute("AllOrNoneHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("StreamingEnabledHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("SessionHeaderValue")]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("result")]
    public SaveResult[] update([System.Xml.Serialization.XmlElementAttribute("sObjects")] sObject[] sObjects)
    {
        object[] results = this.Invoke("update", new object[] {
                        sObjects});
        return ((SaveResult[])(results[0]));
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

    private void OnupdateOperationCompleted(object arg)
    {
        if ((this.updateCompleted != null))
        {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.updateCompleted(this, new updateCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }

    /// <remarks/>
    [System.Web.Services.Protocols.SoapHeaderAttribute("PackageVersionHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("LimitInfoHeaderValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapHeaderAttribute("OwnerChangeOptionsValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("AssignmentRuleHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("MruHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("LocaleOptionsValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("DisableFeedTrackingHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("DebuggingHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("DuplicateRuleHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("CallOptionsValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("EmailHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("AllowFieldTruncationHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("DebuggingInfoValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapHeaderAttribute("AllOrNoneHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("StreamingEnabledHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("SessionHeaderValue")]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("result")]
    public UpsertResult[] upsert(string externalIDFieldName, [System.Xml.Serialization.XmlElementAttribute("sObjects")] sObject[] sObjects)
    {
        object[] results = this.Invoke("upsert", new object[] {
                        externalIDFieldName,
                        sObjects});
        return ((UpsertResult[])(results[0]));
    }

    /// <remarks/>
    public void upsertAsync(string externalIDFieldName, sObject[] sObjects)
    {
        this.upsertAsync(externalIDFieldName, sObjects, null);
    }

    /// <remarks/>
    public void upsertAsync(string externalIDFieldName, sObject[] sObjects, object userState)
    {
        if ((this.upsertOperationCompleted == null))
        {
            this.upsertOperationCompleted = new System.Threading.SendOrPostCallback(this.OnupsertOperationCompleted);
        }
        this.InvokeAsync("upsert", new object[] {
                        externalIDFieldName,
                        sObjects}, this.upsertOperationCompleted, userState);
    }

    private void OnupsertOperationCompleted(object arg)
    {
        if ((this.upsertCompleted != null))
        {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.upsertCompleted(this, new upsertCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }

    /// <remarks/>
    [System.Web.Services.Protocols.SoapHeaderAttribute("PackageVersionHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("LimitInfoHeaderValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapHeaderAttribute("AssignmentRuleHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("MruHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("LocaleOptionsValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("DisableFeedTrackingHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("DebuggingHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("DuplicateRuleHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("CallOptionsValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("EmailHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("AllowFieldTruncationHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("DebuggingInfoValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapHeaderAttribute("StreamingEnabledHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("SessionHeaderValue")]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("result")]
    public MergeResult[] merge([System.Xml.Serialization.XmlElementAttribute("request")] MergeRequest[] request)
    {
        object[] results = this.Invoke("merge", new object[] {
                        request});
        return ((MergeResult[])(results[0]));
    }

    /// <remarks/>
    public void mergeAsync(MergeRequest[] request)
    {
        this.mergeAsync(request, null);
    }

    /// <remarks/>
    public void mergeAsync(MergeRequest[] request, object userState)
    {
        if ((this.mergeOperationCompleted == null))
        {
            this.mergeOperationCompleted = new System.Threading.SendOrPostCallback(this.OnmergeOperationCompleted);
        }
        this.InvokeAsync("merge", new object[] {
                        request}, this.mergeOperationCompleted, userState);
    }

    private void OnmergeOperationCompleted(object arg)
    {
        if ((this.mergeCompleted != null))
        {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.mergeCompleted(this, new mergeCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }

    /// <remarks/>
    [System.Web.Services.Protocols.SoapHeaderAttribute("PackageVersionHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("LimitInfoHeaderValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapHeaderAttribute("StreamingEnabledHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("LocaleOptionsValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("DisableFeedTrackingHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("UserTerritoryDeleteHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("DebuggingHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("DuplicateRuleHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("CallOptionsValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("EmailHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("AllowFieldTruncationHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("DebuggingInfoValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapHeaderAttribute("AllOrNoneHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("SessionHeaderValue")]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("result")]
    public DeleteResult[] delete([System.Xml.Serialization.XmlElementAttribute("ids")] string[] ids)
    {
        object[] results = this.Invoke("delete", new object[] {
                        ids});
        return ((DeleteResult[])(results[0]));
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

    private void OndeleteOperationCompleted(object arg)
    {
        if ((this.deleteCompleted != null))
        {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.deleteCompleted(this, new deleteCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }

    /// <remarks/>
    [System.Web.Services.Protocols.SoapHeaderAttribute("DebuggingInfoValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapHeaderAttribute("LimitInfoHeaderValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapHeaderAttribute("SessionHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("AllOrNoneHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("StreamingEnabledHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("DebuggingHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("AllowFieldTruncationHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("PackageVersionHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("CallOptionsValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("DisableFeedTrackingHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("DuplicateRuleHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("LocaleOptionsValue")]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("result")]
    public UndeleteResult[] undelete([System.Xml.Serialization.XmlElementAttribute("ids")] string[] ids)
    {
        object[] results = this.Invoke("undelete", new object[] {
                        ids});
        return ((UndeleteResult[])(results[0]));
    }

    /// <remarks/>
    public void undeleteAsync(string[] ids)
    {
        this.undeleteAsync(ids, null);
    }

    /// <remarks/>
    public void undeleteAsync(string[] ids, object userState)
    {
        if ((this.undeleteOperationCompleted == null))
        {
            this.undeleteOperationCompleted = new System.Threading.SendOrPostCallback(this.OnundeleteOperationCompleted);
        }
        this.InvokeAsync("undelete", new object[] {
                        ids}, this.undeleteOperationCompleted, userState);
    }

    private void OnundeleteOperationCompleted(object arg)
    {
        if ((this.undeleteCompleted != null))
        {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.undeleteCompleted(this, new undeleteCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }

    /// <remarks/>
    [System.Web.Services.Protocols.SoapHeaderAttribute("CallOptionsValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("LimitInfoHeaderValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapHeaderAttribute("SessionHeaderValue")]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("result")]
    public EmptyRecycleBinResult[] emptyRecycleBin([System.Xml.Serialization.XmlElementAttribute("ids")] string[] ids)
    {
        object[] results = this.Invoke("emptyRecycleBin", new object[] {
                        ids});
        return ((EmptyRecycleBinResult[])(results[0]));
    }

    /// <remarks/>
    public void emptyRecycleBinAsync(string[] ids)
    {
        this.emptyRecycleBinAsync(ids, null);
    }

    /// <remarks/>
    public void emptyRecycleBinAsync(string[] ids, object userState)
    {
        if ((this.emptyRecycleBinOperationCompleted == null))
        {
            this.emptyRecycleBinOperationCompleted = new System.Threading.SendOrPostCallback(this.OnemptyRecycleBinOperationCompleted);
        }
        this.InvokeAsync("emptyRecycleBin", new object[] {
                        ids}, this.emptyRecycleBinOperationCompleted, userState);
    }

    private void OnemptyRecycleBinOperationCompleted(object arg)
    {
        if ((this.emptyRecycleBinCompleted != null))
        {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.emptyRecycleBinCompleted(this, new emptyRecycleBinCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }

    /// <remarks/>
    [System.Web.Services.Protocols.SoapHeaderAttribute("LimitInfoHeaderValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapHeaderAttribute("SessionHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("MruHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("PackageVersionHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("CallOptionsValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("QueryOptionsValue")]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("result", IsNullable = true)]
    public sObject[] retrieve(string fieldList, string sObjectType, [System.Xml.Serialization.XmlElementAttribute("ids")] string[] ids)
    {
        object[] results = this.Invoke("retrieve", new object[] {
                        fieldList,
                        sObjectType,
                        ids});
        return ((sObject[])(results[0]));
    }

    /// <remarks/>
    public void retrieveAsync(string fieldList, string sObjectType, string[] ids)
    {
        this.retrieveAsync(fieldList, sObjectType, ids, null);
    }

    /// <remarks/>
    public void retrieveAsync(string fieldList, string sObjectType, string[] ids, object userState)
    {
        if ((this.retrieveOperationCompleted == null))
        {
            this.retrieveOperationCompleted = new System.Threading.SendOrPostCallback(this.OnretrieveOperationCompleted);
        }
        this.InvokeAsync("retrieve", new object[] {
                        fieldList,
                        sObjectType,
                        ids}, this.retrieveOperationCompleted, userState);
    }

    private void OnretrieveOperationCompleted(object arg)
    {
        if ((this.retrieveCompleted != null))
        {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.retrieveCompleted(this, new retrieveCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }

    /// <remarks/>
    [System.Web.Services.Protocols.SoapHeaderAttribute("DebuggingInfoValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapHeaderAttribute("LimitInfoHeaderValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapHeaderAttribute("SessionHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("LocaleOptionsValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("StreamingEnabledHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("DebuggingHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("AllowFieldTruncationHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("PackageVersionHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("CallOptionsValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("DisableFeedTrackingHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("DuplicateRuleHeaderValue")]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("result")]
    public ProcessResult[] process([System.Xml.Serialization.XmlElementAttribute("actions")] ProcessRequest[] actions)
    {
        object[] results = this.Invoke("process", new object[] {
                        actions});
        return ((ProcessResult[])(results[0]));
    }

    /// <remarks/>
    public void processAsync(ProcessRequest[] actions)
    {
        this.processAsync(actions, null);
    }

    /// <remarks/>
    public void processAsync(ProcessRequest[] actions, object userState)
    {
        if ((this.processOperationCompleted == null))
        {
            this.processOperationCompleted = new System.Threading.SendOrPostCallback(this.OnprocessOperationCompleted);
        }
        this.InvokeAsync("process", new object[] {
                        actions}, this.processOperationCompleted, userState);
    }

    private void OnprocessOperationCompleted(object arg)
    {
        if ((this.processCompleted != null))
        {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.processCompleted(this, new processCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }

    /// <remarks/>
    [System.Web.Services.Protocols.SoapHeaderAttribute("DebuggingInfoValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapHeaderAttribute("LimitInfoHeaderValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapHeaderAttribute("SessionHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("LocaleOptionsValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("StreamingEnabledHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("DebuggingHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("AllowFieldTruncationHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("PackageVersionHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("CallOptionsValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("DisableFeedTrackingHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("DuplicateRuleHeaderValue")]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("result")]
    public LeadConvertResult[] convertLead([System.Xml.Serialization.XmlElementAttribute("leadConverts")] LeadConvert[] leadConverts)
    {
        object[] results = this.Invoke("convertLead", new object[] {
                        leadConverts});
        return ((LeadConvertResult[])(results[0]));
    }

    /// <remarks/>
    public void convertLeadAsync(LeadConvert[] leadConverts)
    {
        this.convertLeadAsync(leadConverts, null);
    }

    /// <remarks/>
    public void convertLeadAsync(LeadConvert[] leadConverts, object userState)
    {
        if ((this.convertLeadOperationCompleted == null))
        {
            this.convertLeadOperationCompleted = new System.Threading.SendOrPostCallback(this.OnconvertLeadOperationCompleted);
        }
        this.InvokeAsync("convertLead", new object[] {
                        leadConverts}, this.convertLeadOperationCompleted, userState);
    }

    private void OnconvertLeadOperationCompleted(object arg)
    {
        if ((this.convertLeadCompleted != null))
        {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.convertLeadCompleted(this, new convertLeadCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }

    /// <remarks/>
    [System.Web.Services.Protocols.SoapHeaderAttribute("CallOptionsValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("LimitInfoHeaderValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapHeaderAttribute("SessionHeaderValue")]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    public void logout()
    {
        this.Invoke("logout", new object[0]);
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

    private void OnlogoutOperationCompleted(object arg)
    {
        if ((this.logoutCompleted != null))
        {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.logoutCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }

    /// <remarks/>
    [System.Web.Services.Protocols.SoapHeaderAttribute("CallOptionsValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("LimitInfoHeaderValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapHeaderAttribute("SessionHeaderValue")]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("result")]
    public InvalidateSessionsResult[] invalidateSessions([System.Xml.Serialization.XmlElementAttribute("sessionIds")] string[] sessionIds)
    {
        object[] results = this.Invoke("invalidateSessions", new object[] {
                        sessionIds});
        return ((InvalidateSessionsResult[])(results[0]));
    }

    /// <remarks/>
    public void invalidateSessionsAsync(string[] sessionIds)
    {
        this.invalidateSessionsAsync(sessionIds, null);
    }

    /// <remarks/>
    public void invalidateSessionsAsync(string[] sessionIds, object userState)
    {
        if ((this.invalidateSessionsOperationCompleted == null))
        {
            this.invalidateSessionsOperationCompleted = new System.Threading.SendOrPostCallback(this.OninvalidateSessionsOperationCompleted);
        }
        this.InvokeAsync("invalidateSessions", new object[] {
                        sessionIds}, this.invalidateSessionsOperationCompleted, userState);
    }

    private void OninvalidateSessionsOperationCompleted(object arg)
    {
        if ((this.invalidateSessionsCompleted != null))
        {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.invalidateSessionsCompleted(this, new invalidateSessionsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }

    /// <remarks/>
    [System.Web.Services.Protocols.SoapHeaderAttribute("CallOptionsValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("LimitInfoHeaderValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapHeaderAttribute("SessionHeaderValue")]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("result")]
    public GetDeletedResult getDeleted(string sObjectType, System.DateTime startDate, System.DateTime endDate)
    {
        object[] results = this.Invoke("getDeleted", new object[] {
                        sObjectType,
                        startDate,
                        endDate});
        return ((GetDeletedResult)(results[0]));
    }

    /// <remarks/>
    public void getDeletedAsync(string sObjectType, System.DateTime startDate, System.DateTime endDate)
    {
        this.getDeletedAsync(sObjectType, startDate, endDate, null);
    }

    /// <remarks/>
    public void getDeletedAsync(string sObjectType, System.DateTime startDate, System.DateTime endDate, object userState)
    {
        if ((this.getDeletedOperationCompleted == null))
        {
            this.getDeletedOperationCompleted = new System.Threading.SendOrPostCallback(this.OngetDeletedOperationCompleted);
        }
        this.InvokeAsync("getDeleted", new object[] {
                        sObjectType,
                        startDate,
                        endDate}, this.getDeletedOperationCompleted, userState);
    }

    private void OngetDeletedOperationCompleted(object arg)
    {
        if ((this.getDeletedCompleted != null))
        {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.getDeletedCompleted(this, new getDeletedCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }

    /// <remarks/>
    [System.Web.Services.Protocols.SoapHeaderAttribute("CallOptionsValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("LimitInfoHeaderValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapHeaderAttribute("SessionHeaderValue")]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("result")]
    public GetUpdatedResult getUpdated(string sObjectType, System.DateTime startDate, System.DateTime endDate)
    {
        object[] results = this.Invoke("getUpdated", new object[] {
                        sObjectType,
                        startDate,
                        endDate});
        return ((GetUpdatedResult)(results[0]));
    }

    /// <remarks/>
    public void getUpdatedAsync(string sObjectType, System.DateTime startDate, System.DateTime endDate)
    {
        this.getUpdatedAsync(sObjectType, startDate, endDate, null);
    }

    /// <remarks/>
    public void getUpdatedAsync(string sObjectType, System.DateTime startDate, System.DateTime endDate, object userState)
    {
        if ((this.getUpdatedOperationCompleted == null))
        {
            this.getUpdatedOperationCompleted = new System.Threading.SendOrPostCallback(this.OngetUpdatedOperationCompleted);
        }
        this.InvokeAsync("getUpdated", new object[] {
                        sObjectType,
                        startDate,
                        endDate}, this.getUpdatedOperationCompleted, userState);
    }

    private void OngetUpdatedOperationCompleted(object arg)
    {
        if ((this.getUpdatedCompleted != null))
        {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.getUpdatedCompleted(this, new getUpdatedCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }

    /// <remarks/>
    [System.Web.Services.Protocols.SoapHeaderAttribute("LimitInfoHeaderValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapHeaderAttribute("SessionHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("MruHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("PackageVersionHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("CallOptionsValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("QueryOptionsValue")]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("result")]
    public QueryResult query(string queryString)
    {
        object[] results = this.Invoke("query", new object[] {
                        queryString});
        return ((QueryResult)(results[0]));
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

    private void OnqueryOperationCompleted(object arg)
    {
        if ((this.queryCompleted != null))
        {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.queryCompleted(this, new queryCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }

    /// <remarks/>
    [System.Web.Services.Protocols.SoapHeaderAttribute("CallOptionsValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("QueryOptionsValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("LimitInfoHeaderValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapHeaderAttribute("SessionHeaderValue")]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("result")]
    public QueryResult queryAll(string queryString)
    {
        object[] results = this.Invoke("queryAll", new object[] {
                        queryString});
        return ((QueryResult)(results[0]));
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

    private void OnqueryAllOperationCompleted(object arg)
    {
        if ((this.queryAllCompleted != null))
        {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.queryAllCompleted(this, new queryAllCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }

    /// <remarks/>
    [System.Web.Services.Protocols.SoapHeaderAttribute("CallOptionsValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("QueryOptionsValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("LimitInfoHeaderValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapHeaderAttribute("SessionHeaderValue")]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("result")]
    public QueryResult queryMore(string queryLocator)
    {
        object[] results = this.Invoke("queryMore", new object[] {
                        queryLocator});
        return ((QueryResult)(results[0]));
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

    private void OnqueryMoreOperationCompleted(object arg)
    {
        if ((this.queryMoreCompleted != null))
        {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.queryMoreCompleted(this, new queryMoreCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }

    /// <remarks/>
    [System.Web.Services.Protocols.SoapHeaderAttribute("PackageVersionHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("CallOptionsValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("LimitInfoHeaderValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapHeaderAttribute("SessionHeaderValue")]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("result")]
    public SearchResult search(string searchString)
    {
        object[] results = this.Invoke("search", new object[] {
                        searchString});
        return ((SearchResult)(results[0]));
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

    private void OnsearchOperationCompleted(object arg)
    {
        if ((this.searchCompleted != null))
        {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.searchCompleted(this, new searchCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }

    /// <remarks/>
    [System.Web.Services.Protocols.SoapHeaderAttribute("CallOptionsValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("LimitInfoHeaderValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapHeaderAttribute("SessionHeaderValue")]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("result")]
    public GetServerTimestampResult getServerTimestamp()
    {
        object[] results = this.Invoke("getServerTimestamp", new object[0]);
        return ((GetServerTimestampResult)(results[0]));
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

    private void OngetServerTimestampOperationCompleted(object arg)
    {
        if ((this.getServerTimestampCompleted != null))
        {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.getServerTimestampCompleted(this, new getServerTimestampCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }

    /// <remarks/>
    [System.Web.Services.Protocols.SoapHeaderAttribute("CallOptionsValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("LimitInfoHeaderValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapHeaderAttribute("SessionHeaderValue")]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("result")]
    public SetPasswordResult setPassword(string userId, string password)
    {
        object[] results = this.Invoke("setPassword", new object[] {
                        userId,
                        password});
        return ((SetPasswordResult)(results[0]));
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

    private void OnsetPasswordOperationCompleted(object arg)
    {
        if ((this.setPasswordCompleted != null))
        {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.setPasswordCompleted(this, new setPasswordCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }

    /// <remarks/>
    [System.Web.Services.Protocols.SoapHeaderAttribute("CallOptionsValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("LimitInfoHeaderValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapHeaderAttribute("SessionHeaderValue")]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("result")]
    public ChangeOwnPasswordResult changeOwnPassword(string oldPassword, string newPassword)
    {
        object[] results = this.Invoke("changeOwnPassword", new object[] {
                        oldPassword,
                        newPassword});
        return ((ChangeOwnPasswordResult)(results[0]));
    }

    /// <remarks/>
    public void changeOwnPasswordAsync(string oldPassword, string newPassword)
    {
        this.changeOwnPasswordAsync(oldPassword, newPassword, null);
    }

    /// <remarks/>
    public void changeOwnPasswordAsync(string oldPassword, string newPassword, object userState)
    {
        if ((this.changeOwnPasswordOperationCompleted == null))
        {
            this.changeOwnPasswordOperationCompleted = new System.Threading.SendOrPostCallback(this.OnchangeOwnPasswordOperationCompleted);
        }
        this.InvokeAsync("changeOwnPassword", new object[] {
                        oldPassword,
                        newPassword}, this.changeOwnPasswordOperationCompleted, userState);
    }

    private void OnchangeOwnPasswordOperationCompleted(object arg)
    {
        if ((this.changeOwnPasswordCompleted != null))
        {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.changeOwnPasswordCompleted(this, new changeOwnPasswordCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }

    /// <remarks/>
    [System.Web.Services.Protocols.SoapHeaderAttribute("CallOptionsValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("LimitInfoHeaderValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapHeaderAttribute("EmailHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("SessionHeaderValue")]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("result")]
    public ResetPasswordResult resetPassword(string userId)
    {
        object[] results = this.Invoke("resetPassword", new object[] {
                        userId});
        return ((ResetPasswordResult)(results[0]));
    }

    /// <remarks/>
    public void resetPasswordAsync(string userId)
    {
        this.resetPasswordAsync(userId, null);
    }

    /// <remarks/>
    public void resetPasswordAsync(string userId, object userState)
    {
        if ((this.resetPasswordOperationCompleted == null))
        {
            this.resetPasswordOperationCompleted = new System.Threading.SendOrPostCallback(this.OnresetPasswordOperationCompleted);
        }
        this.InvokeAsync("resetPassword", new object[] {
                        userId}, this.resetPasswordOperationCompleted, userState);
    }

    private void OnresetPasswordOperationCompleted(object arg)
    {
        if ((this.resetPasswordCompleted != null))
        {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.resetPasswordCompleted(this, new resetPasswordCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }

    /// <remarks/>
    [System.Web.Services.Protocols.SoapHeaderAttribute("CallOptionsValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("LimitInfoHeaderValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapHeaderAttribute("SessionHeaderValue")]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("result")]
    public GetUserInfoResult getUserInfo()
    {
        object[] results = this.Invoke("getUserInfo", new object[0]);
        return ((GetUserInfoResult)(results[0]));
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

    private void OngetUserInfoOperationCompleted(object arg)
    {
        if ((this.getUserInfoCompleted != null))
        {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.getUserInfoCompleted(this, new getUserInfoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }

    /// <remarks/>
    [System.Web.Services.Protocols.SoapHeaderAttribute("PackageVersionHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("LimitInfoHeaderValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapHeaderAttribute("StreamingEnabledHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("LocaleOptionsValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("DisableFeedTrackingHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("UserTerritoryDeleteHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("DebuggingHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("DuplicateRuleHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("CallOptionsValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("EmailHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("AllowFieldTruncationHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("DebuggingInfoValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapHeaderAttribute("AllOrNoneHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("SessionHeaderValue")]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("result")]
    public DeleteByExampleResult[] deleteByExample([System.Xml.Serialization.XmlElementAttribute("sObjects")] sObject[] sObjects)
    {
        object[] results = this.Invoke("deleteByExample", new object[] {
                        sObjects});
        return ((DeleteByExampleResult[])(results[0]));
    }

    /// <remarks/>
    public void deleteByExampleAsync(sObject[] sObjects)
    {
        this.deleteByExampleAsync(sObjects, null);
    }

    /// <remarks/>
    public void deleteByExampleAsync(sObject[] sObjects, object userState)
    {
        if ((this.deleteByExampleOperationCompleted == null))
        {
            this.deleteByExampleOperationCompleted = new System.Threading.SendOrPostCallback(this.OndeleteByExampleOperationCompleted);
        }
        this.InvokeAsync("deleteByExample", new object[] {
                        sObjects}, this.deleteByExampleOperationCompleted, userState);
    }

    private void OndeleteByExampleOperationCompleted(object arg)
    {
        if ((this.deleteByExampleCompleted != null))
        {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.deleteByExampleCompleted(this, new deleteByExampleCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }

    /// <remarks/>
    [System.Web.Services.Protocols.SoapHeaderAttribute("CallOptionsValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("LimitInfoHeaderValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapHeaderAttribute("SessionHeaderValue")]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("result")]
    public SendEmailResult[] sendEmailMessage([System.Xml.Serialization.XmlElementAttribute("ids")] string[] ids)
    {
        object[] results = this.Invoke("sendEmailMessage", new object[] {
                        ids});
        return ((SendEmailResult[])(results[0]));
    }

    /// <remarks/>
    public void sendEmailMessageAsync(string[] ids)
    {
        this.sendEmailMessageAsync(ids, null);
    }

    /// <remarks/>
    public void sendEmailMessageAsync(string[] ids, object userState)
    {
        if ((this.sendEmailMessageOperationCompleted == null))
        {
            this.sendEmailMessageOperationCompleted = new System.Threading.SendOrPostCallback(this.OnsendEmailMessageOperationCompleted);
        }
        this.InvokeAsync("sendEmailMessage", new object[] {
                        ids}, this.sendEmailMessageOperationCompleted, userState);
    }

    private void OnsendEmailMessageOperationCompleted(object arg)
    {
        if ((this.sendEmailMessageCompleted != null))
        {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.sendEmailMessageCompleted(this, new sendEmailMessageCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }

    /// <remarks/>
    [System.Web.Services.Protocols.SoapHeaderAttribute("CallOptionsValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("LimitInfoHeaderValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapHeaderAttribute("SessionHeaderValue")]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("result")]
    public SendEmailResult[] sendEmail([System.Xml.Serialization.XmlElementAttribute("messages")] Email[] messages)
    {
        object[] results = this.Invoke("sendEmail", new object[] {
                        messages});
        return ((SendEmailResult[])(results[0]));
    }

    /// <remarks/>
    public void sendEmailAsync(Email[] messages)
    {
        this.sendEmailAsync(messages, null);
    }

    /// <remarks/>
    public void sendEmailAsync(Email[] messages, object userState)
    {
        if ((this.sendEmailOperationCompleted == null))
        {
            this.sendEmailOperationCompleted = new System.Threading.SendOrPostCallback(this.OnsendEmailOperationCompleted);
        }
        this.InvokeAsync("sendEmail", new object[] {
                        messages}, this.sendEmailOperationCompleted, userState);
    }

    private void OnsendEmailOperationCompleted(object arg)
    {
        if ((this.sendEmailCompleted != null))
        {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.sendEmailCompleted(this, new sendEmailCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }

    /// <remarks/>
    [System.Web.Services.Protocols.SoapHeaderAttribute("CallOptionsValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("LimitInfoHeaderValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapHeaderAttribute("SessionHeaderValue")]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("result")]
    public RenderEmailTemplateResult[] renderEmailTemplate([System.Xml.Serialization.XmlElementAttribute("renderRequests")] RenderEmailTemplateRequest[] renderRequests)
    {
        object[] results = this.Invoke("renderEmailTemplate", new object[] {
                        renderRequests});
        return ((RenderEmailTemplateResult[])(results[0]));
    }

    /// <remarks/>
    public void renderEmailTemplateAsync(RenderEmailTemplateRequest[] renderRequests)
    {
        this.renderEmailTemplateAsync(renderRequests, null);
    }

    /// <remarks/>
    public void renderEmailTemplateAsync(RenderEmailTemplateRequest[] renderRequests, object userState)
    {
        if ((this.renderEmailTemplateOperationCompleted == null))
        {
            this.renderEmailTemplateOperationCompleted = new System.Threading.SendOrPostCallback(this.OnrenderEmailTemplateOperationCompleted);
        }
        this.InvokeAsync("renderEmailTemplate", new object[] {
                        renderRequests}, this.renderEmailTemplateOperationCompleted, userState);
    }

    private void OnrenderEmailTemplateOperationCompleted(object arg)
    {
        if ((this.renderEmailTemplateCompleted != null))
        {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.renderEmailTemplateCompleted(this, new renderEmailTemplateCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }

    /// <remarks/>
    [System.Web.Services.Protocols.SoapHeaderAttribute("CallOptionsValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("LimitInfoHeaderValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapHeaderAttribute("SessionHeaderValue")]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("result")]
    public RenderStoredEmailTemplateResult renderStoredEmailTemplate(RenderStoredEmailTemplateRequest request)
    {
        object[] results = this.Invoke("renderStoredEmailTemplate", new object[] {
                        request});
        return ((RenderStoredEmailTemplateResult)(results[0]));
    }

    /// <remarks/>
    public void renderStoredEmailTemplateAsync(RenderStoredEmailTemplateRequest request)
    {
        this.renderStoredEmailTemplateAsync(request, null);
    }

    /// <remarks/>
    public void renderStoredEmailTemplateAsync(RenderStoredEmailTemplateRequest request, object userState)
    {
        if ((this.renderStoredEmailTemplateOperationCompleted == null))
        {
            this.renderStoredEmailTemplateOperationCompleted = new System.Threading.SendOrPostCallback(this.OnrenderStoredEmailTemplateOperationCompleted);
        }
        this.InvokeAsync("renderStoredEmailTemplate", new object[] {
                        request}, this.renderStoredEmailTemplateOperationCompleted, userState);
    }

    private void OnrenderStoredEmailTemplateOperationCompleted(object arg)
    {
        if ((this.renderStoredEmailTemplateCompleted != null))
        {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.renderStoredEmailTemplateCompleted(this, new renderStoredEmailTemplateCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }

    /// <remarks/>
    [System.Web.Services.Protocols.SoapHeaderAttribute("PackageVersionHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("LimitInfoHeaderValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapHeaderAttribute("OwnerChangeOptionsValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("AssignmentRuleHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("MruHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("LocaleOptionsValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("DisableFeedTrackingHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("DebuggingHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("DuplicateRuleHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("CallOptionsValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("EmailHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("AllowFieldTruncationHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("AllOrNoneHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("StreamingEnabledHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("SessionHeaderValue")]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("result")]
    public PerformQuickActionResult[] performQuickActions([System.Xml.Serialization.XmlElementAttribute("quickActions")] PerformQuickActionRequest[] quickActions)
    {
        object[] results = this.Invoke("performQuickActions", new object[] {
                        quickActions});
        return ((PerformQuickActionResult[])(results[0]));
    }

    /// <remarks/>
    public void performQuickActionsAsync(PerformQuickActionRequest[] quickActions)
    {
        this.performQuickActionsAsync(quickActions, null);
    }

    /// <remarks/>
    public void performQuickActionsAsync(PerformQuickActionRequest[] quickActions, object userState)
    {
        if ((this.performQuickActionsOperationCompleted == null))
        {
            this.performQuickActionsOperationCompleted = new System.Threading.SendOrPostCallback(this.OnperformQuickActionsOperationCompleted);
        }
        this.InvokeAsync("performQuickActions", new object[] {
                        quickActions}, this.performQuickActionsOperationCompleted, userState);
    }

    private void OnperformQuickActionsOperationCompleted(object arg)
    {
        if ((this.performQuickActionsCompleted != null))
        {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.performQuickActionsCompleted(this, new performQuickActionsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }

    /// <remarks/>
    [System.Web.Services.Protocols.SoapHeaderAttribute("LocaleOptionsValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("PackageVersionHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("CallOptionsValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("LimitInfoHeaderValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapHeaderAttribute("SessionHeaderValue")]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("result")]
    public DescribeQuickActionResult[] describeQuickActions([System.Xml.Serialization.XmlElementAttribute("quickActions")] string[] quickActions)
    {
        object[] results = this.Invoke("describeQuickActions", new object[] {
                        quickActions});
        return ((DescribeQuickActionResult[])(results[0]));
    }

    /// <remarks/>
    public void describeQuickActionsAsync(string[] quickActions)
    {
        this.describeQuickActionsAsync(quickActions, null);
    }

    /// <remarks/>
    public void describeQuickActionsAsync(string[] quickActions, object userState)
    {
        if ((this.describeQuickActionsOperationCompleted == null))
        {
            this.describeQuickActionsOperationCompleted = new System.Threading.SendOrPostCallback(this.OndescribeQuickActionsOperationCompleted);
        }
        this.InvokeAsync("describeQuickActions", new object[] {
                        quickActions}, this.describeQuickActionsOperationCompleted, userState);
    }

    private void OndescribeQuickActionsOperationCompleted(object arg)
    {
        if ((this.describeQuickActionsCompleted != null))
        {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.describeQuickActionsCompleted(this, new describeQuickActionsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }

    /// <remarks/>
    [System.Web.Services.Protocols.SoapHeaderAttribute("LocaleOptionsValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("PackageVersionHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("CallOptionsValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("LimitInfoHeaderValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapHeaderAttribute("SessionHeaderValue")]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("result")]
    public DescribeQuickActionResult[] describeQuickActionsForRecordType([System.Xml.Serialization.XmlElementAttribute("quickActions")] string[] quickActions, string recordTypeId)
    {
        object[] results = this.Invoke("describeQuickActionsForRecordType", new object[] {
                        quickActions,
                        recordTypeId});
        return ((DescribeQuickActionResult[])(results[0]));
    }

    /// <remarks/>
    public void describeQuickActionsForRecordTypeAsync(string[] quickActions, string recordTypeId)
    {
        this.describeQuickActionsForRecordTypeAsync(quickActions, recordTypeId, null);
    }

    /// <remarks/>
    public void describeQuickActionsForRecordTypeAsync(string[] quickActions, string recordTypeId, object userState)
    {
        if ((this.describeQuickActionsForRecordTypeOperationCompleted == null))
        {
            this.describeQuickActionsForRecordTypeOperationCompleted = new System.Threading.SendOrPostCallback(this.OndescribeQuickActionsForRecordTypeOperationCompleted);
        }
        this.InvokeAsync("describeQuickActionsForRecordType", new object[] {
                        quickActions,
                        recordTypeId}, this.describeQuickActionsForRecordTypeOperationCompleted, userState);
    }

    private void OndescribeQuickActionsForRecordTypeOperationCompleted(object arg)
    {
        if ((this.describeQuickActionsForRecordTypeCompleted != null))
        {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.describeQuickActionsForRecordTypeCompleted(this, new describeQuickActionsForRecordTypeCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }

    /// <remarks/>
    [System.Web.Services.Protocols.SoapHeaderAttribute("LocaleOptionsValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("PackageVersionHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("CallOptionsValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("LimitInfoHeaderValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapHeaderAttribute("SessionHeaderValue")]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("result")]
    public DescribeAvailableQuickActionResult[] describeAvailableQuickActions([System.Xml.Serialization.XmlElementAttribute(IsNullable = true)] string contextType)
    {
        object[] results = this.Invoke("describeAvailableQuickActions", new object[] {
                        contextType});
        return ((DescribeAvailableQuickActionResult[])(results[0]));
    }

    /// <remarks/>
    public void describeAvailableQuickActionsAsync(string contextType)
    {
        this.describeAvailableQuickActionsAsync(contextType, null);
    }

    /// <remarks/>
    public void describeAvailableQuickActionsAsync(string contextType, object userState)
    {
        if ((this.describeAvailableQuickActionsOperationCompleted == null))
        {
            this.describeAvailableQuickActionsOperationCompleted = new System.Threading.SendOrPostCallback(this.OndescribeAvailableQuickActionsOperationCompleted);
        }
        this.InvokeAsync("describeAvailableQuickActions", new object[] {
                        contextType}, this.describeAvailableQuickActionsOperationCompleted, userState);
    }

    private void OndescribeAvailableQuickActionsOperationCompleted(object arg)
    {
        if ((this.describeAvailableQuickActionsCompleted != null))
        {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.describeAvailableQuickActionsCompleted(this, new describeAvailableQuickActionsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }

    /// <remarks/>
    [System.Web.Services.Protocols.SoapHeaderAttribute("LocaleOptionsValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("PackageVersionHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("CallOptionsValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("LimitInfoHeaderValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapHeaderAttribute("SessionHeaderValue")]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("result")]
    public QuickActionTemplateResult[] retrieveQuickActionTemplates([System.Xml.Serialization.XmlElementAttribute("quickActionNames")] string[] quickActionNames, [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)] string contextId)
    {
        object[] results = this.Invoke("retrieveQuickActionTemplates", new object[] {
                        quickActionNames,
                        contextId});
        return ((QuickActionTemplateResult[])(results[0]));
    }

    /// <remarks/>
    public void retrieveQuickActionTemplatesAsync(string[] quickActionNames, string contextId)
    {
        this.retrieveQuickActionTemplatesAsync(quickActionNames, contextId, null);
    }

    /// <remarks/>
    public void retrieveQuickActionTemplatesAsync(string[] quickActionNames, string contextId, object userState)
    {
        if ((this.retrieveQuickActionTemplatesOperationCompleted == null))
        {
            this.retrieveQuickActionTemplatesOperationCompleted = new System.Threading.SendOrPostCallback(this.OnretrieveQuickActionTemplatesOperationCompleted);
        }
        this.InvokeAsync("retrieveQuickActionTemplates", new object[] {
                        quickActionNames,
                        contextId}, this.retrieveQuickActionTemplatesOperationCompleted, userState);
    }

    private void OnretrieveQuickActionTemplatesOperationCompleted(object arg)
    {
        if ((this.retrieveQuickActionTemplatesCompleted != null))
        {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.retrieveQuickActionTemplatesCompleted(this, new retrieveQuickActionTemplatesCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }

    /// <remarks/>
    [System.Web.Services.Protocols.SoapHeaderAttribute("LocaleOptionsValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("PackageVersionHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("CallOptionsValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("LimitInfoHeaderValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapHeaderAttribute("SessionHeaderValue")]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("result")]
    public QuickActionTemplateResult[] retrieveMassQuickActionTemplates(string quickActionName, [System.Xml.Serialization.XmlElementAttribute("contextIds")] string[] contextIds)
    {
        object[] results = this.Invoke("retrieveMassQuickActionTemplates", new object[] {
                        quickActionName,
                        contextIds});
        return ((QuickActionTemplateResult[])(results[0]));
    }

    /// <remarks/>
    public void retrieveMassQuickActionTemplatesAsync(string quickActionName, string[] contextIds)
    {
        this.retrieveMassQuickActionTemplatesAsync(quickActionName, contextIds, null);
    }

    /// <remarks/>
    public void retrieveMassQuickActionTemplatesAsync(string quickActionName, string[] contextIds, object userState)
    {
        if ((this.retrieveMassQuickActionTemplatesOperationCompleted == null))
        {
            this.retrieveMassQuickActionTemplatesOperationCompleted = new System.Threading.SendOrPostCallback(this.OnretrieveMassQuickActionTemplatesOperationCompleted);
        }
        this.InvokeAsync("retrieveMassQuickActionTemplates", new object[] {
                        quickActionName,
                        contextIds}, this.retrieveMassQuickActionTemplatesOperationCompleted, userState);
    }

    private void OnretrieveMassQuickActionTemplatesOperationCompleted(object arg)
    {
        if ((this.retrieveMassQuickActionTemplatesCompleted != null))
        {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.retrieveMassQuickActionTemplatesCompleted(this, new retrieveMassQuickActionTemplatesCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }

    /// <remarks/>
    [System.Web.Services.Protocols.SoapHeaderAttribute("PackageVersionHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("CallOptionsValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("LimitInfoHeaderValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapHeaderAttribute("SessionHeaderValue")]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("result")]
    public DescribeVisualForceResult describeVisualForce(bool includeAllDetails, [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)] string namespacePrefix)
    {
        object[] results = this.Invoke("describeVisualForce", new object[] {
                        includeAllDetails,
                        namespacePrefix});
        return ((DescribeVisualForceResult)(results[0]));
    }

    /// <remarks/>
    public void describeVisualForceAsync(bool includeAllDetails, string namespacePrefix)
    {
        this.describeVisualForceAsync(includeAllDetails, namespacePrefix, null);
    }

    /// <remarks/>
    public void describeVisualForceAsync(bool includeAllDetails, string namespacePrefix, object userState)
    {
        if ((this.describeVisualForceOperationCompleted == null))
        {
            this.describeVisualForceOperationCompleted = new System.Threading.SendOrPostCallback(this.OndescribeVisualForceOperationCompleted);
        }
        this.InvokeAsync("describeVisualForce", new object[] {
                        includeAllDetails,
                        namespacePrefix}, this.describeVisualForceOperationCompleted, userState);
    }

    private void OndescribeVisualForceOperationCompleted(object arg)
    {
        if ((this.describeVisualForceCompleted != null))
        {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.describeVisualForceCompleted(this, new describeVisualForceCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }

    /// <remarks/>
    [System.Web.Services.Protocols.SoapHeaderAttribute("PackageVersionHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("CallOptionsValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("DuplicateRuleHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("LimitInfoHeaderValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapHeaderAttribute("SessionHeaderValue")]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("result")]
    public FindDuplicatesResult[] findDuplicates([System.Xml.Serialization.XmlElementAttribute("sObjects")] sObject[] sObjects)
    {
        object[] results = this.Invoke("findDuplicates", new object[] {
                        sObjects});
        return ((FindDuplicatesResult[])(results[0]));
    }

    /// <remarks/>
    public void findDuplicatesAsync(sObject[] sObjects)
    {
        this.findDuplicatesAsync(sObjects, null);
    }

    /// <remarks/>
    public void findDuplicatesAsync(sObject[] sObjects, object userState)
    {
        if ((this.findDuplicatesOperationCompleted == null))
        {
            this.findDuplicatesOperationCompleted = new System.Threading.SendOrPostCallback(this.OnfindDuplicatesOperationCompleted);
        }
        this.InvokeAsync("findDuplicates", new object[] {
                        sObjects}, this.findDuplicatesOperationCompleted, userState);
    }

    private void OnfindDuplicatesOperationCompleted(object arg)
    {
        if ((this.findDuplicatesCompleted != null))
        {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.findDuplicatesCompleted(this, new findDuplicatesCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }

    /// <remarks/>
    [System.Web.Services.Protocols.SoapHeaderAttribute("PackageVersionHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("CallOptionsValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("DuplicateRuleHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("LimitInfoHeaderValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapHeaderAttribute("SessionHeaderValue")]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("result")]
    public FindDuplicatesResult[] findDuplicatesByIds([System.Xml.Serialization.XmlElementAttribute("ids")] string[] ids)
    {
        object[] results = this.Invoke("findDuplicatesByIds", new object[] {
                        ids});
        return ((FindDuplicatesResult[])(results[0]));
    }

    /// <remarks/>
    public void findDuplicatesByIdsAsync(string[] ids)
    {
        this.findDuplicatesByIdsAsync(ids, null);
    }

    /// <remarks/>
    public void findDuplicatesByIdsAsync(string[] ids, object userState)
    {
        if ((this.findDuplicatesByIdsOperationCompleted == null))
        {
            this.findDuplicatesByIdsOperationCompleted = new System.Threading.SendOrPostCallback(this.OnfindDuplicatesByIdsOperationCompleted);
        }
        this.InvokeAsync("findDuplicatesByIds", new object[] {
                        ids}, this.findDuplicatesByIdsOperationCompleted, userState);
    }

    private void OnfindDuplicatesByIdsOperationCompleted(object arg)
    {
        if ((this.findDuplicatesByIdsCompleted != null))
        {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.findDuplicatesByIdsCompleted(this, new findDuplicatesByIdsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }

    /// <remarks/>
    [System.Web.Services.Protocols.SoapHeaderAttribute("LocaleOptionsValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("PackageVersionHeaderValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("CallOptionsValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("LimitInfoHeaderValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapHeaderAttribute("SessionHeaderValue")]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("result", IsNullable = true)]
    public DescribeNounResult[] describeNouns([System.Xml.Serialization.XmlElementAttribute("nouns")] string[] nouns, bool onlyRenamed, bool includeFields)
    {
        object[] results = this.Invoke("describeNouns", new object[] {
                        nouns,
                        onlyRenamed,
                        includeFields});
        return ((DescribeNounResult[])(results[0]));
    }

    /// <remarks/>
    public void describeNounsAsync(string[] nouns, bool onlyRenamed, bool includeFields)
    {
        this.describeNounsAsync(nouns, onlyRenamed, includeFields, null);
    }

    /// <remarks/>
    public void describeNounsAsync(string[] nouns, bool onlyRenamed, bool includeFields, object userState)
    {
        if ((this.describeNounsOperationCompleted == null))
        {
            this.describeNounsOperationCompleted = new System.Threading.SendOrPostCallback(this.OndescribeNounsOperationCompleted);
        }
        this.InvokeAsync("describeNouns", new object[] {
                        nouns,
                        onlyRenamed,
                        includeFields}, this.describeNounsOperationCompleted, userState);
    }

    private void OndescribeNounsOperationCompleted(object arg)
    {
        if ((this.describeNounsCompleted != null))
        {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.describeNounsCompleted(this, new describeNounsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }

    /// <remarks/>
    public new void CancelAsync(object userState)
    {
        base.CancelAsync(userState);
    }

    private bool IsLocalFileSystemWebService(string url)
    {
        if (((url == null)
                    || (url == string.Empty)))
        {
            return false;
        }
        System.Uri wsUri = new System.Uri(url);
        if (((wsUri.Port >= 1024)
                    && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0)))
        {
            return true;
        }
        return false;
    }
}
}