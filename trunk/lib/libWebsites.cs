// created on 05/16/2004 at 14:44
using System;
using System.IO;
using System.Xml;
using System.Text;
using System.Collections.Specialized;

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
			XmlNode node = doc.SelectSingleNode("//site[@name='" + name + "']");
			string url = node.Attributes["url"].InnerText;
			doc = null;
			return url;
		}
		
		public StringCollection GetSiteKeywords(string name)
		{
			Console.WriteLine("trying to get keywords");
			XmlDocument doc = GetXmlDocument();			
			// Populate the model.
			string xpath = "/sitelist/site[@name='" + name + "']/keyphrase";
			XmlNodeList nodes = doc.SelectNodes(xpath);
			int i=0;
			StringCollection keywords=new StringCollection();
			foreach (XmlNode node in nodes)
			{
				string kw = node.InnerText;
				keywords.Add(kw);
				Console.WriteLine("Added " + kw + " to array");
			}
			doc = null;
			return keywords;
		}
	}
}