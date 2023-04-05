using Domain.Todos;

namespace DataAccess.Todos;

public class TodoWriteService : ITodoWriteService
{
    private readonly ITodoRepository _repository;

    public TodoWriteService(ITodoRepository repository)
    {
        _repository = repository;
    }

    public Task<Todo> CreateTodoAsync(string? description, CancellationToken cancellationToken)
    {
        var newTodo = new Todo
        {
            Description = description
        };

        return Task.FromResult(_repository.AddTodo(newTodo));
    }

    public Task<Todo> UpdateTodoAsync(Guid id, string? description, bool isPending, CancellationToken cancellationToken)
    {
        var newTodo = new Todo
        {
            Id = id,
            Description = description,
            IsPending = isPending
        };

        return Task.FromResult(_repository.UpdateTodo(newTodo));
    }

    public Task DeleteTodoAsync(Guid id, CancellationToken cancellationToken)
    {
        _repository.DeleteTodoById(id);
        return Task.CompletedTask;
    }
}