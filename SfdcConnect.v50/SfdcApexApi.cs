/****************************************************************************
*
*   File name: SfdcApexApi.cs
*   Author: Sean Fife
*   Create date: 5/27/2022
*   Solution: SfdcConnect
*   Project: SfdcConnect
*   Description: Includes the class for Salesforce Apex API Calls
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
using Sfdc.Soap.Apex;

namespace SfdcConnect
{
    public class SfdcApexApi : System.ServiceModel.ClientBase<ApexPortType>
    {
        public SfdcApexApi(EndpointConfiguration endpointConfiguration, string remoteAddress) :
          base(GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress))
        {
            EndpointsList.Add(endpointConfiguration, remoteAddress);

            Endpoint.Name = endpointConfiguration.ToString();

            Channel = base.ChannelFactory.CreateChannel();

            DataProtector = new SfdcDataProtection();

            SetXmlSerializerFlag(1);
        }
        public SfdcApexApi(Environment env, int apiversion, string refreshToken = "") :
                base(GetDefaultBinding(),
                    new System.ServiceModel.EndpointAddress(string.Format("https://{0}.salesforce.com/services/Soap/s/{1}.0", env == Environment.Sandbox ? "test" : "login", apiversion)))
        {
            EndpointsList.Add(EndpointConfiguration.Login, string.Format("https://{0}.salesforce.com/services/Soap/s/{1}.0", env == Environment.Sandbox ? "test" : "login", apiversion));

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

        protected new ApexPortType Channel { get; set; }

        internal SfdcDataProtection DataProtector;
        protected SfdcSession InternalConnection;

        public CallOptions CallOptions { get; set; }
        public PackageVersion[] PackageVersionHeader { get; set; }
        public AllowFieldTruncationHeader AllowFieldTruncationHeader { get; set; }
        public DisableFeedTrackingHeader DisableFeedTrackingHeader { get; set; }
        public DebuggingHeader DebuggingHeader { get; set; }

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
                EndpointsList[EndpointConfiguration.Active] = conn.Endpoint.Address.Uri.AbsoluteUri.Replace("/u/", "/s/");
            }
            else
            {
                EndpointsList.Add(EndpointConfiguration.Active, conn.Endpoint.Address.Uri.AbsoluteUri.Replace("/u/", "/s/"));
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
        public CompileAndTestResult compileAndTest(CompileAndTestRequest CompileAndTestRequest)
        {
            compileAndTestRequest1 inValue = new compileAndTestRequest1();
            inValue.SessionHeader = GetSessionHeader();
            inValue.DebuggingHeader = DebuggingHeader;
            inValue.PackageVersionHeader = PackageVersionHeader;
            inValue.CallOptions = CallOptions;
            inValue.CompileAndTestRequest = CompileAndTestRequest;
            compileAndTestResponse retVal = Channel.compileAndTest(inValue);
            DebuggingInfo = retVal.DebuggingInfo;
            return retVal.result;
        }

        public async Task<CompileAndTestResult> compileAndTestAsync(CompileAndTestRequest CompileAndTestRequest)
        {
            compileAndTestRequest1 inValue = new compileAndTestRequest1();
            inValue.SessionHeader = GetSessionHeader();
            inValue.DebuggingHeader = DebuggingHeader;
            inValue.PackageVersionHeader = PackageVersionHeader;
            inValue.CallOptions = CallOptions;
            inValue.CompileAndTestRequest = CompileAndTestRequest;
            compileAndTestResponse t = await Channel.compileAndTestAsync(inValue);
            DebuggingInfo = t.DebuggingInfo;
            return t.result;
        }

        public CompileClassResult[] compileClasses(string[] scripts)
        {
            compileClassesRequest inValue = new compileClassesRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.PackageVersionHeader = PackageVersionHeader;
            inValue.CallOptions = CallOptions;
            inValue.scripts = scripts;
            compileClassesResponse retVal = Channel.compileClasses(inValue);
            return retVal.result;
        }

        public async Task<CompileClassResult[]> compileClassesAsync(string[] scripts)
        {
            compileClassesRequest inValue = new compileClassesRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.PackageVersionHeader = PackageVersionHeader;
            inValue.CallOptions = CallOptions;
            inValue.scripts = scripts;
            compileClassesResponse t = await Channel.compileClassesAsync(inValue);
            return t.result;
        }

        public CompileTriggerResult[] compileTriggers(string[] scripts)
        {
            compileTriggersRequest inValue = new compileTriggersRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.PackageVersionHeader = PackageVersionHeader;
            inValue.CallOptions = CallOptions;
            inValue.scripts = scripts;
            compileTriggersResponse retVal = Channel.compileTriggers(inValue);
            return retVal.result;
        }

        public async Task<CompileTriggerResult[]> compileTriggersAsync(string[] scripts)
        {
            compileTriggersRequest inValue = new compileTriggersRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.PackageVersionHeader = PackageVersionHeader;
            inValue.CallOptions = CallOptions;
            inValue.scripts = scripts;
            compileTriggersResponse t = await Channel.compileTriggersAsync(inValue);
            return t.result;
        }

        public ExecuteAnonymousResult executeAnonymous(string apexCode)
        {
            executeAnonymousRequest inValue = new executeAnonymousRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.DebuggingHeader = DebuggingHeader;
            inValue.PackageVersionHeader = PackageVersionHeader;
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
            inValue.PackageVersionHeader = PackageVersionHeader;
            inValue.CallOptions = CallOptions;
            inValue.AllowFieldTruncationHeader = AllowFieldTruncationHeader;
            inValue.DisableFeedTrackingHeader = DisableFeedTrackingHeader;
            inValue.String = apexCode;
            executeAnonymousResponse t = await Channel.executeAnonymousAsync(inValue);
            DebuggingInfo = t.DebuggingInfo;
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

        public WsdlToApexResult wsdlToApex(WsdlToApexInfo info)
        {
            wsdlToApexRequest inValue = new wsdlToApexRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.info = info;
            wsdlToApexResponse retVal = Channel.wsdlToApex(inValue);
            return retVal.result;
        }

        public async Task<WsdlToApexResult> wsdlToApexAsync(WsdlToApexInfo info)
        {
            wsdlToApexRequest inValue = new wsdlToApexRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.info = info;
            wsdlToApexResponse t = await Channel.wsdlToApexAsync(inValue);
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
