// created on 05/17/2004 at 16:38
// created on 05/01/2004 at 15:20
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
			xmlpath = Path.Combine(xmlpath, ".monobrowser/websites.xml");
			doc.Save(xmlpath);	
		}
		
		public void addItem(string name, string url, string keywords)
		{
			XmlNode foldernode = doc.SelectSingleNode("/sitelist");
			XmlNode newitem =doc.CreateNode(XmlNodeType.Element, "site", "");
			newitem.Attributes["name"].Value = name;
			newitem.Attributes["url"].Value = url;
				
			foldernode.AppendChild(newitem);
				
			
			
			SaveConfiguration();
		}
		
		public void on_btnApply_clicked(object o, EventArgs args)
		{
			Console.WriteLine("Adding:" + tbName.Text+ " "+  tbUrl.Text+ " "+  tbKeywords.Text);
			this.addItem(tbName.Text, tbUrl.Text, tbKeywords.Text);
		}
	}
}