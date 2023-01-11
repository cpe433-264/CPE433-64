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

            String clientinfo = request.getPropertyByKey("RemoteEndPoint"); // get user IP and Port
            String ip = clientinfo.Split  (':')[0]; // to split IP and Port  (IP:Port)
            String port = clientinfo.Split (':')[1];

            String userAgent = request.getPropertyByKey("User-Agent"); // to get user brower
            String userLanguage = request.getPropertyByKey("Accept-Language"); 
            String userEncoding = request.getPropertyByKey("Accept-Encoding");
            
            //response.body = Encoding.UTF8.GetBytes("Client IP : "+ip+" Client port: "+Port+" Browser Information : "+userAgent+" Accept Language : "+userLan+" Accept Encoding : "+userEncod);
        
            // response.body = Encoding.UTF8.GetBytes("
            // <html><body>"+
            // "<div>Client IP : "+ip+
            // "</div><div> Client Port: "+port+
            // "</div><div>Browser Information : "+userAgent+
            // "</div><div>Accept Language : "+userLan+
            // "</div><div>Accept Encoding : "+userEncod+
            // "</div>"+
            // "</body></html>");
            // response.body = Encoding.UTF8.GetBytes("<html><body>"+"<div>Client IP : "+ip+"<div></div>"+"</div><div> Client Port: "+port+"</div><div>Browser Information : "+userAgent+"</div><div>Accept Language : "+userLan+"</div><div>Accept Encoding : "+userEncod+"</div>"+"</body></html>");


            // response.body = Encoding.UTF8.GetBytes(                
            //   "Client IP : "+ip+"</br>"+
            //   "Client Port: "+port+"</br>"+
            //   "Browser Information : "+userAgent+"</br>"+
            //   "Accept Language : "+userLanguage+"</br>"+
            //   "Accept Encoding : "+userEncoding);
            response.body = Encoding.UTF8.GetBytes("Client IP : "+ip+"</br>"+"Client Port: "+port+"</br>"+"Browser Information : "+userAgent+"</br>"+"Accept Language : "+userLanguage+"</br>"+"Accept Encoding : "+userEncoding);

            return response;
        }
        public HTTPResponse PostProcessing(HTTPResponse response)
        {
            throw new NotImplementedException();
        }
    }
}