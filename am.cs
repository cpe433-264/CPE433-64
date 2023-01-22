using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace DNWS
{
  class Am : IPlugin
  {
    public Am()
    {
    }

    public void PreProcessing(HTTPRequest request)
    {
      throw new NotImplementedException(); 
    }
    public HTTPResponse GetResponse(HTTPRequest request)
    {
      HTTPResponse response = null;
            response = new HTTPResponse(200); //Credit from TEEWARA 640615018
            String clientInfo = request.getPropertyByKey("RemoteEndPoint"); //Credit from TEEWARA 640615018
            String ip = clientInfo.Split(':')[0]; //Credit from TEEWARA 640615018
            String port = clientInfo.Split(':')[1]; //Credit from TEEWARA 640615018
            String userAgent = request.getPropertyByKey("User-Agent"); //Credit from TEEWARA 640615018
            String userLanguage = request.getPropertyByKey("Accept-Language"); //Credit from TEEWARA 640615018
            String userEncoding = request.getPropertyByKey("Accept-Encoding"); //Credit from TEEWARA 640615018

            response.body = Encoding.UTF8.GetBytes("<div>Client IP: "+ip
                                                    +"</div><div>Client Port: "+port
                                                    + "</div>"+"<div>Browser Information: "+userAgent
                                                    +"</div>"+"<div>"+"Accept Language: "+userLanguage
                                                    +"</div>"+"<div>"+"Accept Encoding: "+userEncoding
                                                    ); 
            return response;
    }

    public HTTPResponse PostProcessing(HTTPResponse response)
    {
      throw new NotImplementedException();
    }
  }
}