using System.Collections.Generic;
using Stocks.Crawl.Models;

namespace Stocks.Crawl
{
    public interface IZseApi
    {
        List<DionickoDrustvo> GetAllDionickaDrustva();

        List<ZseDnevnoTrgovanjeDanas> GetDnevnoTrgovanjeDanas();

        DionickoDrustvo GetDionickoDrustvo(int id);
    }
}