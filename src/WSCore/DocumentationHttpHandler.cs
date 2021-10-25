using System.Collections.Generic;
using System.Text;

namespace System.Web.Services
{
    class DocumentationHttpHandler : IHttpHandler
    {
        private readonly Type serviceType;
        private const string Html = @"<!DOCTYPE html>
<html lang=""en"" xmlns=""http://www.w3.org/1999/xhtml"">
<head>
    <meta charset=""utf-8""/>
    <title>{0}</title>
    <script>
        window.wsdlUri = '{1}'
    </script>
</head>
<body>
    <div id=""app"">
        <h1>{{{{serviceName}}}}</h1>
    </div>
    <script src=""https://cdn.jsdelivr.net/npm/axios/dist/axios.min.js""></script>
    <script src=""/ws/vue/vue.min.js""></script>
    <script src=""/ws/element-ui.js""></script>
    <script src=""/ws/app.js""></script>
</body>
</html>";

        public DocumentationHttpHandler(Type serviceType)
        {
            this.serviceType = serviceType;
        }

        public bool IsReusable => false;

        public void ProcessRequest(HttpContext context)
        {
            //var items = context.Request.GetRequiredService(typeof(IEnumerable<WSCore.WebServiceMiddleware2>));

            var location = "/ws/index.html?" + this.serviceType.Name;
            var response = context.Response;
            response.Redirect(location);
            //var output = "hello xml web services => " + this.serviceType.FullName;
            //output = string.Format(Html, output, "/GreetingService.asmx?wsdl");
            //var data = Encoding.UTF8.GetBytes(output);
            //var data = System.IO.File.ReadAllBytes("./wwwroot/ws/template.html");

            //var response = context.Response;
            //response.OutputStream.Write(data, 0, data.Length);
        }
    }
}
