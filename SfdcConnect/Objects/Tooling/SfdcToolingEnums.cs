using System;

using System.Xml.Serialization;
using System.CodeDom.Compiler;

namespace SfdcConnect.Tooling
{
    #region Enums

    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:metadata.tooling.soap.sforce.com")]
    public enum EmailToCaseRoutingAddressType
    {

        EmailToCase,

        Outlook,
    }
    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum EmailToCaseOnFailureActionType
    {

        Bounce,

        Discard,

        Requeue,
    }
    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum FeedItemDisplayFormat
    {

        Default,

        HideBlankLines,
    }
    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum FileDownloadBehavior
    {

        DOWNLOAD,

        EXECUTE_IN_BROWSER,

        HYBRID,
    }

    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum FileType
    {

        UNKNOWN,

        PDF,

        POWER_POINT,

        POWER_POINT_X,

        POWER_POINT_M,

        POWER_POINT_T,

        WORD,

        WORD_X,

        WORD_M,

        WORD_T,

        PPS,

        PPSX,

        EXCEL,

        EXCEL_X,

        EXCEL_M,

        EXCEL_T,

        GOOGLE_DOCUMENT,

        GOOGLE_PRESENTATION,

        GOOGLE_SPREADSHEET,

        GOOGLE_DRAWING,

        GOOGLE_FORM,

        GOOGLE_SCRIPT,

        LINK,

        SLIDE,

        AAC,

        ACGI,

        AI,

        AVI,

        BMP,

        BOXNOTE,

        CSV,

        EPS,

        EXE,

        FLASH,

        GIF,

        GZIP,

        HTM,

        HTML,

        HTX,

        JPEG,

        JPE,

        PJP,

        PJPEG,

        JFIF,

        JPG,

        JS,

        MHTM,

        MHTML,

        MP3,

        M4A,

        M4V,

        MP4,

        MPEG,

        MPG,

        MOV,

        ODP,

        ODS,

        ODT,

        OGV,

        PNG,

        PSD,

        RTF,

        SHTM,

        SHTML,

        SNOTE,

        STYPI,

        SVG,

        SVGZ,

        TEXT,

        THTML,

        VISIO,

        WMV,

        WRF,

        XML,

        ZIP,

        XZIP,

        WMA,

        XSN,

        TRTF,

        TXML,

        WEBVIEW,

        RFC822,

        ASF,

        DWG,

        JAR,

        XJS,

        OPX,

        XPSD,

        TIF,

        TIFF,

        WAV,

        CSS,

        THUMB720BY480,

        THUMB240BY180,

        THUMB120BY90,

        ALLTHUMBS,

        PAGED_FLASH,

        PACK,

        C,

        CPP,

        WORDT,

        INI,

        JAVA,

        LOG,

        POWER_POINTT,

        SQL,

        XHTML,

        EXCELT,
    }
    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum PeriodTypes
    {

        Month,

        Quarter,

        Week,

        Year,
    }
    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum DisplayCurrency
    {

        CORPORATE,

        PERSONAL,
    }
    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:metadata.tooling.soap.sforce.com")]
    public enum KnowledgeLanguageLookupValueType
    {

        User,

        Queue,
    }
    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:metadata.tooling.soap.sforce.com")]
    public enum KnowledgeCaseEditor
    {

        simple,

        standard,
    }
    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:metadata.tooling.soap.sforce.com")]
    public enum SessionTimeout
    {

        TwentyFourHours,

        TwelveHours,

        EightHours,

        FourHours,

        TwoHours,

        SixtyMinutes,

        ThirtyMinutes,

        FifteenMinutes,
    }

    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:metadata.tooling.soap.sforce.com")]
    public enum QuestionRestriction
    {

        None,

        DoesNotContainPassword,
    }
    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:metadata.tooling.soap.sforce.com")]
    public enum Complexity
    {

        NoRestriction,

        AlphaNumeric,

        SpecialCharacters,

        UpperLowerCaseNumeric,

        UpperLowerCaseNumericSpecialCharacters,
    }

    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:metadata.tooling.soap.sforce.com")]
    public enum Expiration
    {

        ThirtyDays,

        SixtyDays,

        NinetyDays,

        SixMonths,

        OneYear,

        Never,
    }

    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:metadata.tooling.soap.sforce.com")]
    public enum LockoutInterval
    {

        FifteenMinutes,

        ThirtyMinutes,

        SixtyMinutes,

        Forever,
    }

    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:metadata.tooling.soap.sforce.com")]
    public enum MaxLoginAttempts
    {

        ThreeAttempts,

        FiveAttempts,

        TenAttempts,

