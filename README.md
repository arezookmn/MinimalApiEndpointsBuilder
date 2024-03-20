# MinimalApiEndpointsBuilder
In this project, we've developed a generic builder to create CRUD endpoints with minimal api for any entity class, simplifying API development.

## MinimalApiBuilder Project
MinimalApiBuilder is a builder for creating API endpoints based on entities, specifying DTO (Data Transfer Object) response and request objects. With this project, you can easily add as many endpoints as needed based on your requirements, all without writing extensive code for each one.

### Getting Started

Configure Your Endpoints: In your `program.cs` file, add the following code for each entity and corresponding DTO contracts:
```
app.MapEndpoint<TEntity>()
    .WithCreate<TRequest, TResponse>()
    .WithDelete()
    .WithUpdate<TRequest>()
    .WithGetAll<TResponse>()
    .WithGetById<TResponse>();
```

Replace TEntity, TRequest, and TResponse with the appropriate entity types for your specific use case.
That’s It!: With these simple steps, you’ve set up your API endpoints. Now you can focus on your business logic without worrying about repetitive boilerplate code.

### Example Usage

Suppose you have an entity called Product and corresponding DTOs ProductRequest and ProductResponse. Here’s how you’d configure the endpoints:
```
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapEndpoint<Product>()
    .WithGetAll<ProductResponse>()
    .WithDelete()
    .WithCreate<ProductRequest, ProductResponse>()
    .WithUpdate<ProductRequest>()
    .WithGetById<ProductResponse>();
```

### Contributing
Contributions are welcome! If you encounter any issues or have suggestions for improvements, feel free to open an issue or submit a pull request.
