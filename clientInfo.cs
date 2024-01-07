using System;
using System.Collections.Generic;
using System.Text;

namespace DNWS
{
  class clientInfo : IPlugin
  {
    protected static Dictionary<String, int> statDictionary = null;
    public clientInfo()
    {
      if (statDictionary == null)
      {
        statDictionary = new Dictionary<String, int>();

      }
    }

    public void PreProcessing(HTTPRequest request)
    {
      if (statDictionary.ContainsKey(request.Url))
      {
        statDictionary[request.Url] = (int)statDictionary[request.Url] + 1;
      }
      else
      {
        statDictionary[request.Url] = 1;
      }
    }
    public HTTPResponse GetResponse(HTTPRequest request)
    {
      HTTPResponse response = null;
      StringBuilder sb = new StringBuilder();
      
      String[] address = request.getPropertyByKey("RemoteEndPoint").Split( ":");
      
      sb.Append("<html><body>");

      sb.Append("Client IP: " + address[0] + "<br><br>");

      sb.Append("Client Port: " + address[1] + "<br><br>");
      
      sb.Append("Browser Information: " + request.getPropertyByKey("User-Agent") + "<br><br>");
      
      sb.Append("Accept Language: " + request.getPropertyByKey("Accept-Language") + "<br><br>");
      
      sb.Append("Accept Encoding: " + request.getPropertyByKey("Accept-Encoding") + "<br><br>");
      
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