        NoLimit,
    }
    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum ApexCodeUnitStatus
    {

        Inactive,

        Active,

        Deleted,
    }

    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum AuraBundleType
    {

        Application,

        Component,

        Event,

        Interface,

        Tokens,
    }

    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum MappingOperation
    {

        Autofill,

        Overwrite,
    }

    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum CleanRuleStatus
    {

        Inactive,

        Active,
    }
    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum CommunityTemplateBundleInfoType
    {

        Highlight,

        PreviewImage,
    }

    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum CommunityTemplateCategory
    {

        IT,

        Marketing,

        Sales,

        Service,
    }
    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum CommunityThemeLayoutType
    {

        Login,

        Home,

        Inner,
    }
    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum UiType
    {

        Aloha,

        Lightning,
    }
    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum NavType
    {

        Standard,

        Console,
    }
    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum StartsWith
    {

        Consonant,

        Vowel,

        Special,
    }

    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum SetupObjectVisibility
    {

        Protected,

        Public,
    }
    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum EncryptedFieldMaskChar
    {

        asterisk,

        X,
    }

    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum EncryptedFieldMaskType
    {

        all,

        creditCard,

        ssn,

        lastFour,

        sin,

        nino,
    }

    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum SummaryOperations
    {

        count,

        sum,

        min,

        max,
    }

    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum DeleteConstraint
    {

        Cascade,

        Restrict,

        SetNull,
    }

    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum TreatBlanksAs
    {

        BlankAsBlank,

        BlankAsZero,
    }
    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum CustomSettingsType
    {

        List,

        Hierarchy,
    }

    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum DeploymentStatus
    {

        InDevelopment,

        Deployed,
    }

    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum SharingModel
    {

        Private,

        Read,

        ReadSelect,

        ReadWrite,

        ReadWriteTransfer,

        FullAccess,

        ControlledByParent,
    }

    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum Gender
    {

        Neuter,

        Masculine,

        Feminine,

        AnimateMasculine,
    }
    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum DataPipelineType
    {

        Pig,
    }
    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum EmailTemplateStyle
    {

        none,

        freeForm,

        formalLetter,

        promotionRight,

        promotionLeft,

        newsletter,

        products,
    }

    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum EmailTemplateType
    {

        text,

        html,

        custom,

        visualforce,
    }

    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum EmbeddedServiceScenario
    {

        Sales,

        Service,

        Basic,
    }
    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum EscalationStartTimeType
    {

        CaseCreation,

        CaseLastModified,
    }
    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum AssignToLookupValueType
    {

        User,

        Queue,
    }

    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum BusinessHoursSourceType
    {

        None,

        Case,

        Static,
    }
    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum EventDeliveryType
    {

        StartFlow,

        ResumeFlow,
    }

    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum FieldType
    {

        AutoNumber,

        Lookup,

        MasterDetail,

        Checkbox,

        Currency,

        Date,

        DateTime,

        Email,

        Number,

        Percent,

        Phone,

        Picklist,

        MultiselectPicklist,

        Text,

        TextArea,

        LongTextArea,

        Html,

        Url,

        EncryptedText,

        Summary,

        Hierarchy,

        File,

        MetadataRelationship,

        Location,

        ExternalLookup,

        IndirectLookup,

        CustomDataType,
    }
    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum FlexiPageType
    {

        AppPage,

        ObjectPage,

        RecordPage,

        HomePage,

        MailAppAppPage,

        CommAppPage,

        CommForgotPasswordPage,

        CommLoginPage,

        CommObjectPage,

        CommQuickActionCreatePage,

        CommRecordPage,

        CommRelatedListPage,

        CommSearchResultPage,

        CommSelfRegisterPage,

        CommThemeLayoutPage,

        UtilityBar,

        RecordPreview,
    }
    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum ComponentInstancePropertyTypeEnum
    {

        decorator,
    }

    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum FlexiPageRegionMode
    {

        Append,

        Prepend,

        Replace,
    }

    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum FlexiPageRegionType
    {

        Region,

        Facet,
    }

    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum RegionFlagStatus
    {

        disabled,

        enabled,
    }
    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum FlowProcessType
    {

        AutoLaunchedFlow,

        Flow,

        Workflow,

        CustomEvent,

        InvocableProcess,

        LoginFlow,

        ActionPlan,

        JourneyBuilderIntegration,

        UserProvisioningFlow,

        Survey,

        FieldServiceMobile,
    }
    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum IterationOrder
    {

        Asc,

        Desc,
    }

    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum FlowScreenFieldType
    {

        DisplayText,

        InputField,

        LargeTextArea,

        PasswordField,

        RadioButtons,

        DropdownBox,

        MultiSelectCheckboxes,

        MultiSelectPicklist,
    }
    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum FlowDataType
    {

        Currency,

        Date,

        Number,

        String,

        Boolean,

        SObject,

        DateTime,

        Picklist,

        Multipicklist,
    }
    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum FlowAssignmentOperator
    {

        Assign,

        Add,

        Subtract,

        AddItem,
    }
    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum FlowRecordFilterOperator
    {

        EqualTo,

        NotEqualTo,

        GreaterThan,

        LessThan,

        GreaterThanOrEqualTo,

        LessThanOrEqualTo,

        StartsWith,

        EndsWith,

        Contains,

        IsNull,
    }
    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum FlowComparisonOperator
    {

        EqualTo,

        NotEqualTo,

        GreaterThan,

        LessThan,

        GreaterThanOrEqualTo,

        LessThanOrEqualTo,

        StartsWith,

        EndsWith,

        Contains,

        IsNull,

        WasSet,

        WasSelected,

        WasVisited,
    }
    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum InvocableActionType
    {

        apex,

        chatterPost,

        contentWorkspaceEnableFolders,

        emailAlert,

        emailSimple,

        flow,

        metricRefresh,

        quickAction,

        submit,

        thanks,

        thunderResponse,

        createServiceReport,
    }
    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum FolderAccessTypes
    {

        Shared,

        Public,

        Hidden,

        PublicInternal,
    }

    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum PublicFolderAccess
    {

        ReadOnly,

        ReadWrite,
    }
    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum SummaryLayoutStyle
    {

        Default,

        QuoteTemplate,

        DefaultQuoteTemplate,

        ServiceReportTemplate,

        ChildServiceReportTemplateStyle,

        DefaultServiceReportTemplate,

        CaseInteraction,

        QuickActionLayoutLeftRight,

        QuickActionLayoutTopDown,

        PathAssistant,
    }
    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum SortOrder
    {

        Asc,

        Desc,
    }
    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum ReportChartComponentSize
    {

        SMALL,

        MEDIUM,

        LARGE,
    }
    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum FeedLayoutComponentType
    {

        HelpAndToolLinks,

        CustomButtons,

        Following,

        Followers,

        CustomLinks,

        Milestones,

        Topics,

        CaseUnifiedFiles,

        Visualforce,
    }

    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum LayoutHeader
    {

        PersonalTagging,

        PublicTagging,
    }
    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum FeedLayoutFilterType
    {

        AllUpdates,

        FeedItemType,

        Custom,
    }

    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum FeedItemType
    {

        TrackedChange,

        UserStatus,

        TextPost,

        AdvancedTextPost,

        LinkPost,

        ContentPost,

        PollPost,

        RypplePost,

        ProfileSkillPost,

        DashboardComponentSnapshot,

        ApprovalPost,

        CaseCommentPost,

        ReplyPost,

        EmailMessageEvent,

        CallLogPost,

        ChangeStatusPost,

        AttachArticleEvent,

        MilestoneEvent,

        ActivityEvent,

        ChatTranscriptPost,

        CollaborationGroupCreated,

        CollaborationGroupUnarchived,

        SocialPost,

        QuestionPost,

        FacebookPost,

        BasicTemplateFeedItem,

        CreateRecordEvent,

        CanvasPost,

        AnnouncementPost,
    }

    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum FeedLayoutFilterPosition
    {

        CenterDropDown,

        LeftFixed,

        LeftFloat,
    }
    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum VisibleOrRequired
    {

        VisibleOptional,

        VisibleRequired,

        NotVisible,
    }
    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum Language
    {

        en_US,

        de,

        es,

        fr,

        it,

        ja,

        sv,

        ko,

        zh_TW,

        zh_CN,

        pt_BR,

        nl_NL,

        da,

        th,

        fi,

        ru,

        es_MX,

        no,

        hu,

        pl,

        cs,

        tr,

        @in,

        ro,

        vi,

        uk,

        iw,

        el,

        bg,

        en_GB,

        ar,

        sk,

        pt_PT,

        hr,

        sl,

        fr_CA,

        ka,

        sr,

        sh,

        en_AU,

        en_MY,

        en_IN,

        en_PH,

        en_CA,

        ro_MD,

        bs,

        mk,

        lv,

        lt,

        et,

        sq,

        sh_ME,

        mt,

        ga,

        eu,

        cy,

        @is,

        ms,

        tl,

        lb,

        rm,

        hy,

        hi,

        ur,

        bn,

        de_AT,

        de_CH,

        ta,

        ar_DZ,

        ar_BH,

        ar_EG,

        ar_IQ,

        ar_JO,

        ar_KW,

        ar_LB,

        ar_LY,

        ar_MA,

        ar_OM,

        ar_QA,

        ar_SA,

        ar_SD,

        ar_SY,

        ar_TN,

        ar_AE,

        ar_YE,

        zh_SG,

        zh_HK,

        en_HK,

        en_IE,

        en_SG,

        en_ZA,

        fr_BE,

        fr_LU,

        fr_CH,

        de_LU,

        it_CH,

        es_AR,

        es_BO,

        es_CL,

        es_CO,

        es_CR,

        es_DO,

        es_EC,

        es_SV,

        es_GT,

        es_HN,

        es_NI,

        es_PA,

        es_PY,

        es_PE,

        es_PR,

        es_US,

        es_UY,

        es_VE,

        eo,

        iw_EO,
    }
    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum FilterScope
    {

        Everything,

        Mine,

        Queue,

        Delegated,

        MyTerritory,

        MyTeamTerritory,

        Team,

        AssignedToMe,
    }
    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum RateLimitTimePeriod
    {

        Short,

        Medium,
    }

    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum ModerationRuleType
    {

        Content,

        Rate,
    }
    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum ModerationRuleAction
    {

        Block,

        FreezeAndNotify,

        Review,

        Replace,

        Flag,
    }

    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum WebLinkAvailability
    {

        online,

        offline,
    }

    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum WebLinkDisplayType
    {

        link,

        button,

        massActionButton,
    }

    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum Encoding
    {

        [XmlEnumAttribute("UTF-8")]
        UTF8,

        [XmlEnumAttribute("ISO-8859-1")]
        ISO88591,

        Shift_JIS,

        [XmlEnumAttribute("ISO-2022-JP")]
        ISO2022JP,

        [XmlEnumAttribute("EUC-JP")]
        EUCJP,

        [XmlEnumAttribute("ks_c_5601-1987")]
        ks_c_56011987,

        Big5,

        GB2312,

        [XmlEnumAttribute("Big5-HKSCS")]
        Big5HKSCS,

        [XmlEnumAttribute("x-SJIS_0213")]
        xSJIS_0213,
    }

    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum WebLinkType
    {

        url,

        sControl,

        javascript,

        page,

        flow,
    }

    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum WebLinkWindowType
    {

        newWindow,

        sidebar,

        noSidebar,

        replace,

        onClickJavaScript,
    }

    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum WebLinkPosition
    {

        fullScreen,

        none,

        topLeft,
    }
    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum NetworkUserType
    {

        Internal,

        Customer,

        Partner,
    }

    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum MetadataVersionCheckFact
    {

        DescribeLayoutVersion,

        DescribeSObjectVersion,

        SystemConfigurationVersion,
    }

    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum LogCategory
    {

        Db,

        Workflow,

        Validation,

        Callout,

        Apex_code,

        Apex_profiling,

        Visualforce,

        System,

        Wave,

        All,
    }

    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum LogCategoryLevel
    {

        None,

        Finest,

        Finer,

        Fine,

        Debug,

        Info,

        Warn,

        Error,
    }

    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum Operation
    {

        RetrieveTokens,

        ErrorOnNewerVersion,

        SkipOnSameVersion,
    }

    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum LogType
    {

        None,

        Debugonly,

        Db,

        Profiling,

        Callout,

        Detail,
    }

    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum TestLevel
    {

        NoTestRun,

        RunSpecifiedTests,

        RunLocalTests,

        RunAllTestsInOrg,
    }

    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum TabOrderType
    {

        LeftToRight,

        TopToBottom,
    }


    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum ShareAccessLevel
    {

        Read,

        Edit,

        All,
    }

    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum orderByNullsPosition
    {

        first,

        last,
    }

    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum orderByDirection
    {

        ascending,

        descending,
    }

    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum soqlOperator
    {

        equals,

        excludes,

        greaterThan,

        greaterThanOrEqualTo,

        @in,

        includes,

        lessThan,

        lessThanOrEqualTo,

        like,

        notEquals,

        notIn,

        within,
    }

    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum DeployProblemType
    {

        Warning,

        Error,
    }

    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:fault.tooling.soap.sforce.com")]
    public enum ExceptionCode
    {

