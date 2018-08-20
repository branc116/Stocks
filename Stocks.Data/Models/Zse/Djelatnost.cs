using System;
using System.Collections.Generic;

namespace Stocks.Data.Models.Zse
{
    public partial class Djelatnost
    {
        public Djelatnost()
        {
            DionickoDrustvoDjelatnost = new HashSet<DionickoDrustvoDjelatnost>();
        }

        public int IdDjelatnost { get; set; }
        public string OznDjelatnost { get; set; }
        public string NazivDjelatnost { get; set; }
        public int? IdSektor { get; set; }

        public Sektor IdSektorNavigation { get; set; }
        public ICollection<DionickoDrustvoDjelatnost> DionickoDrustvoDjelatnost { get; set; }
    }
}
