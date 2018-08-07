using System;
using System.Collections.Generic;

namespace Stocks.Data.Model.Zse
{
    public partial class Sektor
    {
        public Sektor()
        {
            Djelatnost = new HashSet<Djelatnost>();
        }

        public int IdSektor { get; set; }
        public string OznSektor { get; set; }
        public string NazivSektor { get; set; }

        public ICollection<Djelatnost> Djelatnost { get; set; }
    }
}
