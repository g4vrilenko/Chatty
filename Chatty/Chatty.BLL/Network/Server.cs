using Chatty.BLL.CommunicationEntities;
using Chatty.BLL.Contracts;
using Chatty.BLL.Helpers;
using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Chatty.BLL.Network
{
    public class Server
    {
        private IPAddress _ipAddress;
        private int _port;
        private IServerManager _serverManager;

        public event Action<string> ServerEvent;

        public event Action<string> ServerError;

        public Server(string ipAddress, int port, IServerManager serverManager)
        {
            IPAddress ip;
            if (IPAddress.TryParse(ipAddress, out ip))
                _ipAddress = ip;

            else
                throw new ArgumentException("IP address not valid");

            if (PortIsAvailable(port))
                _port = port;
            else
                throw new ArgumentException("Port not valid or not avalible");
            if (serverManager != null)
                _serverManager = serverManager;
            else
                throw new NullReferenceException("Parameter serverManager cannot be null!");
        }

        public async Task RunAsync()
        {
            var listener = new TcpListener(_ipAddress, _port);
            listener.Start();
            try
            {
                ServerEvent?.Invoke("Server started");
                while (true)
                    Accept(await listener.AcceptTcpClientAsync());
            }
            finally
            {
                listener.Stop();
            }
        }

        private void Accept(TcpClient tcpClient)
        {
            ServerEvent?.Invoke($"Client connected. Endpoint: {tcpClient.Client.RemoteEndPoint.ToString()}");
            new Thread(ClientProcess).Start(tcpClient);
        }

        private void ClientProcess(object obj)
        {
            var client = (TcpClient)obj;
            var buffer = new byte[5000];
            try
            {
                using (client)
                using (var stream = client.GetStream())
                {
                    
                    while (true)
                    {
                        var req = GetRequest(stream, buffer);
                        if (req == null)
                        {
                            ServerError?.Invoke($"Requst is null. Endpoint: {client.Client.RemoteEndPoint.ToString()}");
                            continue;//TODO:send error message
                        }
                        var res = req.Handle(_serverManager);
                        SentRespons(stream, res);                        
                    }
                }
            }
            catch (Exception ex)
            {
                ServerError?.Invoke(ex.Message);
            }
        }

        private void SentRespons(NetworkStream stream, CommunicationObject res)
        {
            var resBytes = CommunicationHelper.Serialize(res);
            var resSize = BitConverter.GetBytes(resBytes.Length);
            stream.Write(resSize, 0, resSize.Length);
            stream.Write(resBytes, 0, resBytes.Length);
            stream.Flush();
        }

        private Request GetRequest(NetworkStream stream, byte[] buffer)
        {
            int bytesToReade = 0, bytesRead = 0;
            stream.Read(buffer, 0, sizeof(int));
            bytesToReade = BitConverter.ToInt32(buffer, 0);
            while (bytesRead < bytesToReade)
                bytesRead += stream.Read(buffer, 0, bytesToReade - bytesRead);
            return CommunicationHelper.Deserialize(buffer.SubArray(0, bytesRead)) as Request;
        }

        private bool PortIsAvailable(int port)
        {
            bool isAvailable = true;
            if (port <= IPEndPoint.MinPort && port >= IPEndPoint.MaxPort)
                return false;
            var ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
            foreach (TcpConnectionInformation tcpi in ipGlobalProperties.GetActiveTcpConnections())
            {
                if (tcpi.LocalEndPoint.Port == port)
                {
                    isAvailable = false;
                    break;
                }
            }
            return isAvailable;
        }
    }
}
