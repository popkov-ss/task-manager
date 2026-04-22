using Microsoft.AspNetCore.Mvc;
using TaskManager.Core.DTOs;
using TaskManager.Core.Entities;
using TaskManagment.Infrastructure.Interfaces;

namespace TaskManagment.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobsController : ControllerBase
    {
        private readonly IJobRepository _jobRepository;
        private readonly IRepository<JobType> _jobTypeRepo;

        public JobsController(IJobRepository jobRepository, IRepository<JobType> JobTypeRepo)
        {
            _jobRepository = jobRepository;
            _jobTypeRepo = JobTypeRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var jobs = await _jobRepository.GetAllAsync();
            var result = jobs.Select(MapToDto);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var task = await _jobRepository.GetByIdAsync(id);
            if (task == null) return NotFound();
            return Ok(MapToDto(task));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTaskRequest request)
        {
            var taskType = await _jobTypeRepo.GetByIdAsync(request.TaskTypeId);
            if (taskType == null) return BadRequest("Invalid TaskTypeId");

            var task = new Job
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                TaskTypeId = request.TaskTypeId,
                CreatedAt = DateTime.UtcNow,
                IsCompleted = false
            };

            await _jobRepository.AddAsync(task);
            await _jobRepository.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = task.Id }, MapToDto(task));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTaskRequest request)
        {
            var task = await _jobRepository.GetByIdAsync(id);
            if (task == null) return NotFound();

            var taskType = await _jobTypeRepo.GetByIdAsync(request.TaskTypeId);
            if (taskType == null) return BadRequest("Invalid TaskTypeId");

            task.Title = request.Title;
            task.IsCompleted = request.IsCompleted;
            task.TaskTypeId = request.TaskTypeId;

            _jobRepository.Update(task);
            await _jobRepository.SaveChangesAsync();

            return Ok(MapToDto(task));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var task = await _jobRepository.GetByIdAsync(id);
            if (task == null) return NotFound();

            _jobRepository.Delete(task);
            await _jobRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("type/{taskTypeId}")]
        public async Task<IActionResult> GetByType(Guid taskTypeId)
        {
            var jobs = await _jobRepository.GetByTypeAsync(taskTypeId);
            return Ok(jobs.Select(MapToDto));
        }

        private static TaskDto MapToDto(Job job) => new(
            job.Id,
            job.Title,
            job.CreatedAt,
            job.IsCompleted,
            job.TaskTypeId,
            job.TaskType?.Name ?? string.Empty
        );
    }
}
