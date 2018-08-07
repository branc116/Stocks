using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using CsQuery;
using Stocks.Crawl.Models;

namespace Stocks.Crawl
{
    public class Crawler : IZseApi
    {
        private static readonly HttpClient Client = new HttpClient();

        public Crawler()
        {
            Client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent",
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.84 Safari/537.36");
        }

        public List<DionickoDrustvo> GetAllDionickaDrustva()
        {
            var requestUrl = $"http://www.zse.hr/default.aspx?id=26474";
            CQ html = Client.GetAsync(requestUrl).Result.Content.ReadAsStringAsync().Result;
            var id = html["#dnevna_trgovanja > tbody > tr > td > strong > a"].Elements.ToArray();
            return id.Select(i => GetDionickoDrustvo(int.Parse(i.Attributes["href"].Split("=").Last()))).ToList();
        }

        public List<ZseDnevnoTrgovanjeDanas> GetDnevnoTrgovanjeDanas()
        {
            var requestUrl = "http://www.zse.hr/default.aspx?id=26521";
            CQ html = Client.GetAsync(requestUrl).Result.Content.ReadAsStringAsync().Result;
            var rows = html["#dnevna_trgovanja > tbody > tr[tip='D']"].Elements.ToArray();
            return rows.Select(i =>
            {
                var columns = i.ChildElements.ToList();
                return new ZseDnevnoTrgovanjeDanas
                {
                    Datum = DateTime.Now,
                    Simbol = columns[0].FirstElementChild.InnerText,
                    Zakljucna = columns[2].FormatDioniceNumbers(),
                    Zadnja = columns[3].FormatDioniceNumbers().GetValueOrDefault(0),
                    Promjena = columns[4].FormatDioniceNumbers().GetValueOrDefault(0),
                    Prva = columns[5].FormatDioniceNumbers(),
                    Najvisa = columns[6].FormatDioniceNumbers(),
                    Najniza = columns[7].FormatDioniceNumbers(),
                    Prosjecna = columns[8].FormatDioniceNumbers(),
                    Kolicina = (int)columns[9].FormatDioniceNumbers().GetValueOrDefault(0),
                    Promet = (int)columns[10].FormatDioniceNumbers().GetValueOrDefault(0)
                };
            }).ToList();
        }

        public DionickoDrustvo GetDionickoDrustvo(int id)
        {
            var requestUrl = $"http://www.zse.hr/default.aspx?id=10006&dionica={id}";
            CQ html = Client.PostAsync(requestUrl,
                    new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("reporttype",
                            "security"),
                        new KeyValuePair<string, string>("DateFrom",
                            "01.01.2008"),
                        new KeyValuePair<string, string>("DateTo",
                            DateTime.Now.ToShortDateString()
                                .TrimEnd('.'))
                    }))
                .Result.Content.ReadAsStringAsync()
                .Result;
            var oznaka = html["#t10007 > div > table:nth-child(1) > tbody > tr > td.c1 > h3"].FirstElement().InnerText;
            var ime = html["#t10007 > div > table:nth-child(1) > tbody > tr > td.c1"].FirstElement()
                .InnerHTML.Split("<br />").ElementAt(1);
            var brojDionica = (int)html["#No_Of_Shares_Issued"].FirstElement().FormatDioniceNumbers().GetValueOrDefault(0);
            var djelatnost = html["#t10007 > div > table:nth-child(1) > tbody > tr > td.c1 > span > a"].FirstElement()
                .InnerText;
            var sektor = djelatnost.Split(" ").First();
            var nazivDjelatnost = html["#t10007 > div > table:nth-child(1) > tbody > tr > td.c1 > span"].Attr("title");
            var likvidnost =
                int.Parse(html["#t10007 > div > table:nth-child(4) > tbody > tr > td:nth-child(1) > table.dioniceSheet1 > tbody > tr:nth-child(1) > td.c2"]
                    .FirstElement().InnerText);
            var razred = int.Parse(html["#t10007 > div > table:nth-child(4) > tbody > tr > td:nth-child(1) > table.dioniceSheet1 > tbody > tr:nth-child(2) > td.c2 > a"].FirstElement().InnerText);
            var dionicari = html["#t49183 > div > table > tbody > tr"].Elements.Select(i => new ZseDionicar
            {
                Ime = i.ChildElements.ElementAt(1).InnerText,
                Postotak = double.Parse(i.ChildElements.ElementAt(2).InnerText)
            }).ToList();
            var povijest = html["#dnevna_trgovanja > tbody > tr"].Elements.Select(i =>
                {
                    var listi = i.ChildElements.ToList();
                    try
                    {
                        var dateTime = DateTime.Parse(listi[1].InnerText);
                        var formatDioniceNumbers = listi[2].FormatDioniceNumbers();
                        var dioniceNumbers = listi[3].FormatDioniceNumbers();
                        var numbers = listi[4].FormatDioniceNumbers();
                        var valueOrDefault = listi[5].FormatDioniceNumbers().GetValueOrDefault(0);
                        var prosjecna = listi[6].FormatDioniceNumbers();
                        var orDefault = listi[7].FormatDioniceNumbers().GetValueOrDefault(0);
                        var brojTransakcija = (int)listi[8].FormatDioniceNumbers().GetValueOrDefault(0);
                        var kolicina = (int?)listi[9].FormatDioniceNumbers().GetValueOrDefault(0);
                        var promet = (int?)listi[10].FormatDioniceNumbers().GetValueOrDefault(0);
                        return new ZseDnevnoTrgovanje
                        {
                            Datum = dateTime,
                            Prva = formatDioniceNumbers,
                            Najvisa = dioniceNumbers,
                            Najniza = numbers,
                            Zadnja = valueOrDefault,
                            Prosjecna = prosjecna,
                            Promjena = orDefault,
                            BrojTransakcija =
                                            brojTransakcija,
                            Kolicina = kolicina,
                            Promet = promet
                        };
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                })
                .ToList();

            return new DionickoDrustvo
            {
                Ime = ime,
                BrojDionica = brojDionica,
                DatumUpita = DateTime.Now,
                Dionicars = dionicari,
                Djelatnost = djelatnost,
                DnevnaTrgovanja = povijest,
                Id = id,
                Likvidnost = likvidnost,
                NazivDjelatnost = nazivDjelatnost,
                Oznaka = oznaka,
                Sektor = sektor,
                Razred = razred,
            };
        }
    }
}