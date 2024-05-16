using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Core.InterFace
{
    public interface IBaseReposatory<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(int id);  
     
        IEnumerable<T> Find(Expression<Func<T,bool>> match , string[] includes = null);
        T Add(T Entity);
        T Update(T entity);
        void Delete(int id);
    }
}
