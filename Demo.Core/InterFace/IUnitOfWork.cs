using Demo.EF.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Core.InterFace
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseReposatory<Emp> EmpRep { get; }
        IBaseReposatory<Department> DptRep { get; }

        int Complete();
    }
}
