// created on 05/16/2004 at 17:16
using System;
using System.Xml;
using System.Xml.XPath;
using System.IO;

namespace ranker.lib {

	public class libConfig {



		private XmlDocument GetXmlDocument()
		{
			// Open the XML File
			string xmlpath = ranker.lib.libConfig.GetConfigPath();
			XmlDocument doc = new XmlDocument();
			xmlpath = Path.Combine(GetConfigPath(), "config.xml");	
			
			try
			{
				doc.Load(xmlpath);
			}
			catch(System.IO.FileNotFoundException) 
			{

				Console.WriteLine("No configuration file found, please create one manually for now");
				this.CreateConfigFile();
				doc.Load(xmlpath);		
				Console.WriteLine("Configuration file created");
			}
			return doc;
		}

		public void CreateConfigFile()
		{
			// o bien: ranker.GUI.GoogleKey Gk = new ranker.GUI.GoogleKey();
			new ranker.GUI.GoogleKey();
		}
		
		public void WriteConfigFile(string gk)
		{
				string xmlpath = ranker.lib.libConfig.GetConfigPath();
				XmlDocument doc = new XmlDocument();	
				xmlpath = Path.Combine(GetConfigPath(), "config.xml");
				doc.LoadXml("<googlekey>"+gk+"</googlekey>");
				doc.Save(xmlpath);
		}


		
		public string GetGoogleKey()
		{
			XmlDocument doc = this.GetXmlDocument();
			XmlNode node = doc.SelectSingleNode("//googlekey");
			string key = node.InnerText;
			doc = null;
			return key;
		}
		
		public static string GetConfigPath()
		{
			string configdir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
			configdir = System.IO.Path.Combine(configdir, "gtkranker");
			if (!System.IO.Directory.Exists(configdir)) 
			{

				System.IO.Directory.CreateDirectory(configdir);
			}
			return configdir;
		}
	}
}
