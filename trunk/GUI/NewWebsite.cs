// created on 05/17/2004 at 16:38
using System;
using Gtk;
using Glade; 
using ranker;
using ranker.lib;
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
		[Glade.Widget] Window winNewWebsite;
		
		
		
		public NewWebsite () 
        {
        	//Connect glade file
        	Glade.XML gxml = new Glade.XML (null, "GTKRanker.glade", "winNewWebsite", null);
            gxml.Autoconnect (this);  
        }
 		public void on_btnApply_clicked(object o, EventArgs args)
		{
			Console.WriteLine("Adding:" + tbName.Text+ " "+  tbUrl.Text+ " "+  tbKeywords.Text);
			libWebsites ws = new libWebsites();
			ws.addItem(tbName.Text, tbUrl.Text, tbKeywords.Text);
			ws = null;
			this.CloseWindow();
		}
		
		public void OnWindowDeleteEvent (object o, DeleteEventArgs args) 
        {
        	this.CloseWindow();
        }
        public void on_btnCancel_clicked(object o, EventArgs args) 
        {
        	this.CloseWindow();
        }
        public void CloseWindow()
        {
        	winNewWebsite.Destroy(); 
        }
	}
}