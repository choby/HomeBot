using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace HomeBot.Infrastructure.Db
{
    public interface IRepository<T> where T : class
    {
        DbSet<T> Table { get; }
        EntityEntry Add([NotNullAttribute] T entity);
        EntityEntry Remove([NotNullAttribute] T entity);
        EntityEntry Update([NotNullAttribute] T entity);
        int SaveChanges(bool acceptAllChangesOnSuccess);
        int SaveChanges();
    }
}
