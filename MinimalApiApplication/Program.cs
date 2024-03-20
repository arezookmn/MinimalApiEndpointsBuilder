using FluentValidation;
using Mapster;
using Microsoft.EntityFrameworkCore;
using MinimalApiApplication.Context;
using MinimalApiApplication.EndpointBuilder;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(opt => opt.UseInMemoryDatabase(databaseName: "MinimalApiDb"));
builder.Services.AddMapster();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.MapEntitiesEndpoint();

app.Run();


