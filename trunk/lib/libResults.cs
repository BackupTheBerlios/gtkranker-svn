// created on 05/17/2004 at 13:28
using System;
using System.IO;

namespace ranker.lib
{
	public class libResults
	{
		public void SaveResults(string ResultXML, string website)
		{
			string resultsdir = System.Environment.GetEnvironmentVariable("HOME");
			resultsdir = Path.Combine(resultsdir, ".gtkranker" + Path.DirectorySeparatorChar + "results"+ Path.DirectorySeparatorChar + website);
			
			if (!Directory.Exists(resultsdir))
			{
				System.IO.Directory.CreateDirectory(resultsdir);
			}
			string filename = resultsdir + Path.DirectorySeparatorChar  + DateTime.Now.ToLongDateString() + ".xml"; 
			StreamWriter sw=File.CreateText(filename);
 			sw.Write(ResultXML);
  			sw.Close();

		}
		
		public string LoadResults()
		{
			return "";
		}
	}
}