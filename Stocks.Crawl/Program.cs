using CsQuery;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Stocks.Crawl
{
    public static class Program
    {
        private const string RequestUri = "https://www.mojedionice.com/dionice";
        private static readonly HttpClient Client = new HttpClient();

        public static async Task Main(string[] args)
        {
            var s = await GetDnevnoTrgovanje();
            Console.ReadLine();
        }

        public static async Task<List<string>> GetAllStocks()
        {
            var html = await Client.GetAsync(RequestUri);
            CQ dom = await html.Content.ReadAsStringAsync();
            var table = dom.Select(".tableBr > tbody > tr").Elements;

            var rows = table.ToList();
            if (!rows.Any()) return null;
            var dateStockNumber = new CQ(rows[0].Cq().RenderSelection())["th"].Elements.ElementAt(3).InnerText.Remove(0, 15);
            rows.RemoveRange(0, 2);
            var retList = new List<string>();
            int i = 0;
            foreach (var row in rows)
            {
                var columns = new CQ(row.Cq().RenderSelection())["a"].Elements.ToArray();
                var oznaka = columns[0].Attributes["href"];
                var dionicaHtml = await Client.GetAsync($"https://mojedionice.com/{oznaka}");
                using (var file = File.Open(Environment.CurrentDirectory + "\\" + i++, FileMode.OpenOrCreate))
                {
                    await (await dionicaHtml.Content.ReadAsStreamAsync()).CopyToAsync(file);
                }
            }

            return retList;
        }

        public static async Task<DnevnoTrgovanje> GetDnevnoTrgovanje()
        {
            CQ dom;
            if (!File.Exists("dnevnoTrg.txt"))
            {
                var html = await Client.GetAsync(
                    "https://www.mojedionice.com/trg/IzvDnevnoTrg.aspx?idF=39&idDrz=1&SortEx=PromjenaZadCalc&SortDir=sil");
                dom = await html.Content.ReadAsStringAsync();
            }
            else
            {
                dom = await File.ReadAllTextAsync("dnevnoTrg.txt");
            }

            var rows = dom[".tableBr > tbody > tr"].Elements.ToList();
            rows.RemoveRange(0, 2);
            rows.RemoveRange(rows.Count - 1, 1);

            var drustva = rows.Select(row =>
            {
                var columns = new CQ(row.Cq().RenderSelection())["td,a"].Elements.ToArray();
                try
                {
                    return new DnevnoTrgovanjeDrustvo
                    {
                        DrustvoOznaka = columns[2].InnerText,
                        Prva = columns[3].FormatDioniceNumbers(),
                        Zadnja = columns[4].FormatDioniceNumbers().Value,
                        Prosjecna = columns[5].FormatDioniceNumbers(),
                        PromjenaZadnje = columns[6].FormatDioniceNumbers().Value,
                        PromjenaProsjecne = columns[7].FormatDioniceNumbers(),
                        Kolicina = (int?)columns[8].FormatDioniceNumbers(),
                        Promet = (int?)columns[9].FormatDioniceNumbers(),
                        Najniza = columns[10].FormatDioniceNumbers(),
                        Najvisa = columns[11].FormatDioniceNumbers(),
                        Kupnja = columns[12].FormatDioniceNumbers(),
                        Prodaja = columns[13].FormatDioniceNumbers()
                    };
                }
                catch (Exception ex)
                {
                    Console.WriteLine(columns[6][0].InnerText);
                    Console.WriteLine(ex);
                    Console.WriteLine(row.InnerHTML);
                    throw;
                }
            }).ToArray();

            return new DnevnoTrgovanje
            {
                Drustvo = drustva,
                Optimizam = 100
            };
        }

        public static double? FormatDioniceNumbers(this IDomElement dom)
        {
            var s = (dom.InnerText != string.Empty ? dom.InnerText : dom[0].InnerText).Replace(".", "").Replace("%", "");
            return s == "&nbsp;" ? null : (double?)double.Parse(s);
        }
    }

    public class DnevnoTrgovanje
    {
        public DnevnoTrgovanjeDrustvo[] Drustvo { get; set; }
        public int Optimizam { get; set; }
        public DateTime Date { get; set; }
    }

    public class DnevnoTrgovanjeDrustvo
    {
        public string DrustvoOznaka { get; set; }
        public double? Prva { get; set; }
        public double Zadnja { get; set; }
        public double? Prosjecna { get; set; }
        public double PromjenaZadnje { get; set; }
        public double? PromjenaProsjecne { get; set; }
        public int? Kolicina { get; set; }
        public int? Promet { get; set; }
        public double? Najniza { get; set; }
        public double? Najvisa { get; set; }
        public double? Kupnja { get; set; }
        public double? Prodaja { get; set; }
    }

    public class Drustvo
    {
        public string Ime { get; set; }
        public string Oznaka { get; set; }
        public string Href { get; set; }
        public List<Dionicar> Dionicari { get; set; }
    }

    public class Dionicar
    {
        public string Name { get; set; }
    }
}