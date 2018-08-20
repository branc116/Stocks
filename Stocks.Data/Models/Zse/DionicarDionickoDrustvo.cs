using System;
using System.Collections.Generic;

namespace Stocks.Data.Models.Zse
{
    public partial class DionicarDionickoDrustvo
    {
        public int IdDionicar { get; set; }
        public int IdDd { get; set; }
        public DateTime Datum { get; set; }
        public decimal? PostoDionica { get; set; }

        public DionickoDrustvo IdDdNavigation { get; set; }
        public Dionicar IdDionicarNavigation { get; set; }
    }
}