        APEX_TRIGGER_COUPLING_LIMIT,

        API_CURRENTLY_DISABLED,

        API_DISABLED_FOR_ORG,

        ARGUMENT_OBJECT_PARSE_ERROR,

        ASYNC_OPERATION_LOCATOR,

        ASYNC_QUERY_UNSUPPORTED_QUERY,

        BATCH_PROCESSING_HALTED,

        BIG_OBJECT_UNSUPPORTED_OPERATION,

        CANNOT_DELETE_ENTITY,

        CANNOT_DELETE_OWNER,

        CANT_ADD_STANDADRD_PORTAL_USER_TO_TERRITORY,

        CANT_ADD_STANDARD_PORTAL_USER_TO_TERRITORY,

        CIRCULAR_OBJECT_GRAPH,

        CLIENT_NOT_ACCESSIBLE_FOR_USER,

        CLIENT_REQUIRE_UPDATE_FOR_USER,

        CONTENT_CUSTOM_DOWNLOAD_EXCEPTION,

        CONTENT_HUB_AUTHENTICATION_EXCEPTION,

        CONTENT_HUB_FILE_DOWNLOAD_EXCEPTION,

        CONTENT_HUB_FILE_NOT_FOUND_EXCEPTION,

        CONTENT_HUB_INVALID_OBJECT_TYPE_EXCEPTION,

