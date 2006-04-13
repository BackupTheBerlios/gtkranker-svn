// created on 05/16/2004 at 16:08
using System;
using System.IO;
using ranker;
using ranker.lib;
using ranker.lib.GoogleService;
using System.Collections.Specialized;
using System.Text;
using System.Xml;


namespace ranker.lib {

	public class libGoogleQuery {

		string googleKey;
		
		public libGoogleQuery()
		{
			ranker.lib.libConfig lc = new ranker.lib.libConfig(); 
			googleKey = lc.GetGoogleKey();
			lc = null;
		}
		public int GetPosition(string keyword, string url)
		{
			GoogleSearchService gs = new GoogleSearchService();
			int position = -1;
			int maxposition = 100;
			try {
			
				for	(int j=0;j<maxposition;j=j+10){

				GoogleService.GoogleSearchResult r = gs.doGoogleSearch(googleKey, keyword,j, 10, false, "", false, "", "", "");
								
				// find out if we are in these results
				for (int i=0;i<r.resultElements.Length;i++) {
			
					Console.WriteLine("r {0}",r.resultElements[i].URL.ToString());

					if (r.resultElements[i].URL.IndexOf(url) >=0 ) {

						position = j+i+1;
						Console.WriteLine("We are rocking at number " + position);
						break;
					}
				}
				if (position > 0) break;
				}
			}
			catch (System.Web.Services.Protocols.SoapException ex)  {

				Console.Write("Error al obtener la posicion: {0}",ex.Message);
			} 
			
			gs = null;
			return position;
		}
		
		public int GetBackLinks(string url)
		{
			GoogleSearchService gs = new GoogleSearchService();
			int backlinks = -1;
			try {

				GoogleService.GoogleSearchResult r = gs.doGoogleSearch(googleKey, "link:"+url,0, 1, false, "", false, "", "", "");
				// find out if we are in these results
				backlinks = r.estimatedTotalResultsCount;
				
		
			}
			catch (System.Web.Services.Protocols.SoapException ex)  {

				Console.Write("Error al obtener los enlaces: {0}",ex.Message);
			} 
			
			gs = null;
			return backlinks;
		}
		
		public XmlDocument ProcessSite(string  url, StringCollection keywords, string sitename, XmlDocument doc)
		{
			Console.WriteLine("Number of keywords:" + keywords.Count.ToString());
/*			StringBuilder sbResult = new StringBuilder("<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?>"+ System.Environment.NewLine);
			sbResult.Append("<?xml-stylesheet type=\"text/xslt\" href=\"" +ranker.lib.libConfig.GetConfigPath() +  Path.DirectorySeparatorChar + "result.xsl\"?>");
			sbResult.Append("<resultset>"+ System.Environment.NewLine);
			sbResult.Append("<url>"+ System.Environment.NewLine);
        	sbResult.Append("" + url + System.Environment.NewLine);
        	sbResult.Append("</url>"+ System.Environment.NewLine);
        	sbResult.Append("<google>"+ System.Environment.NewLine);
			sbResult.Append("<keywords>"+ System.Environment.NewLine);
*/			int position ;
			
			string[] keys = new string[keywords.Count];
	
        	for (int i=0;i<keywords.Count;i++) {

        		Console.WriteLine("Querying for: " +keywords[i]); 
        		position = this.GetPosition(keywords[i],url)	;
        		keys[i] = keywords[i]+":"+position;
//        		sbResult.Append("<keyword name=\"" + keywords[i] + "\">" + position + "</keyword>" + System.Environment.NewLine);
        		Console.WriteLine("done Querying for: " +keywords[i]); 
        		Console.WriteLine("###############");
        	}
        	Console.WriteLine("Getting number of backlinks");
//        	sbResult.Append("</keywords>"+ System.Environment.NewLine);
//        	sbResult.Append("<backlinks>"+ System.Environment.NewLine);
        	int bl = this.GetBackLinks(url);
//        	sbResult.Append("" + bl.ToString() + System.Environment.NewLine);
 //       	sbResult.Append("</backlinks>"+ System.Environment.NewLine);
//        	sbResult.Append("</google>"+ System.Environment.NewLine);
//        	sbResult.Append("</resultset>");
        	libResults lr = new libResults();
        	doc = lr.AddEngine("google",keys,bl.ToString(),doc);
//			resultUrl = lr.generateHtml(resultUrl);
//			Console.WriteLine("antes");
//			lr.AddEngine("google","pedo:1;culo:4;no se:7","1560");
//			Console.WriteLine("despues");
//        	sbResult = null;
//        	Console.WriteLine("Resultados: {0}",resultUrl);
        	return doc;
		}
	}
}
