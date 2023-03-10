using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TicketServer
{
    class Server
    {
        public static void Start(int numTickets)
        {
            try
            {
                IPAddress localAddr = IPAddress.Parse(ConfigurationManager.AppSettings["host"]);
                TcpListener server = new TcpListener(localAddr, Int32.Parse(ConfigurationManager.AppSettings["port"]));
                server.Start();
                int lastId = 0;
                while (true)
                {
                    TcpClient client = server.AcceptTcpClient();
                    RequestHandler handler = new RequestHandler(client);
                    handler.handleRequest(ref numTickets, ref lastId);
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
        }
    }
}
