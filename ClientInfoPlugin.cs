using System;
using System.Collections.Generic;
using System.Text;



namespace DNWS
{
  
  class ClientInfo : IPlugin
  {
    protected static Dictionary<String, int> statDictionary = null;
    public ClientInfo()
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

      // All Question request
      String save = request.getPropertyByKey("remoteendpoint"); // use for getting Client IP and Client port.
      string[] IP = save.Split(':'); 
      String info = request.getPropertyByKey("user-agent");
      String AcLanguage = request.getPropertyByKey("accept-language");
      String AcEncoding = request.getPropertyByKey("accept-encoding");
      // All Question request

      sb.Append("<html><body><pre><h3>");
      sb.Append("Client IP: "  + IP[0] +"<br />");
      sb.Append("Client Port: " + IP[1] +"<br />");
      sb.Append("Browser Information: "+ info +"<br />");
      sb.Append("Accept Language: " + AcLanguage +"<br />");
      sb.Append("Accept Encoding: " + AcEncoding +"<br />");
      sb.Append("</pre></h3></body></html>");
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