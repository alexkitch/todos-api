namespace Domain.Todos;

public interface ITodoWriteService
{
    Task<Todo> CreateTodoAsync(string? description, CancellationToken cancellationToken);

    Task<Todo> UpdateTodoAsync(Guid id, string? description, bool isPending, CancellationToken cancellationToken);

    Task DeleteTodoAsync(Guid id, CancellationToken cancellationToken);
}