using Microsoft.EntityFrameworkCore;
using Stocks.Crawl;
using Stocks.Data.Models.Zse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stocks.BL
{
    public class ZseRepository
    {

        readonly IZseApiAsync _zseApi;
        private readonly ZseContext _zseContext;
        readonly Mapper _mapper;
        public ZseRepository(IZseApiAsync zseApi, Mapper mapper, ZseContext zseContext)
        {
            _mapper = mapper;
            _zseApi = zseApi;
            _zseContext = zseContext;
        }
        public async Task SeedAsync()
        {
            var ids = await _zseApi.GetAlldionickaDrustvaUrl();
            foreach(var id in ids)
            {
                try
                {
                    if (_zseContext.DionickoDrustvo.Any(i => i.IdDd == int.Parse(id)))
                        continue;
                    var allDD = await _zseApi.GetDionickoDrustvo(int.Parse(id));
                    var data = _mapper.Map(allDD);
                    Console.WriteLine(allDD.Ime);
                    foreach (var dionicar in data.DionicarDionickoDrustvo)
                    {
                        var dion = _zseContext.Dionicar.FirstOrDefault(j => j.NazivDionicar == dionicar.IdDionicarNavigation.NazivDionicar);
                        if (dion != null)
                        {
                            dionicar.IdDionicarNavigation = dion;
                            dionicar.IdDionicar = dion.IdDionicar;
                        }
                    }
                    await _zseContext.AddAsync(data);
                    await _zseContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
            //await Task.WhenAll(ids.Select(async i => {
            //    var allDD = await _zseApi.GetDionickoDrustvo(int.Parse(i));
            //    var data = _mapper.Map(allDD);
            //    foreach (var dionicar in data.DionicarDionickoDrustvo)
            //    {
            //        var dion = await _zseContext.Dionicar.FirstOrDefaultAsync(j => j.NazivDionicar == dionicar.IdDionicarNavigation.NazivDionicar);
            //        if (dion != null)
            //        {
            //            dionicar.IdDionicarNavigation = dion;
            //            dionicar.IdDionicar = dion.IdDionicar;
            //        }
            //    }
            //    await _zseContext.AddAsync(data);
            //}));
        }
    }
}
