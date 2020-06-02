using System.Threading.Tasks;

namespace NetDevPack.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}