        CONTENT_HUB_INVALID_PAGE_NUMBER_EXCEPTION,

        CONTENT_HUB_INVALID_PAYLOAD,

        CONTENT_HUB_INVALID_RENDITION_PAGE_NUMBER_EXCEPTION,

        CONTENT_HUB_ITEM_TYPE_NOT_FOUND_EXCEPTION,

        CONTENT_HUB_OBJECT_NOT_FOUND_EXCEPTION,

        CONTENT_HUB_OPERATION_NOT_SUPPORTED_EXCEPTION,

        CONTENT_HUB_SECURITY_EXCEPTION,

        CONTENT_HUB_TIMEOUT_EXCEPTION,

        CONTENT_HUB_UNEXPECTED_EXCEPTION,

        CUSTOM_METADATA_LIMIT_EXCEEDED,

        CUSTOM_SETTINGS_LIMIT_EXCEEDED,

        DATACLOUD_API_CLIENT_EXCEPTION,

        DATACLOUD_API_DISABLED_EXCEPTION,

        DATACLOUD_API_INVALID_QUERY_EXCEPTION,

        DATACLOUD_API_SERVER_BUSY_EXCEPTION,

        DATACLOUD_API_SERVER_EXCEPTION,

        DATACLOUD_API_TIMEOUT_EXCEPTION,

        DATACLOUD_API_UNAVAILABLE,

        DUPLICATE_ARGUMENT_VALUE,

        DUPLICATE_VALUE,

        EMAIL_BATCH_SIZE_LIMIT_EXCEEDED,

        EMAIL_TO_CASE_INVALID_ROUTING,

        EMAIL_TO_CASE_LIMIT_EXCEEDED,

        EMAIL_TO_CASE_NOT_ENABLED,

        ENTITY_NOT_QUERYABLE,

        ENVIRONMENT_HUB_MEMBERSHIP_CONFLICT,

        EXCEEDED_ID_LIMIT,

        EXCEEDED_LEAD_CONVERT_LIMIT,

        EXCEEDED_MAX_SIZE_REQUEST,

        EXCEEDED_MAX_SOBJECTS,

        EXCEEDED_MAX_TYPES_LIMIT,

        EXCEEDED_QUOTA,

        EXTERNAL_OBJECT_AUTHENTICATION_EXCEPTION,

        EXTERNAL_OBJECT_CONNECTION_EXCEPTION,

        EXTERNAL_OBJECT_EXCEPTION,

        EXTERNAL_OBJECT_UNSUPPORTED_EXCEPTION,

        FEDERATED_SEARCH_ERROR,

        FEED_NOT_ENABLED_FOR_OBJECT,

        FUNCTIONALITY_NOT_ENABLED,

        FUNCTIONALITY_TEMPORARILY_UNAVAILABLE,

        ILLEGAL_QUERY_PARAMETER_VALUE,

        INACTIVE_OWNER_OR_USER,

        INACTIVE_PORTAL,

        INSERT_UPDATE_DELETE_NOT_ALLOWED_DURING_MAINTENANCE,

        INSUFFICIENT_ACCESS,

        INTERNAL_CANVAS_ERROR,

        INVALID_ASSIGNMENT_RULE,

        INVALID_BATCH_REQUEST,

        INVALID_BATCH_SIZE,

        INVALID_CLIENT,

        INVALID_CROSS_REFERENCE_KEY,

        INVALID_DATE_FORMAT,

        INVALID_FIELD,

        INVALID_FILTER_LANGUAGE,

        INVALID_FILTER_VALUE,

        INVALID_ID_FIELD,

        INVALID_INPUT_COMBINATION,

        INVALID_LOCALE_LANGUAGE,

        INVALID_LOCATOR,

        INVALID_LOGIN,

        INVALID_MULTIPART_REQUEST,

        INVALID_NEW_PASSWORD,

        INVALID_OPERATION,

        INVALID_OPERATION_WITH_EXPIRED_PASSWORD,

        INVALID_PACKAGE_VERSION,

        INVALID_PAGING_OPTION,

        INVALID_QUERY_FILTER_OPERATOR,

        INVALID_QUERY_LOCATOR,

        INVALID_QUERY_SCOPE,

        INVALID_REPLICATION_DATE,

        INVALID_SEARCH,

        INVALID_SEARCH_SCOPE,

        INVALID_SESSION_ID,

        INVALID_SOAP_HEADER,

        INVALID_SORT_OPTION,

        INVALID_SSO_GATEWAY_URL,

        INVALID_TYPE,

        INVALID_TYPE_FOR_OPERATION,

        JIGSAW_ACTION_DISABLED,

        JIGSAW_IMPORT_LIMIT_EXCEEDED,

        JIGSAW_REQUEST_NOT_SUPPORTED,

        JSON_PARSER_ERROR,

        KEY_HAS_BEEN_DESTROYED,

        LICENSING_DATA_ERROR,

        LICENSING_UNKNOWN_ERROR,

        LIMIT_EXCEEDED,

        LOGIN_CHALLENGE_ISSUED,

        LOGIN_CHALLENGE_PENDING,

        LOGIN_DURING_RESTRICTED_DOMAIN,

        LOGIN_DURING_RESTRICTED_TIME,

        LOGIN_MUST_USE_SECURITY_TOKEN,

        MALFORMED_ID,

        MALFORMED_QUERY,

        MALFORMED_SEARCH,

        MISSING_ARGUMENT,

        MISSING_RECORD,

        MODIFIED,

        MUTUAL_AUTHENTICATION_FAILED,

        NOT_ACCEPTABLE,

        NOT_MODIFIED,

        NO_ACTIVE_DUPLICATE_RULE,

        NO_RECIPIENTS,

        NO_SOFTPHONE_LAYOUT,

        NUMBER_OUTSIDE_VALID_RANGE,

        OPERATION_TOO_LARGE,

        ORG_IN_MAINTENANCE,

        ORG_IS_DOT_ORG,

        ORG_IS_SIGNING_UP,

        ORG_LOCKED,

        ORG_NOT_OWNED_BY_INSTANCE,

        PASSWORD_LOCKOUT,

        PORTAL_NO_ACCESS,

