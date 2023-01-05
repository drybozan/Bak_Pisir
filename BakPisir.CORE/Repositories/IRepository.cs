using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BakPisir.CORE.Repositories
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        IQueryable<T> GetAll(Expression<Func<T, bool>> predicate); // LINQ desteği sunabilmek içinde expression'ları kullanıyoruz.
        T GetById(int id);
        T Get(Expression<Func<T, bool>> predicate);

        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(int id);

        IQueryable<T> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "");
        IQueryable<T> Get(string a, Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "");
        T GetFirstOrDefault(Expression<Func<T, bool>> filter = null, params Expression<Func<T, object>>[] includes);
        T FirstAsNoTracking(Expression<Func<T, bool>> filter = null, params Expression<Func<T, object>>[] includes);
        bool Any(Expression<Func<T, bool>> filter);
        int Max(Expression<Func<T, bool>> filter, Expression<Func<T, int>> source);
        int? Max(Expression<Func<T, bool>> filter, Expression<Func<T, int?>> source);
        long Max(Expression<Func<T, bool>> filter, Expression<Func<T, long>> source);
        double Max(Expression<Func<T, bool>> filter, Expression<Func<T, double>> source);
        int Min(Expression<Func<T, bool>> filter, Expression<Func<T, int>> source);
        long Min(Expression<Func<T, bool>> filter, Expression<Func<T, long>> source);
        double Min(Expression<Func<T, bool>> filter, Expression<Func<T, double>> source);
        int Sum(Expression<Func<T, bool>> filter, Expression<Func<T, int>> source);
        long Sum(Expression<Func<T, bool>> filter, Expression<Func<T, long>> source);
        double Sum(Expression<Func<T, bool>> filter, Expression<Func<T, double>> source);
        double Average(Expression<Func<T, bool>> filter, Expression<Func<T, double>> source);
        T GetById(object id);
        T Insert(T entity);
        IEnumerable<T> InsertRange(IEnumerable<T> entity);
        void Delete(object id);
        void Delete(Expression<Func<T, bool>> filter);
        void DeleteMultiple(Expression<Func<T, bool>> filter);
        void UpdateMultiple(Action<T> statement, Expression<Func<T, bool>> filter);

    }
}
