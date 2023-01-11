using System;
using System.Collections.Generic;
using System.Text;

namespace DNWS
{
  class clientinfo : IPlugin
  {
    
    public void PreProcessing(HTTPRequest request)
    {
      throw new NotImplementedException();
    }
    public HTTPResponse GetResponse(HTTPRequest request)
    {

      String[] arrayClient = request.getPropertyByKey("RemoteEndPoint").Split(":");  
      String clientIP = arrayClient[0];
      String clientPort = arrayClient[1];
      String browInfo = request.getPropertyByKey("User-Agent");
      String AccLang = request.getPropertyByKey("Accept-Language");
      String AccEncode = request.getPropertyByKey("Accept-Encoding");

      HTTPResponse response = null;
      StringBuilder sb = new StringBuilder();
      
      sb.Append("<html><br>");

      sb.Append("Client IP: " + clientIP + "<br><br>");
      sb.Append("Client Port: " + clientPort + "<br><br>");
      sb.Append("Browser Information: " + browInfo + "<br><br>");
      sb.Append("Accept Language: " + AccLang + "<br><br>");
      sb.Append("Accept Encoding: " + AccEncode + "<br><br>");

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