using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sampaio.Shared.Paging;
using Sampaio.Shared.Persistence;

namespace Sampaio.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DataContext _context;
        protected readonly string _connStr;
       
        public DbSet<T> DbSet => _context.Set<T>();

        public Repository(DataContext context)
        {
            _context = context;
            _connStr = _context.Database.GetDbConnection().ConnectionString;
        }

       
        
        public int Count(Expression<Func<T, bool>> where = null)
        {
            where ??= (x => true);
            return _context.Set<T>().Count(where);
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> where = null)
        {
            where ??= (x => true);
            return await _context.Set<T>().CountAsync(where);
        }

        public bool Any(Expression<Func<T, bool>> where)
        {
            return _context.Set<T>().Any(where);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> where)
        {
            return await _context.Set<T>().AnyAsync(where);
        }

        public T FindByKey(params object[] keyValues)
        {
            return _context.Set<T>().Find(keyValues);
        }

        public async Task<T> FindByKeyAsync(params object[] keyValues)
        {
            return await _context.Set<T>().FindAsync(keyValues);
        }

        public IQueryable<T> Find(IEnumerable<string> includes = null)
        {
            IQueryable<T> currentSet = _context.Set<T>();

            if (includes != null)
            {
                currentSet = includes.Where(include => !string.IsNullOrEmpty(include))
                    .Aggregate(currentSet, (current, include) => current.Include(include));
            }

            return currentSet;
        }

        public IQueryable<T> Find(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> currentSet = _context.Set<T>();

            if (includes != null && includes.Any())
            {
                currentSet = includes.Aggregate(currentSet, (current, include) => current.Include(include));
            }

            return currentSet;
        }

        public T Find(Expression<Func<T, bool>> where, IEnumerable<string> includes = null)
        {
            IQueryable<T> currentSet = Find(includes);

            var entity = currentSet.FirstOrDefault(where);

            return entity;
        }

        public T FindAsNoTracking(Expression<Func<T, bool>> where, IEnumerable<string> includes = null)
        {
            IQueryable<T> currentSet = Find(includes).AsNoTracking();

            var entity = currentSet.FirstOrDefault(where);

            return entity;
        }

        public async Task<T> FindAsync(Expression<Func<T, bool>> where, IEnumerable<string> includes = null)
        {
            IQueryable<T> currentSet = Find(includes);

            var entity = await currentSet.FirstOrDefaultAsync(where);

            return entity;
        }

        public async Task<T> FindAsyncAsNoTracking(Expression<Func<T, bool>> where,
            IEnumerable<string> includes = null)
        {
            IQueryable<T> currentSet = Find(includes).AsNoTracking();

            var entity = await currentSet.FirstOrDefaultAsync(where);

            return entity;
        }

        public T Find(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> currentSet = Find(includes);

            var entity = currentSet.FirstOrDefault(where);

            return entity;
        }

        public T FindAsNoTracking(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> currentSet = Find(includes).AsNoTracking();

            var entity = currentSet.FirstOrDefault(where);

            return entity;
        }

        public async Task<T> FindAsync(Expression<Func<T, bool>> where,
            params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> currentSet = Find(includes);

            var entity = await currentSet.FirstOrDefaultAsync(where);

            return entity;
        }

        public async Task<T> FindAsyncAsNoTracking(Expression<Func<T, bool>> where,
            params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> currentSet = Find(includes).AsNoTracking();

            var entity = await currentSet.FirstOrDefaultAsync(where);

            return entity;
        }

        private IQueryable<T> CurrentSet(Expression<Func<T, bool>> where = null, int? page = null,
            int? PageSize = null,
            string SortField = null, string SortType = null, IEnumerable<string> includes = null)
        {
            IQueryable<T> currentSet = _context.Set<T>();

            where ??= (x => true);

            if (includes != null)
            {
                currentSet = includes.Where(include => !string.IsNullOrEmpty(include))
                    .Aggregate(currentSet, (current, include) => current.Include(include));
            }

            currentSet = currentSet.Where(where);

            if (!string.IsNullOrEmpty(SortField) && !string.IsNullOrEmpty(SortType))
            {
                currentSet = currentSet.OrderBy(SortField + " " + SortType);
            }

            if (page != null && PageSize != null)
            {
                currentSet = currentSet
                    .Skip((page.Value - 1) * PageSize.Value)
                    .Take(PageSize.Value);
            }

            return currentSet;
        }

        private IQueryable<T> CurrentSet(Expression<Func<T, bool>> where = null, int? page = null,
            int? PageSize = null,
            string SortField = null, string SortType = null, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> currentSet = _context.Set<T>();

            where ??= (x => true);

            if (includes != null && includes.Any())
            {
                currentSet = includes.Aggregate(currentSet, (current, include) => current.Include(include));
            }

            currentSet = currentSet.Where(where);

            if (!string.IsNullOrEmpty(SortField) && !string.IsNullOrEmpty(SortType))
            {
                currentSet = currentSet.OrderBy(SortField + " " + SortType);
            }

            if (page != null && PageSize != null)
            {
                currentSet = currentSet
                    .Skip((page.Value - 1) * PageSize.Value)
                    .Take(PageSize.Value);
            }

            return currentSet;
        }


        public IQueryable<T> List(Expression<Func<T, bool>> where = null, int? page = null, int? PageSize = null,
            string SortField = null, string SortType = null, IEnumerable<string> includes = null)
        {
            return CurrentSet(where, page, PageSize, SortField, SortType, includes);
        }

        public IQueryable<T> ListAsNoTracking(Expression<Func<T, bool>> where = null, int? page = null,
            int? PageSize = null,
            string SortField = null, string SortType = null, IEnumerable<string> includes = null)
        {
            return List(where, page, PageSize, SortField, SortType, includes).AsNoTracking();
        }

        public IQueryable<T> List(Expression<Func<T, bool>> where = null, int? page = null, int? PageSize = null,
            string SortField = null, string SortType = null, params Expression<Func<T, object>>[] includes)
        {
            return CurrentSet(where, page, PageSize, SortField, SortType, includes);
        }

        public IQueryable<T> ListAsNoTracking(Expression<Func<T, bool>> where = null, int? page = null,
            int? PageSize = null,
            string SortField = null, string SortType = null, params Expression<Func<T, object>>[] includes)
        {
            return List(where, page, PageSize, SortField, SortType, includes).AsNoTracking();
        }

        public IQueryable<T> List(Expression<Func<T, bool>> where, IPagination pagination,
            IEnumerable<string> includes = null)
        {
            return CurrentSet(where, pagination.PageIndex, pagination.PageSize, pagination.SortField,
                pagination.SortType, includes);
        }

        public IQueryable<T> ListAsNoTracking(Expression<Func<T, bool>> where, IPagination pagination,
            IEnumerable<string> includes = null)
        {
            return List(where, pagination, includes).AsNoTracking();
        }

        public IQueryable<T> List(Expression<Func<T, bool>> where, IPagination pagination,
            params Expression<Func<T, object>>[] includes)
        {
            return CurrentSet(where, pagination.PageIndex, pagination.PageSize, pagination.SortField,
                pagination.SortType, includes);
        }

        public IQueryable<T> ListAsNoTracking(Expression<Func<T, bool>> where, IPagination pagination,
            params Expression<Func<T, object>>[] includes)
        {
            return List(where, pagination, includes).AsNoTracking();
        }

        public PagedList<T> PagedList(Expression<Func<T, bool>> where, IPagination pagination,
            params Expression<Func<T, object>>[] includes)
        {
            var total = Count(where);

            var itens = List(where, pagination, includes);

            return new PagedList<T>(itens, total, pagination.PageSize);
        }

        public PagedList<T> PagedListAsNoTracking(Expression<Func<T, bool>> where, IPagination pagination,
            params Expression<Func<T, object>>[] includes)
        {
            var total = Count(where);

            var itens = ListAsNoTracking(where, pagination, includes);

            return new PagedList<T>(itens, total, pagination.PageSize);
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public async Task AddAsync(T entity)
        {
            await Task.Run(() => Add(entity));
        }

        public void Modify(T entity) => _context.Set<T>().Update(entity);

        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
        }
        public void UpdateRange(IEnumerable<T> entities)
        {
            _context.Set<T>().UpdateRange(entities);
        }
    }
}
