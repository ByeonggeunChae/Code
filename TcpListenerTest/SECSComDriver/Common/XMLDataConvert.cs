using SECSControl.SECS;
using System;
using System.Xml;

namespace SECSControl.Common
{
    internal class XMLDataConvert
    {
        private Config mConfig;

        internal XMLDataConvert(Config config)
        {
            mConfig = config;
        }

        internal XmlDocument GetSECStoXML(SECSDataItem item)
        {
            XmlDocument convertXml = new XmlDocument();
            CreateTopNode(ref convertXml);
            CreateCommonInfo(ref convertXml, item);
            CreateHeader(ref convertXml, item);
            return convertXml;
        }

        internal void CreateTopNode(ref XmlDocument sourceXml)
        {
            XmlNode node = sourceXml.CreateElement("SECOM_MSG");
            sourceXml.AppendChild(node);
        }

        internal void CreateCommonInfo(ref XmlDocument sourceXml, SECSDataItem item)
        {
            try
            {
                XmlNode root = sourceXml.GetElementsByTagName("SECOM_MSG")[0];

                XmlNode commonInfoNode = sourceXml.CreateElement("CommonInfo");
                XmlNode childNode = sourceXml.CreateElement("EQPID");
                childNode.InnerText = mConfig.EquipmentID;
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
                childNode.InnerText = item.ErrorCode.ToString();
                commonInfoNode.AppendChild(childNode);

                childNode = sourceXml.CreateElement("ErrorMessage");
                childNode.InnerText = item.ErrorMessage;
                commonInfoNode.AppendChild(childNode);

                root.AppendChild(commonInfoNode);
            }
            catch (Exception ex)
            {

            }
        }

        internal void CreateHeader(ref XmlDocument sourceXml, SECSDataItem item)
        {
            try
            {
                XmlNode root = sourceXml.GetElementsByTagName("SECOM_MSG")[0];

                XmlNode headerNode = sourceXml.CreateElement("Header");

                XmlNode childNode = sourceXml.CreateElement("MessageName");
                childNode.InnerText = "A";
                headerNode.AppendChild(childNode);

                childNode = sourceXml.CreateElement("Stream");
                childNode.InnerText = item.Stream.ToString();
                headerNode.AppendChild(childNode);

                childNode = sourceXml.CreateElement("Function");
                childNode.InnerText = item.Function.ToString();
                headerNode.AppendChild(childNode);

                childNode = sourceXml.CreateElement("Wait");
                childNode.InnerText = item.WaitBit.ToString();
                headerNode.AppendChild(childNode);

                childNode = sourceXml.CreateElement("SystemBytes");
                childNode.InnerText = item.SystemBytes.ToString();
                headerNode.AppendChild(childNode);

                childNode = sourceXml.CreateElement("AutoReply");
                childNode.InnerText = "F";
                headerNode.AppendChild(childNode);

                childNode = sourceXml.CreateElement("NoLogging");
                childNode.InnerText = "G";
                headerNode.AppendChild(childNode);

                childNode = sourceXml.CreateElement("DeviceID");
                childNode.InnerText = item.DeviceID.ToString();
                headerNode.AppendChild(childNode);

                childNode = sourceXml.CreateElement("MessageData");
                childNode.InnerText = "I";
                headerNode.AppendChild(childNode);

                root.AppendChild(headerNode);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
