// created on 05/16/2004 at 14:44
using System;
using System.IO;
using System.Xml;

namespace ranker.lib
{
	public class libWebsites
	{
	
		private XmlDocument GetXmlDocument()
		{
			// Open the XML File
			string xmlpath = System.Environment.GetEnvironmentVariable("HOME");
			xmlpath = Path.Combine(xmlpath, ".gtkranker/websites.xml");
			XmlDocument doc = new XmlDocument();	
			doc.Load(xmlpath);
			return doc;
		}
		
		public void FillStoreNames(Gtk.TreeStore tree_store)
		{
			XmlDocument doc = GetXmlDocument();			
			// Populate the model.
			XmlNodeList nodes = doc.SelectNodes("//site");
			foreach (XmlNode node in nodes)
			{
				string name = node.Attributes["name"].InnerText;
				tree_store.AppendValues(name);
			}
			doc = null;
		}
		
		public string GetSiteUrl(string name)
		{
			XmlDocument doc = this.GetXmlDocument();
			Console.Write("got doc, name = " + name);
			
			XmlNode node = doc.SelectSingleNode("//site[@name='" + name + "']");
			string url = node.Attributes["url"].InnerText;
			doc = null;
			return url;
		}
	}
}