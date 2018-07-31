using System;
using System.Collections.Generic;

namespace Stocks.Data.Models
{
    public partial class Dionicar
    {
        public Dionicar()
        {
            DionicarDrustvo = new HashSet<DionicarDrustvo>();
        }

        public int Rbr { get; set; }
        public string Naziv { get; set; }

        public ICollection<DionicarDrustvo> DionicarDrustvo { get; set; }
    }
}
