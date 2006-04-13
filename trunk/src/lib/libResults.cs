// created on 05/17/2004 at 13:28
using System;
using System.IO;
using System.Xml;
using System.Xml.Xsl;
using System.Xml.XPath;
using System.Text;
using System.Collections.Specialized;

namespace ranker.lib {

	public class libResults {

		public string SaveResults(XmlDocument doc, string website)
		{
			string resultsdir = ranker.lib.libConfig.GetConfigPath() ;
			resultsdir = resultsdir +  Path.DirectorySeparatorChar + "results"+ Path.DirectorySeparatorChar + website;
			
			if (!Directory.Exists(resultsdir)) 
			{
				System.IO.Directory.CreateDirectory(resultsdir);
			}
			
			string filename = resultsdir + Path.DirectorySeparatorChar  + DateTime.Now.ToString("yyyy-MM-dd-HHmm") + ".xml"; 
			/*StreamWriter sw=File.CreateText(filename);
 			sw.Write(ResultXML);
  			sw.Close();*/
  			doc.Save(filename);
  			
			return filename;
		}
		
		public XmlDocument CreateResults(string url)
		{
			StringBuilder sbResult = new StringBuilder("<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?>"+ System.Environment.NewLine);
			sbResult.Append("<?xml-stylesheet type=\"text/xslt\" href=\"" +ranker.lib.libConfig.GetConfigPath() +  Path.DirectorySeparatorChar + "result.xsl\"?>");
			sbResult.Append("<resultset>"+ System.Environment.NewLine);
			sbResult.Append("<url>"+ System.Environment.NewLine);
        	sbResult.Append("" + url + System.Environment.NewLine);
        	sbResult.Append("</url>"+ System.Environment.NewLine);
        	
        	sbResult.Append("</resultset>");
        	
        	Console.WriteLine("start: {0}",sbResult.ToString());
        	
        	XmlDocument doc = new XmlDocument();
        	doc.LoadXml(sbResult.ToString());
        	return doc;
		
		}
		
		public XmlDocument AddEngine(string engine,string[] keywords,string backlinks, XmlDocument doc)
		{
//			XmlDocument doc = this.start("google"); //new XmlDocument();//this.GetXmlDocument();

			XmlNode node = doc.SelectSingleNode("/resultset");

//Console.WriteLine("keywords: {0}", keywords);

//			string [] aKeywords = keywords.Split(";"[0]);

			XmlElement xmlEngine = doc.CreateElement(engine);
			XmlElement xmlKeywords = doc.CreateElement("keywords");
Console.WriteLine("PreForeach");
	        foreach (string s in keywords) 
 	     	{
 	     	Console.WriteLine("s: {0}", s);
 	     		string [] keypos = s.Split(":"[0]);
 	     	
 	     		XmlElement xmlKeyword = doc.CreateElement("keyword");
 	     		xmlKeyword.SetAttribute("name",keypos[0]);
 	     		xmlKeyword.InnerXml = keypos[1];		
				xmlKeywords.AppendChild(xmlKeyword);
				xmlKeyword = null;
				keypos = null;
        	}
Console.WriteLine("PostForeach");
			xmlEngine.AppendChild(xmlKeywords);
			
			XmlElement xmlBacklinks = doc.CreateElement("backlinks");
 	     	xmlBacklinks.InnerXml = backlinks;
								
			xmlEngine.AppendChild(xmlBacklinks);
	   		
	   		node.AppendChild(xmlEngine);
	   		
//	   		Console.WriteLine("xml: {0}",doc.ToString());
	   		
//	   		doc.Save("test.xml");
//	   		this.SaveConfiguration(doc);
	   			   		
			xmlEngine = null;
			xmlKeywords = null;
			xmlBacklinks = null;	   		
	   		
	   		return doc;
		}
		/*
		public string CreateResults(string  url, string engine, StringCollection keywords, string sitename)
		{
			Console.WriteLine("Number of keywords:" + keywords.Count.ToString());
			StringBuilder sbResult = new StringBuilder("<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?>"+ System.Environment.NewLine);
			sbResult.Append("<?xml-stylesheet type=\"text/xslt\" href=\"" +ranker.lib.libConfig.GetConfigPath() +  Path.DirectorySeparatorChar + "result.xsl\"?>");
			sbResult.Append("<resultset>"+ System.Environment.NewLine);
			sbResult.Append("<url>"+ System.Environment.NewLine);
        	sbResult.Append("" + url + System.Environment.NewLine);
        	sbResult.Append("</url>"+ System.Environment.NewLine);
        	sbResult.Append("<"+ engine +">"+ System.Environment.NewLine);
			sbResult.Append("<keywords>"+ System.Environment.NewLine);
			int position ;
			
        	for (int i=0;i<keywords.Count;i++) {

        		Console.WriteLine("Querying for: " +keywords[i]); 
        		position = this.GetPosition(keywords[i],url)	;
        		sbResult.Append("<keyword name=\"" + keywords[i] + "\">" + position + "</keyword>" + System.Environment.NewLine);
        		Console.WriteLine("done Querying for: " +keywords[i]); 
        		Console.WriteLine("###############");
        	}
        	Console.WriteLine("Getting number of backlinks");
        	sbResult.Append("</keywords>"+ System.Environment.NewLine);
        	sbResult.Append("<backlinks>"+ System.Environment.NewLine);
        	int bl = this.GetBackLinks(url);
        	sbResult.Append("" + bl.ToString() + System.Environment.NewLine);
        	sbResult.Append("</backlinks>"+ System.Environment.NewLine);
        	sbResult.Append("</"+ engine +">"+ System.Environment.NewLine);
        	sbResult.Append("</resultset>");
        	libResults lr = new libResults();
        	string resultUrl = lr.SaveResults(sbResult.ToString(),sitename);
			resultUrl = lr.generateHtml(resultUrl);
        	sbResult = null;
        	Console.WriteLine("Resultados: {0}",resultUrl);
        	return resultUrl;
		}
		*/
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
