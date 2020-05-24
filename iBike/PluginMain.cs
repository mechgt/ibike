using System.Text;
using System;
using System.Xml;
using System.Xml.Serialization;
using iBike.Data;
using System.IO;
using ZoneFiveSoftware.Common.Visuals.Fitness;

namespace iBike
{
    class PluginMain : IPlugin
    {
        #region Fields

        private static string name = "iBike Plugin";
        private static IApplication application;

        #endregion

        /// <summary>
        /// Plugin product Id as listed in license application
        /// </summary>
        internal static string ProductId
        {
            get
            {
                return "ib";
            }
        }

        internal static string SupportEmail
        {
            get
            {
                return "support@mechgt.com";
            }
        }

        #region IPlugin Members

        public PluginMain()
        {
        }

        public IApplication Application
        {
            set
            {
                application = value;
            }
        }

        public Guid Id
        {
            get
            {
                return GUIDs.PluginMain;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
        }

        public void ReadOptions(XmlDocument xmlDoc, XmlNamespaceManager nsmgr, XmlElement pluginNode)
        {
            GlobalSettings settings = GlobalSettings.Instance;
            XmlSerializer xs = new XmlSerializer(typeof(GlobalSettings));
            MemoryStream memoryStream = new MemoryStream(Utilities.StringToUTF8ByteArray(pluginNode.InnerText));

            XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);

            object deserialize = xs.Deserialize(memoryStream);

            settings = (GlobalSettings)deserialize;
        }

        public string Version
        {
            get
            {
                return GetType().Assembly.GetName().Version.ToString(3);
            }
        }

        public void WriteOptions(XmlDocument xmlDoc, XmlElement pluginNode)
        {
            // Serialization
            string xmlizedString;
            MemoryStream memoryStream = new MemoryStream();
            XmlSerializer xs = new XmlSerializer(typeof(GlobalSettings));
            XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);

            GlobalSettings settings = new GlobalSettings();
            xs.Serialize(xmlTextWriter, settings);
            memoryStream = (MemoryStream)xmlTextWriter.BaseStream;
            xmlizedString = Utilities.UTF8ByteArrayToString(memoryStream.ToArray());

            pluginNode.InnerText = xmlizedString;
        }

        #endregion

        public static IApplication GetApplication()
        {
            return application;
        }
    }
}
