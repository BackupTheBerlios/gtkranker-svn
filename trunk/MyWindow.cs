// created on 05/01/2004 at 15:52
using System;
using Gtk;
using GtkSharp

namespace MyWindow
{
	public class CreatedWindow : Window
	{
		static GLib.GType gtype;
		
		public static new GLib.GType GType
		{
			get
			{
				if (gtype == GLib.GType.Invalid)
					gtype = RegisterGType (typeof (CreatedWindow));
				return gtype;
			}
		}

		public CreatedWindow () : base (GType)
		{
		}
	}
}
