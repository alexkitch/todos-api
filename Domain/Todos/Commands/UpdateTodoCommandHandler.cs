using Domain.Shared.ValidatedRequestHandler;
using Microsoft.Extensions.Logging;

namespace Domain.Todos.Commands;

public class UpdateTodoCommandHandler : ValidatedRequestHandler<UpdateTodoCommand, Todo>
{
    private readonly ITodoWriteService _writeService;
    private readonly ILogger<UpdateTodoCommandHandler> _logger;

    public UpdateTodoCommandHandler(ITodoWriteService writeService, ILogger<UpdateTodoCommandHandler> logger)
    {
        _writeService = writeService;
        _logger = logger;
    }

    public override async Task<Todo> HandleValidated(UpdateTodoCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating Todo with Id {Id}", request.Id);
        var todo = await _writeService.UpdateTodoAsync(request.Id!.Value, request.Description, request.IsPending!.Value,
            cancellationToken);
        return todo;
    }
}