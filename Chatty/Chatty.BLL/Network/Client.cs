using Chatty.BLL.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Chatty.BLL.CommunicationEntities;
using System.Threading;
using Chatty.BLL.Helpers;

namespace Chatty.BLL.Network
{
    public class Client : IClient
    {
        private IPAddress _hostName;
        private int _port;
        private TcpClient _client;
        private Queue<Tuple<Request, Action<Response>>> _requests;

        public Client(string hostName, int port)
        {
            IPAddress ip;
            if (IPAddress.TryParse(hostName, out ip))
                _hostName = ip;

            else
                throw new ArgumentException("IP address not valid");
            _port = port;

            _requests = new Queue<Tuple<Request, Action<Response>>>();
        }

        public bool Connect()
        {
            if (_client?.Connected ?? false)
                return true;
            try
            {
                _client = new TcpClient();
                _client.Connect(_hostName, _port);
                ThreadPool.QueueUserWorkItem(ProcessRequests);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public void AddRequest(Request req, Action<Response> callback)
        {
            _requests.Enqueue(new Tuple<Request, Action<Response>>(req, callback));
        }

        private void ProcessRequests(object state)
        {
            while (true)
            {
                if (_requests.Count != 0)
                {
                    var req = _requests.Dequeue();
                    var res = Send(req.Item1);
                    req.Item2?.Invoke(res);
                }
            }
        }

        private Response Send(Request req)
        {
            var buffer = CommunicationHelper.Serialize(req);
            var stream = _client.GetStream();
            stream.Write(BitConverter.GetBytes(buffer.Length), 0, sizeof(int));
            stream.Write(buffer, 0, buffer.Length);
            stream.Flush();
            return GetResponse(stream);
        }

        private Response GetResponse(NetworkStream stream)
        {
            var buffer = new byte[sizeof(int)];
            stream.Read(buffer, 0, sizeof(int));
            int bytesToReade = BitConverter.ToInt32(buffer, 0);
            buffer = new byte[bytesToReade];
            int bytesRead = 0;
            while (bytesRead < bytesToReade)
                bytesRead += stream.Read(buffer, 0, bytesToReade - bytesRead);
            return CommunicationHelper.Deserialize(buffer) as Response;
        }
    }
}
