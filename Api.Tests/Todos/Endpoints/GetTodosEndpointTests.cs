using Api.Todos.Endpoints;
using Domain.Todos;
using Domain.Todos.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;

namespace Api.Tests.Todos.Endpoints;

public class GetTodosEndpointTests
{
    private readonly Mock<HttpContext> _httpContextMock;
    private readonly Mock<IMediator> _mediatorMock;

    public GetTodosEndpointTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _httpContextMock = new Mock<HttpContext>();
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsOkResult()
    {
        // Arrange
        var todos = new List<Todo>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Description = "Test",
                IsPending = true
            }
        };
        _mediatorMock.Setup(x => x.Send(It.IsAny<GetTodosQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(todos);

        // Act
        var result =
            (Ok<IReadOnlyCollection<Todo>>)await GetTodosEndpoint.ExecuteAsync(_httpContextMock.Object,
                _mediatorMock.Object);

        // Assert
        Assert.Equal(200, result.StatusCode);
        Assert.Equivalent(todos, result.Value);
    }
}