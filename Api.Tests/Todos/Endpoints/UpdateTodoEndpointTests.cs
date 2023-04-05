using Api.Todos.Endpoints;
using Api.Todos.Requests;
using Domain.Shared.Exceptions;
using Domain.Todos;
using Domain.Todos.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;

namespace Api.Tests.Todos.Endpoints;

public class UpdateTodoEndpointTests
{
    private readonly Mock<HttpContext> _httpContextMock;
    private readonly Mock<IMediator> _mediatorMock;

    public UpdateTodoEndpointTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _httpContextMock = new Mock<HttpContext>();
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsOkResult_IfTodoExists()
    {
        // Arrange
        var body = new UpdateTodoRequest
        {
            Id = Guid.NewGuid(),
            Description = "Test",
            IsPending = true
        };
        var todo = new Todo
        {
            Id = body.Id.Value,
            Description = body.Description,
            IsPending = body.IsPending.Value
        };
        _mediatorMock.Setup(x => x.Send(
                It.Is<UpdateTodoCommand>(c =>
                    c.Id == body.Id &&
                    c.Description == body.Description &&
                    c.IsPending == body.IsPending),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(todo);

        // Act
        var result =
            (Ok<Todo>)await UpdateTodoEndpoint.ExecuteAsync(_httpContextMock.Object, _mediatorMock.Object, body);

        // Assert
        Assert.Equal(200, result.StatusCode);
        Assert.Equivalent(todo, result.Value);
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsNotFoundResult_IfTodoDoesNotExist()
    {
        // Arrange
        var id = Guid.NewGuid();
        _mediatorMock.Setup(x => x.Send(It.IsAny<UpdateTodoCommand>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new TodoNotFoundException(id));

        // Act
        var result = (NotFound)await UpdateTodoEndpoint.ExecuteAsync(_httpContextMock.Object, _mediatorMock.Object,
            new UpdateTodoRequest());

        // Assert
        Assert.Equal(404, result.StatusCode);
    }
}