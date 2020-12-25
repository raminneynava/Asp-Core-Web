using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class, IEntity
    {
        DbSet<TEntity> Entities { get; }
        IQueryable<TEntity> Table { get; }
        IQueryable<TEntity> TableNoTracking { get; }
        ValueTask<TEntity> GetByIdAsync(params object[] ids);
        Task AddAsync(TEntity entity, bool saveNow = true);
        Task AddRangeAsync(IEnumerable<TEntity> entities, bool saveNow = true);
        Task DeleteAsync(TEntity entity, bool saveNow = true);
        Task DeleteRangeAsync(IEnumerable<TEntity> entities, bool saveNow = true);
        Task UpdateAsync(TEntity entity, bool saveNow = true);
        Task UpdateRangeAsync(IEnumerable<TEntity> entities, bool saveNow = true);
        Task LoadCollectionAsync<TProperty>(TEntity entity, Expression<Func<TEntity, IEnumerable<TProperty>>> collectionProperty) where TProperty : class;
        void Attach(TEntity entity);
        void Detach(TEntity entity);

    }
}
