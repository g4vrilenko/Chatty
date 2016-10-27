using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Chatty.DAL.Network
{
    public class Server
    {
        private IPAddress _ipAddress;
        private int _port = 7777;
        private TcpListener _listener;

        public Server(string ipAddress, int port)
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