        POST_BODY_PARSE_ERROR,

        QUERY_TIMEOUT,

        QUERY_TOO_COMPLICATED,

        REALTIME_PROCESSING_TIME_EXCEEDED_LIMIT,

        REQUEST_LIMIT_EXCEEDED,

        REQUEST_RUNNING_TOO_LONG,

        SERVER_UNAVAILABLE,

        SERVICE_DESK_NOT_ENABLED,

        SOCIALCRM_FEEDSERVICE_API_CLIENT_EXCEPTION,

        SOCIALCRM_FEEDSERVICE_API_SERVER_EXCEPTION,

        SOCIALCRM_FEEDSERVICE_API_UNAVAILABLE,

        SSO_SERVICE_DOWN,

        SST_ADMIN_FILE_DOWNLOAD_EXCEPTION,

        TOO_MANY_APEX_REQUESTS,

        TOO_MANY_RECIPIENTS,

        TOO_MANY_RECORDS,

        TRIAL_EXPIRED,

        TXN_SECURITY_END_A_SESSION,

        TXN_SECURITY_NO_ACCESS,

        TXN_SECURITY_TWO_FA_REQUIRED,

        UNABLE_TO_LOCK_ROW,

        UNKNOWN_ATTACHMENT_EXCEPTION,

        UNKNOWN_EXCEPTION,

        UNKNOWN_ORG_SETTING,

        UNSUPPORTED_API_VERSION,

        UNSUPPORTED_ATTACHMENT_ENCODING,

        UNSUPPORTED_CLIENT,

        UNSUPPORTED_MEDIA_TYPE,

