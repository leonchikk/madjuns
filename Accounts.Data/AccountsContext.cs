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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}
