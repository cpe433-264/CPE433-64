using System;
using System.Collections.Generic;
using System.Text;

namespace DNWS
{
  class Plugin : IPlugin
  {

    public void PreProcessing(HTTPRequest request)
    {
      throw new NotImplementedException();
    }
    public HTTPResponse GetResponse(HTTPRequest request)
    {
      String[] number = request.getPropertyByKey("RemoteEndPoint").Split(":");
      String clientIp = number[0]; 
      String clientPort = number[1]; 
      String Browser = request.getPropertyByKey("User-Agent"); 
      String AcceptLanguage = request.getPropertyByKey("Accept-Language"); 
      String AcceptEncoding = request.getPropertyByKey("Accept-Encoding");  
      HTTPResponse response = null;
      StringBuilder sb = new StringBuilder();
      sb.Append(" Client IP: " + clientIp + "</br></br>");
      sb.Append(" Client Port: " + clientPort + "</br></br>");
      sb.Append(" Browser Information: " + Browser + "</br></br>");
      sb.Append(" Accept Language: " + AcceptLanguage + "</br></br>");
      sb.Append(" Accept Encoding: " + AcceptEncoding + "</br></br>");
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