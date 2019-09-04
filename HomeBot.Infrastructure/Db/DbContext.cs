using HomeBot.Infrastructure.Db.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HomeBot.Infrastructure.Db
{
    public class DbContext: Microsoft.EntityFrameworkCore.DbContext
    {
        public DbContext(DbContextOptions options) : base(options)
        { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Log>().HasKey(m => m.Id);
            builder.Entity<Movie>().HasKey(m => m.Id);
            base.OnModelCreating(builder);
        }
    }
}
