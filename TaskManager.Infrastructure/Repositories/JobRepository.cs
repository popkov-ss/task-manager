using Microsoft.EntityFrameworkCore;
using TaskManager.Core.Entities;
using TaskManagment.Infrastructure.Interfaces;

namespace TaskManagment.Infrastructure.Repositories
{
    public class JobRepository : RepositoryBase<Job>, IJobRepository
    {
        public JobRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Job>> GetByTypeAsync(Guid taskTypeId)
            => await _dbSet.Where(t => t.TaskTypeId == taskTypeId)
                .Include(t => t.TaskType)
                .ToListAsync();


        public override async Task<Job?> GetByIdAsync(Guid id)
            => await _dbSet.Include(t => t.TaskType).FirstOrDefaultAsync(t => t.Id == id);

        public override async Task<IEnumerable<Job>> GetAllAsync()
            => await _dbSet.Include(t => t.TaskType).ToListAsync();
    }
}
