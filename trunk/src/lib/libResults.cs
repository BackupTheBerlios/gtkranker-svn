// created on 05/17/2004 at 13:28
using System;
using System.IO;
using System.Xml;
using System.Xml.Xsl;
using System.Xml.XPath;
namespace ranker.lib
{
	public class libResults
	{
		public string SaveResults(string ResultXML, string website)
		{
			string resultsdir = System.Environment.GetEnvironmentVariable("HOME");
			resultsdir = Path.Combine(resultsdir, ".gtkranker" + Path.DirectorySeparatorChar + "results"+ Path.DirectorySeparatorChar + website);
			
			if (!Directory.Exists(resultsdir))
			{
				System.IO.Directory.CreateDirectory(resultsdir);
			}
			string filename = resultsdir + Path.DirectorySeparatorChar  + DateTime.Now.ToString("yyyy-MM-dd-HHmm") + ".xml"; 
			StreamWriter sw=File.CreateText(filename);
 			sw.Write(ResultXML);
  			sw.Close();
			return filename;
		}
		
		//public string generateHtml(string xmlpath)
		//{
		//	// Create the transform object.
		//	XslTransform xslt = new XslTransform();
		//	// Load the xsl stylesheet
		//	
		//	// Load the xml file
		//	string configpath = System.Environment.GetEnvironmentVariable("HOME");
		//	xslt.Load(Path.Combine(configpath, ".gtkranker/result.xsl"));
		//	XPathDocument data = new XPathDocument(xmlpath);
		//	
		//	// Define the path where to store the .html file
		//	string htmlpath = System.Environment.GetEnvironmentVariable("HOME");
		//	htmlpath= Path.Combine(htmlpath, ".gtkranker/" + DateTime.Now.ToString("yyyy-MM-dd-HHss") + ".html");
		//	Console.Write(htmlpath);
		//	// Create our writer to the html file
		//	XmlUrlResolver writer = new XmlUrlResolver();
		//	writer.ResolveUri(null,htmlpath);
		//	// Do the transformation.
		//	XmlReader xmlr = xslt.Transform(data,null, writer);
		//	StreamWriter sw=File.CreateText(htmlpath);
 		//	sw.Write(xmlr.ReadOuterXml());
  		//	sw.Close();
			
		//	return htmlpath;
		//}
		public string LoadResults()
		{
			return "";
		}
	}
}