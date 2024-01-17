using SECSControl.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SECSControl.XML
{
    internal class XMLWriter
    {
        private Config mConfig;

        internal XMLWriter(Config config)
        {
            mConfig = config;
        }

        internal void CreateTopNode(ref XmlDocument sourceXml)
        {
            if(sourceXml == null)
                sourceXml = new XmlDocument();
            XmlNode node = sourceXml.CreateElement("SECOM_MSG");
            sourceXml.AppendChild(node);
        }

        internal void CreateCommonInfo(ref XmlDocument sourceXml)
        {
            XmlNode root = sourceXml.GetElementsByTagName("SECOM_MSG")[0];

            XmlNode commonInfoNode = sourceXml.CreateElement("CommonInfo");
            XmlNode childNode = sourceXml.CreateElement("EQPID");
            childNode.InnerText = "AAA";
            commonInfoNode.AppendChild(childNode);

            childNode = sourceXml.CreateElement("TIME");
            childNode.InnerText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff");
            commonInfoNode.AppendChild(childNode);

            childNode = sourceXml.CreateElement("Direction");
            childNode.InnerText = "FromHost";
            commonInfoNode.AppendChild(childNode);

            childNode = sourceXml.CreateElement("Identity");
            childNode.InnerText = "NORMAL_MSG";
            commonInfoNode.AppendChild(childNode);

            childNode = sourceXml.CreateElement("ErrorCode");
            childNode.InnerText = "0";
            commonInfoNode.AppendChild(childNode);

            childNode = sourceXml.CreateElement("ErrorMessage");
            childNode.InnerText = "0";
            commonInfoNode.AppendChild(childNode);

            root.AppendChild(commonInfoNode);
        }

        internal void CreateHeader(ref XmlDocument sourceXml)
        {        
            XmlNode root = sourceXml.GetElementsByTagName("SECOM_MSG")[0];

            XmlNode headerNode = sourceXml.CreateElement("Header");

            XmlNode childNode = sourceXml.CreateElement("MessageName");
            childNode.InnerText = "A";
            headerNode.AppendChild(childNode);

            childNode = sourceXml.CreateElement("Stream");
            childNode.InnerText = "B";
            headerNode.AppendChild(childNode);

            childNode = sourceXml.CreateElement("Function");
            childNode.InnerText = "C";
            headerNode.AppendChild(childNode);

            childNode = sourceXml.CreateElement("Wait");
            childNode.InnerText = "D";
            headerNode.AppendChild(childNode);

            childNode = sourceXml.CreateElement("SystemBytes");
            childNode.InnerText = "E";
            headerNode.AppendChild(childNode);

            childNode = sourceXml.CreateElement("AutoReply");
            childNode.InnerText = "F";
            headerNode.AppendChild(childNode);

            childNode = sourceXml.CreateElement("NoLogging");
            childNode.InnerText = "G";
            headerNode.AppendChild(childNode);

            childNode = sourceXml.CreateElement("DeviceID");
            childNode.InnerText = "H";
            headerNode.AppendChild(childNode);

            childNode = sourceXml.CreateElement("MessageData");
            childNode.InnerText = "I";
            headerNode.AppendChild(childNode);

            root.AppendChild(headerNode);
        }
    }
}
