// project created on 05/01/2004 at 15:05
using System;
using Gtk;
using Glade;
using ranker;

public class gtkRanker
{
	[STAThread]
	public static void Main (string[] args)
	{
		new ranker.GUI.Mainwindow ();
	}
}

