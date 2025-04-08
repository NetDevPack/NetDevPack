<img src="https://repository-images.githubusercontent.com/268701472/8bf84980-a6ce-11ea-83da-e2133c5a3a7a" alt=".NET DevPack" width="300px" />

| Package |  Version | Popularity |
| ------- | ----- | ----- |
| `NetDevPack` | [![NuGet](https://img.shields.io/nuget/v/NetDevPack.svg)](https://nuget.org/packages/NetDevPack) | [![Nuget](https://img.shields.io/nuget/dt/NetDevPack.svg)](https://nuget.org/packages/NetDevPack) |

## What is the .NET DevPack?

NetDevPack is a comprehensive set of reusable classes and interfaces designed to improve development experience and productivity in .NET applications. It encapsulates best practices and common patterns such as Domain-Driven Design (DDD), CQRS, Validation, Notification, and Mediator.

## Give a Star! ⭐

If you find this project useful, please give it a star! It helps us grow and improve the community.

## Features

- ✅ Domain-driven design base classes and interfaces  
- ✅ CQRS support via Mediator pattern  
- ✅ FluentValidation integration  
- ✅ Domain events and notifications handling  
- ✅ Unit of Work and repository base contracts

## Installation

Install via NuGet:

```bash
dotnet add package NetDevPack
```

## Basic Usage

### Domain Entity

```csharp
using NetDevPack.Domain;

public class Customer : Entity
{
    public string Name { get; private set; }

    public Customer(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}
```

### Repository Interface

```csharp
using NetDevPack.Data;

public interface ICustomerRepository : IRepository<Customer>
{
    Task<Customer> GetByName(string name);
}
```

### Using the Mediator

```csharp
public class CustomerCommandHandler : IRequestHandler<RegisterCustomerCommand, ValidationResult>
{
    private readonly IMediatorHandler _mediator;
    
    public CustomerCommandHandler(IMediatorHandler mediator)
    {
        _mediator = mediator;
    }

    public async Task<ValidationResult> Handle(RegisterCustomerCommand request, CancellationToken cancellationToken)
    {
        // Business logic
        await _mediator.PublishEvent(new CustomerRegisteredEvent(...));
        return new ValidationResult();
    }
}
```

### Fluent Validation

```csharp
using FluentValidation;

public class CustomerValidator : AbstractValidator<Customer>
{
    public CustomerValidator()
    {
        RuleFor(c => c.Name).NotEmpty();
    }
}
```

## Example

For a full implementation example, check [Equinox Project](https://github.com/EduardoPires/EquinoxProject/)

## Compatibility

Supports:
- ✅ .NET Standard 2.1  
- ✅ .NET 5 through 9 (including latest versions)  
- ⚠️ Legacy support for .NET Core 3.1 and older (with limitations)
  
## About
.NET DevPack was developed by [Eduardo Pires](http://eduardopires.net.br) under the [MIT license](LICENSE).
