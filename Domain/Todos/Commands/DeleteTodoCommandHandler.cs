using Domain.Shared.ValidatedRequestHandler;
using Microsoft.Extensions.Logging;

namespace Domain.Todos.Commands;

public class DeleteTodoCommandHandler : ValidatedRequestHandler<DeleteTodoCommand, Guid>
{
    private readonly ITodoWriteService _writeService;
    private readonly ILogger<DeleteTodoCommandHandler> _logger;

    public DeleteTodoCommandHandler(ITodoWriteService writeService, ILogger<DeleteTodoCommandHandler> logger)
    {
        _writeService = writeService;
        _logger = logger;
    }

    public override async Task<Guid> HandleValidated(DeleteTodoCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting Todo with Id {Id}", request.Id);
        await _writeService.DeleteTodoAsync(request.Id, cancellationToken);
        return request.Id;
    }
}