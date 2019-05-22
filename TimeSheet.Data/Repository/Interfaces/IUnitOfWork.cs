using System;
using System.Collections.Generic;
using System.Text;

namespace TimeSheet.Data.Repository.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TPocoEntity> GetRepository<TPocoEntity>() where TPocoEntity : class;
        int Complete();
    }
}
