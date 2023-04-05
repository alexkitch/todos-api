using Api.Todos.Endpoints;
using Api.Todos.Requests;
using Domain.Todos;
using Domain.Todos.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;

namespace Api.Tests.Todos.Endpoints;

public class CreateTodoEndpointTests
{
    private readonly Mock<HttpContext> _httpContextMock;
    private readonly Mock<IMediator> _mediatorMock;

    public CreateTodoEndpointTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _httpContextMock = new Mock<HttpContext>();
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsOkResult()
    {
        // Arrange
        var todo = new Todo
        {
            Id = Guid.NewGuid(),
            Description = "Test",
            IsPending = true
        };
        _mediatorMock.Setup(x => x.Send(It.Is<CreateTodoCommand>(x => x.Description == todo.Description),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(todo);

        // Act
        var result = (Created<Todo>)await CreateTodoEndpoint.ExecuteAsync(_httpContextMock.Object, _mediatorMock.Object,
            new CreateTodoRequest
            {
                Description = "Test"
            });

        // Assert
        Assert.Equal(201, result.StatusCode);
        Assert.Equal(todo, result.Value);
        Assert.Equal(todo.Id.ToString(), result.Location);
    }
}