        XML_PARSER_ERROR,
    }

    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum StatusCode
    {

        ALL_OR_NONE_OPERATION_ROLLED_BACK,

        ALREADY_IN_PROCESS,

        APEX_DATA_ACCESS_RESTRICTION,

        ASSIGNEE_TYPE_REQUIRED,

        AURA_COMPILE_ERROR,

        BAD_CUSTOM_ENTITY_PARENT_DOMAIN,

        BCC_NOT_ALLOWED_IF_BCC_COMPLIANCE_ENABLED,

        CANNOT_CASCADE_PRODUCT_ACTIVE,

        CANNOT_CHANGE_FIELD_TYPE_OF_APEX_REFERENCED_FIELD,

        CANNOT_CHANGE_FIELD_TYPE_OF_REFERENCED_FIELD,

        CANNOT_CREATE_ANOTHER_MANAGED_PACKAGE,

        CANNOT_DEACTIVATE_DIVISION,

        CANNOT_DELETE_GLOBAL_ACTION_LIST,

        CANNOT_DELETE_LAST_DATED_CONVERSION_RATE,

        CANNOT_DELETE_MANAGED_OBJECT,

        CANNOT_DISABLE_LAST_ADMIN,

        CANNOT_ENABLE_IP_RESTRICT_REQUESTS,

        CANNOT_EXECUTE_FLOW_TRIGGER,

        CANNOT_FREEZE_SELF,

        CANNOT_INSERT_UPDATE_ACTIVATE_ENTITY,

        CANNOT_MODIFY_MANAGED_OBJECT,

        CANNOT_PASSWORD_LOCKOUT,

        CANNOT_POST_TO_ARCHIVED_GROUP,

        CANNOT_RENAME_APEX_REFERENCED_FIELD,

        CANNOT_RENAME_APEX_REFERENCED_OBJECT,

        CANNOT_RENAME_REFERENCED_FIELD,

        CANNOT_RENAME_REFERENCED_OBJECT,

        CANNOT_REPARENT_RECORD,

        CANNOT_UPDATE_CONVERTED_LEAD,

        CANT_DISABLE_CORP_CURRENCY,

        CANT_UNSET_CORP_CURRENCY,

        CHILD_SHARE_FAILS_PARENT,

        CIRCULAR_DEPENDENCY,

        CLEAN_SERVICE_ERROR,

        COLLISION_DETECTED,

        COMMUNITY_NOT_ACCESSIBLE,

        CONFLICTING_ENVIRONMENT_HUB_MEMBER,

        CONFLICTING_SSO_USER_MAPPING,

        CUSTOM_APEX_ERROR,

        CUSTOM_CLOB_FIELD_LIMIT_EXCEEDED,

        CUSTOM_ENTITY_OR_FIELD_LIMIT,

        CUSTOM_FIELD_INDEX_LIMIT_EXCEEDED,

        CUSTOM_INDEX_EXISTS,

        CUSTOM_LINK_LIMIT_EXCEEDED,

        CUSTOM_METADATA_LIMIT_EXCEEDED,

        CUSTOM_METADATA_REL_FIELD_MANAGEABILITY,

        CUSTOM_SETTINGS_LIMIT_EXCEEDED,

        CUSTOM_TAB_LIMIT_EXCEEDED,

        DATACLOUDADDRESS_NO_RECORDS_FOUND,

        DATACLOUDADDRESS_PROCESSING_ERROR,

        DATACLOUDADDRESS_SERVER_ERROR,

        DELETE_FAILED,

        DELETE_OPERATION_TOO_LARGE,

        DELETE_REQUIRED_ON_CASCADE,

        DEPENDENCY_EXISTS,

        DUPLICATES_DETECTED,

        DUPLICATE_CASE_SOLUTION,

        DUPLICATE_COMM_NICKNAME,

        DUPLICATE_CUSTOM_ENTITY_DEFINITION,

        DUPLICATE_CUSTOM_TAB_MOTIF,

        DUPLICATE_DEVELOPER_NAME,

        DUPLICATE_EXTERNAL_ID,

        DUPLICATE_MASTER_LABEL,

        DUPLICATE_SENDER_DISPLAY_NAME,

        DUPLICATE_USERNAME,

        DUPLICATE_VALUE,

        EMAIL_ADDRESS_BOUNCED,

        EMAIL_EXTERNAL_TRANSPORT_CONNECTION_ERROR,

        EMAIL_EXTERNAL_TRANSPORT_PERMISSION_ERROR,

        EMAIL_EXTERNAL_TRANSPORT_TOKEN_ERROR,

        EMAIL_EXTERNAL_TRANSPORT_TOO_MANY_REQUESTS_ERROR,

        EMAIL_EXTERNAL_TRANSPORT_UNKNOWN_ERROR,

        EMAIL_NOT_PROCESSED_DUE_TO_PRIOR_ERROR,

        EMAIL_OPTED_OUT,

        EMAIL_TEMPLATE_FORMULA_ERROR,

        EMAIL_TEMPLATE_MERGEFIELD_ACCESS_ERROR,

        EMAIL_TEMPLATE_MERGEFIELD_ERROR,

        EMAIL_TEMPLATE_MERGEFIELD_VALUE_ERROR,

        EMAIL_TEMPLATE_PROCESSING_ERROR,

        EMPTY_SCONTROL_FILE_NAME,

        ENTITY_FAILED_IFLASTMODIFIED_ON_UPDATE,

        ENTITY_IS_ARCHIVED,

        ENTITY_IS_DELETED,

        ENTITY_IS_LOCKED,

        ENTITY_SAVE_ERROR,

        ENTITY_SAVE_VALIDATION_ERROR,

        ENVIRONMENT_HUB_MEMBERSHIP_CONFLICT,

        ENVIRONMENT_HUB_MEMBERSHIP_ERROR_JOINING_HUB,

        ENVIRONMENT_HUB_MEMBERSHIP_USER_ALREADY_IN_HUB,

        ENVIRONMENT_HUB_MEMBERSHIP_USER_NOT_ORG_ADMIN,

        ERROR_IN_MAILER,

        EXCHANGE_WEB_SERVICES_URL_INVALID,

        FAILED_ACTIVATION,

        FIELD_CUSTOM_VALIDATION_EXCEPTION,

        FIELD_FILTER_VALIDATION_EXCEPTION,

        FIELD_INTEGRITY_EXCEPTION,

        FIELD_KEYWORD_LIST_MATCH_LIMIT,

        FIELD_MAPPING_ERROR,

        FIELD_MODERATION_RULE_BLOCK,

        FIELD_NOT_UPDATABLE,

        FILE_EXTENSION_NOT_ALLOWED,

        FILE_SIZE_LIMIT_EXCEEDED,

        FILTERED_LOOKUP_LIMIT_EXCEEDED,

        FIND_DUPLICATES_ERROR,

        FUNCTIONALITY_NOT_ENABLED,

        HAS_PUBLIC_REFERENCES,

        HTML_FILE_UPLOAD_NOT_ALLOWED,

        IMAGE_TOO_LARGE,

        INACTIVE_OWNER_OR_USER,

        INACTIVE_RULE_ERROR,

        INSERT_UPDATE_DELETE_NOT_ALLOWED_DURING_MAINTENANCE,

        INSUFFICIENT_ACCESS_ON_CROSS_REFERENCE_ENTITY,

        INSUFFICIENT_ACCESS_OR_READONLY,

        INSUFFICIENT_ACCESS_TO_INSIGHTSEXTERNALDATA,

        INSUFFICIENT_CREDITS,

        INTERNAL_ERROR,

        INVALID_ACCESS_LEVEL,

        INVALID_ACCESS_TOKEN,

        INVALID_ARGUMENT_TYPE,

        INVALID_ASSIGNEE_TYPE,

        INVALID_ASSIGNMENT_RULE,

        INVALID_BATCH_OPERATION,

        INVALID_CONTENT_TYPE,

        INVALID_CREDIT_CARD_INFO,

        INVALID_CROSS_REFERENCE_KEY,

        INVALID_CROSS_REFERENCE_TYPE_FOR_FIELD,

        INVALID_CURRENCY_CONV_RATE,

        INVALID_CURRENCY_CORP_RATE,

        INVALID_CURRENCY_ISO,

        INVALID_DATA_CATEGORY_GROUP_REFERENCE,

        INVALID_DATA_URI,

        INVALID_EMAIL_ADDRESS,

        INVALID_EMPTY_KEY_OWNER,

        INVALID_ENTITY_FOR_MATCH_ENGINE_ERROR,

        INVALID_ENTITY_FOR_MATCH_OPERATION_ERROR,

        INVALID_ENTITY_FOR_UPSERT,

        INVALID_ENVIRONMENT_HUB_MEMBER,

        INVALID_EVENT_DELIVERY,

        INVALID_EVENT_SUBSCRIPTION,

        INVALID_FIELD,

        INVALID_FIELD_FOR_INSERT_UPDATE,

        INVALID_FIELD_WHEN_USING_TEMPLATE,

        INVALID_FILTER_ACTION,

        INVALID_GOOGLE_DOCS_URL,

        INVALID_ID_FIELD,

        INVALID_INET_ADDRESS,

        INVALID_INPUT,

        INVALID_LINEITEM_CLONE_STATE,

        INVALID_MARKUP,

        INVALID_MASTER_OR_TRANSLATED_SOLUTION,

        INVALID_MESSAGE_ID_REFERENCE,

        INVALID_NAMESPACE_PREFIX,

        INVALID_OAUTH_URL,

        INVALID_OPERATION,

        INVALID_OPERATOR,

        INVALID_OR_NULL_FOR_RESTRICTED_PICKLIST,

        INVALID_OWNER,

        INVALID_PACKAGE_LICENSE,

        INVALID_PACKAGE_VERSION,

        INVALID_PARTNER_NETWORK_STATUS,

        INVALID_PERSON_ACCOUNT_OPERATION,

        INVALID_PROVIDER_TYPE,

        INVALID_QUERY_LOCATOR,

        INVALID_READ_ONLY_USER_DML,

        INVALID_REFRESH_TOKEN,

        INVALID_RUNTIME_VALUE,

        INVALID_SAVE_AS_ACTIVITY_FLAG,

        INVALID_SESSION_ID,

        INVALID_SETUP_OWNER,

        INVALID_SIGNUP_COUNTRY,

        INVALID_SIGNUP_OPTION,

        INVALID_SITE_DELETE_EXCEPTION,

        INVALID_SITE_FILE_IMPORTED_EXCEPTION,

        INVALID_SITE_FILE_TYPE_EXCEPTION,

        INVALID_STATUS,

        INVALID_SUBDOMAIN,

        INVALID_TYPE,

        INVALID_TYPE_FOR_OPERATION,

        INVALID_TYPE_ON_FIELD_IN_RECORD,

        INVALID_USERID,

        IP_RANGE_LIMIT_EXCEEDED,

        JIGSAW_IMPORT_LIMIT_EXCEEDED,

        LICENSE_LIMIT_EXCEEDED,

        LIGHT_PORTAL_USER_EXCEPTION,

        LIMIT_EXCEEDED,

        MALFORMED_ID,

        MANAGER_NOT_DEFINED,

        MASSMAIL_RETRY_LIMIT_EXCEEDED,

        MASS_MAIL_LIMIT_EXCEEDED,

        MATCH_DEFINITION_ERROR,

        MATCH_OPERATION_ERROR,

        MATCH_OPERATION_INVALID_ENGINE_ERROR,

        MATCH_OPERATION_INVALID_RULE_ERROR,

        MATCH_OPERATION_MISSING_ENGINE_ERROR,

        MATCH_OPERATION_MISSING_OBJECT_TYPE_ERROR,

        MATCH_OPERATION_MISSING_OPTIONS_ERROR,

        MATCH_OPERATION_MISSING_RULE_ERROR,

        MATCH_OPERATION_UNKNOWN_RULE_ERROR,

        MATCH_OPERATION_UNSUPPORTED_VERSION_ERROR,

        MATCH_PRECONDITION_FAILED,

        MATCH_RUNTIME_ERROR,

        MATCH_SERVICE_ERROR,

        MATCH_SERVICE_TIMED_OUT,

        MATCH_SERVICE_UNAVAILABLE_ERROR,

        MAXIMUM_CCEMAILS_EXCEEDED,

        MAXIMUM_DASHBOARD_COMPONENTS_EXCEEDED,

        MAXIMUM_HIERARCHY_CHILDREN_REACHED,

        MAXIMUM_HIERARCHY_LEVELS_REACHED,

        MAXIMUM_HIERARCHY_TREE_SIZE_REACHED,

        MAXIMUM_SIZE_OF_ATTACHMENT,

        MAXIMUM_SIZE_OF_DOCUMENT,

        MAX_ACTIONS_PER_RULE_EXCEEDED,

        MAX_ACTIVE_RULES_EXCEEDED,

        MAX_APPROVAL_STEPS_EXCEEDED,

        MAX_DEPTH_IN_FLOW_EXECUTION,

        MAX_FORMULAS_PER_RULE_EXCEEDED,

        MAX_RULES_EXCEEDED,

        MAX_RULE_ENTRIES_EXCEEDED,

        MAX_TASK_DESCRIPTION_EXCEEEDED,

        MAX_TM_RULES_EXCEEDED,

        MAX_TM_RULE_ITEMS_EXCEEDED,

        MAX_TRIGGERS_EXCEEDED,

        MERGE_FAILED,

        METADATA_FIELD_UPDATE_ERROR,

        MISSING_ARGUMENT,

        MISSING_RECORD,

        MIXED_DML_OPERATION,

        NONUNIQUE_SHIPPING_ADDRESS,

        NO_ACCESS_TOKEN,

        NO_ACCESS_TOKEN_FROM_REFRESH,

        NO_APPLICABLE_PROCESS,

        NO_ATTACHMENT_PERMISSION,

        NO_AUTH_PROVIDER,

        NO_INACTIVE_DIVISION_MEMBERS,

        NO_MASS_MAIL_PERMISSION,

        NO_PARTNER_PERMISSION,

        NO_REFRESH_TOKEN,

        NO_SUCH_USER_EXISTS,

        NO_TOKEN_ENDPOINT,

        NUMBER_OUTSIDE_VALID_RANGE,

        NUM_HISTORY_FIELDS_BY_SOBJECT_EXCEEDED,

        OPTED_OUT_OF_MASS_MAIL,

        OP_WITH_INVALID_USER_TYPE_EXCEPTION,

        PACKAGE_LICENSE_REQUIRED,

        PACKAGING_API_INSTALL_FAILED,

        PACKAGING_API_UNINSTALL_FAILED,

        PALI_INVALID_ACTION_ID,

        PALI_INVALID_ACTION_NAME,

        PALI_INVALID_ACTION_TYPE,

        PAL_INVALID_ASSISTANT_RECOMMENDATION_TYPE_ID,

        PAL_INVALID_ENTITY_ID,

        PAL_INVALID_FLEXIPAGE_ID,

        PAL_INVALID_LAYOUT_ID,

        PAL_INVALID_PARAMETERS,

        PA_API_EXCEPTION,

        PA_AXIS_FAULT,

        PA_INVALID_ID_EXCEPTION,

        PA_NO_ACCESS_EXCEPTION,

        PA_NO_DATA_FOUND_EXCEPTION,

        PA_URI_SYNTAX_EXCEPTION,

        PA_VISIBLE_ACTIONS_FILTER_ORDERING_EXCEPTION,

        PORTAL_NO_ACCESS,

        PORTAL_USER_ALREADY_EXISTS_FOR_CONTACT,

        PORTAL_USER_CREATION_RESTRICTED_WITH_ENCRYPTION,

        PRIVATE_CONTACT_ON_ASSET,

        PROCESSING_HALTED,

        QA_INVALID_CREATE_FEED_ITEM,

        QA_INVALID_SUCCESS_MESSAGE,

        QUERY_TIMEOUT,

        QUICK_ACTION_LIST_ITEM_NOT_ALLOWED,

        QUICK_ACTION_LIST_NOT_ALLOWED,

        RECORD_IN_USE_BY_WORKFLOW,

        REL_FIELD_BAD_ACCESSIBILITY,

        REPUTATION_MINIMUM_NUMBER_NOT_REACHED,

        REQUEST_RUNNING_TOO_LONG,

        REQUIRED_FEATURE_MISSING,

        REQUIRED_FIELD_MISSING,

        RETRIEVE_EXCHANGE_ATTACHMENT_FAILED,

        RETRIEVE_EXCHANGE_EMAIL_FAILED,

        RETRIEVE_EXCHANGE_EVENT_FAILED,

        RETRIEVE_GOOGLE_EMAIL_FAILED,

        RETRIEVE_GOOGLE_EVENT_FAILED,

        SALESFORCE_INBOX_TRANSPORT_CONNECTION_ERROR,

        SALESFORCE_INBOX_TRANSPORT_TOKEN_ERROR,

        SALESFORCE_INBOX_TRANSPORT_UNKNOWN_ERROR,

        SELF_REFERENCE_FROM_FLOW,

        SELF_REFERENCE_FROM_TRIGGER,

        SHARE_NEEDED_FOR_CHILD_OWNER,

        SINGLE_EMAIL_LIMIT_EXCEEDED,

        SOCIAL_ACCOUNT_NOT_FOUND,

        SOCIAL_ACTION_INVALID,

        SOCIAL_POST_INVALID,

        SOCIAL_POST_NOT_FOUND,

        STANDARD_PRICE_NOT_DEFINED,

        STORAGE_LIMIT_EXCEEDED,

        STRING_TOO_LONG,

        SUBDOMAIN_IN_USE,

        TABSET_LIMIT_EXCEEDED,

        TEMPLATE_NOT_ACTIVE,

        TEMPLATE_NOT_FOUND,

        TERMS_OF_SERVICE_UNREAD,

        TERRITORY_REALIGN_IN_PROGRESS,

        TEXT_DATA_OUTSIDE_SUPPORTED_CHARSET,

        TOO_MANY_APEX_REQUESTS,

        TOO_MANY_ENUM_VALUE,

        TOO_MANY_POSSIBLE_USERS_EXIST,

        TRANSFER_REQUIRES_READ,

        UNABLE_TO_LOCK_ROW,

        UNAVAILABLE_RECORDTYPE_EXCEPTION,

        UNAVAILABLE_REF,

        UNDELETE_FAILED,

        UNKNOWN_EXCEPTION,

        UNKNOWN_TOKEN_ERROR,

        UNSAFE_HTML_CONTENT,

        UNSPECIFIED_EMAIL_ADDRESS,

        UNSUPPORTED_APEX_TRIGGER_OPERATON,

        UNVERIFIED_SENDER_ADDRESS,

        USER_OWNS_PORTAL_ACCOUNT_EXCEPTION,

        USER_WITH_APEX_SHARES_EXCEPTION,

        VF_COMPILE_ERROR,

        WEBLINK_SIZE_LIMIT_EXCEEDED,

        WEBLINK_URL_INVALID,

        WRONG_CONTROLLER_TYPE,

        XCLEAN_UNEXPECTED_ERROR,
    }

    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum soqlConjunction
    {

        and,

        or,
    }

    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum WorkflowActionType
    {

        FieldUpdate,

        KnowledgePublish,

        Task,

        Alert,

        Send,

        OutboundMessage,

        FlowAction,
    }
    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum FilterOperation
    {

        equals,

        notEqual,

        lessThan,

        greaterThan,

        lessOrEqual,

        greaterOrEqual,

        contains,

        notContain,

        startsWith,

        includes,

        excludes,

        within,
    }

    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum WorkflowTriggerTypes
    {

        onCreateOnly,

        onCreateOrTriggeringUpdate,

        onAllChanges,

        OnRecursiveUpdate,
    }
    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum WorkflowTimeUnits
    {

        Hours,

        Days,
    }
    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum ActionTaskAssignedToTypes
    {

        user,

        role,

        opportunityTeam,

        accountTeam,

        owner,

        accountOwner,

        creator,

        accountCreator,

        partnerUser,

        portalRole,
    }
    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum SendAction
    {

        Send,
    }
    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum KnowledgeWorkflowAction
    {

        PublishAsNew,

        Publish,
    }
    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum LookupValueType
    {

        User,

        Queue,

        RecordType,
    }

    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum FieldUpdateOperation
    {

        Formula,

        Literal,

        Null,

        NextValue,

        PreviousValue,

        LookupValue,
    }

    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum ActionEmailRecipientTypes
    {

        group,

        role,

        user,

        opportunityTeam,

        accountTeam,

        roleSubordinates,

        owner,

        creator,

        partnerUser,

        accountOwner,

        customerPortalUser,

        portalRole,

        portalRoleSubordinates,

        contactLookup,

        userLookup,

        roleSubordinatesInternal,

        email,

        caseTeam,

        campaignMemberDerivedOwner,
    }

    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum ActionEmailSenderType
    {

        CurrentUser,

        OrgWideEmailAddress,

        DefaultWorkflowUser,
    }
    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum MonitoredEvents
    {

        AuditTrail,

        Login,

        Entity,

        DataExport,

        AccessResource,
    }
    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum StaticResourceCacheControl
    {

        Private,

        Public,
    }
    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum ForecastCategories
    {

        Omitted,

        Pipeline,

        BestCase,

        Forecast,

        Closed,
    }
    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum LayoutSectionStyle
    {

        TwoColumnsTopToBottom,

        TwoColumnsLeftToRight,

        OneColumn,

        CustomLinks,
    }
    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum UiBehavior
    {

        Edit,

        Required,

        Readonly,
    }

    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum QuickActionLabel
    {

        LogACall,

        LogANote,

        New,

        NewRecordType,

        Update,

        NewChild,

        NewChildRecordType,

        CreateNew,

        CreateNewRecordType,

        SendEmail,

        QuickRecordType,

        Quick,

        EditDescription,

        Defer,

        ChangeDueDate,

        ChangePriority,

        ChangeStatus,

        SocialPost,

        Escalate,

        EscalateToRecord,

        OfferFeedback,

        RequestFeedback,

        AddRecord,

        AddMember,
    }

    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum QuickActionType
    {

        Create,

        VisualforcePage,

        Post,

        SendEmail,

        LogACall,

        SocialPost,

        Canvas,

        Update,

        LightningComponent,
    }
    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum FormFactor
    {

        Small,

        Medium,

        Large,
    }

    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum ActionOverrideType
    {

        Default,

        Standard,

        Scontrol,

        Visualforce,

        Flexipage,
    }
    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:metadata.tooling.soap.sforce.com")]
    public enum TabVisibility
    {

        Hidden,

        DefaultOff,

        DefaultOn,
    }
    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum PlatformActionListContext
    {

        ListView,

        RelatedList,

        ListViewRecord,

        RelatedListRecord,

        Record,

        FeedElement,

        Chatter,

        Global,

        Flexipage,

        MruList,

        MruRow,

        RecordEdit,

        Photo,

        BannerPhoto,

        ObjectHomeChart,

        ListViewDefinition,

        Dockable,

        Lookup,

        Assistant,
    }
    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum PlatformActionType
    {

        QuickAction,

        StandardButton,

        CustomButton,

        ProductivityAction,

        ActionLink,

        InvocableAction,
    }
    [GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:tooling.soap.sforce.com")]
    public enum PermissionSetTabVisibility
    {

        None,

        Available,

        Visible,
    }


    #endregion

}
