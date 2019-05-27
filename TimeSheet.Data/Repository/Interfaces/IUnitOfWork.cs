using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TimeSheet.Data.Repository.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TPocoEntity> GetRepository<TPocoEntity>() where TPocoEntity : class;
        int Complete();
        Task<int> CompleteAsync();
    }
}
