using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashFlow.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using static System.Formats.Asn1.AsnWriter;

namespace CashFlow.Infrastructure.Migrations
{
    public static class DataBaseMigration
    {
        public static async Task MigrateDatabase(this IServiceProvider serviceProvider)
        {
            var dbContext = serviceProvider.GetRequiredService<CashFlowDbContext>();

            await dbContext.Database.MigrateAsync();
        }
    }
}
