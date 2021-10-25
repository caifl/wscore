using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Services;
using System.Web.Services.Protocols;

namespace WSCore.Samples
{
    [WebService(Name = "GreetingService", Namespace = "https://github.com/caifl/wscore")]
    public class GreetingService : WebService {
        public GreetingService()
        {
        }

        [WebMethod]
        //[SoapDocumentMethod(ParameterStyle = SoapParameterStyle.Bare)]
        public string Say(string name)
        {
            return $"Hello {name}.";
        }
    }
}
