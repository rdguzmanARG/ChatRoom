using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ChatRoom.Handlers
{
    public static class QuotesParsing
    {
        public static string GetQuote(string stockCode)
        {
            try
            {
                // TODO: Add this url in the configuration.
                string url = string.Format("https://stooq.com/q/l/?s={0}&f=sd2t2ohlcv&h&e=csv", stockCode);
                string[] results = GetCSV(url).Replace("\n", "").Split("\r");
                int index = 0;
                string[] cols = results[0].Split(",");
                for (int i = 0; i < cols.Count(); i++)
                {
                    if (cols[i] == "Close")
                    {
                        index = i;
                        break;
                    }
                }
                string value = results[1].Split(",")[index];
                return string.Format("{0} quote is ${1} per share.", stockCode, value);
            }
            catch
            {
                return "There was an error trying to get the Stock quotes.";
            }
       
        }

        public static string GetCSV(string url)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

            StreamReader sr = new StreamReader(resp.GetResponseStream());
            string results = sr.ReadToEnd();
            sr.Close();

            return results;
        }

    }
}
