// created on 05/16/2004 at 13:35
using System;
using Gtk;
using Glade; 
using System.Xml;
using System.Xml.XPath;
using System.IO;
using ranker;
namespace ranker.GUI
{
public class Mainwindow
{
		[Glade.Widget] TreeView tvSitePane;
       	public Mainwindow () 
        {
         	Application.Init();
			Glade.XML gxml = new Glade.XML (null, "GTKRanker.glade", "MainWindow", null);
			gxml.Autoconnect (this);
			this.FillSiteList();
			Application.Run();
        }

        /* Connect the Signals defined in Glade */
        public void OnWindowDeleteEvent (object o, DeleteEventArgs args) 
        {
            Application.Quit ();
            args.RetVal = true;
        }
        
        public void OpenPreferences(object o, EventArgs args)
        {
        	WebSites ws = new WebSites();
        } 
        
        public void ShowAbout(object o, EventArgs args)
        {
        //this is not working for some reason.
        	AboutDialog ad = new AboutDialog();
        }
        
        public void on_btnExecute_clicked(object o, EventArgs args)
        {
        	lib.libWebsites lws = new lib.libWebsites(); 
			
        	string url = lws.GetSiteUrl(this.GetSelectedSite());
        	Console.Write(url);
        	ranker.lib.GoogleService gs = new ranker.lib.GoogleService();
        	//gs.
        }
        
        public void FillSiteList()
        {
        	// Create our model.
            Gtk.TreeStore tree_store = new TreeStore(typeof(string));
			
			// Assign the model.
			tvSitePane.Model = tree_store;
			
			// Set the column
			tvSitePane.AppendColumn("Name", new CellRendererText(), "text", 0);
			
			lib.libWebsites lws = new lib.libWebsites();
			lws.FillStoreNames(tree_store);
        }
        
        public string GetSelectedSite()
        {
        	TreeIter iter = new TreeIter();
				
			TreeModel model = tvSitePane.Model;
			tvSitePane.Selection.GetSelected(out model, out iter);
				
			TreeStore store = (TreeStore)model;
			string site = (string) store.GetValue(iter, 0);
			return site;
        }
}    
}