using Stocks.Data.Model.Zse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stocks.BL
{
    public class Mapper
    {
        public List<DionickoDrustvo> Map(List<Crawl.DionickoDrustvo> dionickoDrustvos)
        {
            return dionickoDrustvos.Select(i => new DionickoDrustvo
            {
                DionicarDionickoDrustvo = i.Dionicars.Select(j => new DionicarDionickoDrustvo
                {
                    Datum = i.DatumUpita,
                    IdDd = i.Id,
                    IdDionicar = 0,
                    PostoDionica = (decimal)j.Postotak
                }).ToList(),
                DionickoDrustvoKategorijaLikvidnosti = new List<DionickoDrustvoKategorijaLikvidnosti>
                 {
                     new DionickoDrustvoKategorijaLikvidnosti
                     {
                         Datum = i.DatumUpita,
                         IdDd = i.Id,
                         Vrijednost = (short)i.Likvidnost
                     }
                 },
                IdDd = i.Id,
                DionickoDrustvoDjelatnost = new DionickoDrustvoDjelatnost
                {
                    IdDd = i.Id,
                    IdDjelatnostNavigation = new Djelatnost
                    {
                        IdSektorNavigation = new Sektor
                        {
                            OznSektor = i.Sektor
                        },
                        NazivDjelatnost = i.NazivDjelatnost,
                        OznDjelatnost = i.Djelatnost
                    }
                },
                DnevnoTrgovanje = i.DnevnaTrgovanja.Select(j => new DnevnoTrgovanje
                {
                    BrojTransakcija = j.BrojTransakcija,
                    Datum = j.Datum,
                    IdDd = i.Id,
                    Najniza = (decimal?)j.Najniza,
                    Najvisa = (decimal?)j.Najvisa,
                    Promet = j.Promet,
                    Promijena = (decimal?)j.Promjena,
                    Prosijek = (decimal?)j.Prosjecna,
                    Prva = (decimal?)j.Prva,
                    Zadnja = (decimal?)j.Zadnja
                }).ToList(),
                ImeDd = i.Ime,
                OznakaDd = i.Oznaka
            }).ToList();
        }
    }
}
