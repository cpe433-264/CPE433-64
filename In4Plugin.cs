using System;
using System.Collections.Generic;
using System.Text;

namespace DNWS
{
  class In4Plugin : IPlugin
  {
    public void PreProcessing(HTTPRequest request)
    {
        throw new NotImplementedException();
    }
    public HTTPResponse GetResponse(HTTPRequest request)
    {
     String[] getPortandIP = request.getPropertyByKey("RemoteEndPoint").Split(":");
      String IP = getPortandIP[0];
      String Port = getPortandIP[1];
      String In4 = request.getPropertyByKey("User-Agent");
      String Lang = request.getPropertyByKey("Accept-Language");
      String Ncode = request.getPropertyByKey("Accept-Encoding");
      HTTPResponse response = null;
      StringBuilder sb = new StringBuilder();
      sb.Append("<html><body><h1>Stat:</h1>");
      sb.Append("Client IP: " + IP + "<br><br>");
      sb.Append("Client Port: " + Port + "<br><br>");
      sb.Append("Browser Information: " + In4 + "<br><br>");
      sb.Append("Accept Language: " + Lang + "<br><br>");
      sb.Append("Accept Encoding: " + Ncode + "<br><br>");
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