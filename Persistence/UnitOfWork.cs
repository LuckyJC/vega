using System.Threading.Tasks;

namespace vega.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly VegaDbContext _context;
        public UnitOfWork(VegaDbContext _context)
        {
            this._context = _context;

        }
        
        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}