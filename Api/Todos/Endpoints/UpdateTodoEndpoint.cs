using Api.Todos.Requests;
using Domain.Shared.Exceptions;
using Domain.Todos.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Todos.Endpoints;

public static class UpdateTodoEndpoint
{
    public static async Task<IResult> ExecuteAsync(HttpContext context, IMediator mediator,
        [FromBody] UpdateTodoRequest body)
    {
        try
        {
            var response = await mediator.Send(new UpdateTodoCommand
            {
                Id = body.Id,
                Description = body.Description,
                IsPending = body.IsPending
            });

            return Results.Ok(response);
        }
        catch (TodoNotFoundException)
        {
            return Results.NotFound();
        }
    }
}