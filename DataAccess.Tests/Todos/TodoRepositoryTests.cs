using DataAccess.Todos;
using Domain.Shared.Exceptions;
using Domain.Todos;

namespace DataAccess.Tests.Todos;

public class TodoRepositoryTests
{
    private readonly ITodoRepository _todoRepository;

    public TodoRepositoryTests()
    {
        _todoRepository = new TodoRepository();
    }

    [Fact]
    public void ListTodos_ReturnsEmptyList_IfNoTodosExist()
    {
        // Arrange

        // Act
        var todos = _todoRepository.ListTodos();

        // Assert
        Assert.Empty(todos);
    }

    [Fact]
    public void ListTodos_ReturnsTodos_IfTodosExist()
    {
        // Arrange
        var todo = new Todo
        {
            Id = Guid.NewGuid(),
            Description = "Test",
            IsPending = true
        };
        _todoRepository.AddTodo(todo);

        // Act
        var todos = _todoRepository.ListTodos();

        // Assert
        Assert.Single(todos);
        Assert.Equal(todo, todos.First());
    }

    [Fact]
    public void AddTodo_AddsTodo()
    {
        // Arrange
        var todo = new Todo
        {
            Id = Guid.NewGuid(),
            Description = "Test",
            IsPending = true
        };

        // Act
        var addedTodo = _todoRepository.AddTodo(todo);

        // Assert
        Assert.Equal(todo, addedTodo);
    }

    [Fact]
    public void UpdateTodo_UpdatesTodo()
    {
        // Arrange
        var todo = new Todo
        {
            Id = Guid.NewGuid(),
            Description = "Test",
            IsPending = true
        };
        _todoRepository.AddTodo(todo);

        var updatedTodo = todo with
        {
            Description = "Updated",
            IsPending = false
        };

        // Act
        var result = _todoRepository.UpdateTodo(updatedTodo);

        // Assert
        Assert.Equal(updatedTodo, result);
    }

    [Fact]
    public void UpdateTodo_ThrowsTodoNotFoundException_IfTodoDoesNotExist()
    {
        // Arrange
        var todo = new Todo
        {
            Id = Guid.NewGuid(),
            Description = "Test",
            IsPending = true
        };

        // Act

        // Assert
        Assert.Throws<TodoNotFoundException>(() => _todoRepository.UpdateTodo(todo));
    }

    [Fact]
    public void DeleteTodoById_DeletesTodo()
    {
        // Arrange
        var todo = new Todo
        {
            Id = Guid.NewGuid(),
            Description = "Test",
            IsPending = true
        };
        _todoRepository.AddTodo(todo);

        // Act
        _todoRepository.DeleteTodoById(todo.Id);

        // Assert
        Assert.Empty(_todoRepository.ListTodos());
    }

    [Fact]
    public void DeleteTodoById_ThrowsTodoNotFoundException_IfTodoDoesNotExist()
    {
        // Arrange
        var todo = new Todo
        {
            Id = Guid.NewGuid(),
            Description = "Test",
            IsPending = true
        };

        // Act

        // Assert
        Assert.Throws<TodoNotFoundException>(() => _todoRepository.DeleteTodoById(todo.Id));
    }
}