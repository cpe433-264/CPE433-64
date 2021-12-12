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

      String [] arrayClient = request.getPropertyByKey("RemoteEndPoint").Split(":");
      String ClientIP = arrayClient[0];
      String ClientPort = arrayClient[1];
      String BrowserInfo = request.getPropertyByKey("User-Agent");
      String AcceptLang = request.getPropertyByKey("Accept-Language");
      String AcceptEn = request.getPropertyByKey("Accept-Encoding");

      HTTPResponse response = null;
      StringBuilder sb = new StringBuilder();

      sb.Append("<br>");
      sb.Append("Client IP: " + ClientIP +"<br><br>" );
      sb.Append("Client Port: " + ClientPort+"<br><br>" );
      sb.Append("Browser Infomation: " + BrowserInfo + "<br><br>" );
      sb.Append("Accept Language: " + AcceptLang +"<br><br>" );
      sb.Append("Accept Encoding: " + AcceptEn +"<br><br>" );

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