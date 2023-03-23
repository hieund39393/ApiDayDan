using EVN.Core.Models.Interface;
using System.Threading.Tasks;

namespace EVN.Core.Interfaces.Database
{
    public interface IUnitOfWork
    {
        int SaveChanges();

        Task<int> SaveChangesAsync();        
        Task Dispose();
    }
}
