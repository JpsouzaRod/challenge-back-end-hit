using Microsoft.AspNetCore.Mvc;
using StarWarsChallenge.Adapter.Postgres.Context;
using StarWarsChallenge.Adapter.Postgres.Repository;
using StarWarsChallenge.Domain.Application.Interface;
using StarWarsChallenge.Domain.Application.Usecase;
using StarWarsChallenge.Domain.Core.Models;
using System.Xml.Linq;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IPlanetUsecase, PlanetUsecase>();
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
    return Results.Ok(result);
});

app.MapPost("/planet", (IPlanetUsecase usecase, PlanetRequest planet) =>
{
    usecase.AddPlanet(planet);
    return Results.Ok("Planeta cadastrado com sucesso");
});

app.MapPost("/planet/id/{id}", (IPlanetUsecase usecase, int id) =>
{
    var result = usecase.FindPlanetById(id);
    return Results.Ok(result);
});

app.MapGet("/planet/{name}", (IPlanetUsecase usecase, string name) =>
{
    var result = usecase.FindPlanetByName(name);
    return Results.Ok(result);
});

app.MapDelete("/planet/{id}", (IPlanetUsecase usecase, int id) =>
{
    usecase.RemovePlanetById(id);
    return Results.Ok("Planeta excluido com sucesso");
});

app.Run();

