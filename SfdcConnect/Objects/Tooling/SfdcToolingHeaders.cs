using System;
using System.Xml.Serialization;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.ComponentModel;

namespace SfdcConnect.Tooling
{
    #region Headers

    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "urn:tooling.soap.sforce.com")]
    [XmlRootAttribute(Namespace = "urn:tooling.soap.sforce.com", IsNullable = false)]
    public partial class MetadataWarningsHeader : System.Web.Services.Protocols.SoapHeader
    {
        private bool ignoreSaveWarningsField;

        public bool ignoreSaveWarnings
        {
            get
            {
                return this.ignoreSaveWarningsField;
            }
            set
            {
                this.ignoreSaveWarningsField = value;
            }
        }
    }

    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "urn:tooling.soap.sforce.com")]
    [XmlRootAttribute(Namespace = "urn:tooling.soap.sforce.com", IsNullable = false)]
    public partial class APIPerformanceInfo : System.Web.Services.Protocols.SoapHeader
    {
        private string encodedIntervalTimerTreeField;
        private NameValuePair[] handlerMetricsField;

        public string encodedIntervalTimerTree
        {
            get
            {
                return this.encodedIntervalTimerTreeField;
            }
            set
            {
                this.encodedIntervalTimerTreeField = value;
            }
        }

        [XmlElementAttribute("handlerMetrics")]
        public NameValuePair[] handlerMetrics
        {
            get
            {
                return this.handlerMetricsField;
            }
            set
            {
                this.handlerMetricsField = value;
            }
        }
    }

    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "urn:tooling.soap.sforce.com")]
    [XmlRootAttribute(Namespace = "urn:tooling.soap.sforce.com", IsNullable = false)]
    public partial class MetadataVersionCheck : System.Web.Services.Protocols.SoapHeader
    {
        private Fact[] factsField;
        private Operation operationField;

        [XmlElementAttribute("facts")]
        public Fact[] facts
        {
            get
            {
                return this.factsField;
            }
            set
            {
                this.factsField = value;
            }
        }

        public Operation operation
        {
            get
            {
                return this.operationField;
            }
            set
            {
                this.operationField = value;
            }
        }
    }

    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "urn:tooling.soap.sforce.com")]
    [XmlRootAttribute(Namespace = "urn:tooling.soap.sforce.com", IsNullable = false)]
    public partial class DebuggingInfo : System.Web.Services.Protocols.SoapHeader
    {
        private string debugLogField;

        public string debugLog
        {
            get
            {
                return this.debugLogField;
            }
            set
            {
                this.debugLogField = value;
            }
        }
    }

    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "urn:tooling.soap.sforce.com")]
    [XmlRootAttribute(Namespace = "urn:tooling.soap.sforce.com", IsNullable = false)]
    public partial class AllOrNoneHeader : System.Web.Services.Protocols.SoapHeader
    {
        private bool allOrNoneField;

        public bool allOrNone
        {
            get
            {
                return this.allOrNoneField;
            }
            set
            {
                this.allOrNoneField = value;
            }
        }
    }

    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "urn:tooling.soap.sforce.com")]
    [XmlRootAttribute(Namespace = "urn:tooling.soap.sforce.com", IsNullable = false)]
    public partial class PackageVersionHeader : System.Web.Services.Protocols.SoapHeader
    {
        private PackageVersion1[] packageVersionsField;

        [XmlElementAttribute("packageVersions")]
        public PackageVersion1[] packageVersions
        {
            get
            {
                return this.packageVersionsField;
            }
            set
            {
                this.packageVersionsField = value;
            }
        }
    }

    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "urn:tooling.soap.sforce.com")]
    [XmlRootAttribute(Namespace = "urn:tooling.soap.sforce.com", IsNullable = false)]
    public partial class AllowFieldTruncationHeader : System.Web.Services.Protocols.SoapHeader
    {
        private bool allowFieldTruncationField;

        public bool allowFieldTruncation
        {
            get
            {
                return this.allowFieldTruncationField;
            }
            set
            {
                this.allowFieldTruncationField = value;
            }
        }
    }

    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "urn:tooling.soap.sforce.com")]
    [XmlRootAttribute(Namespace = "urn:tooling.soap.sforce.com", IsNullable = false)]
    public partial class SessionHeader : System.Web.Services.Protocols.SoapHeader
    {
        private string sessionIdField;

        public string sessionId
        {
            get
            {
                return this.sessionIdField;
            }
            set
            {
                this.sessionIdField = value;
            }
        }
    }

    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "urn:tooling.soap.sforce.com")]
    [XmlRootAttribute(Namespace = "urn:tooling.soap.sforce.com", IsNullable = false)]
    public partial class CallOptions : System.Web.Services.Protocols.SoapHeader
    {
        private string clientField;

        public string client
        {
            get
            {
                return this.clientField;
            }
            set
            {
                this.clientField = value;
            }
        }
    }

    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "urn:tooling.soap.sforce.com")]
    [XmlRootAttribute(Namespace = "urn:tooling.soap.sforce.com", IsNullable = false)]
    public partial class DebuggingHeader : System.Web.Services.Protocols.SoapHeader
    {
        private LogInfo[] categoriesField;
        private LogType debugLevelField;

        [XmlElementAttribute("categories")]
        public LogInfo[] categories
        {
            get
            {
                return this.categoriesField;
            }
            set
            {
                this.categoriesField = value;
            }
        }

        public LogType debugLevel
        {
            get
            {
                return this.debugLevelField;
            }
            set
            {
                this.debugLevelField = value;
            }
        }
    }

    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "urn:tooling.soap.sforce.com")]
    [XmlRootAttribute(Namespace = "urn:tooling.soap.sforce.com", IsNullable = false)]
    public partial class DisableFeedTrackingHeader : System.Web.Services.Protocols.SoapHeader
    {
        private bool disableFeedTrackingField;

        public bool disableFeedTracking
        {
            get
            {
                return this.disableFeedTrackingField;
            }
            set
            {
                this.disableFeedTrackingField = value;
            }
        }
    }
    #endregion

}
