using Ajmera.Assessment.DL.Models;

namespace Ajmera.Assessment.DL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AjmeraContext context;

        public UnitOfWork(AjmeraContext context)
        {
            this.context = context;
        }

        public async Task SaveChangesAsync()
        {
            _ = await context.SaveChangesAsync();
        }
    }
}