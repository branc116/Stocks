using System;
using System.Collections.Generic;

namespace Stocks.Crawl.Models
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
}