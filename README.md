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

| NetDevPack | Target framework |
| ---------- | ---------------- |
| 9.x        | .NET 10 (LTS)    |
| 8.x        | .NET Standard 2.1 |

### Upgrading from 8.x to 9.x

- The package now targets **.NET 10** only. Stay on 8.x if you need .NET Standard 2.1.
- **FluentValidation 12**. See the [FluentValidation 12 upgrade guide](https://docs.fluentvalidation.net/en/latest/upgrading-to-12.html).
- **NetDevPack.SimpleMediator 1.2**: `IMediator`, `IRequest<T>`, `INotification` and the handler interfaces moved from `NetDevPack.SimpleMediator.Core.Interfaces` to the `NetDevPack.SimpleMediator` namespace. Replace `using NetDevPack.SimpleMediator.Core.Interfaces;` with `using NetDevPack.SimpleMediator;`.

## About
.NET DevPack was developed by [Eduardo Pires](http://eduardopires.net.br) under the [MIT license](LICENSE).
