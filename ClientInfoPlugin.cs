using System;
using System.Collections.Generic;
using System.Text;

namespace DNWS
{
    class ClientInfoPlugin : IPlugin
    {
        public ClientInfoPlugin()
        {
        }

        public void PreProcessing(HTTPRequest request)
        {
            throw new NotImplementedException();
        }
        public HTTPResponse GetResponse(HTTPRequest request)
        {
            HTTPResponse response = null;
            StringBuilder sb = new StringBuilder();

            String[] getClientInfo = request.getPropertyByKey("RemoteEndPoint").Split(":"); // Seperate IP Address and Port
            String clientIP = getClientInfo[0];
            String clientPort = getClientInfo[1];

            // Show Client Information
            sb.Append("<html><body>" + "<h1>Client Information</h1>");
            sb.Append("<b>Client IP Address:</b> " + clientIP + "<br><br>");
            sb.Append("<b>Client Port:</b> " + clientPort + "<br><br>");
            sb.Append("<b>Browser Information:</b> " + request.getPropertyByKey("user-agent") + "<br><br>");
            sb.Append("<b>Accept Language:</b> " + request.getPropertyByKey("accept-language") + "<br><br>");
            sb.Append("<b>Accept Encoding:</b> " + request.getPropertyByKey("accept-encoding") + "<br><br>");
            sb.Append("</body></html>");

            response = new HTTPResponse(200);
            response.body = Encoding.UTF8.GetBytes(sb.ToString());
            return response;
        }

        public HTTPResponse PostProcessing(HTTPResponse response)
        {
            throw new NotImplementedException();
        }
    }
}