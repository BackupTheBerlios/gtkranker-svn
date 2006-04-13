// created on 05/16/2004 at 14:44
using System;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using System.Text;
using System.Collections.Specialized;

namespace ranker.lib
{
	public class libWebsites
	{
	
		private XmlDocument GetXmlDocument()
		{
			// Open the XML File
			string xmlpath = ranker.lib.libConfig.GetConfigPath();
			XmlDocument doc = new XmlDocument();
			xmlpath = Path.Combine(xmlpath, "sites.xml");	
			try
			{
				doc.Load(xmlpath);
			}
			catch(System.IO.FileNotFoundException) 
			{

				doc.LoadXml("<sites></sites>");
				doc.Save(Path.Combine(ranker.lib.libConfig.GetConfigPath(), "sites.xml"));	
			}
			return doc;
		}
				
	        
		private void SaveConfiguration(XmlDocument doc)
		{
			doc.Save(Path.Combine(ranker.lib.libConfig.GetConfigPath(), "sites.xml"));
		}
		
		
		public void FillStoreNames(Gtk.TreeStore tree_store)
		{
				
			XmlDocument doc = this.GetXmlDocument();
			XmlNodeList nodelist = doc.SelectNodes("/sites/site/name");
			
			// Iterate on the node set
			
			foreach (XmlNode n in nodelist)
			{
				Console.WriteLine(n.InnerText);
				tree_store.AppendValues(n.InnerText);
			}
			
			doc = null;
			nodelist = null;

		}
		
		public string GetSiteUrl(string name)
		{
			XmlDocument doc = this.GetXmlDocument();
			XmlNode node = doc.SelectSingleNode("/sites/site[name='"+name+"']/url");
			string url = node.InnerText;
			doc = null;
			node = null;
			return url;
		}
		
		public StringCollection GetSiteKeywords(string name)
		{
			StringCollection keywords=new StringCollection();

			XmlDocument doc = this.GetXmlDocument();
			
			XmlNodeList nodelist = doc.SelectNodes("/sites/site[name='"+name+"']/keywords");

			// Iterate on the node set
			foreach (XmlNode n in nodelist)
			{
				Console.WriteLine(n.InnerText);
				keywords.Add(n.InnerText);
			}

			doc = null;
			nodelist = null;
			
			return keywords;
		}
		
		public void addItem(string name, string url, string keywords)
		{
			XmlDocument doc = this.GetXmlDocument();

			string [] aKeywords = keywords.Split(";"[0]);

			XmlElement xmlNewSite = doc.CreateElement("site");
			XmlElement xmlNewName = doc.CreateElement("name");
			xmlNewName.InnerXml = name;
			XmlElement xmlNewUrl = doc.CreateElement("url");
			xmlNewUrl.InnerXml = url;
		
			xmlNewSite.AppendChild(xmlNewName);
			xmlNewSite.AppendChild(xmlNewUrl);

	        foreach (string s in aKeywords) 
 	     	{
 	     		XmlElement xmlNewKey = doc.CreateElement("keywords");
 	     		xmlNewKey.InnerXml = s;		
				xmlNewSite.AppendChild(xmlNewKey);
				xmlNewKey = null;
        	}

	   		doc.DocumentElement.AppendChild(xmlNewSite);
	   		
	   		this.SaveConfiguration(doc);
	   		
	   		doc = null;
			xmlNewUrl = null;
			xmlNewName = null;
			xmlNewSite = null;
	   		
		}
		
		public void ChangeSiteName(string oldname, string newname)
		{
		
		}
		
		public void ChangeSiteUrl(string name, string newurl)
		{
		
		}
		
		public void RemoveSiteKeywords(string name, string oldkeywords) //doesn't work
		{
			string [] keywords = oldkeywords.Split(";"[0]);
			
			XmlDocument doc = this.GetXmlDocument();
			XmlNode node = doc.SelectSingleNode("/sites/site[name='"+name+"']");
			
			foreach (string s in keywords)
			{
				XmlNode noderemove = node.SelectSingleNode("[keywords='"+s+"']/keywords");		
				node.RemoveChild(noderemove);
				noderemove = null;
			}
			
			this.SaveConfiguration(doc);
			
			doc = null;
			node = null;
		
		
		}
		
		public void AddSiteKeywords(string name, string newkeywords)
		{
			string [] keywords = newkeywords.Split(";"[0]);
			
			XmlDocument doc = this.GetXmlDocument();
			XmlNode node = doc.SelectSingleNode("/sites/site[name='"+name+"']");
			
			Console.WriteLine("antes de foreach node {0}", node.InnerXml);
			
			foreach (string s in keywords) 
 	     	{
 	     		Console.WriteLine("foreach1");
 	     		XmlElement xmlNewKey = doc.CreateElement("keywords");
 	     		Console.WriteLine("foreach2");
 	     		xmlNewKey.InnerXml = s;		
 	     		Console.WriteLine("foreach3");
				node.AppendChild(xmlNewKey);
				Console.WriteLine("foreach4");
				xmlNewKey = null;
        	}
        	
        	this.SaveConfiguration(doc);
        	
        	doc = null;
        	node = null;
			
		}
		
		
		public void deleteItem(string name)
		{
			XmlDocument doc = this.GetXmlDocument();
			XmlNode noderemove = doc.SelectSingleNode("/sites/site[name='"+name+"']");
			XmlNode node = doc.SelectSingleNode("/sites");
			
			node.RemoveChild(noderemove);
			this.SaveConfiguration(doc);
			
			doc = null;
			noderemove = null;
			node = null;
		}
	}
}