using System.Diagnostics.CodeAnalysis;
using Api.Todos.Endpoints;
using DataAccess.Todos;
using Domain;
using Domain.Todos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDomainServices();
builder.Services.AddSingleton<ITodoRepository, TodoRepository>();
builder.Services.AddScoped<ITodoReadService, TodoReadService>();
builder.Services.AddScoped<ITodoWriteService, TodoWriteService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(b =>
    {
        b.AllowAnyOrigin();
        b.AllowAnyMethod();
        b.AllowAnyHeader();
    });
}

app.UseHttpsRedirection();
app.MapControllers();

app.MapGet("/todos", GetTodosEndpoint.ExecuteAsync);
app.MapPost("/todos", CreateTodoEndpoint.ExecuteAsync);
app.MapPut("/todos", UpdateTodoEndpoint.ExecuteAsync);
app.MapDelete("/todos/{id:guid}", DeleteTodoEndpoint.ExecuteAsync);

app.Run();


[ExcludeFromCodeCoverage(Justification = "This file is just for service registration and configuration.")]
public partial class Program
{
}