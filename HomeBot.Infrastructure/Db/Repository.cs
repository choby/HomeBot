using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace HomeBot.Infrastructure.Db
{
    public class Repository<T> : IRepository<T> where T : class
    {
        DbContext _dbContext;
        public Repository(DbContext dbContext) 
        {
            _dbContext = dbContext;
        }
        public DbSet<T> Table => _dbContext.Set<T>();

        public EntityEntry Add([NotNull] T entity)
        {
           return _dbContext.Add(entity);
        }

        public EntityEntry Remove([NotNull] T entity)
        {
            return _dbContext.Remove(entity);
        }

        public int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            return _dbContext.SaveChanges(acceptAllChangesOnSuccess);
        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }

        public EntityEntry Update([NotNull] T entity)
        {
            return _dbContext.Update(entity);
        }
    }
}
