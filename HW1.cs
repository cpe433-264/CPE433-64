using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace DNWS
{
  class HW1 : IPlugin
  {
    public HW1(){}       

    public void PreProcessing(HTTPRequest request)
    {
      throw new NotImplementedException();
    }
    public HTTPResponse GetResponse(HTTPRequest request)
    {
      HTTPResponse response = null;
      response = new HTTPResponse(200);
      String clientInfo = request.getPropertyByKey("RemoteEndPoint");
      String ip = clientInfo.Split(':')[0];
      String port = clientInfo.Split(':')[1];
      String userAgent = request.getPropertyByKey("User-Agent");
      String userLanguage = request.getPropertyByKey("Accept-Language");
      String userEncoding = request.getPropertyByKey("Accept-Encoding");
      response.body = Encoding.UTF8.GetBytes("Client Ip: " + ip + "ClientPort: " + port + "Browser Information: " + userAgent + "Lang: " + userLanguage + "Encoding: "+ userEncoding);
      return response;
    }

    public HTTPResponse PostProcessing(HTTPResponse response)
    {
      throw new NotImplementedException();
    }
  }
}