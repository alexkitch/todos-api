using Domain.Todos;
using Domain.Todos.Commands;
using Microsoft.Extensions.Logging;
using Moq;

namespace Domain.Tests.Todos.Commands;

public class DeleteTodoCommandHandlerTests
{
    private readonly DeleteTodoCommandHandler _handler;
    private readonly Mock<ITodoWriteService> _writeServiceMock;

    public DeleteTodoCommandHandlerTests()
    {
        _writeServiceMock = new Mock<ITodoWriteService>();
        Mock<ILogger<DeleteTodoCommandHandler>> loggerMock = new();
        _handler = new DeleteTodoCommandHandler(_writeServiceMock.Object, loggerMock.Object);
    }

    [Fact]
    public async Task Handle_DeletesTodo()
    {
        // Arrange
        var request = new DeleteTodoCommand
        {
            Id = Guid.NewGuid()
        };
        _writeServiceMock.Setup(x => x.DeleteTodoAsync(request.Id, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask)
            .Verifiable();

        var result = await _handler.Handle(request, CancellationToken.None);
        _writeServiceMock.Verify(x => x.DeleteTodoAsync(request.Id, It.IsAny<CancellationToken>()),
            Times.Once);
    }
}