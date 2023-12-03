using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infrastructure.Interfaces
{

    public class CtxUserstFactory : IDesignTimeDbContextFactory<CtxUsers>
    {
        public CtxUserstFactory()
        {
            // A parameter-less constructor is required by the EF Core CLI tools.
        }

        public CtxUsers CreateDbContext(string[] args)
        {
            var connectionString = Environment.GetEnvironmentVariable("EF_DBUSERS");
            if (string.IsNullOrEmpty(connectionString))
                throw new InvalidOperationException("The connection string was not set " +
                "in the 'EFCORETOOLSDB' environment variable.");

            //var options = new DbContextOptionsBuilder<CtxUsers>()
            //   .UseMySql(connectionString, new MariaDbServerVersion(new Version(10, 5, 9))) 
            //   .Options;
            return new CtxUsers(connectionString);
        }
    }
}
