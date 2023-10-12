# Project "Dogs House"

This project is an example demonstrating the use of clean architecture with REST API, the CQRS pattern.

## Project Highlights

- **Clean Architecture** is used to simplify the development, testing, and maintenance of the application.
- The **CQRS (Command Query Responsibility Segregation)** pattern is applied to separate write operations (commands) from read operations (queries).
- **MsSql** is used as the database.
- **Entity Framework Core** is used as the Object-Relational Mapper (ORM) to manage the database.
- Testing is facilitated by the following libraries:
  - **xUnit**.
  - **Moq**.
  - **FluentAssertions**.

## Project Structure

```
- core/
  - DogsHouse.Domain/           # Definition of Dog model
  - DogsHouse.Application/      # Application layer containing command and query handlers for dogs feature
- infrastructure/
  - DogsHouse.DataAccess/   # Infrastructure code, including EF core code-first db configuration
- presentation/
  - DogsHouse.Api/              # Web API
- tests/
  - DogsHouse.Application.Tests/ # Tests for Application layer
  - DogsHouse.Presentation.Tests/ # Tests for Presentation layer            
```
