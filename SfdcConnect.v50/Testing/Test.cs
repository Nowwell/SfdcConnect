using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SfdcConnect.Testing
{
    public class Test
    {
        public static IMockCallout MockClass { get; set; }
        public static bool IsTesting { get; set; } = false;

        public static void SetMockCallout(IMockCallout instantiatedCalloutObject)
        {
            MockClass = instantiatedCalloutObject;
        }

        public static HttpResponseMessage GetResponse(HttpRequestMessage request)
        {
            return MockClass.GetResponse(request);
        }
    }

    public interface IMockCallout
    {
        public HttpResponseMessage GetResponse(HttpRequestMessage request);
    }
}
