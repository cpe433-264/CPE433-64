using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace DNWS
{
  class Clinet : IPlugin
  {
    public Clinet(){}

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
            String userLan = request.getPropertyByKey("Accept-Language");
            String userEncode = request.getPropertyByKey("Accept-Encoding");
            response.body = Encoding.UTF8.GetBytes("<html><body>"+"<div>Clinet IP: "+ip+"</div><div>Clinet Port: "+port+"</div><div>Browse infomation: "+userAgent+"</div><div>Accept Language: "+userLan+"</div><div>Accept Encoding: "+userEncode+"</div>"+"</body></html>");
            return response;
        }

    public HTTPResponse PostProcessing(HTTPResponse response)
    {
      throw new NotImplementedException();
    }
  }
}