using CoffeeShop.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;
using System.Reflection;
using CoffeeShop.Constants;
using System.Threading.Tasks.Dataflow;

namespace CoffeeShop.Repositories.Implements
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly CoffeeShopDBContext _context;

        public GenericRepository(CoffeeShopDBContext context)
        {
            _context = context;
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }
        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void SoftDelete(T entity)
        {
            //_context.Set<T>().Remove(entity);
            PropertyInfo propertyInfo = entity.GetType().GetProperty("IsDeleted");
            propertyInfo.SetValue(entity, true);
            _context.Set<T>().Update(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }
            if (expression != null)
            {
                query = query.Where(expression);
            }
            return await query.ToListAsync();
        }

        public async Task<(IEnumerable<T> items, int totalCount)> GetAllAsync(int page = 1, Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string includeProperties = "", Expression<Func<T, bool>>? prioritizeCondition = null)
        {
            IQueryable<T> query = _context.Set<T>();
            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                         (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            var totalCount = query.Count();
            List<T> prioritizedItems = new List<T>();
            List<T> nonPrioritizedItems = new List<T>();

            if (prioritizeCondition != null)
            {
                var prioritizedQuery = query.Where(prioritizeCondition);
                var nonPrioritizedQuery = query.Where(Expression.Lambda<Func<T, bool>>(
                    Expression.Not(prioritizeCondition.Body), prioritizeCondition.Parameters));

                if (orderBy != null)
                {
                    nonPrioritizedQuery = orderBy(nonPrioritizedQuery);
                }

                prioritizedItems = await prioritizedQuery.ToListAsync();
                nonPrioritizedItems = await nonPrioritizedQuery.ToListAsync();
            }
            else
            {
                if (orderBy != null)
                {
                    query = orderBy(query);
                }
                nonPrioritizedItems = await query.ToListAsync();
            }

            var items = prioritizedItems.Concat(nonPrioritizedItems).ToList();

            var paginatedItems = items.Skip((page - 1) * PageSizeConstant.PAGE_SIZE).Take(PageSizeConstant.PAGE_SIZE).ToList();
            return (paginatedItems, totalCount);
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }
            if (expression != null)
            {
                query = query.Where(expression);
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> expression)
        {
            IQueryable<T> query = _context.Set<T>();
            if (expression != null)
            {
                query = query.Where(expression);
            }
            return await query.FirstOrDefaultAsync();
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }


        public void RemoveRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> expression = null)
        {
            IQueryable<T> query = _context.Set<T>();
            if (expression != null)
            {
                query = query.Where(expression);
            }
            return await query.CountAsync();
        }
    }
}