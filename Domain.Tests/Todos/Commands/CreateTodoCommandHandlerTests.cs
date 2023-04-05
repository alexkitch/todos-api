using Domain.Todos;
using Domain.Todos.Commands;
using Microsoft.Extensions.Logging;
using Moq;

namespace Domain.Tests.Todos.Commands;

public class CreateTodoCommandHandlerTests
{
    private readonly CreateTodoCommandHandler _handler;
    private readonly Mock<ITodoWriteService> _writeServiceMock;

    public CreateTodoCommandHandlerTests()
    {
        _writeServiceMock = new Mock<ITodoWriteService>();
        Mock<ILogger<CreateTodoCommandHandler>> loggerMock = new();
        _handler = new CreateTodoCommandHandler(_writeServiceMock.Object, loggerMock.Object);
    }

    [Fact]
    public async Task Handle_CreatesAndReturnsTodo()
    {
        // Arrange
        var request = new CreateTodoCommand
        {
            Description = "Test"
        };
        var todo = new Todo
        {
            Id = Guid.NewGuid(),
            Description = request.Description,
            IsPending = true,
            DateAdded = DateTime.UtcNow
        };
        _writeServiceMock.Setup(x => x
                .CreateTodoAsync(request.Description, It.IsAny<CancellationToken>()))
            .ReturnsAsync(todo);

        var result = await _handler.Handle(request, CancellationToken.None);

        Assert.Equivalent(result, todo);
    }
}