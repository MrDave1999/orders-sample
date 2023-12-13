# sample order system 

A sample order system that uses ASP.NET Core.

## Technologies used

- Web API
  - [ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/release-notes/aspnetcore-7.0?view=aspnetcore-7.0)
  - [Pomelo.EntityFrameworkCore.MySql](https://github.com/PomeloFoundation/Pomelo.EntityFrameworkCore.MySql)
  - [Scrutor](https://github.com/khellang/Scrutor)
  - [Fluent Validation](https://github.com/FluentValidation/FluentValidation)
  - [DotEnv.Core](https://github.com/MrDave1999/dotenv.core)
  - [SimpleResults](https://github.com/MrDave1999/SimpleResults)

- Testing
  - [NUnit](https://github.com/nunit/nunit)
  - [Fluent Assertions](https://github.com/fluentassertions/fluentassertions)
  - [Respawn](https://github.com/jbogard/Respawn)
  - [JustMockLite](https://github.com/telerik/JustMockLite)

## Patterns used

- [Vertical Slice Architecture](https://garywoodfine.com/implementing-vertical-slice-architecture)
- [Repository pattern](https://martinfowler.com/eaaCatalog/repository.html)
- [CQRS](https://en.wikipedia.org/wiki/Command_Query_Responsibility_Segregation) without [MediatR](https://github.com/jbogard/MediatR)

## Common design principles

- Separation of concerns
- Explicit dependencies
- Open-closed principle

## Installation

- Web API
  - Install [.NET SDK 7.0](https://dotnet.microsoft.com/en-us/download/dotnet/7.0). 
  - Install [MariaDb Server](https://mariadb.com/downloads) and set the user as root.
  - Create a file called `.env`. Example:
  ```.env
  copy src/WebApi/.env.example src/WebApi/.env
  ```
  - Configure the connection string in the .env file.
  - Apply migrations with the command:
  ```sh
  dotnet ef database update --project src/WebApi/WebApi.csproj
  ```
  - Run `dotnet run` for a dev server. Navigate to `https://localhost:7120/swagger/`.

- Testing
  - Create a file called `test.env`. Example:
  ```.env
  copy tests/IntegrationTests/sample.env tests/IntegrationTests/test.env
  ```
  - Specify your credentials in the `test.env`.
  - Run the `dotnet test` command to run the tests.