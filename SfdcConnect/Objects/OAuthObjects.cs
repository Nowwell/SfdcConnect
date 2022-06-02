using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SfdcConnect.Objects
{
    public enum LoginFlow { JWTBearer, UserAgent, WebServer, OAuthUsernamePassword, AssetToken, SOAP };

    public class OAuthResponse
    {
        public string code;
        public string access_token;
        public string signature;
        public string scope;
        public string id_token;
        public string instance_url;
        public string id;
        public string token_type;
        public string issued_at;
        public string refresh_token;
        public string error;
        public string sfdc_site_url;
        public string sfdc_site_id;
        public string issued_token_type;
        public string expires_in;
    }
    public class OAuthRequest
    {
        public string client_id;
        public string client_secret;
        public string grant_type;
        public string username;
        public string password;
        public string scope;
        public string redirect_uri;
        public string response_type;
        public string code;
        public string state;
        public string refresh_token;
        public string token;
        public string login_hint;
        public string prompt;
        public string include_granted_scopes;
        public string access_type;
        public string assertion;
        public string nonce;
        public string display;
        public string subject_token_type;
        public string subject_token;
        public string actor_token_type;
        public string actor_token;

        public override string ToString()
        {
            FieldInfo[] props = this.GetType().GetFields();
            List<string> values = new List<string>();
            foreach (FieldInfo pi in props)
            {
                if (!string.IsNullOrEmpty(pi.Name) && pi.GetValue(this) != null) values.Add(pi.Name + "=" + HttpUtility.UrlEncode(pi.GetValue(this).ToString()));
            }


            //if (!string.IsNullOrEmpty(client_id)) values.Add("client_id=" + HttpUtility.UrlEncode(client_id));
            //if (!string.IsNullOrEmpty(client_secret)) values.Add("client_secret=" + HttpUtility.UrlEncode(client_secret));
            //if (!string.IsNullOrEmpty(grant_type)) values.Add("grant_type=" + HttpUtility.UrlEncode(grant_type));
            //if (!string.IsNullOrEmpty(username)) values.Add("username=" + HttpUtility.UrlEncode(username));
            //if (!string.IsNullOrEmpty(password)) values.Add("password=" + HttpUtility.UrlEncode(password));
            //if (!string.IsNullOrEmpty(scope)) values.Add("scope=" + HttpUtility.UrlEncode(scope));
            //if (!string.IsNullOrEmpty(redirect_uri)) values.Add("redirect_uri=" + HttpUtility.UrlEncode(redirect_uri));
            //if (!string.IsNullOrEmpty(response_type)) values.Add("response_type=" + HttpUtility.UrlEncode(response_type));
            //if (!string.IsNullOrEmpty(code)) values.Add("code=" + HttpUtility.UrlEncode(code));
            //if (!string.IsNullOrEmpty(state)) values.Add("state=" + HttpUtility.UrlEncode(state));
            //if (!string.IsNullOrEmpty(refresh_token)) values.Add("refresh_token=" + HttpUtility.UrlEncode(refresh_token));
            //if (!string.IsNullOrEmpty(token)) values.Add("token=" + HttpUtility.UrlEncode(token));
            //if (!string.IsNullOrEmpty(assertion)) values.Add("assertion=" + HttpUtility.UrlEncode(assertion));
            //if (!string.IsNullOrEmpty(login_hint)) values.Add("login_hint=" + HttpUtility.UrlEncode(login_hint));
            //if (!string.IsNullOrEmpty(nonce)) values.Add("nonce=" + HttpUtility.UrlEncode(nonce));

            return string.Join("&", values);

        }
    }

}
