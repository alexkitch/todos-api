using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Domain.Todos.Commands;

public sealed record DeleteTodoCommand : IRequest<Guid>
{
    [Required] public Guid Id { get; init; }
}