using Domain.Todos;

namespace DataAccess.Todos;

public class TodoReadService : ITodoReadService
{
    private readonly ITodoRepository _repository;

    public TodoReadService(ITodoRepository repository)
    {
        _repository = repository;
    }

    public Task<IReadOnlyCollection<Todo>> GetAllTodos(CancellationToken cancellationToken)
    {
        return Task.FromResult(_repository.ListTodos());
    }
}