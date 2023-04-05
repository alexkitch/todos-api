using Domain.Todos;

namespace DataAccess.Todos;

public interface ITodoRepository
{
    public IReadOnlyCollection<Todo> ListTodos();

    public Todo AddTodo(Todo todo);

    public Todo UpdateTodo(Todo todo);

    public void DeleteTodoById(Guid id);
}