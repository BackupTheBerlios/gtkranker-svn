// created on 05/16/2004 at 16:08
using System;
using ranker;
using ranker.lib.GoogleService;

namespace ranker.lib
{
	public class libGoogleQuery
	{
		public int GetPosition(string keyword, string url)
		{
			GoogleSearchService gs = new GoogleSearchService();
			try
			{
				GoogleService.GoogleSearchResult r = gs.doGoogleSearch("", keyword,0, 1, false, "", false, "", "", "");
				Console.WriteLine("Estimated results:" + r.estimatedTotalResultsCount.ToString());
				Console.WriteLine("Search Time" + r.searchTime.ToString());
			}
			catch (System.Web.Services.Protocols.SoapException ex) 
			{
				Console.Write(ex.Message);
			} 
			gs = null;
			return 0;
		}
	}
}