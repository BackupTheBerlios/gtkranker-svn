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
	
		// Xmldocument for the websites xml file
		XmlDocument doc;
		
		public libWebsites()
		{
			//Open Website configuration file
            this.LoadConfiguration();
		}
	
		private void LoadConfiguration()
        {
        	doc = new XmlDocument();	
			
			string xmlpath = System.Environment.GetEnvironmentVariable("HOME");
			xmlpath = Path.Combine(xmlpath, ".gtkranker/websites.xml");
			
			// Check if the file exists
			if(!File.Exists(xmlpath))
			{
				// Create an empty root element
				doc.AppendChild(doc.CreateNode(XmlNodeType.Element, "sitelist", ""));
				// Save the file
				this.SaveConfiguration();
			}
			else
			{
				doc.Load(xmlpath);
			}
        }        
        
        
		void SaveConfiguration()
		{
			string xmlpath = System.Environment.GetEnvironmentVariable("HOME");
			xmlpath = Path.Combine(xmlpath, ".gtkranker/websites.xml");
			doc.Save(xmlpath);	
		}
		
		public void FillStoreNames(Gtk.TreeStore tree_store)
		{
			// Populate the model.
			XmlNodeList nodes = doc.SelectNodes("//site");
			foreach (XmlNode node in nodes)
			{
				string name = node.Attributes["name"].InnerText;
				tree_store.AppendValues(name);
			}
		}
		
		public string GetSiteUrl(string name)
		{
			XmlNode node = doc.SelectSingleNode("//site[@name='" + name + "']");
			string url = node.Attributes["url"].InnerText;
			return url;
		}
		
		public StringCollection GetSiteKeywords(string name)
		{
			// Populate the model.
			string xpath = "/sitelist/site[@name='" + name + "']/keyphrase";
			XmlNodeList nodes = doc.SelectNodes(xpath);
			int i=0;
			StringCollection keywords=new StringCollection();
			foreach (XmlNode node in nodes)
			{
				string kw = node.InnerText;
				keywords.Add(kw);
			}
			return keywords;
		}
		
		public void addItem(string name, string url, string keywords)
		{
			//select root node
			XmlNode foldernode = doc.SelectSingleNode("/sitelist");
			//create our new site's element
			XmlNode newitem =doc.CreateNode(XmlNodeType.Element, "site", "");
			//Add the name and url attributes
			string ns = newitem.GetNamespaceOfPrefix("bk");
			XmlNode attr = doc.CreateNode(XmlNodeType.Attribute, "name",ns);
			attr.Value = name;
			newitem.Attributes.SetNamedItem(attr);
			attr = doc.CreateNode(XmlNodeType.Attribute, "url",ns);
			attr.Value = url;
			newitem.Attributes.SetNamedItem(attr);
			//Add the keywords childs
			string [] aKeywords = keywords.Split(";"[0]);

        	foreach (string s in aKeywords) 
        	{
				XmlNode keywordnode =doc.CreateNode(XmlNodeType.Element, "keyphrase", "");
				keywordnode.InnerText = s;
				newitem.AppendChild(keywordnode);    
        	}
			foldernode.AppendChild(newitem);
			this.SaveConfiguration();
			Console.WriteLine("Added new website");
		}
		
		public void deleteItem(string name)
		{
			XmlNode root = doc.DocumentElement;
			XmlNode node = root.SelectSingleNode("//site[@name='" + name + "']");
			root.RemoveChild(node);
			this.SaveConfiguration();
		}
	}
}