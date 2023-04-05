namespace Domain.Shared.Exceptions;

public sealed class TodoNotFoundException : Exception
{
    public TodoNotFoundException(Guid id) : base($"Todo with id {id} not found.")
    {
    }
}