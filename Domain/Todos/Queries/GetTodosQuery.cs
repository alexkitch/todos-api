using MediatR;

namespace Domain.Todos.Queries;

public sealed record GetTodosQuery : IRequest<IReadOnlyCollection<Todo>>
{
}