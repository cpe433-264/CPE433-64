using System;
using System.Collections.Generic;
using System.Text;

namespace DNWS
{
  class ClientInfo : IPlugin
  {
   
    public void PreProcessing(HTTPRequest request)
    {
        throw new NotImplementedException();
    }
    public HTTPResponse GetResponse(HTTPRequest request)
    {


      String[] InfoArray = request.getPropertyByKey("RemoteEndPoint").Split(":");   // Split info of Client IP and Client Port at ":"
      String ClientInfo = InfoArray[0];     // Index 0: IP of client
      String ClientPort = InfoArray[1];     // Index 1: Port of Client
      String BrowInfo = request.getPropertyByKey("User-Agent");
      String AcceptLang = request.getPropertyByKey("Accept-Language");
      String AcceptEncode = request.getPropertyByKey("Accept-Encoding");


      HTTPResponse response = null;
      StringBuilder sb = new StringBuilder();
      sb.Append("<html><body><br>");

      sb.Append("Client IP: " + ClientInfo + "<br><br>");
      sb.Append("Client Port: " + ClientPort + "<br><br>");
      sb.Append("Browser Information: " + BrowInfo + "<br><br>");
      sb.Append("Accept Language: " + AcceptLang + "<br><br>");
      sb.Append("Accept Encoding: " + AcceptEncode + "<br><br>");

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