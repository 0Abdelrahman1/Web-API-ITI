using Microsoft.EntityFrameworkCore;
using Project.Contexts;
using System.Linq.Expressions;

namespace Project.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly StudentManagementDB _context;
        public BaseRepository(StudentManagementDB context)
        {
            _context = context;
        }

        public List<T> GetAll(params Expression<Func<T, object>>[] includes)
        {
            var query = _context.Set<T>().AsNoTracking().AsQueryable();
            foreach (var include in includes)
                query = query.Include(include);
            return query.ToList();
        }

        public T? GetByKey<TKey>(TKey id, Func<T, TKey> keySelector, params Expression<Func<T, object>>[] includes)
        {
            var query = _context.Set<T>().AsNoTracking().AsQueryable();
            foreach (var include in includes)
                query = query.Include(include);
            return query.AsEnumerable().FirstOrDefault(s => keySelector(s)!.Equals(id));
        }

        public List<T> GetByAttribute<TAttribute>(TAttribute value, Func<T, TAttribute> attributeSelector, bool equals = true, params Expression<Func<T, object>>[] includes)
        {
            var query = _context.Set<T>().AsNoTracking().AsQueryable();
            foreach (var include in includes)
                query = query.Include(include);
            return query.AsEnumerable().Where(s => equals ? attributeSelector(s)!.Equals(value) : !attributeSelector(s)!.Equals(value)).ToList();
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
            _context.SaveChanges();
        }

        public void Delete<TKey>(TKey id)
        {
            var entity = _context.Set<T>().Find(id);
            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
                _context.SaveChanges();
            }
        }
    }
}
