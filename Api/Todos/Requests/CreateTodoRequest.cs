using System.ComponentModel.DataAnnotations;

namespace Api.Todos.Requests;

public sealed record CreateTodoRequest
{
    [Required] public string? Description { get; set; }
}