using System.Collections.Generic;
using System.Threading.Tasks;
using Stocks.Crawl.Models;

namespace Stocks.Crawl
{
    public interface IZseApiAsync
    {
        Task<List<DionickoDrustvo>> GetAllDionickaDrustva();

        Task<List<ZseDnevnoTrgovanjeDanas>> GetDnevnoTrgovanjeDanas();

        Task<DionickoDrustvo> GetDionickoDrustvo(int id);
    }
}