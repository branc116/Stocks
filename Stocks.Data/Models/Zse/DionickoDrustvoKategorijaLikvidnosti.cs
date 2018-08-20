using System;
using System.Collections.Generic;

namespace Stocks.Data.Models.Zse
{
    public partial class DionickoDrustvoKategorijaLikvidnosti
    {
        public int IdDd { get; set; }
        public DateTime Datum { get; set; }
        public short? Vrijednost { get; set; }

        public DionickoDrustvo IdDdNavigation { get; set; }
    }
}
