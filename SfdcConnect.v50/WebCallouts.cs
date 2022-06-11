using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

using SfdcConnect.Testing;
using System.Threading;

namespace SfdcConnect
{
    public class WebCallout : HttpClient
    {
        public new HttpResponseMessage Send(HttpRequestMessage request)
        {
            if (Test.IsTesting)
            {
                return Test.GetResponse(request);
            }
            else
            {
                return base.Send(request);
            }
        }

        public override HttpResponseMessage Send(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (Test.IsTesting)
            {
                return Test.GetResponse(request);
            }
            else
            {
                return base.Send(request, cancellationToken);
            }
        }
    }
}
