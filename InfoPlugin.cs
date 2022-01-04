using System;
using System.Collections.Generic;
using System.Text;

namespace DNWS
{
  class InfoPlugin : IPlugin
  {
    public void PreProcessing(HTTPRequest request)
    {
      throw new NotImplementedException();
    }
    public HTTPResponse GetResponse(HTTPRequest request)
    {
      String[] getPortIP = request.getPropertyByKey("RemoteEndPoint").Split(":");
      String clientIP = getPortIP[0];
      String clientPort = getPortIP[1];
      String BrowsInfo = request.getPropertyByKey("User-Agent");
      String accptLang = request.getPropertyByKey("Accept-Language");
      String accptEncode = request.getPropertyByKey("Accept-Encoding");
      HTTPResponse response = null;
      StringBuilder sb = new StringBuilder();
      sb.Append("Client IP: " + clientIP + "<br><br>");
      sb.Append("Client Port: " + clientPort + "<br><br>");
      sb.Append("Browser Information: " + BrowsInfo + "<br><br>");
      sb.Append("Accept Language: " + accptLang + "<br><br>");
      sb.Append("Accept Encoding: " + accptEncode + "<br><br>");  
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