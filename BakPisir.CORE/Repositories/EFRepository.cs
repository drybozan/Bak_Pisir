using BakPisir.CORE.Paging;
using BakPisir.EF.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BakPisir.CORE.Repositories
{
    public class EFRepository<T> : IRepository<T> where T : class
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public EFRepository(BakPisirDBEntities dbContext)
        {
            if (dbContext == null)
                throw new ArgumentNullException("dbContext can not be null.");

            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        #region IRepository Members
        //Veritabanındaki bütün tabloyu döndürür.
        public IQueryable<T> GetAll()
        {
            
            return _dbSet;
        }
        public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate)
        {
           return _dbSet.Where(predicate);
        }
    


        //Veritabanındaki verilen id'ye sahip veriyi bulur ve döndürür.
        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        //Veritabanındaki istenilen parametreye uygun veriyi döndürür.
        public T Get(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate).SingleOrDefault();
        }

        //Veritabanına gönderilen veri ekler.
        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        //Gönderilen varlığa göre veritabanındaki veriyi günceller.
        public void Update(T entity)
        {
            _dbSet.AddOrUpdate(entity);
            //_dbContext.Entry(entity).State = EntityState.Modified;
        }

        //Veritabanındaki verilen id'ye sahip veriyi silmez, silinmiş olarak gösterir.
        public void Delete(T entity)
        {
            // Eğer sizlerde genelde bir kayıtı silmek yerine IsDelete şeklinde bool bir flag alanı tutuyorsanız,
            // Küçük bir refleciton kodu yardımı ile bunuda otomatikleştirebiliriz.
            if (entity.GetType().GetProperty("isDelete") != null)
            {
                T _entity = entity;

                _entity.GetType().GetProperty("isDelete").SetValue(_entity, true);

                this.Update(_entity);
            }
            else
            {
                // Önce entity'nin state'ini kontrol etmeliyiz.
                DbEntityEntry dbEntityEntry = _dbContext.Entry(entity);

                if (dbEntityEntry.State != EntityState.Deleted)
                {
                    dbEntityEntry.State = EntityState.Deleted;
                }
                else
                {
                    _dbSet.Attach(entity);
                    _dbSet.Remove(entity);
                }
            }
        }

        //Veritabanındaki verilen id'ye sahip veriyi siler.
        public void Delete(int id)
        {
            var entity = GetById(id);
            if (entity == null) return;
            else
            {
                if (entity.GetType().GetProperty("isDelete") != null)
                {
                    T _entity = entity;
                    _entity.GetType().GetProperty("isDelete").SetValue(_entity, true);

                    this.Update(_entity);
                }
                else
                {
                    Delete(entity);
                }
            }
        }

        public void DeleteMultiple(Expression<Func<T, bool>> filter)
        {
            throw new NotImplementedException();

        }


        public IQueryable<T> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "")
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> Get(string a, Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "")
        {
            throw new NotImplementedException();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter = null, params Expression<Func<T, object>>[] includes)
        {
            throw new NotImplementedException();
        }

        public T FirstAsNoTracking(Expression<Func<T, bool>> filter = null, params Expression<Func<T, object>>[] includes)
        {
            throw new NotImplementedException();
        }

        public bool Any(Expression<Func<T, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public int Max(Expression<Func<T, bool>> filter, Expression<Func<T, int>> source)
        {
            throw new NotImplementedException();
        }

        public int? Max(Expression<Func<T, bool>> filter, Expression<Func<T, int?>> source)
        {
            throw new NotImplementedException();
        }

        public long Max(Expression<Func<T, bool>> filter, Expression<Func<T, long>> source)
        {
            throw new NotImplementedException();
        }

        public double Max(Expression<Func<T, bool>> filter, Expression<Func<T, double>> source)
        {
            throw new NotImplementedException();
        }

        public int Min(Expression<Func<T, bool>> filter, Expression<Func<T, int>> source)
        {
            throw new NotImplementedException();
        }

        public long Min(Expression<Func<T, bool>> filter, Expression<Func<T, long>> source)
        {
            throw new NotImplementedException();
        }

        public double Min(Expression<Func<T, bool>> filter, Expression<Func<T, double>> source)
        {
            throw new NotImplementedException();
        }

        public int Sum(Expression<Func<T, bool>> filter, Expression<Func<T, int>> source)
        {
            throw new NotImplementedException();
        }

        public long Sum(Expression<Func<T, bool>> filter, Expression<Func<T, long>> source)
        {
            throw new NotImplementedException();
        }

        public double Sum(Expression<Func<T, bool>> filter, Expression<Func<T, double>> source)
        {
            throw new NotImplementedException();
        }

        public double Average(Expression<Func<T, bool>> filter, Expression<Func<T, double>> source)
        {
            throw new NotImplementedException();
        }

        public T GetById(object id)
        {
            throw new NotImplementedException();
        }

        public T Insert(T entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> InsertRange(IEnumerable<T> entity)
        {
            throw new NotImplementedException();
        }



        public void Delete(Expression<Func<T, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public void UpdateMultiple(Action<T> statement, Expression<Func<T, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public void Delete(object id)
        {
            throw new NotImplementedException();
        }

       

        #endregion
    }
}


