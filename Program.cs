using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Threading;
using Microsoft.Extensions.Configuration;

namespace DNWS
{
    // Main class
    public class Program
    {
        static public IConfigurationRoot Configuration { get; set; }

        // Log to console
        public void Log(String msg)
        {
            Console.WriteLine(msg);
        }

        // Start the server, Singleton here
        public void Start()
        {
            // Start server
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("config.json");
            Configuration = builder.Build();
            DotNetWebServer ws = DotNetWebServer.GetInstance(this);
            ws.Start();
        }

        static void Main(string[] args)
        {
            Program p = new Program();
            p.Start();
        }
    }

    /// <summary>
    /// HTTP processor will process each http request
    /// </summary>

    public class HTTPProcessor
    {
        protected class PluginInfo
        {
            protected string _path;
            protected string _type;
            protected bool _preprocessing;
            protected bool _postprocessing;
            protected IPlugin _reference;

            public string path
            {
                get { return _path; }
                set { _path = value; }
            }
            public string type
            {
                get { return _type; }
                set { _type = value; }
            }
            public bool preprocessing
            {
                get { return _preprocessing; }
                set { _preprocessing = value; }
            }
            public bool postprocessing
            {
                get { return _postprocessing; }
                set { _postprocessing = value; }
            }
            public IPlugin reference
            {
                get { return _reference; }
                set { _reference = value; }
            }
        }
        // Get config from config manager, e.g., document root and port
        protected string ROOT = Program.Configuration["DocumentRoot"];
        protected Socket _client;
        protected Program _parent;
        protected Dictionary<string, PluginInfo> plugins;

        /// <summary>
        /// Constructor, set the client socket and parent ref, also init stat hash
        /// </summary>
        /// <param name="client">Client socket</param>
        /// <param name="parent">Parent ref</param>
        public HTTPProcessor(Socket client, Program parent)
        {
            _client = client;
            _parent = parent;
            plugins = new Dictionary<string, PluginInfo>();
            // load plugins
            var sections = Program.Configuration.GetSection("Plugins").GetChildren();
            foreach (ConfigurationSection section in sections)
            {
                PluginInfo pi = new PluginInfo();
                pi.path = section["Path"];
                pi.type = section["Class"];
                pi.preprocessing = section["Preprocessing"].ToLower().Equals("true");
                pi.postprocessing = section["Postprocessing"].ToLower().Equals("true");
                pi.reference = (IPlugin)Activator.CreateInstance(Type.GetType(pi.type));
                plugins[section["Path"]] = pi;
            }
        }

