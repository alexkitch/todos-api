using Domain.Shared.Exceptions;
using Domain.Todos.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Todos.Endpoints;

public static class DeleteTodoEndpoint
{
    public static async Task<IResult> ExecuteAsync(HttpContext context, IMediator mediator, [FromRoute] Guid id)
    {
        try
        {
            var response = await mediator.Send(new DeleteTodoCommand
            {
                Id = id
            });

            return Results.Ok();
        }
        catch (TodoNotFoundException)
        {
            return Results.NotFound();
        }
    }
}