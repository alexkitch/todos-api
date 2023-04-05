using MediatR;

namespace Domain.Todos.Commands;

public sealed record CreateTodoCommand : IRequest<Todo>
{
    public string? Description { get; init; } = string.Empty;
}