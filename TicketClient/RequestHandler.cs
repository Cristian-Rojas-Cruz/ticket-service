using System;
using System.Configuration;
using System.Net.Sockets;
using System.Xml;

namespace TicketClient
{
    public class RequestHandler
    {
        public static string HandleRequest(int TicketClientToReserve, string clientName)
        {
            TcpClient client = new TcpClient(ConfigurationManager.AppSettings["host"], Int32.Parse(ConfigurationManager.AppSettings["port"]));
            NetworkStream stream = client.GetStream();
            Byte[] data = System.Text.Encoding.ASCII.GetBytes(GenerateXMLRequest(TicketClientToReserve, clientName));
            stream.Write(data, 0, data.Length);

            data = new Byte[256];
            Int32 bytes = stream.Read(data, 0, data.Length);
            stream.Close();
            client.Close();

            XmlDocument xml = new XmlDocument();
            xml.LoadXml(System.Text.Encoding.ASCII.GetString(data, 0, bytes));

            return HandleResponse(xml);
        }

        public static string GenerateXMLRequest(int TicketClientToReserve, string clientName)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.AppendChild(docNode);
            XmlNode requestNode = doc.CreateElement("PeticionCita");
            doc.AppendChild(requestNode);
            XmlNode reqTicket = doc.CreateElement("ticket");
            XmlNode reqNameClient = doc.CreateElement("name");
            reqTicket.InnerText = $"{TicketClientToReserve}";
            reqNameClient.InnerText = $"{clientName}";
            requestNode.AppendChild(reqTicket);
            requestNode.AppendChild(reqNameClient);
            return doc.InnerXml;
        }

        public static string HandleResponse(XmlDocument xml)
        {
            if (xml.SelectSingleNode("//message") != null)
            {
                return xml.SelectSingleNode("//message").InnerText;
            } else
            {
                return $"Ticket No. {xml.SelectSingleNode("//ticket").InnerText}, ha sido reservado para {xml.SelectSingleNode("//name").InnerText}";
            }
        }
    }
}