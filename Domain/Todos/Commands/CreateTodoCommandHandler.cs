using Domain.Shared.ValidatedRequestHandler;
using Microsoft.Extensions.Logging;

namespace Domain.Todos.Commands;

public class CreateTodoCommandHandler : ValidatedRequestHandler<CreateTodoCommand, Todo>
{
    private readonly ITodoWriteService _writeService;
    private readonly ILogger<CreateTodoCommandHandler> _logger;

    public CreateTodoCommandHandler(ITodoWriteService writeService, ILogger<CreateTodoCommandHandler> logger)
    {
        _writeService = writeService;
        _logger = logger;
    }

    public override async Task<Todo> HandleValidated(CreateTodoCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating a new Todo");
        var todo = await _writeService.CreateTodoAsync(request.Description, cancellationToken);
        return todo;
    }
}