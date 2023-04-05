namespace Domain.Todos;

public sealed record Todo
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public string? Description { get; init; }

    public bool IsPending { get; init; } = true;

    public DateTime DateAdded { get; init; } = DateTime.UtcNow;
}