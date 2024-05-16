using Demo.Core.InterFace;
using Demo.EF.DataBase;
using Demo.EF.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Core.Repasatory
{
    public class UnitOfWork : IUnitOfWork 
    {
        private readonly ITIEntity contex;
        public IBaseReposatory<Emp> EmpRep { get;private set; }
        public IBaseReposatory<Department> DptRep { get;private set; }
        public UnitOfWork(ITIEntity contex)
        {
            this.contex = contex;
            EmpRep = new BaseReposatory<Emp>(contex);
            DptRep = new BaseReposatory<Department>(contex);
        }

 
        public int Complete()
        {
            return contex.SaveChanges();
        }

        public void Dispose()
        {
            contex.Dispose();
        }
    }
}
