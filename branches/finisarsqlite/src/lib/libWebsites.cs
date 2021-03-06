// created on 05/16/2004 at 14:44
using System;
using System.IO;
using System.Xml;
using System.Text;
using System.Data;
using System.Collections.Specialized;
//using Finisar.SQLite;
using Mono.Data.SqliteClient;

namespace ranker.lib
{
	public class libWebsites
	{
		IDbConnection dbcon;
		IDbCommand dbcmd;
		
		public libWebsites()
		{
			this.LoadConfiguration();
			Console.WriteLine("libwebsites created");
			try
			{
				dbcmd.CommandText = "SELECT name, sql FROM sqlite_master WHERE type = 'table' ORDER BY name;";
				IDataReader reader = dbcmd.ExecuteReader();
				if (reader.Read())
					Console.WriteLine("reads");
				else
				{
					dbcmd.CommandText = "create table websites (name varchar(50), url varchar (100)) ";
					dbcmd.ExecuteNonQuery();
					dbcmd.CommandText = "create table keywords (sitename varchar(50), keyphrase varchar (100)) ";
					dbcmd.ExecuteNonQuery();
					Console.WriteLine("not reads");
				}
					
				reader.Close();
			}
			catch (Exception ex) // need to catch nearly every exception, sqllite (or the wrapper) seems unreliable in throwing the right one
			{
				Console.WriteLine(ex.ToString());
			}
			this.EndConfiguration();
		}
	
		private void LoadConfiguration()
        {
        		//the replace is needed because on windows the path is with \ while the uri needs /
			string connectionString = "URI=file:"+ranker.lib.libConfig.GetConfigPath().Replace("\\","/") + "/websites.db";
			Console.WriteLine(connectionString);
			dbcon = new SqliteConnection(connectionString);
			dbcon.Open();
        	dbcmd = dbcon.CreateCommand();        	
        }        
        
        private void EndConfiguration()
        {
        	dbcmd = null;
        	dbcon.Close();
        	dbcon = null;
        }
        
		private void SaveConfiguration()
		{
				
		}
		
		
		public void FillStoreNames(Gtk.TreeStore tree_store)
		{
			this.LoadConfiguration();
			// Populate the model.
			dbcmd.CommandText = "select name from websites";
			IDataReader reader = dbcmd.ExecuteReader();
			while (reader.Read())
			{
				string name = reader.GetString(0);
				tree_store.AppendValues(name);
			}
			this.EndConfiguration();
		}
		
		public string GetSiteUrl(string name)
		{
			this.LoadConfiguration();
			dbcmd.CommandText = "select url from websites where name = '" + name.Replace("'","''") + "'";
			string url = (string)dbcmd.ExecuteScalar();
			this.EndConfiguration();
			return url;
		}
		
		public StringCollection GetSiteKeywords(string name)
		{
			this.LoadConfiguration();
			StringCollection keywords=new StringCollection();
			dbcmd.CommandText = "select keyphrase from keywords where sitename = '" + name.Replace("'","''") + "'";
			IDataReader reader = dbcmd.ExecuteReader();			
			while (reader.Read())
			{
				keywords.Add(reader.GetString(0));				
			}			
			this.EndConfiguration();
			return keywords;
		}
		
		public void addItem(string name, string url, string keywords)
		{
			this.LoadConfiguration();
			dbcmd.CommandText = "Insert into websites (name, url) values ('" + name +"','" + url +"')";
			dbcmd.ExecuteNonQuery();
			string [] aKeywords = keywords.Split(";"[0]);
	        	foreach (string s in aKeywords) 
 	     	  	{
				dbcmd.CommandText = "insert into keywords (sitename, keyphrase) values ('" + name + "','" + s + "')";
				dbcmd.ExecuteNonQuery();
        		}
			this.EndConfiguration();
		}
		
		public void deleteItem(string name)
		{
			this.LoadConfiguration();
			dbcmd.CommandText = "delete from websites where name= '" + name+  "'";
			dbcmd.ExecuteNonQuery();
			this.EndConfiguration();
		}
	}
}
