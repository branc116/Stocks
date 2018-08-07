using Stocks.Crawl;
using System;
using System.Threading.Tasks;

namespace Stocks.BL
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var context = new ZseRepository(new ZseApiAsync(), new Mapper(), new Data.Model.Zse.ZseContext());
            await context.SeedAsync();
        }

    }
}
