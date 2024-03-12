using AutoMapper;
using Domain;
using Domain.DTOs;
using PostApplication;
using PostApplication.Interfaces;
using PostInfrastructure;
using PostInfrastructure.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var mapperConfig = new MapperConfiguration(config =>{
    config.CreateMap<PostDto, Post>();
}).CreateMapper();
builder.Services.AddSingleton(mapperConfig);
builder.Services.AddDbContext<PostDBContext>();
PostApplication.DependencyResolver.DependencyResolverService.RegisterServices(builder.Services);
PostInfrastructure.DependencyResolver.DependencyResolverService.RegisterServices(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();