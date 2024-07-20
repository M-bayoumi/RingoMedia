using RingoMedia.DAL.Data.Context;
using RingoMedia.DAL.Data.Models;
using RingoMedia.DAL.Repos.DepartmentRepo;

namespace RingoMedia.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public IDepartmentRepository DepartmentRepository { get; } = null!;
        public AppDbContext AppDbContext { get; }

        public UnitOfWork
            (
            AppDbContext appDbContext,
            IDepartmentRepository departmentRepository
            )
        {
            AppDbContext = appDbContext;
            DepartmentRepository = departmentRepository;

        }

        public async Task<int> SaveChangesAsync()
        {
            return await AppDbContext.SaveChangesAsync();
        }
        public Response Response(bool success, object? data, object? messages)
        {
            return new Response
            {
                Success = success,
                Data = data,
                Messages = messages
            };
        }
    }
}
