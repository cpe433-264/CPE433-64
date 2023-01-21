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
            string clientInfo = request.getPropertyByKey("RemoteEndPoint");
            string ip = clientInfo.Split(':')[0]; //get client ip addr 
            string port = clientInfo.Split(':')[1]; //get client port from the ClientInformation object
            string userAgent = request.getPropertyByKey("User-Agent"); //get client browser information
            string userLanguage = request.getPropertyByKey("Accept-Language"); //get accept language
            string userEncoding = request.getPropertyByKey("Accept-Encoding"); //get accept encode
            string html = "<html><body></body></html>" ; // html for more easy to read 

            response.body = Encoding.UTF8.GetBytes("<html><body"+"<div>Client IP: "+ip
                                                    +"</div><br/><br/><div>Client Port: "+port+ 
                                                    "</div><br/>"+"<div>Browser Information: "
                                                    +userAgent+"</div><br/>"+"<div>"+"Accept Language: "
                                                    +userLanguage+"<br/><br/>"+"</div>"+"<div>"+"Accept Encoding: "
                                                    +userEncoding+"<br/><br/>"+"</body></html>"); 
            return response;
    }

    public HTTPResponse PostProcessing(HTTPResponse response)
    {
      throw new NotImplementedException();
    }
  }
}