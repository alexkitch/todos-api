namespace Domain.Todos;

public interface ITodoReadService
{
    Task<IReadOnlyCollection<Todo>> GetAllTodos(CancellationToken cancellationToken);
}