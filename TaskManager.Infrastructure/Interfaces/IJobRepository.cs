using TaskManager.Core.Entities;

namespace TaskManagment.Infrastructure.Interfaces
{
    /// <summary>
    /// Специфический репозиторий для Job
    /// </summary>
    public interface IJobRepository : IRepository<Job>
    {
        /// <summary>
        /// Получить задачи по типу
        /// </summary>
        /// <param name="taskTypeId"></param>
        /// <returns></returns>
        Task<IEnumerable<Job>> GetByTypeAsync(Guid taskTypeId);
    }
}
