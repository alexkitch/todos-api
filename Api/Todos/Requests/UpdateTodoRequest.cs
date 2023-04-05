using System.ComponentModel.DataAnnotations;

namespace Api.Todos.Requests;

public sealed record UpdateTodoRequest
{
    [Required] public Guid? Id { get; set; }

    [Required] public string? Description { get; set; }

    [Required] public bool? IsPending { get; set; }
}