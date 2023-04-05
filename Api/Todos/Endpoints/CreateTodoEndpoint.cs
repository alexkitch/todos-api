using Api.Todos.Requests;
using Domain.Todos.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Todos.Endpoints;

public static class CreateTodoEndpoint
{
    public static async Task<IResult> ExecuteAsync(HttpContext context, IMediator mediator,
        [FromBody] CreateTodoRequest body)
    {
        var response = await mediator.Send(new CreateTodoCommand
        {
            Description = body.Description
        });

        return Results.Created(response.Id.ToString(), response);
    }
}