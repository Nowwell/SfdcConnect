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
    public class GenericMockCallout : IMockCallout
    {
        HttpResponseMessage IMockCallout.GetResponse(HttpRequestMessage request)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            response.StatusCode = HttpStatusCode.OK;

            string responseObject = "Success";

            // Construct a response.
            if (responseObject != null)
            {
                response.Content = new StringContent(responseObject);
            }

            return response;
        }
    }
}
