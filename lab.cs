using System;
using System.Text;

namespace DNWS
{
    class lab: IPlugin
    {
        public lab(){}

        public void PreProcessing(HTTPRequest request)
        {
            throw new NotImplementedException();
        }
        public HTTPResponse GetResponse(HTTPRequest request)
    {
      String[] clientTemp = request.getPropertyByKey("RemoteEndPoint").Split(":");
      String clientIP = clientTemp[0];
      String clientPort = clientTemp[1];
      String broswerInfo = request.getPropertyByKey("User-Agent");
      String acceptLang = request.getPropertyByKey("Accept-Language");
      String acceptEncode = request.getPropertyByKey("Accept-Encoding");

      HTTPResponse response = null;
      StringBuilder sb = new StringBuilder();
      sb.Append("<html><body><pre>");
      sb.Append("Client IP: " + clientIP + "\n\n");
      sb.Append("Client Port: " + clientPort + "\n\n");
      sb.Append("Browser Information: " + broswerInfo + "\n\n");
      sb.Append("Accept Language: " + acceptLang + "\n\n");
      sb.Append("Accept Encoding: " + acceptEncode + "\n\n");
      sb.Append("</pre></body></html>");
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