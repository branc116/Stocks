using System;
using System.Collections.Generic;

namespace Stocks.Data.Models.Zse
{
    public partial class DnevnoTrgovanje
    {
        public DateTime Datum { get; set; }
        public int IdDd { get; set; }
        public decimal? Prva { get; set; }
        public decimal? Najniza { get; set; }
        public decimal? Najvisa { get; set; }
        public decimal? Zadnja { get; set; }
        public decimal? Prosijek { get; set; }
        public decimal? Promijena { get; set; }
        public int? BrojTransakcija { get; set; }
        public int? Promet { get; set; }

        public DionickoDrustvo IdDdNavigation { get; set; }
    }
}
