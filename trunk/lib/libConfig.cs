// created on 05/16/2004 at 17:16
using System;
using System.Xml;
using System.Xml.XPath;
using System.IO;

namespace ranker.lib
{
	public class libConfig
	{
		private XmlDocument GetXmlDocument()
		{
			// Open the XML File
			string xmlpath = System.Environment.GetEnvironmentVariable("HOME");
			xmlpath = Path.Combine(xmlpath, ".gtkranker/config.xml");
			XmlDocument doc = new XmlDocument();	
			doc.Load(xmlpath);
			return doc;
		}
		
		public string GetGoogleKey()
		{
			XmlDocument doc = this.GetXmlDocument();
			XmlNode node = doc.SelectSingleNode("//googlekey");
			string url = node.InnerText;
			doc = null;
			return url;
		}
	}
}