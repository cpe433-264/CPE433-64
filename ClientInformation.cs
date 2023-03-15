using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DNWS
{
    class Client : IPlugin
    {
        protected string ROOT = Program.Configuration["DocumentRoot"];
        public Client()
        {

        }
        public class ClientInformation//The ClientInformation to provided the information for the client socket connecting to the web browser
        {
            public string ipAddress, port, browserInformation, acceptLanguage, acceptEncoding,body;//Fields
            public ClientInformation()//Initialize all the string fields 
            {
                this.ipAddress = "";
                this.port = "";
                this.browserInformation = "";
                this.acceptLanguage = "";
                this.acceptEncoding = "";
                this.body = "";
            }
        }
        protected string clientInformationProcessing(HTTPResponse response, string path, ClientInformation clientInformation)// process the client information into html
        {
            string html = System.IO.File.ReadAllText(path);//the whole text from html
            html = html.Replace("{clientIP}", clientInformation.ipAddress);// get clientIpaddress from the ClientInformation object
            html = html.Replace("{clientPort}",clientInformation.port);// get clientPort from the ClientInformation object
            html = html.Replace("{browserInformation}",clientInformation.browserInformation);// get browserInformation from the ClientInformation object
            html = html.Replace("{acceptLanguage}",clientInformation.acceptLanguage);// get acceptLanguage from the ClientInformation object
            html = html.Replace("{acceptEncoding}",clientInformation.acceptEncoding);// get acceptEncoding from the ClientInformation object
            return html;//return a new html
        }
        protected HTTPResponse getFile(String path, ClientInformation clientInformation)//get the client.html file index
        {
            HTTPResponse response = null;
            //get html file
            string fileType = "text/html";
            // Try to read the file, if not found then 404, otherwise, 500.
            try
            {
                response = new HTTPResponse(200);
                response.type = fileType;
                response.body = Encoding.UTF8.GetBytes(clientInformationProcessing(response, path, clientInformation));
            }
            catch (FileNotFoundException ex)
            {
                response = new HTTPResponse(404);
                response.body = Encoding.UTF8.GetBytes("<h1>404 Not found</h1>" + ex.Message);
            }
            catch (Exception ex)
            {
                response = new HTTPResponse(500);
                response.body = Encoding.UTF8.GetBytes("<h1>500 Internal Server Error</h1>" + ex.Message);
            }
            return response;
        }
        public void PreProcessing(HTTPRequest request)
        {
            throw new NotImplementedException();
        }
        public HTTPResponse GetResponse(HTTPRequest request)
        {
            HTTPResponse response = null;
            String cIPAddress = request.getPropertyByKey("RemoteEndPoint");
            //Create clientInformation
            ClientInformation cInformation = new ClientInformation();
            try//prevent the error for the server
            {
                cInformation.ipAddress = cIPAddress.Split(':')[0];//using split ':' string to get the ip address at index 0 of an array eg. 127.0.0.1:60015 => ["127.0.0.1","60015"]
                cInformation.port = cIPAddress.Split(':')[1];//using split ':' string to get the port at index 1 of an array eg. 127.0.0.1:60015 => ["127.0.0.1","60015"]
                cInformation.browserInformation = request.getPropertyByKey("User-Agent");// using get propertyByKey is easier than get the substring
                cInformation.acceptLanguage = request.getPropertyByKey("Accept-Language");// using get propertyByKey is easier than get the substring
                cInformation.acceptEncoding = request.getPropertyByKey("Accept-Encoding");// using get propertyByKey is easier than get the substring
            }
            catch (Exception)
            {
                //nothing to do here
            }
            response = getFile(ROOT + "/client.html", cInformation);//pass clientInformation
            return response;
        }

        public HTTPResponse PostProcessing(HTTPResponse response)
        {
            throw new NotImplementedException();
        }
    }
}