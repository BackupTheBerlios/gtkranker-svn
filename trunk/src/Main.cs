// project created on 05/01/2004 at 15:05
using System;
using Gtk;
using Glade;
using ranker;

public class gtkRanker
{
	public static void Main (string[] args)
	{
		// check if we have a configuration directory
		string configdir = System.Environment.GetEnvironmentVariable("HOME");
		configdir = System.IO.Path.Combine(configdir, ".gtkranker");
		
		if (!System.IO.Directory.Exists(configdir))
		{
			System.IO.Directory.CreateDirectory(configdir);
		}
		new ranker.GUI.Mainwindow ();
	}
}

