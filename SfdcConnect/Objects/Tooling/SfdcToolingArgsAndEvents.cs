using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.ComponentModel;

namespace SfdcConnect.Tooling
{
    #region Args and Handlers

    [GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    public delegate void createCompletedEventHandler(object sender, createCompletedEventArgs e);

    [GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    public partial class createCompletedEventArgs : AsyncCompletedEventArgs
    {
        private object[] results;
        internal createCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        public SaveResult[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((SaveResult[])(this.results[0]));
            }
        }
    }

    [GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    public delegate void deleteCompletedEventHandler(object sender, deleteCompletedEventArgs e);

    [GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    public partial class deleteCompletedEventArgs : AsyncCompletedEventArgs
    {
        private object[] results;
        internal deleteCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        public DeleteResult[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((DeleteResult[])(this.results[0]));
            }
        }
    }

    [GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    public delegate void describeGlobalCompletedEventHandler(object sender, describeGlobalCompletedEventArgs e);

    [GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    public partial class describeGlobalCompletedEventArgs : AsyncCompletedEventArgs
    {
        private object[] results;
        internal describeGlobalCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        public DescribeGlobalResult Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((DescribeGlobalResult)(this.results[0]));
            }
        }
    }

    [GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    public delegate void describeLayoutCompletedEventHandler(object sender, describeLayoutCompletedEventArgs e);

    [GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    public partial class describeLayoutCompletedEventArgs : AsyncCompletedEventArgs
    {
        private object[] results;
        internal describeLayoutCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        public DescribeLayout[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((DescribeLayout[])(this.results[0]));
            }
        }
    }

    [GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    public delegate void describeSObjectCompletedEventHandler(object sender, describeSObjectCompletedEventArgs e);

    [GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    public partial class describeSObjectCompletedEventArgs : AsyncCompletedEventArgs
    {
        private object[] results;
        internal describeSObjectCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        public DescribeSObjectResult Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((DescribeSObjectResult)(this.results[0]));
            }
        }
    }

    [GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    public delegate void describeSObjectsCompletedEventHandler(object sender, describeSObjectsCompletedEventArgs e);

    [GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    public partial class describeSObjectsCompletedEventArgs : AsyncCompletedEventArgs
    {
        private object[] results;
        internal describeSObjectsCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        public DescribeSObjectResult[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((DescribeSObjectResult[])(this.results[0]));
            }
        }
    }

    [GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    public delegate void describeSoqlListViewsCompletedEventHandler(object sender, describeSoqlListViewsCompletedEventArgs e);

    [GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    public partial class describeSoqlListViewsCompletedEventArgs : AsyncCompletedEventArgs
    {
        private object[] results;
        internal describeSoqlListViewsCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        public DescribeSoqlListView[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((DescribeSoqlListView[])(this.results[0]));
            }
        }
    }

    [GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    public delegate void describeValueTypeCompletedEventHandler(object sender, describeValueTypeCompletedEventArgs e);

    [GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    public partial class describeValueTypeCompletedEventArgs : AsyncCompletedEventArgs
    {
        private object[] results;
        internal describeValueTypeCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        public DescribeValueTypeResult Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((DescribeValueTypeResult)(this.results[0]));
            }
        }
    }

    [GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    public delegate void describeWorkitemActionsCompletedEventHandler(object sender, describeWorkitemActionsCompletedEventArgs e);

    [GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    public partial class describeWorkitemActionsCompletedEventArgs : AsyncCompletedEventArgs
    {
        private object[] results;
        internal describeWorkitemActionsCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        public DescribeWorkitemActionResult[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((DescribeWorkitemActionResult[])(this.results[0]));
            }
        }
    }

    [GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    public delegate void executeAnonymousCompletedEventHandler(object sender, executeAnonymousCompletedEventArgs e);

    [GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    public partial class executeAnonymousCompletedEventArgs : AsyncCompletedEventArgs
    {
        private object[] results;
        internal executeAnonymousCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        public ExecuteAnonymousResult Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((ExecuteAnonymousResult)(this.results[0]));
            }
        }
    }

    [GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    public delegate void getDeletedCompletedEventHandler(object sender, getDeletedCompletedEventArgs e);

    [GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    public partial class getDeletedCompletedEventArgs : AsyncCompletedEventArgs
    {
        private object[] results;
        internal getDeletedCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        public GetDeletedResult Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((GetDeletedResult)(this.results[0]));
            }
        }
    }

    [GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    public delegate void getServerTimestampCompletedEventHandler(object sender, getServerTimestampCompletedEventArgs e);

    [GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    public partial class getServerTimestampCompletedEventArgs : AsyncCompletedEventArgs
    {
        private object[] results;
        internal getServerTimestampCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        public GetServerTimestampResult Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((GetServerTimestampResult)(this.results[0]));
            }
        }
    }

    [GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    public delegate void getUpdatedCompletedEventHandler(object sender, getUpdatedCompletedEventArgs e);

    [GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    public partial class getUpdatedCompletedEventArgs : AsyncCompletedEventArgs
    {
        private object[] results;
        internal getUpdatedCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        public GetUpdatedResult Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((GetUpdatedResult)(this.results[0]));
            }
        }
    }

    [GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    public delegate void getUserInfoCompletedEventHandler(object sender, getUserInfoCompletedEventArgs e);

    [GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    public partial class getUserInfoCompletedEventArgs : AsyncCompletedEventArgs
    {
        private object[] results;
        internal getUserInfoCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        public GetUserInfoResult Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((GetUserInfoResult)(this.results[0]));
            }
        }
    }

    [GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    public delegate void invalidateSessionsCompletedEventHandler(object sender, invalidateSessionsCompletedEventArgs e);

    [GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    public partial class invalidateSessionsCompletedEventArgs : AsyncCompletedEventArgs
    {
        private object[] results;
        internal invalidateSessionsCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        public InvalidateSessionsResult[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((InvalidateSessionsResult[])(this.results[0]));
            }
        }
    }

    [GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    public delegate void loginCompletedEventHandler(object sender, loginCompletedEventArgs e);

    [GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    public partial class loginCompletedEventArgs : AsyncCompletedEventArgs
    {
        private object[] results;
        internal loginCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        public LoginResult Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((LoginResult)(this.results[0]));
            }
        }
    }

    [GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    public delegate void logoutCompletedEventHandler(object sender, AsyncCompletedEventArgs e);

    [GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    public delegate void queryCompletedEventHandler(object sender, queryCompletedEventArgs e);

    [GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    public partial class queryCompletedEventArgs : AsyncCompletedEventArgs
    {
        private object[] results;
        internal queryCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        public QueryResult Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((QueryResult)(this.results[0]));
            }
        }
    }

    [GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    public delegate void queryAllCompletedEventHandler(object sender, queryAllCompletedEventArgs e);

    [GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    public partial class queryAllCompletedEventArgs : AsyncCompletedEventArgs
    {
        private object[] results;
        internal queryAllCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        public QueryResult Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((QueryResult)(this.results[0]));
            }
        }
    }

    [GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    public delegate void queryMoreCompletedEventHandler(object sender, queryMoreCompletedEventArgs e);

    [GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    public partial class queryMoreCompletedEventArgs : AsyncCompletedEventArgs
    {
        private object[] results;
        internal queryMoreCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        public QueryResult Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((QueryResult)(this.results[0]));
            }
        }
    }

    [GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    public delegate void retrieveCompletedEventHandler(object sender, retrieveCompletedEventArgs e);

    [GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    public partial class retrieveCompletedEventArgs : AsyncCompletedEventArgs
    {
        private object[] results;
        internal retrieveCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        public sObject[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((sObject[])(this.results[0]));
            }
        }
    }

    [GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    public delegate void runTestsCompletedEventHandler(object sender, runTestsCompletedEventArgs e);

    [GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    public partial class runTestsCompletedEventArgs : AsyncCompletedEventArgs
    {
        private object[] results;
        internal runTestsCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        public RunTestsResult Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((RunTestsResult)(this.results[0]));
            }
        }
    }

    [GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    public delegate void runTestsAsynchronousCompletedEventHandler(object sender, runTestsAsynchronousCompletedEventArgs e);

    [GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    public partial class runTestsAsynchronousCompletedEventArgs : AsyncCompletedEventArgs
    {
        private object[] results;
        internal runTestsAsynchronousCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        public string Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }

    [GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    public delegate void searchCompletedEventHandler(object sender, searchCompletedEventArgs e);

    [GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    public partial class searchCompletedEventArgs : AsyncCompletedEventArgs
    {
        private object[] results;
        internal searchCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        public SearchResult Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((SearchResult)(this.results[0]));
            }
        }
    }

    [GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    public delegate void setPasswordCompletedEventHandler(object sender, setPasswordCompletedEventArgs e);

    [GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    public partial class setPasswordCompletedEventArgs : AsyncCompletedEventArgs
    {
        private object[] results;
        internal setPasswordCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        public SetPasswordResult Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((SetPasswordResult)(this.results[0]));
            }
        }
    }

    [GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    public delegate void updateCompletedEventHandler(object sender, updateCompletedEventArgs e);

    [GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    public partial class updateCompletedEventArgs : AsyncCompletedEventArgs
    {
        private object[] results;
        internal updateCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        public SaveResult[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((SaveResult[])(this.results[0]));
            }
        }
    }

    [GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    public delegate void upsertCompletedEventHandler(object sender, upsertCompletedEventArgs e);

    [GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    public partial class upsertCompletedEventArgs : AsyncCompletedEventArgs
    {
        private object[] results;
        internal upsertCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        public UpsertResult[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((UpsertResult[])(this.results[0]));
            }
        }
    }
    #endregion

}
