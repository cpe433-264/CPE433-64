using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace DNWS
{
    class client : IPlugin
  {
    public client()
    {
    }

    public void PreProcessing(HTTPRequest request)
    {
        throw new NotImplementedException();
    }
    public HTTPResponse GetResponse(HTTPRequest request)
        {
            HTTPResponse response = null;
            response = new HTTPResponse(200);
            String clientInfo = request.getPropertyByKey("RemoteEndPoint");
            String browserInfo = request.getPropertyByKey("User-Agent");
            String languageInfo = request.getPropertyByKey("Accept-Language");
            String encodeInfo = request.getPropertyByKey("Accept-Encoding");
            String ip = clientInfo.Split(':')[0];
            String port = clientInfo.Split(':')[1];
            response.body = Encoding.UTF8.GetBytes("Client IP: " + ip + "<h1>\n</h1>" 
                                                 + "Client Port: " + port + "<h1>\n</h1>" 
                                                 + "Browser Information: " + browserInfo + "<h1>\n</h1>"
                                                 + "Accept Language: " + languageInfo + "<h1>\n</h1>"
                                                 + "Accept Encoding: " + encodeInfo);
            return response;
        }
    public HTTPResponse PostProcessing(HTTPResponse response)
    {
        throw new NotImplementedException();
    }
  }
}

//credit from 640615020