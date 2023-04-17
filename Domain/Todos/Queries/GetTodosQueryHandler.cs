using Domain.Shared.ValidatedRequestHandler;
using Microsoft.Extensions.Logging;

namespace Domain.Todos.Queries;

public class GetTodosQueryHandler : ValidatedRequestHandler<GetTodosQuery, IReadOnlyCollection<Todo>>
{
    private readonly ITodoReadService _readService;
    private readonly ILogger<GetTodosQueryHandler> _logger;

    public GetTodosQueryHandler(ITodoReadService readService, ILogger<GetTodosQueryHandler> logger)
    {
        _readService = readService;
        _logger = logger;
    }

    public override async Task<IReadOnlyCollection<Todo>> HandleValidated(GetTodosQuery query,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all Todos");
        var todos = await _readService.GetAllTodos(cancellationToken);
        return todos.OrderBy(x => x.DateAdded).ToList();
    }
}