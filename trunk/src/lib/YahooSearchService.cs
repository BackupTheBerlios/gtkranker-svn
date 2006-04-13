using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Xml.Serialization;

namespace ranker.lib.YahooAPI
{
	/// <summary>
	/// YahooSearchService wrappes calls to the Yahoo Web Service.
	/// </summary>
	public class YahooSearchService
	{
		
		public ranker.lib.YahooAPI.WebSearchResponse.ResultSet WebSearch(string appId, string query, string type, short results, int start, string format, bool adultOk, bool similarOk, string language)
		{
			string requestUri = String.Format("http://api.search.yahoo.com/WebSearchService/V1/webSearch?appid={0}&query={1}&type={2}&results={3}&start={4}&format={5}&adult_ok={6}&similar_ok={7}&language={8}", appId, query , type, results, start, format, adultOk ? "1" : "0", similarOk ? "1" : "0", language);

			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUri);

			ranker.lib.YahooAPI.WebSearchResponse.ResultSet resultSet = null;
			using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
			{
				using (Stream responseStream = response.GetResponseStream())
				{
					XmlSerializer serializer = new XmlSerializer(typeof(ranker.lib.YahooAPI.WebSearchResponse.ResultSet));
					resultSet = (ranker.lib.YahooAPI.WebSearchResponse.ResultSet)serializer.Deserialize(responseStream);
				}
			}

			return resultSet;
		}
		
		public ranker.lib.YahooAPI.InlinkDataResponse.ResultSet InlinkData(string appId, string query, short results, int start)
		{
			string requestUri = String.Format("http://api.search.yahoo.com/SiteExplorerService/V1/inlinkData?appid={0}&query={1}&results={2}&start={3}", appId, query ,results, start);

			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUri);

			ranker.lib.YahooAPI.InlinkDataResponse.ResultSet resultSet = null;
			using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
			{
				using (Stream responseStream = response.GetResponseStream())
				{
					XmlSerializer serializer = new XmlSerializer(typeof(ranker.lib.YahooAPI.InlinkDataResponse.ResultSet));
					resultSet = (ranker.lib.YahooAPI.InlinkDataResponse.ResultSet)serializer.Deserialize(responseStream);
				}
			}

			return resultSet;
		}
		

	}
}
