using DataAccess.Todos;
using Domain.Todos;

namespace DataAccess.Tests.Todos;

public class TodoReadServiceTests
{
    private readonly ITodoReadService _todoReadService;
    private readonly ITodoRepository _todoRepository;

    public TodoReadServiceTests()
    {
        _todoRepository = new TodoRepository();
        _todoReadService = new TodoReadService(_todoRepository);
    }

    [Fact]
    public async Task GetAllTodos_ReturnsEmptyList_IfNoTodosExist()
    {
        // Arrange

        // Act
        var todos = await _todoReadService.GetAllTodos(CancellationToken.None);

        // Assert
        Assert.Empty(todos);
    }

    [Fact]
    public async Task GetAllTodos_ReturnsTodos_IfTodosExist()
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
        var todos = await _todoReadService.GetAllTodos(CancellationToken.None);

        // Assert
        Assert.Single(todos);
        Assert.Equal(todo, todos.First());
    }
}