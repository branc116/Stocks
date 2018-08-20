using System;
using System.Collections.Generic;

namespace Stocks.Data.Models.Zse
{
    public partial class DionickoDrustvo
    {
        public DionickoDrustvo()
        {
            DionicarDionickoDrustvo = new HashSet<DionicarDionickoDrustvo>();
            DionickoDrustvoKategorijaLikvidnosti = new HashSet<DionickoDrustvoKategorijaLikvidnosti>();
            DnevnoTrgovanje = new HashSet<DnevnoTrgovanje>();
        }

        public int IdDd { get; set; }
        public string OznakaDd { get; set; }
        public string ImeDd { get; set; }

        public DionickoDrustvoDjelatnost DionickoDrustvoDjelatnost { get; set; }
        public ICollection<DionicarDionickoDrustvo> DionicarDionickoDrustvo { get; set; }
        public ICollection<DionickoDrustvoKategorijaLikvidnosti> DionickoDrustvoKategorijaLikvidnosti { get; set; }
        public ICollection<DnevnoTrgovanje> DnevnoTrgovanje { get; set; }
    }
}
