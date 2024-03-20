using FluentValidation;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinimalApiApplication.Context;
using MinimalApiApplication.Entities;
using System.ComponentModel.DataAnnotations;
namespace MinimalApiApplication.EndpointBuilder;
public class EndpointBuilder<TEntity> where TEntity : BaseEntity
{
    private readonly IEndpointRouteBuilder _builder;
    private RouteGroupBuilder _routeGroup;
    public EndpointBuilder(IEndpointRouteBuilder builder)
    {
        _builder = builder;
        _routeGroup = builder.MapGroup($"/api/{typeof(TEntity).Name.ToLower()}/");
    }

    public EndpointBuilder<TEntity> WithCreate<TRequest, TResponse>()
    {
        _routeGroup.MapPost("/", async (ApplicationDbContext dbContext, [FromBody] TRequest requestEntity, [FromServices] IValidator<TRequest> validator, [FromServices] IMapper mapper) =>
        {
            var validationResult = validator.Validate(requestEntity);
            if (!validationResult.IsValid)
                return Results.ValidationProblem(validationResult.ToDictionary()); 

            var entity = mapper.Map<TEntity>(requestEntity);
            dbContext.Add(entity); 
            await dbContext.SaveChangesAsync();
            var responseEntity = mapper.Map<TResponse>(entity);
            return Results.Ok(responseEntity);
        });
        return this;
    }

    public EndpointBuilder<TEntity> WithUpdate<TRequest>()
    {
        _routeGroup.MapPut("/{id}", async ([FromServices] ApplicationDbContext dbContext, [FromServices] IValidator<TRequest> validator, [FromRoute] int id, [FromBody] TRequest requestDto,[FromServices] IMapper mapper) =>
        {
            var validationResult = validator.Validate(requestDto);
            if(!validationResult.IsValid)
                return Results.ValidationProblem(validationResult.ToDictionary());

            var request = mapper.Map<TEntity>(requestDto);
            var dbSet = dbContext.Set<TEntity>();
            var entity = await dbSet.FirstOrDefaultAsync(e => e.Id == id);
            if (entity != null)
            {
                request.Id = entity.Id;
                dbContext.Entry(entity).CurrentValues.SetValues(request);
                await dbContext.SaveChangesAsync();
                return Results.Ok("Entity update successfully");
            }

            return Results.NotFound("Entity not found");
        });
        return this;
    }

    public EndpointBuilder<TEntity> WithDelete()
    {

        _routeGroup.MapDelete("/{id}", async ([FromServices] ApplicationDbContext dbContext, [FromRoute] int id) =>
        {
            var dbSet = dbContext.Set<TEntity>();
            var entity = await dbSet.FindAsync(id);
            if (entity != null)
            {
                entity.IsDeleted = true;
                await dbContext.SaveChangesAsync();
                return Results.Ok("Entity deleted successfully");
            }

            return Results.NotFound("Entity not found");
        });
        return this;
    }

    public EndpointBuilder<TEntity> WithGetAll<TResponse>()
    {
        _routeGroup.MapGet("/", async ([FromServices] ApplicationDbContext dbContext, [FromServices] IMapper mapper) =>
        {
            var responses =await dbContext.Set<TEntity>().ProjectToType<TResponse>().ToListAsync();
            return Results.Ok(responses);
        });
        return this;
    }


    public EndpointBuilder<TEntity> WithGetById<TResponse>()
    {
        _routeGroup.MapGet("/{id}", async ([FromServices] ApplicationDbContext dbContext, [FromRoute] int id, [FromServices] IMapper mapper) =>
        {
            var dbSet = dbContext.Set<TEntity>();
            var entity = await dbSet.FindAsync(id);
            if (entity != null)
            {
                var responseEntity = mapper.Map<TResponse>(entity);
                return Results.Ok(responseEntity);
            }
            return Results.NotFound("Entity Not Found");
        });
        return this;
    }
}