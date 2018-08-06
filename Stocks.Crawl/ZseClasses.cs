using System;
using System.Collections.Generic;

namespace Stocks.Crawl
{
    public class DionickoDrustvo
    {
        public string Ime { get; set; }
        public string Oznaka { get; set; }
        public int Id { get; set; }
        public int BrojDionica { get; set; }
        public string Sektor { get; set; }
        public string Djelatnost { get; set; }
        public string NazivDjelatnost { get; set; }
        public int Likvidnost { get; set; }
        public int Razred { get; set; }
        public List<ZseDionicar> Dionicars { get; set; }
        public List<ZseDnevnoTrgovanje> DnevnaTrgovanja { get; set; }
        public DateTime DatumUpita { get; set; }
    }

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

    public class ZseDnevnoTrgovanjeDanas
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
        public string Simbol { get; set; }
    }

    public class ZseDionicar
    {
        public string Ime { get; set; }
        public int Postotak { get; set; }
    }
}