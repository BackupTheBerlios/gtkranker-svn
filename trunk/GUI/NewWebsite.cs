// created on 05/17/2004 at 16:38
using System;
using Gtk;
using Glade; 
using ranker;
using System.Xml;
using System.Xml.XPath;
using System.IO;

namespace ranker.GUI
{
	public class NewWebsite
	{
		// Glade widgets
		[Glade.Widget] Entry tbName;
		[Glade.Widget] Entry tbUrl;
		[Glade.Widget] Entry tbKeywords;
		
		// Xmldocument for the websites xml file
		XmlDocument doc;
		
		public NewWebsite () 
        {
        	//Connect glade file
        	Glade.XML gxml = new Glade.XML (null, "GTKRanker.glade", "NewWebsite", null);
            gxml.Autoconnect (this);  
            
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
				SaveConfiguration();
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
			SaveConfiguration();
			Console.WriteLine("Added new website");
			this.CloseWindow();
		}
		
		public void on_btnApply_clicked(object o, EventArgs args)
		{
			Console.WriteLine("Adding:" + tbName.Text+ " "+  tbUrl.Text+ " "+  tbKeywords.Text);
			this.addItem(tbName.Text, tbUrl.Text, tbKeywords.Text);
		}
		
		public void OnWindowDeleteEvent (object o, DeleteEventArgs args) 
        {
        	this.CloseWindow();
            args.RetVal = true;
        }
        
        public void CloseWindow()
        {
			//find out how to close the window       
        }
	}
}