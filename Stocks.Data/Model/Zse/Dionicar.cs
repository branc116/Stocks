using System;
using System.Collections.Generic;

namespace Stocks.Data.Model.Zse
{
    public partial class Dionicar
    {
        public Dionicar()
        {
            DionicarDionickoDrustvo = new HashSet<DionicarDionickoDrustvo>();
        }

        public int IdDionicar { get; set; }
        public string NazivDionicar { get; set; }

        public ICollection<DionicarDionickoDrustvo> DionicarDionickoDrustvo { get; set; }
    }
}
