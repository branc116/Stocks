using System;
using System.Collections.Generic;

namespace Stocks.Data.Models
{
    public partial class DionicarDrustvo
    {
        public int RbrDionicar { get; set; }
        public string OznakaDrustvo { get; set; }
        public DateTime Datum { get; set; }
        public int? BrojDionica { get; set; }

        public Drustvo OznakaDrustvoNavigation { get; set; }
        public Dionicar RbrDionicarNavigation { get; set; }
    }
}
