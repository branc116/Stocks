using CsQuery;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Stocks.Crawl
{
    public static class Program
    {
        private const string RequestUri = "https://www.mojedionice.com/dionice";

        public static async Task Main(string[] args)
        {
            var a = new HttpClient();
            var b = await a.GetAsync(RequestUri);
            CQ c = await b.Content.ReadAsStringAsync();
            var d = c["#ctl00_ContentPlaceHolder1_gv"];
            var e = d["tr"];
            foreach(var f in e.Elements)
            {
                CQ g = f.Cq();


                var h = g["td"];
                foreach (var i in h.Elements)
                {
                    Console.WriteLine(i.InnerHTML);
                }
                Console.WriteLine();
            }
        }
    }
}
