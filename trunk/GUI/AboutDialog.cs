// created on 05/01/2004 at 15:20
using System;
using Gtk;
using Glade;
using Gnome;

public class AboutDialog
{

        public AboutDialog() 
        {
        	Console.Write("running about");
        	string[] authors = {"Tom Vergote", ""};
			Gdk.Pixbuf pixbuf = new Gdk.Pixbuf ("MonoIcon.png");
            
			Gnome.About aboutdlg = new Gnome.About ("gtkRanker", "0.0", "Copyright 2004 Tom Vergote", 
			"Tool to check your ranking in Google", authors, null, null, pixbuf);
			
			aboutdlg.Run();
		}
       
}