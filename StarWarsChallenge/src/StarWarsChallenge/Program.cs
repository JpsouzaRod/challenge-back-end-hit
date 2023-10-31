using Microsoft.AspNetCore.Mvc;
using StarWarsChallenge.Adapter.Postgres.Context;
using StarWarsChallenge.Adapter.Postgres.Repository;
using StarWarsChallenge.Adapter.Redis.Service;
using StarWarsChallenge.Adapter.StarWarsApi.Service;
using StarWarsChallenge.Domain.Application.Interface;
using StarWarsChallenge.Domain.Application.Usecase;
using StarWarsChallenge.Domain.Core.Models.Request;
using System.Diagnostics;
using System.Xml.Linq;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddStackExchangeRedisCache(x => {
    x.InstanceName = "Planet";
    x.Configuration = "localhost:";
});

builder.Services.AddScoped<DbContext>();

builder.Services.AddScoped<IPlanetCache, PlanetCache>();
builder.Services.AddScoped<IPlanetRepository, PlanetRepository>();
builder.Services.AddScoped<IPlanetUsecase, PlanetUsecase>();
builder.Services.AddScoped<IPlanetService, PlanetService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var sw = new Stopwatch();

app.MapGet("/planet", (IPlanetUsecase usecase) =>
{
    sw.Start();
        var result = usecase.ListPlanets();
    sw.Stop();

    Console.WriteLine("Tempo gasto     GET /planet : " + sw.ElapsedMilliseconds.ToString() + " milisegundos");
    Console.WriteLine("Tempo decorrido GET /planet: {0:hh\\:mm\\:ss}", sw.Elapsed);

    sw.Reset();

    if (result.StatusOk)
        return Results.Ok(result);
    else
        return Results.BadRequest(result);
});

app.MapPost("/planet", (IPlanetUsecase usecase, PlanetRequest planet) =>
{
    sw.Start();
        var result = usecase.AddPlanet(planet);
    sw.Stop();

    Console.WriteLine("Tempo gasto     POST /planet : " + sw.ElapsedMilliseconds.ToString() + " milisegundos");
    Console.WriteLine("Tempo decorrido POST /planet : {0:hh\\:mm\\:ss}", sw.Elapsed);

    sw.Reset();

    if (result.StatusOk)
        return Results.Ok(result);
    else
        return Results.BadRequest(result);
});

app.MapGet("/planet/{id}", (IPlanetUsecase usecase, int id) =>
{
    sw.Start();
        var result = usecase.FindPlanetById(id);
    sw.Stop();

    Console.WriteLine("Tempo gasto     GET /planet/id : " + sw.ElapsedMilliseconds.ToString() + " milisegundos");
    Console.WriteLine("Tempo decorrido GET /planet/id : {0:hh\\:mm\\:ss}", sw.Elapsed);

    sw.Reset();

    if (result.StatusOk)
        return Results.Ok(result);
    else
        return Results.BadRequest(result);
});

app.MapGet("/planet/name/{name}", (IPlanetUsecase usecase, string name) =>
{
    sw.Start();
        var result = usecase.FindPlanetByName(name);
    sw.Stop();

    Console.WriteLine("Tempo gasto     GET /planet/name : " + sw.ElapsedMilliseconds.ToString() + " milisegundos");
    Console.WriteLine("Tempo decorrido GET /planet/name : {0:hh\\:mm\\:ss}", sw.Elapsed);

    sw.Reset();

    if (result.StatusOk)
        return Results.Ok(result);
    else
        return Results.BadRequest(result);
});

app.MapDelete("/planet/{id}", (IPlanetUsecase usecase, int id) =>
{
    sw.Start();
    var result = usecase.RemovePlanetById(id);
    sw.Stop();

    Console.WriteLine("Tempo gasto     DELETE /planet/id: " + sw.ElapsedMilliseconds.ToString() + " milisegundos");
    Console.WriteLine("Tempo decorrido DELETE /planet/id: {0:hh\\:mm\\:ss}", sw.Elapsed);

    sw.Reset();

    if (result.StatusOk)
        return Results.Ok(result);
    else
        return Results.BadRequest(result);
});

app.Run();

