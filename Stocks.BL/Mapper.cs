using Stocks.Data.Models.Zse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stocks.BL
{
    public class Mapper
    {
        public List<DionickoDrustvo> Map(List<Crawl.Models.DionickoDrustvo> dionickoDrustvos)
        {
            return dionickoDrustvos.Select(Map).ToList();
        }
        public DionickoDrustvo Map(Crawl.Models.DionickoDrustvo drustvo)
        {
            return new DionickoDrustvo
            {
                DionicarDionickoDrustvo = drustvo.Dionicars.Select(j => new DionicarDionickoDrustvo
                {
                    Datum = drustvo.DatumUpita,
                    IdDd = drustvo.Id,
                    IdDionicar = 0,
                    PostoDionica = (decimal)j.Postotak,
                    IdDionicarNavigation = new Dionicar
                    {
                        NazivDionicar = j.Ime
                    }
                }).ToList(),
                DionickoDrustvoKategorijaLikvidnosti = new List<DionickoDrustvoKategorijaLikvidnosti>
                 {
                     new DionickoDrustvoKategorijaLikvidnosti
                     {
                         Datum = drustvo.DatumUpita,
                         IdDd = drustvo.Id,
                         Vrijednost = (short)drustvo.Likvidnost
                     }
                 },
                IdDd = drustvo.Id,
                DionickoDrustvoDjelatnost = new DionickoDrustvoDjelatnost
                {
                    IdDd = drustvo.Id,
                    IdDjelatnostNavigation = new Djelatnost
                    {
                        IdSektorNavigation = new Sektor
                        {
                            OznSektor = drustvo.Sektor
                        },
                        NazivDjelatnost = drustvo.NazivDjelatnost,
                        OznDjelatnost = drustvo.Djelatnost
                    }
                },
                DnevnoTrgovanje = drustvo.DnevnaTrgovanja.Distinct(new Crawl.Models.ZseDnevnoTrgovanjeComparer()).Select(j => new DnevnoTrgovanje
                {
                    BrojTransakcija = j.BrojTransakcija,
                    Datum = j.Datum,
                    IdDd = drustvo.Id,
                    Najniza = (decimal?)j.Najniza,
                    Najvisa = (decimal?)j.Najvisa,
                    Promet = j.Promet,
                    Promijena = (decimal?)j.Promjena,
                    Prosijek = (decimal?)j.Prosjecna,
                    Prva = (decimal?)j.Prva,
                    Zadnja = (decimal?)j.Zadnja
                }).ToList(),
                ImeDd = drustvo.Ime,
                OznakaDd = drustvo.Oznaka
            };
        }
    }

}
