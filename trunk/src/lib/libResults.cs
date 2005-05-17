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
			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.Load(xmlpath);
			
			StringWriter sw = new StringWriter(); 
			xslt.Transform(xmlDoc, null, sw, null);

			return sw.ToString(); 
		}

		public string LoadResults()
		{
			return "";
		}
	}
}
