// created on 05/01/2004 at 15:20
using System;
using Gtk;
using Glade;
using Gnome;

public class AboutDialog
{

        public AboutDialog() 
        {
        	string[] authors = {"Xavier Amado", ""};
			Gdk.Pixbuf pixbuf = new Gdk.Pixbuf ("MonoIcon.png");
            
			Gnome.About aboutdlg = new Gnome.About ("MonoBrowser", "0.1", "Copyright 2003 Xavier Amado", 
			"A Mono powered web browser.", authors, null, null, pixbuf);
			
			aboutdlg.Run();
		}
       
}