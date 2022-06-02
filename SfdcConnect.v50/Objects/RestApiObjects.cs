/****************************************************************************
*
*   File name: DataObjects\RestApiObjects.cs
*   Author: Sean Fife
*   Create date: 5/21/2022
*   Solution: SfdcConnect
*   Project: SfdcConnect
*   Description: Includes Objects returned from the REST API
*
****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SfdcConnect.Rest
{
    //DescribeGlobalResult
    public class DescribeGlobalResult
    {
        public string encoding { get; set; }
        public int maxBatchSize { get; set; }
        public List<DescribeSObjectResult> sobjects { get; set; }
    }

    public class DescribeSObjectResult
    {
        public bool activateable { get; set; }
        public object associateEntityType { get; set; }
        public object associateParentEntity { get; set; }
        public bool createable { get; set; }
        public bool custom { get; set; }
        public bool customSetting { get; set; }
        public bool deepCloneable { get; set; }
        public bool deletable { get; set; }
        public bool deprecatedAndHidden { get; set; }
        public bool feedEnabled { get; set; }
        public bool hasSubtypes { get; set; }
        public bool isInterface { get; set; }
        public bool isSubtype { get; set; }
        public string keyPrefix { get; set; }
        public string label { get; set; }
        public string labelPlural { get; set; }
        public bool layoutable { get; set; }
        public bool mergeable { get; set; }
        public bool mruEnabled { get; set; }
        public string name { get; set; }
        public bool queryable { get; set; }
        public bool replicateable { get; set; }
        public bool retrieveable { get; set; }
        public bool searchable { get; set; }
        public bool triggerable { get; set; }
        public bool undeletable { get; set; }
        public bool updateable { get; set; }
        public Urls urls { get; set; }


        public List<ActionOverride> actionOverrides { get; set; }
        public List<ChildRelationship> childRelationships { get; set; }
        public bool compactLayoutable { get; set; }
        public object defaultImplementation { get; set; }
        public object extendedBy { get; set; }
        public object extendsInterfaces { get; set; }
        public List<Field> fields { get; set; }
        public object implementedBy { get; set; }
        public object implementsInterfaces { get; set; }
        public object listviewable { get; set; }
        public object lookupLayoutable { get; set; }
        public List<NamedLayoutInfo> namedLayoutInfos { get; set; }
        public object networkScopeFieldName { get; set; }
        public List<RecordTypeInfo> recordTypeInfos { get; set; }
        public bool searchLayoutable { get; set; }
        public string sobjectDescribeOption { get; set; }
        public List<ScopeInfo> supportedScopes { get; set; }
    }

    /// <summary>
    /// This class doesn't seem to exist in the SOAP API anywhere, not sure what it's name is...
    /// </summary>
    public class GetSObjectResult
    {
        public DescribeSObjectResult objectDescribe { get; set; }
        public List<object> recentItems { get; set; }
    }

    //Is there a performance hit for using dyanmic for this???
    public class sObject : Dictionary<string, dynamic>
    {
        /*public Attribute attributes { get; set; }
        public string Id { get; set; }

        public Dictionary<string, dynamic> record { get; set; }*/
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Photos
    {
        public string picture { get; set; }
        public string thumbnail { get; set; }
    }

    public class Identity
    {
        public string id { get; set; }
        public bool asserted_user { get; set; }
        public string user_id { get; set; }
        public string organization_id { get; set; }
        public string username { get; set; }
        public string nick_name { get; set; }
        public string display_name { get; set; }
        public string email { get; set; }
        public bool email_verified { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string timezone { get; set; }
        public Photos photos { get; set; }
        public object addr_street { get; set; }
        public object addr_city { get; set; }
        public object addr_state { get; set; }
        public string addr_country { get; set; }
        public object addr_zip { get; set; }
        public string mobile_phone { get; set; }
        public bool mobile_phone_verified { get; set; }
        public bool is_lightning_login_user { get; set; }
        public Status status { get; set; }
        public Urls urls { get; set; }
        public bool active { get; set; }
        public string user_type { get; set; }
        public string language { get; set; }
        public string locale { get; set; }
        public int utcOffset { get; set; }
        public DateTime last_modified_date { get; set; }
        public bool is_app_installed { get; set; }
    }

    public class Status
    {
        public object created_date { get; set; }
        public object body { get; set; }
    }

    public class Urls
    {
        public string enterprise { get; set; }
        public string metadata { get; set; }
        public string partner { get; set; }
        public string rest { get; set; }
        public string sobjects { get; set; }
        public string search { get; set; }
        public string query { get; set; }
        public string recent { get; set; }
        public string tooling_soap { get; set; }
        public string tooling_rest { get; set; }
        public string profile { get; set; }
        public string feeds { get; set; }
        public string groups { get; set; }
        public string users { get; set; }
        public string feed_items { get; set; }
        public string feed_elements { get; set; }
        public string custom_domain { get; set; }

        public string rowTemplate { get; set; }
        public string describe { get; set; }
        public string sobject { get; set; }
        public string eventSchema { get; set; }
        public string compactLayouts { get; set; }
        public string approvalLayouts { get; set; }
        public string listviews { get; set; }
        public string quickActions { get; set; }
        public string layouts { get; set; }


        public string layout { get; set; }
        public string uiDetailTemplate { get; set; }
        public string uiEditTemplate { get; set; }
        public string uiNewRecord { get; set; }
    }





    public class ActionOverride
    {
        public string formFactor { get; set; }
        public bool isAvailableInTouch { get; set; }
        public string name { get; set; }
        public string pageId { get; set; }
        public string url { get; set; }
    }

    public class ChildRelationship
    {
        public bool cascadeDelete { get; set; }
        public string childSObject { get; set; }
        public bool deprecatedAndHidden { get; set; }
        public string field { get; set; }
        public List<object> junctionIdListNames { get; set; }
        public List<object> junctionReferenceTo { get; set; }
        public string relationshipName { get; set; }
        public bool restrictedDelete { get; set; }
    }

    public class Field
    {
        public bool aggregatable { get; set; }
        public bool aiPredictionField { get; set; }
        public bool autoNumber { get; set; }
        public int byteLength { get; set; }
        public bool calculated { get; set; }
        public object calculatedFormula { get; set; }
        public bool cascadeDelete { get; set; }
        public bool caseSensitive { get; set; }
        public string compoundFieldName { get; set; }
        public object controllerName { get; set; }
        public bool createable { get; set; }
        public bool custom { get; set; }
        public bool? defaultValue { get; set; }
        public object defaultValueFormula { get; set; }
        public bool defaultedOnCreate { get; set; }
        public bool dependentPicklist { get; set; }
        public bool deprecatedAndHidden { get; set; }
        public int digits { get; set; }
        public bool displayLocationInDecimal { get; set; }
        public bool encrypted { get; set; }
        public bool externalId { get; set; }
        public string extraTypeInfo { get; set; }
        public bool filterable { get; set; }
        public object filteredLookupInfo { get; set; }
        public bool formulaTreatNullNumberAsZero { get; set; }
        public bool groupable { get; set; }
        public bool highScaleNumber { get; set; }
        public bool htmlFormatted { get; set; }
        public bool idLookup { get; set; }
        public object inlineHelpText { get; set; }
        public string label { get; set; }
        public int length { get; set; }
        public object mask { get; set; }
        public object maskType { get; set; }
        public string name { get; set; }
        public bool nameField { get; set; }
        public bool namePointing { get; set; }
        public bool nillable { get; set; }
        public bool permissionable { get; set; }
        public List<PicklistValue> picklistValues { get; set; }
        public bool polymorphicForeignKey { get; set; }
        public int precision { get; set; }
        public bool queryByDistance { get; set; }
        public object referenceTargetField { get; set; }
        public List<string> referenceTo { get; set; }
        public string relationshipName { get; set; }
        public object relationshipOrder { get; set; }
        public bool restrictedDelete { get; set; }
        public bool restrictedPicklist { get; set; }
        public int scale { get; set; }
        public bool searchPrefilterable { get; set; }
        public string soapType { get; set; }
        public bool sortable { get; set; }
        public string type { get; set; }
        public bool unique { get; set; }
        public bool updateable { get; set; }
        public bool writeRequiresMasterRead { get; set; }
    }

    public class PicklistValue
    {
        public bool active { get; set; }
        public bool defaultValue { get; set; }
        public string label { get; set; }
        public object validFor { get; set; }
        public string value { get; set; }
    }

    public class RecordTypeInfo
    {
        public bool active { get; set; }
        public bool available { get; set; }
        public bool defaultRecordTypeMapping { get; set; }
        public string developerName { get; set; }
        public bool master { get; set; }
        public string name { get; set; }
        public string recordTypeId { get; set; }
        public Urls urls { get; set; }
    }

    public class ScopeInfo
    {
        public string label { get; set; }
        public string name { get; set; }
    }

    public class NamedLayoutInfo
    {
        public string name { get; set; }
    }

    public class DeletedRecord
    {
        public DateTime deletedDate { get; set; }
        public string id { get; set; }
    }

    public class GetDeletedResult
    {
        public List<DeletedRecord> deletedRecords { get; set; }
        public DateTime earliestDateAvailable { get; set; }
        public DateTime latestDateCovered { get; set; }
    }

    public class GetUpdatedResult
    {
        public List<string> ids { get; set; }
        public DateTime latestDateCovered { get; set; }
    }

    public class QueryResult
    {
        public bool done { get; set; }
        public int totalSize { get; set; }
        public List<sObject> records { get; set; }
        public string nextRecordsUrl { get; set; }


        public string queryLocator { get; set; }
    }

    public class SaveResult
    {
        public string id { get; set;}
        public bool success { get; set; }
        public bool created { get; set; }
        public List<Error> errors { get; set; }
    }

    public class UpsertResult
    {
        public string id { get; set; }
        public bool success { get; set; }
        public bool created { get; set; }
        public List<Error> errors { get; set; }
    }

    public class DeleteResult
    {
        public string id { get; set; }
        public bool success { get; set; }
        public bool created { get; set; }
        public List<Error> errors { get; set; }
    }

    public class Error
    {
        private List<string> fields {get;set;}
        private string message { get; set; }
        private Sfdc.Rest.StatusCode statusCode { get; set; }
    }

    public class Attribute
    {
        public string type { get; set; }
        public string url { get; set; }
    }

    //SOSL Search public class SearchResult
}
