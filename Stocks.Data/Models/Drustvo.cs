using System;
using System.Collections.Generic;

namespace Stocks.Data.Models
{
    public partial class Drustvo
    {
        public Drustvo()
        {
            DionicarDrustvo = new HashSet<DionicarDrustvo>();
            PovijestTransakcija = new HashSet<PovijestTransakcija>();
        }

        public string Oznaka { get; set; }
        public string Naziv { get; set; }
        public string MojedioniceUrl { get; set; }

        public ICollection<DionicarDrustvo> DionicarDrustvo { get; set; }
        public ICollection<PovijestTransakcija> PovijestTransakcija { get; set; }
    }
}
