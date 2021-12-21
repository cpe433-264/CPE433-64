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
      HTTPResponse response = null;
      StringBuilder sb = new StringBuilder();

      //input data
      String[] GetInfo = request.getPropertyByKey("RemoteEndPoint").Split(":"); 
            
      //output data
      sb.Append("<html><body><h1>Client-INFO:</h1>");
      sb.Append("Client IP: " + GetInfo[0] + "<br><br>");
      sb.Append("Client Port: " + GetInfo[1] + "<br><br>");
      sb.Append("Browser Information: " + request.getPropertyByKey("user-agent") + "<br><br>");
      sb.Append("Accept Language: " + request.getPropertyByKey("accept-language") + "<br><br>");
      sb.Append("Accept Encoding: " + request.getPropertyByKey("accept-encoding"));
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