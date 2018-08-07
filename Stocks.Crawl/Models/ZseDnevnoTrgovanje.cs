using System;

namespace Stocks.Crawl.Models
{
    public class ZseDnevnoTrgovanje
    {
        public DateTime Datum { get; set; }
        public double? Prva { get; set; }
        public double Zadnja { get; set; }
        public double? Prosjecna { get; set; }
        public double Promjena { get; set; }
        public int? Kolicina { get; set; }
        public int? Promet { get; set; }
        public double? Najniza { get; set; }
        public double? Najvisa { get; set; }
        public int BrojTransakcija { get; set; }
    }
}