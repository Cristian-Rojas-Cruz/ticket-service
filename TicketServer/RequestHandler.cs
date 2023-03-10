using System;
using System.Linq;
using System.Net.Sockets;
using System.Collections;

namespace TicketServer
{
    public class RequestHandler
    {
        private readonly TcpClient client;
        private readonly static ArrayList reservedAppointments = new ArrayList();
        public RequestHandler(TcpClient client)
        {
            this.client = client;
        }
        public void handleRequest(ref int numTicket, ref int lastId)
        {
            String data;
            Byte[] bytes = new Byte[256];
            NetworkStream stream = this.client.GetStream();
            Int32 i = stream.Read(bytes, 0, bytes.Length);
            data = System.Text.Encoding.ASCII.GetString(bytes, 0, i).Trim();
            string requestedTickets = XmlConverter.ProcesarXmlConvertRequest(data);
            
            int ticket = Int32.Parse(requestedTickets);

            if (!(reservedAppointments.Contains(ticket)))
            {
                reservedAppointments.Add(ticket);
                lastId = lastId + 1;
                data = XmlConverter.GenerarXmlResponse(lastId, ticket);
            } else
            {
                data = XmlConverter.GenerarXmlResponseError(ticket);
            }
            
            Byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);
            stream.Write(msg, 0, msg.Length);
            this.client.Close();
            
        }
    }
}