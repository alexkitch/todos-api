using Domain.Todos.Queries;
using MediatR;

namespace Api.Todos.Endpoints;

public static class GetTodosEndpoint
{
    public static async Task<IResult> ExecuteAsync(HttpContext context, IMediator mediator)
    {
        var response = await mediator.Send(new GetTodosQuery());
        return Results.Ok(response);
    }
}