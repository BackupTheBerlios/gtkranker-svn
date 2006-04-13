// created on 09/04/2006 at 20:16
using System;
using System.IO;
using ranker;
using ranker.lib;
using ranker.lib.YahooAPI;
using System.Collections.Specialized;
using System.Text;
using System.Xml;


namespace ranker.lib {

	public class libYahooAPIQuery {

		string yahooKey;
		
		public libYahooAPIQuery()
		{
			ranker.lib.libConfig lc = new ranker.lib.libConfig(); 
			yahooKey = lc.GetGoogleKey();
			lc = null;
		}
		public int GetPosition(string keyword, string url)
		{
			yahooKey = "YahooExample";
			YahooAPI.YahooSearchService ya = new YahooAPI.YahooSearchService();
			int position = -1;
			int i = 0;
			int maxposition = 100;
			try {
			
				for	(int j=0;j<maxposition;j=j+10){

				YahooAPI.WebSearchResponse.ResultSet resultSet = ya.WebSearch (yahooKey, keyword,"all", 10, 1, "any", true, true, "en");
								
								
				foreach (YahooAPI.WebSearchResponse.ResultType result in resultSet.Result)
				{	
					Console.WriteLine("URL: {0}", result.Url);
					if (result.Url.IndexOf(url) >=0 ) {

						position = j+i+1;
						Console.WriteLine("We are rocking at number " + position);
						break;
					}
				
					i++;
				}
					if (position > 0) break;
				}
				}
			
			catch (System.Web.Services.Protocols.SoapException ex)  {

				Console.Write("Error al obtener la posicion: {0}",ex.Message);
			} 
			
//			gs = null;
			return position;
		}
		
		public string GetBackLinks(string url)
		{
			yahooKey = "YahooExample";
			YahooAPI.YahooSearchService ya = new YahooAPI.YahooSearchService();
			string backlinks ="";
			try {

				YahooAPI.InlinkDataResponse.ResultSet r = ya.InlinkData(yahooKey,url,1, 1);
				// find out if we are in these results
				backlinks = r.totalResultsAvailable;
				
		
			}
			catch (System.Web.Services.Protocols.SoapException ex)  {

				Console.Write("Error al obtener los enlaces: {0}",ex.Message);
			} 
			
			ya = null;
			return backlinks;
		}
		
		public XmlDocument ProcessSite(string  url, StringCollection keywords, string sitename, XmlDocument doc)
		{
			Console.WriteLine("Number of keywords (YahooAPI):" + keywords.Count.ToString());
			int position ;
			
			string[] keys = new string[keywords.Count];
			
        	for (int i=0;i<keywords.Count;i++) {

        		Console.WriteLine("Querying for: " +keywords[i]); 
        		position = this.GetPosition(keywords[i],url)	;
        		keys[i]= keywords[i]+":"+position;
//        		sbResult.Append("<keyword name=\"" + keywords[i] + "\">" + position + "</keyword>" + System.Environment.NewLine);
        		Console.WriteLine("done Querying for: " +keywords[i]); 
        		Console.WriteLine("###############");
        	}
        	Console.WriteLine("Getting number of yahoo backlinks");
//        	sbResult.Append("</keywords>"+ System.Environment.NewLine);
//        	sbResult.Append("<backlinks>"+ System.Environment.NewLine);
        	string bl = this.GetBackLinks(url);
//        	sbResult.Append("" + bl.ToString() + System.Environment.NewLine);
//        	sbResult.Append("</backlinks>"+ System.Environment.NewLine);
//        	sbResult.Append("</resultset>");
//        	libResults lr = new libResults();
//        	string resultUrl = lr.SaveResults(sbResult.ToString(),sitename);
//			resultUrl = lr.generateHtml(resultUrl);
//        	sbResult = null;
Console.WriteLine("Yahoo Backlinks: {0}",bl);
		    libResults lr = new libResults();
        	doc = lr.AddEngine("yahoo",keys,bl.ToString(),doc);
Console.WriteLine("AddEngine");
        	return doc;
		}
	}
}
