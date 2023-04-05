using Api.Todos.Endpoints;
using Domain.Shared.Exceptions;
using Domain.Todos.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;

namespace Api.Tests.Todos.Endpoints;

public class DeleteTodoEndpointTests
{
    private readonly Mock<HttpContext> _httpContextMock;
    private readonly Mock<IMediator> _mediatorMock;

    public DeleteTodoEndpointTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _httpContextMock = new Mock<HttpContext>();
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsOkResult_IfTodoExists()
    {
        // Arrange
        var id = Guid.NewGuid();
        _mediatorMock.Setup(x => x.Send(It.IsAny<DeleteTodoCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(id);

        // Act
        var result = (Ok)await DeleteTodoEndpoint.ExecuteAsync(_httpContextMock.Object, _mediatorMock.Object, id);

        // Assert
        Assert.Equal(200, result.StatusCode);
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsNotFoundResult_IfTodoDoesNotExist()
    {
        // Arrange
        var id = Guid.NewGuid();
        _mediatorMock.Setup(x => x.Send(It.Is<DeleteTodoCommand>(x => x.Id == id), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new TodoNotFoundException(id));

        // Act
        var result = (NotFound)await DeleteTodoEndpoint.ExecuteAsync(_httpContextMock.Object, _mediatorMock.Object, id);

        // Assert
        Assert.Equal(404, result.StatusCode);
    }
}