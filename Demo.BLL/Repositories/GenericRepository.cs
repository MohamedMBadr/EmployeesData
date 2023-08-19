using Demo.BLL.InterFaces;
using Demo.DAL.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositories
{
    public class GenericRepository <T> : IGenericRepository<T>  where T : class
    {

        private readonly MVCExceedDBContext _dbContext;

        public GenericRepository(MVCExceedDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Add(T item)
        {
            _dbContext.Set<T>().Add(item);
            return _dbContext.SaveChanges();
        }

        public int Delete(T item)
        {
            _dbContext.Set<T>().Remove(item);
            return _dbContext.SaveChanges();
        }

        public T Get(int id)
            => _dbContext.Set<T>().Find(id);


        public IEnumerable<T> GetAll()
            =>      _dbContext.Set<T>().ToList();


        public int Update(T item)
        {
            _dbContext.Set<T>().Update(item);
            return _dbContext.SaveChanges();
        }
    }
}
