using System;
using System.Collections.Generic;
using System.Text;

namespace DNWS
{
  class ClientInfoPlugin : IPlugin
  {
    public void PreProcessing(HTTPRequest request)
    {
      throw new NotImplementedException();
    }
    public HTTPResponse GetResponse(HTTPRequest request)
    {
        String[] PortIP = request.getPropertyByKey("RemoteEndPoint").Split(":");
        String clientIP = PortIP[0];
        String clientPort = PortIP[1];
        String Info = request.getPropertyByKey("User-Agent");
        String Lang = request.getPropertyByKey("Accept-Language");
        String Encod = request.getPropertyByKey("Accept-Encoding");
      HTTPResponse response = null;
      StringBuilder sb = new StringBuilder();
      sb.Append("<html><body>");
      sb.Append("Client IP: " + clientIP + "<br><br>");
      sb.Append("Client Port: " + clientPort + "<br><br>");
      sb.Append("Browser Information: " + Info + "<br><br>");
      sb.Append("Accept Language: " + Lang + "<br><br>");
      sb.Append("Accept Encoding: " + Encod + "<br><br>");
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