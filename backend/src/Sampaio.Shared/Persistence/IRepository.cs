using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Sampaio.Shared.Paging;

namespace Sampaio.Shared.Persistence
{
    public interface IRepository<T>
        where T : class
    {
        int Count(Expression<Func<T, bool>> where = null);
        Task<int> CountAsync(Expression<Func<T, bool>> where = null);
        bool Any(Expression<Func<T, bool>> where);
        Task<bool> AnyAsync(Expression<Func<T, bool>> where);
        T FindByKey(params object[] keyValues);
        Task<T> FindByKeyAsync(params object[] keyValues);
        IQueryable<T> Find(IEnumerable<string> includes = null);
        IQueryable<T> Find(params Expression<Func<T, object>>[] includes);

        T Find(Expression<Func<T, bool>> where,
            IEnumerable<string> includes = null);

        T FindAsNoTracking(Expression<Func<T, bool>> where,
            IEnumerable<string> includes = null);

        Task<T> FindAsync(Expression<Func<T, bool>> where,
            IEnumerable<string> includes = null);

        Task<T> FindAsyncAsNoTracking(Expression<Func<T, bool>> where,
            IEnumerable<string> includes = null);

        T Find(Expression<Func<T, bool>> where,
            params Expression<Func<T, object>>[] includes);

        T FindAsNoTracking(Expression<Func<T, bool>> where,
            params Expression<Func<T, object>>[] includes);

        Task<T> FindAsync(Expression<Func<T, bool>> where,
            params Expression<Func<T, object>>[] includes);

        Task<T> FindAsyncAsNoTracking(Expression<Func<T, bool>> where,
            params Expression<Func<T, object>>[] includes);

        IQueryable<T> List(Expression<Func<T, bool>> where = null,
            int? page = null,
            int? PageSize = null,
            string SortField = null,
            string SortType = null,
            IEnumerable<string> includes = null);

        IQueryable<T> ListAsNoTracking(Expression<Func<T, bool>> where = null,
            int? page = null,
            int? PageSize = null,
            string SortField = null,
            string SortType = null,
            IEnumerable<string> includes = null);

        IQueryable<T> List(Expression<Func<T, bool>> where = null,
            int? page = null,
            int? PageSize = null,
            string SortField = null,
            string SortType = null,
            params Expression<Func<T, object>>[] includes);

        IQueryable<T> ListAsNoTracking(Expression<Func<T, bool>> where = null,
            int? page = null,
            int? PageSize = null,
            string SortField = null,
            string SortType = null,
            params Expression<Func<T, object>>[] includes);

        IQueryable<T> List(Expression<Func<T, bool>> where,
            IPagination pagination,
            IEnumerable<string> includes = null);

        IQueryable<T> ListAsNoTracking(Expression<Func<T, bool>> where,
            IPagination pagination,
            IEnumerable<string> includes = null);

        IQueryable<T> List(Expression<Func<T, bool>> where,
            IPagination pagination,
            params Expression<Func<T, object>>[] includes);

        IQueryable<T> ListAsNoTracking(Expression<Func<T, bool>> where,
            IPagination pagination,
            params Expression<Func<T, object>>[] includes);

        PagedList<T> PagedList(Expression<Func<T, bool>> where,
            IPagination pagination,
            params Expression<Func<T, object>>[] includes);

        PagedList<T> PagedListAsNoTracking(Expression<Func<T, bool>> where,
            IPagination pagination,
            params Expression<Func<T, object>>[] includes);

        void Add(T entity);
        Task AddAsync(T entity);
        void Modify(T entity);
        void Remove(T entity);
        void AddRange(IEnumerable<T> entities);
        Task AddRangeAsync(IEnumerable<T> entities);
        void UpdateRange(IEnumerable<T> entities);

    }
}