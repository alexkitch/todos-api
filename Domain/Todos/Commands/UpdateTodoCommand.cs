using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Domain.Todos.Commands;

public sealed record UpdateTodoCommand : IRequest<Todo>
{
    [Required] public Guid? Id { get; init; }

    public string? Description { get; init; }

    [Required] public bool? IsPending { get; init; }
}