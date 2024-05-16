using Demo.Core.InterFace;
using Demo.EF.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Core.Repasatory
{
    public class BaseReposatory<T> : IBaseReposatory<T> where T : class
    {
        private readonly ITIEntity contex;

        public BaseReposatory(ITIEntity contex)
        {
            this.contex = contex;
        }

        public IEnumerable<T> GetAll()
        {
            return contex.Set<T>().ToList();
        }

        public T GetById(int id)
        {
          
            return contex.Set<T>().Find(id);
        }
        public IEnumerable<T> Find(Expression<Func<T, bool>> match , string[] includes = null)
        {
            IQueryable<T> quary = contex.Set<T>();
            if(includes != null)
            {
                foreach(var include in includes)
                    quary = quary.Include(include);
            }
           return quary.Where(match).ToList();   
        }
        public T Add(T Entity)
        {
            contex.Set<T>().Add(Entity);
            return Entity;
        }
        public  T Update(T entity)
        {
            contex.Update(entity);
            return entity;
        }
        public void Delete(int id)
        {
            var oldData = contex.Set<T>().Find(id);
            contex.Remove(oldData);
        }
    }
}
