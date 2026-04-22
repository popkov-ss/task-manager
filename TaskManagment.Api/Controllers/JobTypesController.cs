using Microsoft.AspNetCore.Mvc;
using TaskManager.Core.Entities;
using TaskManagment.Infrastructure.Interfaces;

namespace TaskManagment.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobTypesController : ControllerBase
    {
        private readonly IRepository<JobType> _taskTypeRepo;

        public JobTypesController(IRepository<JobType> taskTypeRepo)
        {
            _taskTypeRepo = taskTypeRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _taskTypeRepo.GetAllAsync());
    }
}
