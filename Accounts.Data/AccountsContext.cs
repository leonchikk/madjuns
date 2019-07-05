using Accounts.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounts.Data
{
    public class AccountsContext : DbContext
    {
        public AccountsContext()
        {

        }

        public AccountsContext(DbContextOptions<AccountsContext> options)
            : base(options)
        {

        }

        public virtual DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.Id);
            });
        }
    }
}
