// created on 05/17/2004 at 13:28
using System;
using System.IO;
using System.Xml;
using System.Xml.Xsl;
using System.Xml.XPath;
namespace ranker.lib {

	public class libResults {

		public string SaveResults(string ResultXML, string website)
		{
			string resultsdir = ranker.lib.libConfig.GetConfigPath() ;
			resultsdir = resultsdir +  Path.DirectorySeparatorChar + "results"+ Path.DirectorySeparatorChar + website;
			
			if (!Directory.Exists(resultsdir)) {

				System.IO.Directory.CreateDirectory(resultsdir);
			}
			string filename = resultsdir + Path.DirectorySeparatorChar  + DateTime.Now.ToString("yyyy-MM-dd-HHmm") + ".xml"; 
			StreamWriter sw=File.CreateText(filename);
 			sw.Write(ResultXML);
  			sw.Close();
			return filename;
		}
		
		public string generateHtml(string xmlpath)
		{
			string xslpath = ranker.lib.libConfig.GetConfigPath() +  Path.DirectorySeparatorChar + "result.xsl";
			XslTransform xslt = new XslTransform();
			xslt.Load(xslpath);
			string htmlpath =  ranker.lib.libConfig.GetConfigPath() + Path.DirectorySeparatorChar +"temp.html";
			xslt.Transform(xmlpath,htmlpath ); 
			return htmlpath;
		}

		public string LoadResults()
		{
			return "";
		}
	}
}
