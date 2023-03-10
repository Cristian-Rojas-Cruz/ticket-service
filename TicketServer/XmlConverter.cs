using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace TicketServer
{
    class XmlConverter
    {
        public static string ProcesarXmlConvertRequest(string xmlData)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlData);
            var ticket = doc.SelectSingleNode("//ticket");
            return ticket.InnerText;
        }
        public static string GenerarXmlResponse(int idTicket, int name)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.AppendChild(docNode);
            XmlNode responseNode = doc.CreateElement("RespuestaCita");
            doc.AppendChild(responseNode);
            XmlNode resTicket = doc.CreateElement("ticket");
            resTicket.InnerText = $"{idTicket}";
            responseNode.AppendChild(resTicket);
            XmlNode resClientName = doc.CreateElement("name");
            resClientName.InnerText = $"{name}";
            responseNode.AppendChild(resClientName);
            return doc.InnerXml;
        }

        public static string GenerarXmlResponseError(int ticket)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.AppendChild(docNode);
            XmlNode responseNode = doc.CreateElement("ErrorExpedicionTicket");
            doc.AppendChild(responseNode);
            XmlNode resTicket = doc.CreateElement("message");
            resTicket.InnerText = $"cita con id {ticket} ya ocupada";
            responseNode.AppendChild(resTicket);
            return doc.InnerXml;
        }
    }
}
