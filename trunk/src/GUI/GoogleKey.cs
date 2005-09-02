// created on 02/09/2005 at 17:24
// created on 05/17/2004 at 16:38
using System;
using Gtk;
using Glade; 
using ranker;
using ranker.lib;
using System.Xml;
using System.Xml.XPath;
using System.IO;

namespace ranker.GUI {

	public class GoogleKey  {

		// Glade widgets
		[Glade.Widget] Entry tbGoogleKey;
		[Glade.Widget] Window winGoogleKey;
		
		
		
		public GoogleKey () 
	        {
	        	//Connect glade file
    			Glade.XML gxml = new Glade.XML (null, "GTKRanker.glade", "winGoogleKey", null);
				gxml.Autoconnect (this);
	        }
	
			
			public void on_btnKeyOK_clicked(object o, EventArgs args)
			{
				ranker.lib.libConfig lc = new ranker.lib.libConfig();
				lc.WriteConfigFile(tbGoogleKey.Text);
				lc = null;
				this.CloseWindow();
				
			}
			
			public void on_btnKeyCancel_clicked(object o, EventArgs args)
			{
				this.CloseWindow();
			}		
	

		
			public void OnWindowDeleteEvent (object o, DeleteEventArgs args) 
	        {
        		this.CloseWindow();
        	}
		
	        		
	        public void CloseWindow()
        	{
        		winGoogleKey.Destroy(); 
        	}
	}
}
