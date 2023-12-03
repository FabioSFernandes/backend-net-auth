using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Infrastructure.Repositories
{
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using System.Configuration;

    public class CtxTokens : DbContext
    {
        protected String _connectionString;
        public DbSet<User> Users { get; set; }
        public DbSet<Login> Logins { get; set; }
        public DbSet<AccessRecord> AccessRecords { get; set; }
        public DbSet<Token> Tokens { get; set; }

        public CtxTokens() : base()
        {
           // _connectionString = connectionString;
        }

        public CtxTokens(String connectionString): base()
        {
            _connectionString = connectionString;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(_connectionString,new MariaDbServerVersion(new Version(10, 5, 9))); // Substitua a versão pela versão do seu MariaDB
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.Logins)
                .WithOne(l => l.User);

            modelBuilder.Entity<User>()
                .HasMany(l => l.Logins);
            modelBuilder.Entity<User>().ToView("vw_token_users");

            modelBuilder.Entity<AccessRecord>()
                .HasOne(t => t.Token);

            modelBuilder.Entity<AccessRecord>()
                .HasOne(l => l.Login);

            modelBuilder.Entity<Login>()
            .HasOne(u => u.User);

            modelBuilder.Entity<Login>().ToView("vw_token_logins");

            modelBuilder.Entity<Token>()
            .HasMany(ar => ar.AccessRecords);

            modelBuilder.Entity<Token>()
            .HasOne(l => l.Login);

        }
    }


}
