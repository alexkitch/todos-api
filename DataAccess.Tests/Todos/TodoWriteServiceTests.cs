using DataAccess.Todos;
using Domain.Todos;

namespace DataAccess.Tests.Todos;

public class TodoWriteServiceTests
{
    private readonly ITodoRepository _todoRepository;
    private readonly ITodoWriteService _todoWriteService;

    public TodoWriteServiceTests()
    {
        _todoRepository = new TodoRepository();
        _todoWriteService = new TodoWriteService(_todoRepository);
    }

    [Fact]
    public async Task CreateTodoAsync_AddsTodo()
    {
        // Arrange
        var description = "Test";

        // Act
        var todo = await _todoWriteService.CreateTodoAsync(description, CancellationToken.None);

        // Assert
        Assert.Equal(description, todo.Description);
        Assert.True(todo.IsPending);
    }

    [Fact]
    public async Task UpdateTodoAsync_UpdatesTodo()
    {
        // Arrange
        var todo = new Todo
        {
            Id = Guid.NewGuid(),
            Description = "Test",
            IsPending = true,
            DateAdded = DateTime.UtcNow
        };
        _todoRepository.AddTodo(todo);

        var description = "Test2";
        var isPending = false;

        // Act
        var updatedTodo =
            await _todoWriteService.UpdateTodoAsync(todo.Id, description, isPending, CancellationToken.None);

        // Assert
        Assert.Equal(description, updatedTodo.Description);
        Assert.Equal(isPending, updatedTodo.IsPending);
        Assert.Equal(todo.DateAdded, updatedTodo.DateAdded);
    }

    [Fact]
    public async Task DeleteTodoAsync_DeletesTodo()
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
        await _todoWriteService.DeleteTodoAsync(todo.Id, CancellationToken.None);

        // Assert
        Assert.Empty(_todoRepository.ListTodos());
    }
}