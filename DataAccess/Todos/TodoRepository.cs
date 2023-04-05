using Domain.Shared.Exceptions;
using Domain.Todos;

namespace DataAccess.Todos;

public class TodoRepository : ITodoRepository
{
    private readonly IList<Todo> _todos = new List<Todo>();

    public IReadOnlyCollection<Todo> ListTodos()
    {
        return _todos.AsReadOnly();
    }

    public Todo AddTodo(Todo todo)
    {
        _todos.Add(todo);
        return todo;
    }

    public Todo UpdateTodo(Todo todo)
    {
        var existing = _todos.FirstOrDefault(t => t.Id == todo.Id)
                       ?? throw new TodoNotFoundException(todo.Id);
        var index = _todos.IndexOf(existing);

        var newTodo = todo with
        {
            DateAdded = existing.DateAdded
        };

        _todos.RemoveAt(index);
        _todos.Add(newTodo);

        return newTodo;
    }

    public void DeleteTodoById(Guid id)
    {
        var existing = _todos.FirstOrDefault(t => t.Id == id)
                       ?? throw new TodoNotFoundException(id);
        var index = _todos.IndexOf(existing);

        _todos.RemoveAt(index);
    }
}