        /// <summary>
        /// Get a file from local harddisk based on path
        /// </summary>
        /// <param name="path">Absolute path to the file</param>
        /// <returns></returns>
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
        protected HTTPResponse getFile(String path, ClientInformation clientInformation)
        {
            HTTPResponse response = null;

            // Guess the content type from file extension
            string fileType = "text/html";
            if (path.ToLower().EndsWith("jpg") || path.ToLower().EndsWith("jpeg"))
            {
                fileType = "image/jpeg";
            }
            if (path.ToLower().EndsWith("png"))
            {
                fileType = "image/png";
            }

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

        /// <summary>
        /// Get a request from client, process it, then return response to client
        /// </summary>
        public void Process()
        {
            NetworkStream ns = new NetworkStream(_client);
            string requestStr = "";
            HTTPRequest request = null;
            HTTPResponse response = null;
            byte[] bytes = new byte[1024];
            int bytesRead;

            // Read all request
            do
            {
                bytesRead = ns.Read(bytes, 0, bytes.Length);
                requestStr += Encoding.UTF8.GetString(bytes, 0, bytesRead);
            } while (ns.DataAvailable);

            request = new HTTPRequest(requestStr);
            request.addProperty("RemoteEndPoint", _client.RemoteEndPoint.ToString());

            //Create clientInformation
            ClientInformation cInformation = new ClientInformation();
            try//prevent the error for the server
            {
                cInformation.body = requestStr;// the whole request GET body
                cInformation.ipAddress = _client.RemoteEndPoint.ToString().Split(':')[0];//using split ':' string to get the ip address at index 0 of an array eg. 127.0.0.1:60015 => ["127.0.0.1","60015"]
                cInformation.port = _client.RemoteEndPoint.ToString().Split(':')[1];//using split ':' string to get the port at index 1 of an array eg. 127.0.0.1:60015 => ["127.0.0.1","60015"]
                cInformation.browserInformation = requestStr.Substring(requestStr.IndexOf("User-Agent:"),requestStr.IndexOf("Accept:")-requestStr.IndexOf("User-Agent:")-1).Split(':')[1];// get the substring from the text 'User-Agent:' to the text 'Accept:' which is a User-Agent data 
                cInformation.acceptLanguage = requestStr.Substring(requestStr.IndexOf("Accept-Language:"),requestStr.IndexOf("Cookie:")-requestStr.IndexOf("Accept-Language:")-1).Split(':')[1];// get the substring from the text 'Accept-Language:' to the text 'Cookie:' which is a Accept-Language data 
                cInformation.acceptEncoding = requestStr.Substring(requestStr.IndexOf("Accept-Encoding:"),requestStr.IndexOf("Accept-Language:")-requestStr.IndexOf("Accept-Encoding:")-1).Split(':')[1];// get the substring from the text 'Accept-Encoding:' to the text 'Accept-Language:' which is a Accept-Encoding data
            }
            catch (Exception)
            {
                //nothing to do here
            }

            // We can handle only GET now
            if (request.Status != 200)
            {
                response = new HTTPResponse(request.Status);
            }
            else
            {
                bool processed = false;
                // pre processing
                foreach (KeyValuePair<string, PluginInfo> plugininfo in plugins)
                {
                    if (plugininfo.Value.preprocessing)
                    {
                        plugininfo.Value.reference.PreProcessing(request);
                    }
                }
                // plugins
                foreach (KeyValuePair<string, PluginInfo> plugininfo in plugins)
                {
                    if (request.Filename.StartsWith(plugininfo.Key))
                    {
                        response = plugininfo.Value.reference.GetResponse(request);
                        processed = true;
                    }
                }
                // local file
                if (!processed)
                {
                    if (request.Filename.Equals(""))
                    {
                        response = getFile(ROOT + "/index.html", cInformation);//pass clientInformation
                    }
                    else
                    {
                        response = getFile(ROOT + "/" + request.Filename, cInformation);//pass clientInformation
                    }
                }
                // post processing pipe
                foreach (KeyValuePair<string, PluginInfo> plugininfo in plugins)
                {
                    if (plugininfo.Value.postprocessing)
                    {
                        response = plugininfo.Value.reference.PostProcessing(response);
                    }
                }
            }
            // Generate response
            ns.Write(Encoding.UTF8.GetBytes(response.header), 0, response.header.Length);
            if (response.body != null)
            {
                ns.Write(response.body, 0, response.body.Length);
            }

            // Shuting down
            //ns.Close();
            _client.Shutdown(SocketShutdown.Both);
            //_client.Close();

        }
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

    public class TaskInfo
    {
        private HTTPProcessor _hp;
        public HTTPProcessor hp
        {
            get { return _hp; }
            set { _hp = value; }
        }
        public TaskInfo(HTTPProcessor hp)
        {
            this.hp = hp;
        }
    }

    /// <summary>
    /// Main server class, open the socket and wait for client
    /// </summary>
    public class DotNetWebServer
    {
        protected int _port;
        protected Program _parent;
        protected Socket serverSocket;
        protected Socket clientSocket;
        private static DotNetWebServer _instance = null;
        protected int id;

        private DotNetWebServer(Program parent)
        {
            _parent = parent;
            id = 0;
        }

        /// <summary>
        /// Singleton here
        /// </summary>
        /// <param name="parent">parent ref</param>
        /// <returns></returns>
        public static DotNetWebServer GetInstance(Program parent)
        {
            if (_instance == null)
            {
                _instance = new DotNetWebServer(parent);
            }
            return _instance;
        }

        public void ThreadProc(Object stateinfo)
        {
            TaskInfo ti = stateinfo as TaskInfo;
            ti.hp.Process();
        }

        /// <summary>
        /// Server starting point
        /// </summary>
        public void Start()
        {
            _port = Convert.ToInt32(Program.Configuration["Port"]);
            IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, _port);
            // Create listening socket, queue size is 5 now.
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(localEndPoint);
            serverSocket.Listen(5);
            _parent.Log("Server started at port " + _port + ".");
            while (true)
            {
                try
                {
                    // Wait for client
                    clientSocket = serverSocket.Accept();
                    // Get one, show some info
                    _parent.Log("Client accepted:" + clientSocket.RemoteEndPoint.ToString());
                    HTTPProcessor hp = new HTTPProcessor(clientSocket, _parent);
                    hp.Process();
                }
                catch (Exception ex)
                {
                    _parent.Log("Server starting error: " + ex.Message + "\n" + ex.StackTrace);
                }
            }
        }
    }
}
