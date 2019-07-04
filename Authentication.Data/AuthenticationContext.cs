using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Authentication.Data
{
    public class AuthenticationContext: DbContext
    {
        public AuthenticationContext()
        {

        }

        public AuthenticationContext(DbContextOptions<AuthenticationContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}
