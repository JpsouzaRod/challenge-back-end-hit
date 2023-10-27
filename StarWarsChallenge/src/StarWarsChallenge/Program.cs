using Microsoft.AspNetCore.Mvc;
using StarWarsChallenge.Adapter.Postgres.Context;
using StarWarsChallenge.Adapter.Postgres.Repository;
using StarWarsChallenge.Adapter.StarWarsApi.Service;
using StarWarsChallenge.Domain.Application.Interface;
using StarWarsChallenge.Domain.Application.Usecase;
using StarWarsChallenge.Domain.Core.Models;
using System.Xml.Linq;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IPlanetUsecase, PlanetUsecase>();
builder.Services.AddScoped<IPlanetService, PlanetService>();
builder.Services.AddScoped<IPlanetRepository, PlanetRepository>();
builder.Services.AddScoped<DbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("/planet", (IPlanetUsecase usecase) =>
{
    var result = usecase.ListPlanets();
    
    if (result.StatusOk)
        return Results.Ok(result);
    else
        return Results.BadRequest(result);
    

});

app.MapPost("/planet", (IPlanetUsecase usecase, PlanetRequest planet) =>
{
    var result = usecase.AddPlanet(planet);
    if (result.StatusOk)
        return Results.Ok(result);
    else
        return Results.BadRequest(result);
});

app.MapGet("/planet/{id}", (IPlanetUsecase usecase, int id) =>
{
    var result = usecase.FindPlanetById(id);

    if (result.StatusOk)
        return Results.Ok(result);
    else
        return Results.BadRequest(result);
});

app.MapGet("/planet/name/{name}", (IPlanetUsecase usecase, string name) =>
{
    var result = usecase.FindPlanetByName(name);

    if (result.StatusOk)
        return Results.Ok(result);
    else
        return Results.BadRequest(result);
});

app.MapDelete("/planet/{id}", (IPlanetUsecase usecase, int id) =>
{
    var result = usecase.RemovePlanetById(id);

    if (result.StatusOk)
        return Results.Ok(result);
    else
        return Results.BadRequest(result);
});

app.Run();

