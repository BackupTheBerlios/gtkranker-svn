// created on 05/16/2004 at 14:44
using System;
using System.IO;
using System.Xml;
using System.Text;
using System.Data;
using System.Collections.Specialized;
using Mono.Data.SqliteClient;

namespace ranker.lib
{
	public class libWebsites
	{
		IDbConnection dbcon;
		IDbCommand dbcmd;
		
		public libWebsites()
		{
			//Open Website configuration file
            this.LoadConfiguration();
		}
	
		private void LoadConfiguration()
        {
			string connectionString = "URI=file:"+ranker.lib.libConfig.GetConfigPath() + Path.DirectorySeparatorChar + ".websites.db";
			dbcon = new SqliteConnection(connectionString);
			dbcon.Open();
			dbcmd = dbcon.CreateCommand();        	
        }        
        
        
		void SaveConfiguration()
		{
				
		}
		
		public void FillStoreNames(Gtk.TreeStore tree_store)
		{
			// Populate the model.
			dbcmd.CommandText = "select name from websites";
			IDataReader reader = dbcmd.ExecuteReader();
			while (reader.Read())
			{
				string name = reader.GetString(0);
				tree_store.AppendValues(name);
			}
		}
		
		public string GetSiteUrl(string name)
		{
			dbcmd.CommandText = "select url from websites where name = '" + name.Replace("'","''") + "'";
			string url = (string)dbcmd.ExecuteScalar();
			return url;
		}
		
		public StringCollection GetSiteKeywords(string name)
		{
			StringCollection keywords=new StringCollection();
			dbcmd.CommandText = "select keyphrase from keywords where sitename = '" + name.Replace("'","''") + "'";
			IDataReader reader = dbcmd.ExecuteReader();
			while (reader.Read())
			{
				keywords.Add(reader.GetString(0));				
			}
			return keywords;
		}
		
		public void addItem(string name, string url, string keywords)
		{
			//select root node
//			XmlNode foldernode = doc.SelectSingleNode("/sitelist");
//			//create our new site's element
//			XmlNode newitem =doc.CreateNode(XmlNodeType.Element, "site", "");
			//Add the name and url attributes
//			string ns = newitem.GetNamespaceOfPrefix("bk");
//			XmlNode attr = doc.CreateNode(XmlNodeType.Attribute, "name",ns);
//			attr.Value = name;
//			newitem.Attributes.SetNamedItem(attr);
//			attr = doc.CreateNode(XmlNodeType.Attribute, "url",ns);
//			attr.Value = url;
//			newitem.Attributes.SetNamedItem(attr);
//			//Add the keywords childs
//			string [] aKeywords = keywords.Split(";"[0]);

//        	foreach (string s in aKeywords) 
 //       	{
//				XmlNode keywordnode =doc.CreateNode(XmlNodeType.Element, "keyphrase", "");
//				keywordnode.InnerText = s;
//				newitem.AppendChild(keywordnode);    
 //       	}
//			foldernode.AppendChild(newitem);
//			this.SaveConfiguration();
//			Console.WriteLine("Added new website");
		}
		
		public void deleteItem(string name)
		{
//			XmlNode root = doc.DocumentElement;
//			XmlNode node = root.SelectSingleNode("//site[@name='" + name + "']");
//			root.RemoveChild(node);
//			this.SaveConfiguration();
		}
	}
}