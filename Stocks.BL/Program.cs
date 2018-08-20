using Stocks.Crawl;
using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace Stocks.BL
{
    class Program
    {
        static async Task Main(string[] args)
        {
            ServicePointManager.DefaultConnectionLimit = 16;
            var options = new Microsoft.EntityFrameworkCore.DbContextOptions<Data.Models.Zse.ZseContext>
            {

            };
            var context = new Data.Models.Zse.ZseContext();

            var repo = new ZseRepository(new CrawlerAsync(), new Mapper(), context);
            await repo.SeedAsync();
        }

    }
}
