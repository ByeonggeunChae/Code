using SECSControl;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace TcpListenerTest
{
    public partial class Form1 : Form
    {
        SECSManager manager = new SECSManager();

        public Form1()
        {
            InitializeComponent();
        }

        private void LinkEventSECS()
        {
            manager.OnSECSConnected += Manager_OnSECSConnected;
        }

        private void Manager_OnSECSConnected(string EquipmentID, XmlDocument XML)
        {
            MessageBox.Show(EquipmentID);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LinkEventSECS();
            manager.Initialize("aaaa");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //XmlDocument aDispDoc = null;
            //aDispDoc = MakeBasicXml(aDispDoc);

            //Test();

            //ProcessModule mainModule = Process.GetCurrentProcess().MainModule;
            //string str = mainModule.FileName.Replace(mainModule.ModuleName, "SEComEnabler.SEComDriver.dll");
        }

        private void Test()
        {
            SECSManager manager = new SECSManager();
            //XmlDocument doc = manager.GetSECSXmlData();
        } 

        internal XmlDocument CreateTopNode(ref XmlElement aRootElement)
        {
            XmlDocument topNode = new XmlDocument();
            aRootElement = (XmlElement)null;
            aRootElement = topNode.CreateElement("SECOM_MSG");
            topNode.AppendChild((XmlNode)aRootElement);
            return topNode;
        }


        private XmlDocument MakeBasicXml(XmlDocument aDoc)
        {
            XmlElement xmlElement = (XmlElement)null;
            aDoc = CreateTopNode(ref xmlElement);
            aDoc = CreateCommonNode(aDoc, ref xmlElement, "FromHost", "NORMAL_MSG", 0, "0");
            aDoc = CreateHeaderNode(ref aDoc, ref xmlElement);
            //aDoc = this.mXmlMaker.CreateDataBlockNode(ref aDoc, ref xmlElement, aSECSCollection);
            return aDoc;
        }

        internal XmlDocument CreateCommonNode(XmlDocument aXmlDoc, ref XmlElement aXmlRootElement, string aDirection, string aIdentity, int aErrorCode, string aErrorMsg)
        {
            try
            {
                XmlElement element1 = aXmlDoc.CreateElement("CommonInfo");
                XmlElement element2 = aXmlDoc.CreateElement("EQPID");
                element2.AppendChild((XmlNode)aXmlDoc.CreateTextNode("AAA"));
                element1.AppendChild((XmlNode)element2);
                XmlElement element3 = aXmlDoc.CreateElement("Time");
                element3.AppendChild((XmlNode)aXmlDoc.CreateTextNode(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff")));
                element1.AppendChild((XmlNode)element3);
                XmlElement element4 = aXmlDoc.CreateElement("Direction");
                element4.AppendChild((XmlNode)aXmlDoc.CreateTextNode(aDirection));
                element1.AppendChild((XmlNode)element4);
                XmlElement element5 = aXmlDoc.CreateElement("Identity");
                element5.AppendChild((XmlNode)aXmlDoc.CreateTextNode(aIdentity));
                element1.AppendChild((XmlNode)element5);
                XmlElement element6 = aXmlDoc.CreateElement("ErrorCode");
                element6.AppendChild((XmlNode)aXmlDoc.CreateTextNode(aErrorCode.ToString()));
                element1.AppendChild((XmlNode)element6);
                XmlElement element7 = aXmlDoc.CreateElement("ErrorMessage");
                element7.AppendChild((XmlNode)aXmlDoc.CreateTextNode(aErrorMsg));
                element1.AppendChild((XmlNode)element7);
                aXmlRootElement.AppendChild((XmlNode)element1);
            }
            catch
            {
            }
            return aXmlDoc;
        }

        private XmlDocument CreateHeaderNode(ref XmlDocument aXmlDoc, ref XmlElement aXmlRootElement)
        {
            XmlElement element1 = aXmlDoc.CreateElement("Header");
            XmlElement element2 = aXmlDoc.CreateElement("MessageName");
            element2.AppendChild((XmlNode)aXmlDoc.CreateTextNode("A"));
            element1.AppendChild((XmlNode)element2);
            XmlElement element3 = aXmlDoc.CreateElement("Stream");
            element3.AppendChild((XmlNode)aXmlDoc.CreateTextNode("B"));
            element1.AppendChild((XmlNode)element3);
            XmlElement element4 = aXmlDoc.CreateElement("Function");
            element4.AppendChild((XmlNode)aXmlDoc.CreateTextNode("C"));
            element1.AppendChild((XmlNode)element4);
            XmlElement element5 = aXmlDoc.CreateElement("Wait");
            element5.AppendChild((XmlNode)aXmlDoc.CreateTextNode("D"));
            element1.AppendChild((XmlNode)element5);
            XmlElement element6 = aXmlDoc.CreateElement("SystemBytes");
            element6.AppendChild((XmlNode)aXmlDoc.CreateTextNode("E"));
            element1.AppendChild((XmlNode)element6);
            XmlElement element7 = aXmlDoc.CreateElement("AutoReply");
            element7.AppendChild((XmlNode)aXmlDoc.CreateTextNode("F"));
            element1.AppendChild((XmlNode)element7);
            XmlElement element8 = aXmlDoc.CreateElement("NoLogging");
            element8.AppendChild((XmlNode)aXmlDoc.CreateTextNode("G"));
            element1.AppendChild((XmlNode)element8);
            XmlElement element9 = aXmlDoc.CreateElement("DeviceID");
            element9.AppendChild((XmlNode)aXmlDoc.CreateTextNode("H"));
            element1.AppendChild((XmlNode)element9);
            XmlElement element10 = aXmlDoc.CreateElement("MessageData");
            element10.AppendChild((XmlNode)aXmlDoc.CreateTextNode("I"));
            element1.AppendChild((XmlNode)element10);
            aXmlRootElement.AppendChild((XmlNode)element1);
            return aXmlDoc;
        }
    }
}
