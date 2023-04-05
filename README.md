# todos-api

This is a fairly simply .NET Web API with Minimal API, CQRS and a 'relaxed' implementation of CLEAN architecture. This project doesn't really need this type of complexity given its scale,
but it's intended to show off some good practices.

The Domain layer provides business logic, and a data access contract for the Data layer to conform to along with the main Domain entity `Todo`. 
Domain level validation of inbound requests/commands is provided via a custom `ValidatedRequestHandler` class, which is essentially a validation wrapper around the MediatR `IRequestHandler` interface.

For the sake of simplicity, data storage is done in memory with a simple repository class which sits inside of the Data layer.

# Build and Test

Build and run the todos solution as normal using your preferred IDE or CLI.

If running from the CLI, you can run the following commands from the root of the solution:

```
dotnet build
dotnet test
dotnet run --project Api/Api.csproj
```

Test coverage should be at 100%, although this isn't really a very complex project in any case.

# Swagger

With the solution running locally, Swagger documentation can be viewed at http://localhost:5005/swagger/index.html