using RingoMedia.DAL.Data.Models;
using RingoMedia.DAL.Repos.DepartmentRepo;

namespace RingoMedia.DAL.UnitOfWork
{
    public interface IUnitOfWork
    {
        public IDepartmentRepository DepartmentRepository { get; }

        Task<int> SaveChangesAsync();
        Response Response(bool success, object? data, object? messages);

    }
}
