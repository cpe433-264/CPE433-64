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

        String[] clientArray = request.getPropertyByKey("RemoteEndPoint").Split(":");      // 0: IP, 1: Port
        String clientIP    = clientArray[0];
        String clientPort  = clientArray[1];
        String browserInfo = request.getPropertyByKey("User-Agent");
        String acceptLang  = request.getPropertyByKey("Accept-Language");
        String acceptEncode = request.getPropertyByKey("Accept-Encoding");
 
      HTTPResponse response = null;
      StringBuilder sb = new StringBuilder();

      sb.Append("<br>");
      sb.Append("Client IP: "  + clientIP + "<br><br>");
      sb.Append("Client Port: " + clientPort + "<br><br>");
      sb.Append("Browser Information: " + browserInfo + "<br><br>");
      sb.Append("Accept Language:" + acceptLang + "<br><br>");
      sb.Append("Accept Encoding: " + acceptEncode + "<br><br>");
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