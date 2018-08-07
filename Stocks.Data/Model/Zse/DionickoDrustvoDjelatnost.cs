using System;
using System.Collections.Generic;

namespace Stocks.Data.Model.Zse
{
    public partial class DionickoDrustvoDjelatnost
    {
        public int? IdDjelatnost { get; set; }
        public int IdDd { get; set; }

        public DionickoDrustvo IdDdNavigation { get; set; }
        public Djelatnost IdDjelatnostNavigation { get; set; }
    }
}
