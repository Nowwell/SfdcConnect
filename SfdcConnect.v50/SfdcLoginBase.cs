using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services.Protocols;

namespace SfdcConnect
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public class SfdcLoginBase : System.ServiceModel.ClientBase<Salesforce.Partner.Soap>
    { 
        public SfdcLoginBase()
        {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

            state = ConnectionState.Closed;

        }
        public SfdcLoginBase(string uri)
        {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            state = ConnectionState.Closed;

            Url = uri;

            if ((this.IsLocalFileSystemWebService(this.Url) == true))
            {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else
            {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        public SfdcLoginBase(bool isTest, int apiversion)
        {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            state = ConnectionState.Closed;

            if (isTest)
            {
                Url = string.Format("https://test.salesforce.com/services/Soap/u/{0}.0", apiversion);
            }
            else
            {
                Url = string.Format("https://login.salesforce.com/services/Soap/u/{0}.0", apiversion);
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

        protected string baseUrl = "";
        protected string identityEndpoint;

        /// <summary>
        /// Salesforce SessionId, only valid after Opening a connection
        /// </summary>
        public string SessionId { get; protected set; }
        /// <summary>
        /// Salesforce API Endpoint Uri, only valid after opening a connection.
        /// There is also Url which should contain the same value.
        /// </summary>
        protected string ServerUrl;
        /// <summary>
        /// State of the connection.  e.g. closed, open, connecting, etc.
        /// </summary>
        protected ConnectionState state;
        /// <summary>
        /// Date and time of the last Open or OpenAsync call
        /// </summary>
        protected DateTime LoginTime;

        /// <summary>
        /// Username for the connection
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// Plain text password for the connection
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Salseforce security token, if necessary
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// API Version, set after opening a connection
        /// </summary>
        public string Version { get; protected set; }

        /// <summary>
        /// The connections current state
        /// </summary>
        public ConnectionState State
        {
            get { return state; }
        }
        /// <summary>
        /// The last HTTP Status Code
        /// </summary>
        public System.Net.HttpStatusCode LastStatusCode { get; protected set; }

        /// <summary>
        /// Uri used for the REST API
        /// </summary>
        public Uri ApiEndPoint { get; protected set; }
        /// <summary>
        /// Uri used for the Metadata API
        /// </summary>
        public Uri MetadataEndPoint { get; protected set; }

        /// <summary>
        /// OAuth Refresh Token
        /// </summary>
        public string RefreshToken { get; protected set; }

        public string AssetToken { get; set; }

        /// <summary>
        /// Callback Endpoint for OAuth. If unset, it will generate an endpoint on http://127.0.0.1 for a random port.
        /// </summary>
        public string CallbackEndpoint;
        /// <summary>
        /// Determines which OAuth flow is being used, or if SOAP is used
        /// </summary>
        public Objects.LoginFlow Flow { get; protected set; }
        /// <summary>
        /// OAuth ClientId
        /// </summary>
        public string ClientId { get; set; }
        /// <summary>
        /// OAuth ClientSecret
        /// </summary>
        public string ClientSecret { get; set; }



        /// <summary>
        /// Sets the password from a base 64 encoded, windows encrypted password
        /// </summary>
        /// <param name="base64Encryptedpassword">encrypted, base 64 password</param>
        /// <param name="scope">Data Protection Scope, either Current User or Local Machine</param>
        public void FromEncryptedPassword(string base64Encryptedpassword, DataProtectionScope scope = DataProtectionScope.LocalMachine)
        {
            byte[] pwd = Convert.FromBase64String(base64Encryptedpassword);
            byte[] bytesPwd = ProtectedData.Unprotect(pwd, null, scope);
            Password = System.Text.Encoding.Unicode.GetString(bytesPwd);
        }



        /// <summary>
        /// Open a connection to Salesforce.  Requires Usernamd and Password to be filled in.
        /// </summary>
        public virtual void Open()
        {
        }

        /// <summary>
        /// Close the Salesforce connection
        /// </summary>
        public virtual void Close()
        {
        }


        public string Url
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
                    UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }

        protected bool useDefaultCredentialsSetExplicitly;
        public bool UseDefaultCredentials
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
        protected bool IsLocalFileSystemWebService(string url)
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
