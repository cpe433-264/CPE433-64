using System;
using System.Text;

namespace DNWS
{
    class Homework : IPlugin
    {
        public Homework(){}

        public void PreProcessing(HTTPRequest request)
        {
            throw new NotImplementedException();
        }
        public HTTPResponse GetResponse(HTTPRequest request)
        {
            HTTPResponse response = null;
            response = new HTTPResponse(200);

            String clientinfo = request.getPropertyByKey("RemoteEndPoint"); 
            String ip = clientinfo.Split  (':')[0]; 
            String port = clientinfo.Split (':')[1];

            String userAgent = request.getPropertyByKey("User-Agent"); 
            String userLanguage = request.getPropertyByKey("Accept-Language"); 
            String userEncoding = request.getPropertyByKey("Accept-Encoding");
            
            response.body = Encoding.UTF8.GetBytes("Client IP : "+ip+"</br>"+"Client Port: "+port+"</br>"+"Browser Information : "+userAgent+"</br>"+"Accept Language : "+userLanguage+"</br>"+"Accept Encoding : "+userEncoding);

            return response;
        }
        public HTTPResponse PostProcessing(HTTPResponse response)
        {
            throw new NotImplementedException();
        }
    }
}