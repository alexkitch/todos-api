using System.ComponentModel.DataAnnotations;
using Domain.Todos;
using Domain.Todos.Commands;
using Microsoft.Extensions.Logging;
using Moq;

namespace Domain.Tests.Todos.Commands;

public class UpdateTodoCommandHandlerTests
{
    private readonly UpdateTodoCommandHandler _handler;
    private readonly Mock<ITodoWriteService> _writeServiceMock;

    public UpdateTodoCommandHandlerTests()
    {
        _writeServiceMock = new Mock<ITodoWriteService>();
        var loggerMock = new Mock<ILogger<UpdateTodoCommandHandler>>();
        _handler = new UpdateTodoCommandHandler(_writeServiceMock.Object, loggerMock.Object);
    }

    [Fact]
    public async Task Handle_ValidRequest_ShouldReturnTodo()
    {
        // Arrange
        var request = new UpdateTodoCommand
        {
            Id = Guid.NewGuid(),
            Description = "Description",
            IsPending = true
        };
        var todo = new Todo
        {
            Id = request.Id.Value,
            Description = request.Description,
            IsPending = request.IsPending.Value
        };
        _writeServiceMock.Setup(x => x.UpdateTodoAsync(request.Id.Value, request.Description, request.IsPending.Value,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(todo);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.Equivalent(todo, result);
    }

    [Fact]
    public async Task Handle_MissingId_ShouldThrowException()
    {
        // Arrange
        var request = new UpdateTodoCommand
        {
            Description = "Description",
            IsPending = true
        };

        // Act
        var exception =
            await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(request, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_MissingIsPending_ShouldThrowException()
    {
        // Arrange
        var request = new UpdateTodoCommand
        {
            Id = Guid.NewGuid(),
            Description = "Description"
        };

        // Act
        var exception =
            await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(request, CancellationToken.None));
    }
}