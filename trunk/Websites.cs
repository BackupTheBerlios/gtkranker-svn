// created on 05/01/2004 at 15:20
using System;
using Gtk;
using GtkSharp;
using Glade; 
using System.Xml;
using System.Xml.XPath;
using System.IO;

public class WebSites
{
		[Glade.Widget] TreeView tvSiteList;
        public WebSites () 
        {
        	Glade.XML gxml = new Glade.XML (null, "GTKRanker.glade", "WebSites", null);
            gxml.Autoconnect (this);            
            this.FillSiteList();
        }
        /* Connect the Signals defined in Glade */
        public void OnWindowDeleteEvent (object o, DeleteEventArgs args) 
        {
        	Application.Quit ();
        	args.RetVal = true;
        }
        
        public void FillSiteList()
        {
        	// Create our model.
            Gtk.TreeStore tree_store = new TreeStore(typeof(string));
			
			// Assign the model.
			tvSiteList.Model = tree_store;
			
			// Set the column
			tvSiteList.AppendColumn("Name", new CellRendererText(), "text", 0);
			
			// Open the XML File
			string xmlpath = System.Environment.GetEnvironmentVariable("HOME");
			xmlpath = Path.Combine(xmlpath, ".gtkranker/websites.xml");
			XmlDocument doc = new XmlDocument();	
			doc.Load(xmlpath);
			
			// Populate the model.
						XmlNodeList foldernodes = doc.SelectNodes("//site");
			foreach (XmlNode node in foldernodes)
			{
				string name = node.Attributes["name"].InnerText;
				tree_store.AppendValues(name);
			}
        }
        
        public void on_btnAdd_clicked(object o, EventArgs args)
        {
        	TreeIter iter = new TreeIter();
				
			TreeModel model = tvSiteList.Model;
			tvSiteList.Selection.GetSelected(out model, out iter);
				
			TreeStore store = (TreeStore)model;
			string site = (string) store.GetValue(iter, 0);
        	Console.WriteLine("selected item = " + site);
        }
}








