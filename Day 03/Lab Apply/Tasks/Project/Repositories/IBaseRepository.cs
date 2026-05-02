using System.Linq.Expressions;

namespace Project.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        public List<T> GetAll(params Expression<Func<T, object>>[] includes);
        public T? GetByKey<TKey>(TKey id, Func<T, TKey> keySelector, params Expression<Func<T, object>>[] includes);
        public List<T> GetByAttribute<TAttribute>(TAttribute value, Func<T, TAttribute> attributeSelector, bool equals = true, params Expression<Func<T, object>>[] includes);
        public void Add(T entity);
        public void Update(T entity);
        public void Delete<TKey>(TKey id);
    }
}
