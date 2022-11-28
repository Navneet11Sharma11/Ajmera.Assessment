using Ajmera.Assessment.DL.Entities;

namespace Ajmera.Assessment.DL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BookDbContext context;

        public UnitOfWork(BookDbContext context)
        {
            this.context = context;
        }

        public async Task SaveChangesAsync()
        {
            _ = await context.SaveChangesAsync();
        }
    }
}