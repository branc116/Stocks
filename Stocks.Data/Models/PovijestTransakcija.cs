using System;
using System.Collections.Generic;

namespace Stocks.Data.Models
{
    public partial class PovijestTransakcija
    {
        public DateTime Datum { get; set; }
        public string OznakaDrustvo { get; set; }
        public decimal? Prva { get; set; }
        public decimal? Zadnja { get; set; }
        public decimal? Prosijek { get; set; }
        public int? Kolicina { get; set; }
        public int? Promet { get; set; }
        public decimal? Najniza { get; set; }
        public decimal? Najvisa { get; set; }
        public decimal? Kupnja { get; set; }
        public decimal? Prodaja { get; set; }

        public Drustvo OznakaDrustvoNavigation { get; set; }
    }
}
