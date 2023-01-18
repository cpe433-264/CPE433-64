using System;
using System.Collections.Generic;
using System.Text;

namespace DNWS
{
  class client : IPlugin
  {
 
    public void PreProcessing(HTTPRequest request)
    {
      throw new NotImplementedException();
    }
    public HTTPResponse GetResponse(HTTPRequest request)
    {
        string[] client = request.getPropertyByKey("RemoteEndPoint").Split(":");
        string IP = client[0];
        string Port = client[1];
        string BrowserInfo = request.getPropertyByKey("User-Agent");
        string AcceptLanguage = request.getPropertyByKey("Accept-Language");
        string AcceptEncoding = request.getPropertyByKey("Accept-Encoding");
      HTTPResponse response = null;
      StringBuilder sb = new StringBuilder();
    sb.Append("<html><body><h1>Stat:</h1>");
    sb.Append("Client IP: " + IP + "<br><br>");
    sb.Append("Client Port: " + Port + "<br><br>");
    sb.Append("Browser Information: " + BrowserInfo +"<br><br>");
    sb.Append("Accept Language: " + AcceptLanguage +"<br><br>");
    sb.Append("Accept Encoding: " + AcceptEncoding +"<br><br>");
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