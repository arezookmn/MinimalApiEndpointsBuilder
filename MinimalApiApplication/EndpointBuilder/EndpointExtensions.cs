using MinimalApiApplication.Contracts.TodoContracts;
using MinimalApiApplication.Entities;

namespace MinimalApiApplication.EndpointBuilder;

public static class EndpointExtensions
{
    public static IEndpointRouteBuilder MapEntitiesEndpoint(this IEndpointRouteBuilder builder)
    {
        builder.MapEndpoint<Todo>()
                .WithGetAll<TodoResponse>()
                .WithDelete()
                .WithCreate<CreateTodoRequest, TodoResponse>()
                .WithUpdate<UpdateTodoRequest>()
                .WithGetById<TodoResponse>();

        return builder;
    }
    public static EndpointBuilder<TEntity> MapEndpoint<TEntity>(this IEndpointRouteBuilder builder) where TEntity : BaseEntity
    {
        var endpointBuilder = new EndpointBuilder<TEntity>(builder);
        return endpointBuilder;
    }
}
