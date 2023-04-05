using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using MediatR;

namespace Domain.Shared.ValidatedRequestHandler;

public abstract class ValidatedRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
    {
        var validationResults = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(request, new ValidationContext(request), validationResults, true);
        if (!isValid) throw new ValidationException(JsonSerializer.Serialize(validationResults));
        return HandleValidated(request, cancellationToken);
    }

    [ExcludeFromCodeCoverage(Justification = "This method is implemented in the derived classes")]
    public virtual Task<TResponse> HandleValidated(TRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}