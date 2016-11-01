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
using Chatty.BLL.CommunicationEntities.Requests;
using Chatty.BLL.Entities;

namespace Chatty.BLL.Network
{
    public class Client : IClient
    {
        private IPAddress _hostName;
        private int _port;
        private TcpClient _client;
        private Queue<Tuple<Request, Action<Response>>> _requests;
        private object _syncObj = new object();

        public event Action<List<Message>> IncomingMessages;

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
                //ThreadPool.QueueUserWorkItem(GetUpdates);
            }
            catch
            {
                return false;
            }
            return true;
        }

        private void GetUpdates(object state)
        {
            while (true)
            {
                lock (_syncObj)
                {
                    if (_requests.Count != 0)
                    {
                        var req = new Update();
                        Action<Response> callback = res =>
                        {

                        };
                        var job = new Tuple<Request, Action<Response>>(req, callback);
                        _requests.Enqueue(job);
                    }
                }
                Thread.Sleep(500);
            }
        }

        public void AddRequest(Request req, Action<Response> callback)
        {
            lock (_syncObj)
            {
                _requests.Enqueue(new Tuple<Request, Action<Response>>(req, callback));
            }
        }

        private void ProcessRequests(object state)
        {
            Tuple<Request, Action<Response>> req = null;
            var hasReq = false;
            while (true)
            {
                lock (_syncObj)
                {
                    if (_requests.Count != 0)
                    {
                        req = _requests.Dequeue();
                        hasReq = true;
                    }
                }
                if (hasReq)
                {
                    var res = Send(req.Item1);    
                    req.Item2?.Invoke(res);
                    hasReq = false;
                }
                Thread.Sleep(50);
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
