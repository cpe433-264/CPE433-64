// Wait for client
                    clientSocket = serverSocket.Accept();
                    // Get one, show some info
                    //_parent.Log("Client accepted:" + clientSocket.RemoteEndPoint.ToString());
                    HTTPProcessor hp = new HTTPProcessor(clientSocket, _parent);
                    //hp.Process(); //move to use when thread start

                    // Implement Threads
                    Thread t = new Thread(new ThreadStart(hp.Process)); //use hp.Process when thread start
                    t.Start();

                }
                catch (Exception ex)
                {
