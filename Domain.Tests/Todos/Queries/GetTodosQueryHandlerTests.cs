using Domain.Todos;
using Domain.Todos.Queries;
using Microsoft.Extensions.Logging;
using Moq;

namespace Domain.Tests.Todos.Queries;

public class GetTodosQueryHandlerTests
{
    private readonly GetTodosQueryHandler _handler;
    private readonly Mock<ITodoReadService> _readServiceMock;

    public GetTodosQueryHandlerTests()
    {
        _readServiceMock = new Mock<ITodoReadService>();
        var loggerMock = new Mock<ILogger<GetTodosQueryHandler>>();
        _handler = new GetTodosQueryHandler(_readServiceMock.Object, loggerMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnTodos()
    {
        // Arrange
        var todos = new List<Todo>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Description = "Todo 1",
                IsPending = true,
                DateAdded = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.NewGuid(),
                Description = "Todo 2",
                IsPending = false,
                DateAdded = DateTime.UtcNow.AddDays(1)
            }
        };
        _readServiceMock.Setup(x => x.GetAllTodos(It.IsAny<CancellationToken>()))
            .ReturnsAsync(todos);

        // Act
        var result = await _handler.Handle(new GetTodosQuery(), CancellationToken.None);

        // Assert
        Assert.Equivalent(result, todos);
    }
}