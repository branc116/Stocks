using System.Collections.Generic;

namespace Stocks.Crawl
{
    public interface IZseApi
    {
        List<DionickoDrustvo> GetAllDionickaDrustva();

        List<ZseDnevnoTrgovanjeDanas> GetDnevnoTrgovanjeDanas();

        DionickoDrustvo GetDionickoDrustvo(int id);
    }
}