// created on 05/16/2004 at 13:35
using System;
using Gtk;
using Glade; 
using System.Xml;
using System.Xml.XPath;
using System.IO;
using ranker;
using System.Collections.Specialized;
using Gecko;
namespace ranker.GUI {

	public class Mainwindow {

		[Glade.Widget] TreeView tvSitePane;
		[Glade.Widget] Frame frmResultsContent;
		[Glade.Widget] Window winMainWindow;
		[Glade.Widget] Gtk.TreeStore tree_store;
		Gtk.MessageDialog dialog;
		WebControl web;
       	public Mainwindow () 
        {
         	Application.Init();
			Glade.XML gxml = new Glade.XML (null, "GTKRanker2.glade", "winMainWindow", null);
			gxml.Autoconnect (this);
			winMainWindow.DeleteEvent += new DeleteEventHandler (OnWindowDelete);
			this.FillSiteList();
			this.AddGeckoPanel();
			Application.Run();
        }

        /* Connect the Signals defined in Glade */
        public void OnWindowDelete (object o, DeleteEventArgs args) 
        {
            Application.Quit ();
            args.RetVal = true;
        }
        
        public void on_btnAdd_clicked(object o, EventArgs args)
        {
			
        	NewWebsite ws = new NewWebsite();

			tree_store.Clear();		
			this.FillSiteList();
        } 
        
		public void on_btnExit_clicked(object o, EventArgs args)
        {
			Application.Quit();
		}

        public void on_btnAbout_clicked(object o, EventArgs args)
        {        	
        	About ad = new About();
        }
        
        public void on_btnExecute_clicked(object o, EventArgs args)
        {
			try
			{
        	lib.libWebsites lws = new lib.libWebsites(); 
			string sitename= this.GetSelectedSite();
        	string url = lws.GetSiteUrl(sitename);
        	Console.Write(url);
        	
        	StringCollection keywords = lws.GetSiteKeywords(sitename);
        	lib.libGoogleQuery lgc = new lib.libGoogleQuery();
        	string resulturl = lgc.ProcessSite(url,keywords,sitename);
			web.RenderData (resulturl, "file:///tmp", "text/html");
			}
        }
        
        public void on_btnDelete_clicked(object o, EventArgs args)
        {
        	dialog = new MessageDialog (winMainWindow, DialogFlags.DestroyWithParent,MessageType.Question,ButtonsType.None, "Are you sure you want to delete " + GetSelectedSite());
        	dialog.Modal = true;
        	dialog.AddButton ("Cancel", Gtk.ResponseType.Cancel);
        	dialog.AddButton ("Delete", Gtk.ResponseType.Ok);
            dialog.Response += new ResponseHandler (on_dialog_response);
            dialog.Run ();
            dialog.Destroy ();
        }

        void on_dialog_response (object obj, ResponseArgs args)
        {
        	if (args.ResponseId == Gtk.ResponseType.Ok) {

        		lib.libWebsites lws = new lib.libWebsites(); 
        		lws.deleteItem(GetSelectedSite());
				lws = null;
				tree_store.Clear();		
				this.FillSiteList();
        	}            
        }
        public void FillSiteList()
        {
        	// Create our model.
            tree_store = new TreeStore(typeof(string));
			
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
        
        public void AddGeckoPanel()
        {
			// First we create a WebControl, using its default constructor
			web = new WebControl();

			// Then we ask it to show itself.
			// This is required because of a Gecko bug, and doesn't actually show the control yet. 
			web.Show();

			// Next, we'll add the web control to our existing frame:
			frmResultsContent.Add(web);
        }
	}    
}
