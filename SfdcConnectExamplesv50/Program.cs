using SfdcConnect;
using System;
using System.IO;
using System.Reflection;

using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace SfdcConnectExamplesv50
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MethodInfo method = typeof(XmlSerializer).GetMethod("set_Mode", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
            method.Invoke(null, new object[] { 1 });

            string origxml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns=""urn:partner.soap.sforce.com"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:sf=""urn:sobject.partner.soap.sforce.com"">
  <soapenv:Header>
    <LimitInfoHeader>
      <limitInfo>
        <current>87</current>
        <limit>15000</limit>
        <type>API REQUESTS</type>
      </limitInfo>
    </LimitInfoHeader>
  </soapenv:Header>
  <soapenv:Body>
    <queryResponse>
      <result xsi:type=""QueryResult"">
        <done>true</done>
        <queryLocator xsi:nil=""true"" />
        <records xsi:type=""sf:sObject"">
          <sf:type>Contact</sf:type>
          <sf:Id xsi:nil=""true"" />
          <sf:Name>third</sf:Name>
        </records>
        <records xsi:type=""sf:sObject"">
          <sf:type>Contact</sf:type>
          <sf:Id xsi:nil=""true"" />
          <sf:Name>Rose Gonzalez</sf:Name>
        </records>
        <records xsi:type=""sf:sObject"">
          <sf:type>Contact</sf:type>
          <sf:Id xsi:nil=""true"" />
          <sf:Name>Sean Forbes</sf:Name>
        </records>
        <records xsi:type=""sf:sObject"">
          <sf:type>Contact</sf:type>
          <sf:Id xsi:nil=""true"" />
          <sf:Name>Jack Rogers</sf:Name>
        </records>
        <records xsi:type=""sf:sObject"">
          <sf:type>Contact</sf:type>
          <sf:Id xsi:nil=""true"" />
          <sf:Name>Pat Stumuller</sf:Name>
        </records>
        <records xsi:type=""sf:sObject"">
          <sf:type>Contact</sf:type>
          <sf:Id xsi:nil=""true"" />
          <sf:Name>Andy Young</sf:Name>
        </records>
        <records xsi:type=""sf:sObject"">
          <sf:type>Contact</sf:type>
          <sf:Id xsi:nil=""true"" />
          <sf:Name>Tim Barr</sf:Name>
        </records>
        <records xsi:type=""sf:sObject"">
          <sf:type>Contact</sf:type>
          <sf:Id xsi:nil=""true"" />
          <sf:Name>John Bond</sf:Name>
        </records>
        <records xsi:type=""sf:sObject"">
          <sf:type>Contact</sf:type>
          <sf:Id xsi:nil=""true"" />
          <sf:Name>Stella Pavlova</sf:Name>
        </records>
        <records xsi:type=""sf:sObject"">
          <sf:type>Contact</sf:type>
          <sf:Id xsi:nil=""true"" />
          <sf:Name>Lauren Boyle</sf:Name>
        </records>
        <records xsi:type=""sf:sObject"">
          <sf:type>Contact</sf:type>
          <sf:Id xsi:nil=""true"" />
          <sf:Name>Babara Levy</sf:Name>
        </records>
        <records xsi:type=""sf:sObject"">
          <sf:type>Contact</sf:type>
          <sf:Id xsi:nil=""true"" />
          <sf:Name>Josh Davis</sf:Name>
        </records>
        <records xsi:type=""sf:sObject"">
          <sf:type>Contact</sf:type>
          <sf:Id xsi:nil=""true"" />
          <sf:Name>Jane Grey</sf:Name>
        </records>
        <records xsi:type=""sf:sObject"">
          <sf:type>Contact</sf:type>
          <sf:Id xsi:nil=""true"" />
          <sf:Name>Arthur Song</sf:Name>
        </records>
        <records xsi:type=""sf:sObject"">
          <sf:type>Contact</sf:type>
          <sf:Id xsi:nil=""true"" />
          <sf:Name>Ashley James</sf:Name>
        </records>
        <records xsi:type=""sf:sObject"">
          <sf:type>Contact</sf:type>
          <sf:Id xsi:nil=""true"" />
          <sf:Name>Tom Ripley</sf:Name>
        </records>
        <records xsi:type=""sf:sObject"">
          <sf:type>Contact</sf:type>
          <sf:Id xsi:nil=""true"" />
          <sf:Name>Liz D'Cruz</sf:Name>
        </records>
        <records xsi:type=""sf:sObject"">
          <sf:type>Contact</sf:type>
          <sf:Id xsi:nil=""true"" />
          <sf:Name>Edna Frank</sf:Name>
        </records>
        <records xsi:type=""sf:sObject"">
          <sf:type>Contact</sf:type>
          <sf:Id xsi:nil=""true"" />
          <sf:Name>Avi Green</sf:Name>
        </records>
        <records xsi:type=""sf:sObject"">
          <sf:type>Contact</sf:type>
          <sf:Id xsi:nil=""true"" />
          <sf:Name>Siddartha Nedaerk</sf:Name>
        </records>
        <records xsi:type=""sf:sObject"">
          <sf:type>Contact</sf:type>
          <sf:Id xsi:nil=""true"" />
          <sf:Name>Jake Llorrac</sf:Name>
        </records>
        <records xsi:type=""sf:sObject"">
          <sf:type>Contact</sf:type>
          <sf:Id xsi:nil=""true"" />
          <sf:Name>Patrick Fife</sf:Name>
        </records>
        <records xsi:type=""sf:sObject"">
          <sf:type>Contact</sf:type>
          <sf:Id xsi:nil=""true"" />
          <sf:Name>Jill Johnson</sf:Name>
        </records>
        <size>23</size>
      </result>
    </queryResponse>
  </soapenv:Body>
</soapenv:Envelope>";

            string xml = @"<?xml version=""1.0"" encoding=""utf-16""?>
<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns=""urn:partner.soap.sforce.com"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:sf=""urn:sobject.partner.soap.sforce.com"">
  <soapenv:Header>
    <LimitInfoHeader>
      <limitInfo>
        <current>125</current>
        <limit>15000</limit>
        <type>API REQUESTS</type>
      </limitInfo>
    </LimitInfoHeader>
  </soapenv:Header>
  <soapenv:Body>
    <queryResponse>
      <result xsi:type=""QueryResult"">
        <done>true</done>
        <queryLocator xsi:nil=""true"" />
        <records xsi:type=""sObject"">
          <sf:type>Contact</sf:type>
          <sf:Id>0031K00002lKCS1QAO</sf:Id>
          <sf:Id>0031K00002lKCS1QAO</sf:Id>
          <sf:Name>third</sf:Name>
        </records>
        <size>1</size>
      </result>
    </queryResponse>
  </soapenv:Body>
</soapenv:Envelope>";

            try
            {
                ////Envelope eee = stuff();

                ////XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ////ns.Add("", "urn:partner.soap.sforce.com");
                ////ns.Add("soapenv", "http://schemas.xmlsoap.org/soap/envelope/");
                ////ns.Add("xsi", "http://www.w3.org/2001/XMLSchema-instance");
                ////ns.Add("sf", "urn:sobject.partner.soap.sforce.com");

                ////Sfdc.Soap.Partner.queryResponse qr = new Sfdc.Soap.Partner.queryResponse();
                ////qr.LimitInfoHeader = new Sfdc.Soap.Partner.LimitInfo[1];
                ////qr.LimitInfoHeader[0] = new Sfdc.Soap.Partner.LimitInfo();
                ////qr.LimitInfoHeader[0].limit = 1500;
                ////qr.LimitInfoHeader[0].current = 85;
                ////qr.LimitInfoHeader[0].type = "API REQUESTS";

                ////qr.result = new Sfdc.Soap.Partner.QueryResult();
                ////qr.result.done = true;
                ////qr.result.size = 1;
                ////qr.result.queryLocator = null;
                ////qr.result.records = new Sfdc.Soap.Partner.sObject[1];
                ////qr.result.records[0] = new Sfdc.Soap.Partner.sObject();
                ////qr.result.records[0].type = "Contact";
                ////qr.result.records[0].Id = "null";


                //XmlSerializer serializer = new XmlSerializer(typeof(Envelope));


                ////StringBuilder output = new StringBuilder();
                ////XmlWriter writer = XmlWriter.Create(output);
                ////serializer.Serialize(writer, qr, ns);

                ////string xml2 = output.ToString();


                //XmlReader reader = XmlReader.Create(new StringReader(xml));
                //Envelope t = (Envelope)serializer.Deserialize(reader);

                //Task<string> str = open();

















                open();

                //doit();
                


            }
            catch(Exception e)
            {
                string msg = e.Message;
            }
        }

        static Envelope stuff()
        {
            var type = typeof(Envelope);

            var serializer = new XmlSerializer(type);

            var xmlString = @"
               	<env:Envelope xmlns:env='http://schemas.xmlsoap.org/soap/envelope/' xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:soapenc='http://schemas.xmlsoap.org/soap/encoding/' xmlns:xsd='http://www.w3.org/2001/XMLSchema'>
				<env:Body>
				<env:Fault>
				<faultcode>env:Client.Parameters</faultcode>
				<faultstring>The number of items to retrieve value null is negative or excessive.</faultstring>
				</env:Fault>
				</env:Body>
				</env:Envelope>";

            using (var stringReader = new StringReader(xmlString))
            {
                return serializer.Deserialize(stringReader) as Envelope;
            }
        }

        static async Task<string> open()
        {
            try
            {

                Sfdc.Rest.loginRequest lr = new Sfdc.Rest.loginRequest();
                lr.username = "seanfife@hotmail.com";
                lr.password = "HH!F`@4*|YG7aFGaJFyEsTT56wTsbY71J79d";
                SfdcSession session = new SfdcSession(SfdcConnect.Environment.Production, 54);
                session.Open(SfdcConnect.OAuth.LoginFlow.SOAP, lr);

                SfdcBulkApi bulk = new SfdcBulkApi();
                bulk.Open(session);

                SfdcConnect.Bulk.Job j = bulk.CreateJob("Account", ContentType.CSV, Operations.query, ConcurrencyMode.Serial);


                session.Close();











                //Sfdc.Soap.Partner.loginRequest lr2 = new Sfdc.Soap.Partner.loginRequest();
                //lr2.username = "seanfife@hotmail.com";
                //lr2.password = "HH!F`@4*|YG7aFGaJFyEsTT56wTsbY71J79d";
                //lr2.CallOptions = new Sfdc.Soap.Partner.CallOptions();
                //lr2.LoginScopeHeader = new Sfdc.Soap.Partner.LoginScopeHeader();
                //SfdcSoapApi api = new SfdcSoapApi(SfdcConnect.Environment.Production, 54);
                ////api.Endpoint.EndpointBehaviors.Add(new InspectorBehavior());
                //api.Open(lr2);
                //Sfdc.Soap.Partner.QueryResult qr2 = null;
                //qr2 = api.query("SELECT Id, Name FROM Contact LIMIT 1");//, new Sfdc.Soap.Partner.QueryOptions() { batchSize = 200 });
                //api.Close();


















                //Sfdc.Rest.loginRequest lr = new Sfdc.Rest.loginRequest();
                //lr.username = "seanfife@hotmail.com";
                //lr.password = "HH!F`@4*|YG7aFGaJFyEsTT56wTsbY71J79d";
                //SfdcSession session = new SfdcSession(SfdcConnect.Environment.Production, 54);
                //session.Open(SfdcConnect.OAuth.LoginFlow.SOAP, lr);

                //Sfdc.Soap.Partner.SoapClient.EndpointsList.Add(Sfdc.Soap.Partner.SoapClient.EndpointConfiguration.LoggedIn, session.Endpoint.Address.Uri.AbsoluteUri);

                //Sfdc.Soap.Partner.SoapClient client = new Sfdc.Soap.Partner.SoapClient(Sfdc.Soap.Partner.SoapClient.EndpointConfiguration.LoggedIn);// Sfdc.Soap.Partner.SoapClient.EndpointConfiguration.Soap);
                //client.Endpoint.EndpointBehaviors.Add(new InspectorBehavior());
                //client.InnerChannel.Open();


                //Sfdc.Soap.Partner.QueryResult qr = null;

                //MethodInfo method = typeof(XmlSerializer).GetMethod("set_Mode", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
                //method.Invoke(null, new object[] { 1 });
                //client.query(new Sfdc.Soap.Partner.SessionHeader() { sessionId = session.SessionId }, new Sfdc.Soap.Partner.CallOptions(), new Sfdc.Soap.Partner.QueryOptions() { batchSize = 200 }, new Sfdc.Soap.Partner.MruHeader(), null, "SELECT Id FROM Contact", out qr);

                //session.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return "";
        }

        static async Task<string> doit()
        {
            Console.WriteLine("before await");
            string str = await open();
            Console.WriteLine("after await");
            return str;
        }

    }




    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.xmlsoap.org/soap/envelope/", IsNullable = false)]
    public partial class Envelope
    {

        private EnvelopeHeader headerField;

        private EnvelopeBody bodyField;

        /// <remarks/>
        public EnvelopeHeader Header
        {
            get
            {
                return this.headerField;
            }
            set
            {
                this.headerField = value;
            }
        }

        /// <remarks/>
        public EnvelopeBody Body
        {
            get
            {
                return this.bodyField;
            }
            set
            {
                this.bodyField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public partial class EnvelopeHeader
    {

        private LimitInfoHeader limitInfoHeaderField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "urn:partner.soap.sforce.com")]
        public LimitInfoHeader LimitInfoHeader
        {
            get
            {
                return this.limitInfoHeaderField;
            }
            set
            {
                this.limitInfoHeaderField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:partner.soap.sforce.com")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "urn:partner.soap.sforce.com", IsNullable = false)]
    public partial class LimitInfoHeader
    {

        private LimitInfoHeaderLimitInfo limitInfoField;

        /// <remarks/>
        public LimitInfoHeaderLimitInfo limitInfo
        {
            get
            {
                return this.limitInfoField;
            }
            set
            {
                this.limitInfoField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:partner.soap.sforce.com")]
    public partial class LimitInfoHeaderLimitInfo
    {

        private byte currentField;

        private ushort limitField;

        private string typeField;

        /// <remarks/>
        public byte current
        {
            get
            {
                return this.currentField;
            }
            set
            {
                this.currentField = value;
            }
        }

        /// <remarks/>
        public ushort limit
        {
            get
            {
                return this.limitField;
            }
            set
            {
                this.limitField = value;
            }
        }

        /// <remarks/>
        public string type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public partial class EnvelopeBody
    {

        private queryResponse queryResponseField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "urn:partner.soap.sforce.com")]
        public queryResponse queryResponse
        {
            get
            {
                return this.queryResponseField;
            }
            set
            {
                this.queryResponseField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:partner.soap.sforce.com")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "urn:partner.soap.sforce.com", IsNullable = false)]
    public partial class queryResponse
    {

        private QueryResult resultField;

        /// <remarks/>
        public QueryResult result
        {
            get
            {
                return this.resultField;
            }
            set
            {
                this.resultField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:partner.soap.sforce.com")]
    public partial class QueryResult
    {

        private bool doneField;

        private object queryLocatorField;

        private sObject recordsField;

        private byte sizeField;

        /// <remarks/>
        public bool done
        {
            get
            {
                return this.doneField;
            }
            set
            {
                this.doneField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
        public object queryLocator
        {
            get
            {
                return this.queryLocatorField;
            }
            set
            {
                this.queryLocatorField = value;
            }
        }

        /// <remarks/>
        public sObject records
        {
            get
            {
                return this.recordsField;
            }
            set
            {
                this.recordsField = value;
            }
        }

        /// <remarks/>
        public byte size
        {
            get
            {
                return this.sizeField;
            }
            set
            {
                this.sizeField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:partner.soap.sforce.com")]
    public partial class sObject
    {

        private string typeField;

        private string[] idField;

        private string nameField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "urn:sobject.partner.soap.sforce.com")]
        public string type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Id", Namespace = "urn:sobject.partner.soap.sforce.com")]
        public string[] Id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "urn:sobject.partner.soap.sforce.com")]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }
    }

















}
