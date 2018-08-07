using Microsoft.EntityFrameworkCore;
using Stocks.Crawl;
using Stocks.Data.Model.Zse;
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
            var allDD = await _zseApiAsync.GetAllDionickaDrustvaAsync();
            var data = _mapper.Map(allDD);
            foreach(var d in data)
            {
                foreach(var dionicar in d.DionicarDionickoDrustvo)
                {
                    var dion = await _zseContext.Dionicar.FirstOrDefaultAsync(i => i.NazivDionicar == dionicar.IdDionicarNavigation.NazivDionicar);
                    if (dion != null)
                    {
                        dionicar.IdDionicarNavigation = dion;
                        dionicar.IdDionicar = dion.IdDionicar;
                    }
                }
            }
            await _zseContext.AddRangeAsync(data);
        }
    }
}
