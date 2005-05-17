// created on 05/16/2004 at 16:08
using System;
using System.IO;
using ranker;
using ranker.lib;
using ranker.lib.GoogleService;
using System.Collections.Specialized;
using System.Text;
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
			try {

				GoogleService.GoogleSearchResult r = gs.doGoogleSearch(googleKey, keyword,0, 10, false, "", false, "", "", "");
				// find out if we are in these results
				for (int i=0;i<r.resultElements.Length;i++) {

					if (r.resultElements[i].URL.IndexOf(url) >=0 ) {

						position = i+1;
						Console.WriteLine("We are rocking at number " + position);
						break;
					}
				}	
			}
			catch (System.Web.Services.Protocols.SoapException ex)  {

				Console.Write(ex.Message);
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

				Console.Write(ex.Message);
			} 
			
			gs = null;
			return backlinks;
		}
		
		public string ProcessSite(string  url, StringCollection keywords, string sitename)
		{
			Console.WriteLine("Number of keywords:" + keywords.Count.ToString());
			StringBuilder sbResult = new StringBuilder("<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?>"+ System.Environment.NewLine);
			sbResult.Append("<?xml-stylesheet type=\"text/xslt\" href=\"" +ranker.lib.libConfig.GetConfigPath() +  Path.DirectorySeparatorChar + "result.xsl\"?>");
			sbResult.Append("<resultset>"+ System.Environment.NewLine);
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
        	sbResult.Append("</resultset>");
        	libResults lr = new libResults();
        	string resultUrl = lr.SaveResults(sbResult.ToString(),sitename);		
			resultUrl = lr.generateHtml(resultUrl);
        	sbResult = null;
        	return resultUrl;
		}
	}
}
