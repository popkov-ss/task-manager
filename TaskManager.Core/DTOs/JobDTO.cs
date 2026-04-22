namespace TaskManager.Core.DTOs
{
    public record TaskDto(Guid Id, string Title,  DateTime CreatedAt,
                       bool IsCompleted, Guid TaskTypeId, string TaskTypeName);

    public record CreateTaskRequest(string Title,  Guid TaskTypeId);
    public record UpdateTaskRequest(string Title,  bool IsCompleted, Guid TaskTypeId);
}