/****************************************************************************
*
*   File name: SfdcMetadataApi.cs
*   Author: Sean Fife
*   Create date: 5/27/2022
*   Solution: SfdcConnect
*   Project: SfdcConnect
*   Description: Includes the class for Salesforce Metadata API Calls
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
using Sfdc.Soap.Metadata;

namespace SfdcConnect
{
    public class SfdcMetadataApi : System.ServiceModel.ClientBase<MetadataPortType>
    {
        public SfdcMetadataApi(EndpointConfiguration endpointConfiguration, string remoteAddress) :
                 base(GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress))
        {
            EndpointsList.Add(endpointConfiguration, remoteAddress);

            Endpoint.Name = endpointConfiguration.ToString();

            Channel = base.ChannelFactory.CreateChannel();

            DataProtector = new SfdcDataProtection();

            SetXmlSerializerFlag(1);
        }
        public SfdcMetadataApi(Environment env, int apiversion, string refreshToken = "") :
                base(GetDefaultBinding(),
                    new System.ServiceModel.EndpointAddress(string.Format("https://{0}.salesforce.com/services/Soap/m/{1}.0", env == Environment.Sandbox ? "test" : "login", apiversion)))
        {
            EndpointsList.Add(EndpointConfiguration.Login, string.Format("https://{0}.salesforce.com/services/Soap/m/{1}.0", env == Environment.Sandbox ? "test" : "login", apiversion));

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

        protected new MetadataPortType Channel { get; set; }

        internal SfdcDataProtection DataProtector;
        protected SfdcSession InternalConnection;

        public CallOptions CallOptions { get; set; }
        public PackageVersion[] PackageVersionHeader { get; set; }
        public AllOrNoneHeader AllOrNoneHeader { get; set; }
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
                EndpointsList[EndpointConfiguration.Active] = conn.Endpoint.Address.Uri.AbsoluteUri.Replace("/u/", "/m/");
            }
            else
            {
                EndpointsList.Add(EndpointConfiguration.Active, conn.Endpoint.Address.Uri.AbsoluteUri.Replace("/u/", "/m/"));
            }

            ChangeEndpoint(EndpointConfiguration.Active);

            ((IClientChannel)Channel).Close();

            Channel = base.ChannelFactory.CreateChannel();

            ((IClientChannel)Channel).Open();
        }

        public new void Close()
        {
            //InternalConnection.Close();
            ((IClientChannel)Channel).Close();
        }

        #region Methods
        public CancelDeployResult cancelDeploy(string String)
        {
            cancelDeployRequest inValue = new cancelDeployRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.String = String;
            cancelDeployResponse retVal = Channel.cancelDeploy(inValue);
            return retVal.result;
        }

        public async Task<CancelDeployResult> cancelDeployAsync(string String)
        {
            cancelDeployRequest inValue = new cancelDeployRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.String = String;
            cancelDeployResponse t = await Channel.cancelDeployAsync(inValue);
            return t.result;
        }

        public DeployResult checkDeployStatus(string asyncProcessId, bool includeDetails)
        {
            checkDeployStatusRequest inValue = new checkDeployStatusRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.asyncProcessId = asyncProcessId;
            inValue.includeDetails = includeDetails;
            checkDeployStatusResponse retVal = Channel.checkDeployStatus(inValue);
            DebuggingInfo = retVal.DebuggingInfo;
            return retVal.result;
        }

        public async Task<DeployResult> checkDeployStatusAsync(string asyncProcessId, bool includeDetails)
        {
            checkDeployStatusRequest inValue = new checkDeployStatusRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.asyncProcessId = asyncProcessId;
            inValue.includeDetails = includeDetails;
            checkDeployStatusResponse t = await Channel.checkDeployStatusAsync(inValue);
            DebuggingInfo = t.DebuggingInfo;
            return t.result;
        }

        public RetrieveResult checkRetrieveStatus(string asyncProcessId, bool includeZip)
        {
            checkRetrieveStatusRequest inValue = new checkRetrieveStatusRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.asyncProcessId = asyncProcessId;
            inValue.includeZip = includeZip;
            checkRetrieveStatusResponse retVal = Channel.checkRetrieveStatus(inValue);
            return retVal.result;
        }

        public async Task<RetrieveResult> checkRetrieveStatusAsync(string asyncProcessId, bool includeZip)
        {
            checkRetrieveStatusRequest inValue = new checkRetrieveStatusRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.asyncProcessId = asyncProcessId;
            inValue.includeZip = includeZip;
            checkRetrieveStatusResponse t = await Channel.checkRetrieveStatusAsync(inValue);
            return t.result;
        }

        public SaveResult[] createMetadata(Metadata[] metadata)
        {
            createMetadataRequest inValue = new createMetadataRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.AllOrNoneHeader = AllOrNoneHeader;
            inValue.metadata = metadata;
            createMetadataResponse retVal = Channel.createMetadata(inValue);
            return retVal.result;
        }

        public async Task<SaveResult[]> createMetadataAsync(Metadata[] metadata)
        {
            createMetadataRequest inValue = new createMetadataRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.AllOrNoneHeader = AllOrNoneHeader;
            inValue.metadata = metadata;
            createMetadataResponse t = await Channel.createMetadataAsync(inValue);
            return t.result;
        }

        public DeleteResult[] deleteMetadata(string type, string[] fullNames)
        {
            deleteMetadataRequest inValue = new deleteMetadataRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.AllOrNoneHeader = AllOrNoneHeader;
            inValue.type = type;
            inValue.fullNames = fullNames;
            deleteMetadataResponse retVal = Channel.deleteMetadata(inValue);
            return retVal.result;
        }

        public async Task<DeleteResult[]> deleteMetadataAsync(string type, string[] fullNames)
        {
            deleteMetadataRequest inValue = new deleteMetadataRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.AllOrNoneHeader = AllOrNoneHeader;
            inValue.type = type;
            inValue.fullNames = fullNames;
            deleteMetadataResponse t = await Channel.deleteMetadataAsync(inValue);
            return t.result;
        }

        public AsyncResult deploy(byte[] ZipFile, DeployOptions DeployOptions)
        {
            deployRequest inValue = new deployRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.DebuggingHeader = DebuggingHeader;
            inValue.CallOptions = CallOptions;
            inValue.ZipFile = ZipFile;
            inValue.DeployOptions = DeployOptions;
            deployResponse retVal = Channel.deploy(inValue);
            return retVal.result;
        }

        public async Task<AsyncResult> deployAsync(byte[] ZipFile, DeployOptions DeployOptions)
        {
            deployRequest inValue = new deployRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.DebuggingHeader = DebuggingHeader;
            inValue.CallOptions = CallOptions;
            inValue.ZipFile = ZipFile;
            inValue.DeployOptions = DeployOptions;
            deployResponse t = await Channel.deployAsync(inValue);
            return t.result;
        }

        public string deployRecentValidation(string validationId)
        {
            deployRecentValidationRequest inValue = new deployRecentValidationRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.DebuggingHeader = DebuggingHeader;
            inValue.CallOptions = CallOptions;
            inValue.validationId = validationId;
            deployRecentValidationResponse retVal = Channel.deployRecentValidation(inValue);
            return retVal.result;
        }

        public async Task<string> deployRecentValidationAsync(string validationId)
        {
            deployRecentValidationRequest inValue = new deployRecentValidationRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.DebuggingHeader = DebuggingHeader;
            inValue.CallOptions = CallOptions;
            inValue.validationId = validationId;
            deployRecentValidationResponse t = await Channel.deployRecentValidationAsync(inValue);
            return t.result;
        }

        public DescribeMetadataResult describeMetadata(double asOfVersion)
        {
            describeMetadataRequest inValue = new describeMetadataRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.asOfVersion = asOfVersion;
            describeMetadataResponse retVal = Channel.describeMetadata(inValue);
            return retVal.result;
        }

        public async Task<DescribeMetadataResult> describeMetadataAsync(double asOfVersion)
        {
            describeMetadataRequest inValue = new describeMetadataRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.asOfVersion = asOfVersion;
            describeMetadataResponse t = await Channel.describeMetadataAsync(inValue);
            return t.result;
        }

        public DescribeValueTypeResult describeValueType(string type)
        {
            describeValueTypeRequest inValue = new describeValueTypeRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.type = type;
            describeValueTypeResponse retVal = Channel.describeValueType(inValue);
            return retVal.result;
        }

        public async Task<DescribeValueTypeResult> describeValueTypeAsync(string type)
        {
            describeValueTypeRequest inValue = new describeValueTypeRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.type = type;
            describeValueTypeResponse t = await Channel.describeValueTypeAsync(inValue);
            return t.result;
        }

        public FileProperties[] listMetadata(ListMetadataQuery[] queries, double asOfVersion)
        {
            listMetadataRequest inValue = new listMetadataRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.queries = queries;
            inValue.asOfVersion = asOfVersion;
            listMetadataResponse retVal = Channel.listMetadata(inValue);
            return retVal.result;
        }

        public async Task<FileProperties[]> listMetadataAsync(ListMetadataQuery[] queries, double asOfVersion)
        {
            listMetadataRequest inValue = new listMetadataRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.queries = queries;
            inValue.asOfVersion = asOfVersion;
            listMetadataResponse t = await Channel.listMetadataAsync(inValue);
            return t.result;
        }

        public Metadata[] readMetadata(string type, string[] fullNames)
        {
            readMetadataRequest inValue = new readMetadataRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.type = type;
            inValue.fullNames = fullNames;
            readMetadataResponse retVal = Channel.readMetadata(inValue);
            return retVal.result;
        }

        public async Task<Metadata[]> readMetadataAsync(string type, string[] fullNames)
        {
            readMetadataRequest inValue = new readMetadataRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.type = type;
            inValue.fullNames = fullNames;
            readMetadataResponse t = await Channel.readMetadataAsync(inValue);
            return t.result;
        }

        public SaveResult renameMetadata(string type, string oldFullName, string newFullName)
        {
            renameMetadataRequest inValue = new renameMetadataRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.type = type;
            inValue.oldFullName = oldFullName;
            inValue.newFullName = newFullName;
            renameMetadataResponse retVal = Channel.renameMetadata(inValue);
            return retVal.result;
        }

        public async Task<SaveResult> renameMetadataAsync(string type, string oldFullName, string newFullName)
        {
            renameMetadataRequest inValue = new renameMetadataRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.type = type;
            inValue.oldFullName = oldFullName;
            inValue.newFullName = newFullName;
            renameMetadataResponse t = await Channel.renameMetadataAsync(inValue);
            return t.result;
        }

        public AsyncResult retrieve(RetrieveRequest retrieveRequest)
        {
            retrieveRequest1 inValue = new retrieveRequest1();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.retrieveRequest = retrieveRequest;
            retrieveResponse retVal = Channel.retrieve(inValue);
            return retVal.result;
        }

        public async Task<AsyncResult> retrieveAsync(RetrieveRequest retrieveRequest)
        {
            retrieveRequest1 inValue = new retrieveRequest1();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.retrieveRequest = retrieveRequest;
            retrieveResponse t = await Channel.retrieveAsync(inValue);
            return t.result;
        }

        public SaveResult[] updateMetadata(Metadata[] metadata)
        {
            updateMetadataRequest inValue = new updateMetadataRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.AllOrNoneHeader = AllOrNoneHeader;
            inValue.metadata = metadata;
            updateMetadataResponse retVal = Channel.updateMetadata(inValue);
            return retVal.result;
        }

        public async Task<SaveResult[]> updateMetadataAsync(Metadata[] metadata)
        {
            updateMetadataRequest inValue = new updateMetadataRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.AllOrNoneHeader = AllOrNoneHeader;
            inValue.metadata = metadata;
            updateMetadataResponse t = await Channel.updateMetadataAsync(inValue);
            return t.result;
        }

        public UpsertResult[] upsertMetadata(Metadata[] metadata)
        {
            upsertMetadataRequest inValue = new upsertMetadataRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.AllOrNoneHeader = AllOrNoneHeader;
            inValue.metadata = metadata;
            upsertMetadataResponse retVal = Channel.upsertMetadata(inValue);
            return retVal.result;
        }

        public async Task<UpsertResult[]> upsertMetadataAsync(Metadata[] metadata)
        {
            upsertMetadataRequest inValue = new upsertMetadataRequest();
            inValue.SessionHeader = GetSessionHeader();
            inValue.CallOptions = CallOptions;
            inValue.AllOrNoneHeader = AllOrNoneHeader;
            inValue.metadata = metadata;
            upsertMetadataResponse t = await Channel.upsertMetadataAsync(inValue);
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
