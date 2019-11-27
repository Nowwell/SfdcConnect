/****************************************************************************
*
*   File name: SfdcLogin.cs
*   Author: Sean Fife
*   Create date: 4/20/2016
*   Solution: SfdcConnect
*   Project: SfdcConnect
*   Description: Includes the base class for Salesforce API Connections
*
****************************************************************************/
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Services.Protocols;
using SfdcConnect.SoapObjects;
using System.Security.Cryptography;

namespace SfdcConnect
{
    /// <summary>
    /// Base connection class for all of the Salesforce API classes
    /// 
    /// Has login and logout functionality to obtain a session id for use in
    ///   the derived classes.
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name = "SoapBinding", Namespace = "urn:partner.soap.sforce.com")]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(ApiFault))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(NameObjectValuePair))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(SearchLayoutFieldsDisplayed))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(SearchLayoutButtonsDisplayed))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(location))]
    public class SfdcConnection : SfdcLoginBase
    {
        public SfdcConnection() : base()
        {
        }
        public SfdcConnection(string uri) : base(uri)
        {
        }
        public SfdcConnection(bool isTest, int apiversion) : base(isTest, apiversion)
        {

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

            state = ConnectionState.Open;
        }
        /// <summary>
        /// Open a connection to Salesforce asynchronously.  Requires Usernamd and Password to be filled in.
        /// </summary>
        public virtual void OpenAsync()
        {
            if (state == ConnectionState.Open) return;
            state = ConnectionState.Connecting;

            loginCompleted += ls_loginCompleted;
            LoginAsync(Username, Password + Token);

            //return default(Task);
        }
        /// <summary>
        /// Open a connection to Salesforce asynchronously.  Requires Usernamd and Password to be filled in.
        /// </summary>
        public virtual void OpenAsync(customLoginCompletedEventHandler loginCompleted)
        {
            if (state == ConnectionState.Open) return;
            state = ConnectionState.Connecting;

            customLoginCompleted += loginCompleted;
            loginCompleted += ls_loginCompleted;
            LoginAsync(Username, Password + Token);

            //return default(Task);
        }

        /// <summary>
        /// Open a connection to Salesforce asynchronously.  Requires Usernamd and Password to be filled in.
        /// </summary>
        public virtual async Task OpenAsync(CancellationToken cancellationToken)
        {
            if (state == ConnectionState.Open) return;
            state = ConnectionState.Connecting;

            loginCompleted += ls_loginCompleted;
            await LoginAsync(Username, Password + Token, cancellationToken);
        }

        /// <summary>
        /// Callback for OpenAsync calls
        /// </summary>
        /// <param name="sender">should be this object</param>
        /// <param name="e">Event Args holding the login information from Salesforce</param>
        private void ls_loginCompleted(object sender, loginCompletedEventArgs e)
        {

            LoginTime = DateTime.Now;
            Url = e.Result.serverUrl;
            SessionHeaderValue = new SessionHeader();
            SessionHeaderValue.sessionId = e.Result.sessionId;

            SessionId = e.Result.sessionId;
            ServerUrl = e.Result.serverUrl;

            string[] pieces = ServerUrl.Split('/');

            Version = pieces[pieces.Length - 2];

            ApiEndPoint = new Uri(e.Result.serverUrl);

            state = ConnectionState.Open;

            if (customLoginCompleted != null) customLoginCompleted(sender, e);
        }

        /// <summary>
        /// Close the Salesforce connection
        /// </summary>
        public override void Close()
        {
            Url = ServerUrl;

            logout();

            state = ConnectionState.Closed;
        }

        /// <summary>
        /// Returns the Limit Info returned with the SOAP login call
        /// </summary>
        public LimitInfo[] LimitInfoFromLogin
        {
            get
            {
                if (LimitInfoHeaderValue == null) return null;
                return LimitInfoHeaderValue.limitInfo;
            }
        }

        #region SFDC - Originally from auto generated wsdl
        public LoginScopeHeader LoginScopeHeaderValue { get; set; }
        public CallOptions CallOptionsValue { get; set; }
        public SessionHeader SessionHeaderValue { get; set; }
        public LimitInfoHeader LimitInfoHeaderValue { get; set; }

        public delegate void customLoginCompletedEventHandler(object sender, loginCompletedEventArgs e);
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
        public delegate void loginCompletedEventHandler(object sender, loginCompletedEventArgs e);
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
        public delegate void logoutCompletedEventHandler(object sender, System.ComponentModel.AsyncCompletedEventArgs e);

        private SendOrPostCallback loginOperationCompleted;
        private SendOrPostCallback logoutOperationCompleted;

        public event loginCompletedEventHandler loginCompleted;
        public event customLoginCompletedEventHandler customLoginCompleted;
        public event logoutCompletedEventHandler logoutCompleted;

        [SoapHeaderAttribute("LoginScopeHeaderValue")]
        [SoapHeaderAttribute("CallOptionsValue")]
        [SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("result")]
        public LoginResult login(string username, string password)
        {
            object[] results = this.Invoke("login", new object[] { username, password });
            return ((LoginResult)(results[0]));
        }

        [SoapHeaderAttribute("SessionHeaderValue")]
        [SoapHeaderAttribute("CallOptionsValue")]
        [SoapHeaderAttribute("LimitInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
        [SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
        public void logout()
        {
            this.Invoke("logout", new object[0]);
        }

        /// <remarks/>
        public void LoginAsync(string username, string password)
        {
            this.LoginAsync(username, password, null);
        }
        /// <remarks/>
        public async Task LoginAsync(string username, string password, object userState)
        {
            if ((this.loginOperationCompleted == null))
            {
                this.loginOperationCompleted = new SendOrPostCallback(this.OnloginOperationCompleted);
            }
            await Task.Run(() =>
            {
                base.InvokeAsync("login", new object[] {
                        username,
                        password}, this.loginOperationCompleted, userState);
            });
        }
        private void OnloginOperationCompleted(object arg)
        {
            if ((this.loginCompleted != null))
            {
                InvokeCompletedEventArgs invokeArgs = ((InvokeCompletedEventArgs)(arg));
                this.loginCompleted(this, new loginCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        public void LogoutAsync()
        {
            this.LogoutAsync(null);
        }
        /// <remarks/>
        public void LogoutAsync(object userState)
        {
            if ((this.logoutOperationCompleted == null))
            {
                this.logoutOperationCompleted = new SendOrPostCallback(this.OnlogoutOperationCompleted);
            }
            base.InvokeAsync("logout", new object[0], this.logoutOperationCompleted, userState);
        }
        private void OnlogoutOperationCompleted(object arg)
        {
            if ((this.logoutCompleted != null))
            {
                InvokeCompletedEventArgs invokeArgs = ((InvokeCompletedEventArgs)(arg));
                this.logoutCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        #endregion
    }
}
