// created on 05/16/2004 at 16:08
using System;
using ranker;
using ranker.lib;
using ranker.lib.GoogleService;

namespace ranker.lib
{
	public class libGoogleQuery
	{
		string googleKey;
		
		public libGoogleQuery()
		{
			ranker.lib.libConfig lc = new ranker.lib.libConfig(); 
			googleKey = lc.GetGoogleKey();
		}
		public int GetPosition(string keyword, string url)
		{
			GoogleSearchService gs = new GoogleSearchService();
			int position = -1;
			try
			{
				GoogleService.GoogleSearchResult r = gs.doGoogleSearch(googleKey, keyword,0, 10, false, "", false, "", "", "");
				// find out if we are in these results
				for (int i=0;i<r.resultElements.Length;i++)
				{
					if (r.resultElements[i].URL.IndexOf(url) >=0 )
					{
						position = i+1;
						Console.WriteLine("We are rocking at number " + position);
						break;
					}
				}	
			}
			catch (System.Web.Services.Protocols.SoapException ex) 
			{
				Console.Write(ex.Message);
			} 
			
			gs = null;
			return position;
		}
		
		public int GetBackLinks(string url)
		{
			GoogleSearchService gs = new GoogleSearchService();
			int backlinks = -1;
			try
			{
				GoogleService.GoogleSearchResult r = gs.doGoogleSearch(googleKey, "link:"+url,0, 10, false, "", false, "", "", "");
				// find out if we are in these results
				backlinks = r.estimatedTotalResultsCount;
			}
			catch (System.Web.Services.Protocols.SoapException ex) 
			{
				Console.Write(ex.Message);
			} 
			
			gs = null;
			return backlinks;
		}
	}
}