using AutoMapper;
using Domain;
using Domain.DTOs;
using EasyNetQ;
using PostApplication;
using PostApplication.Interfaces;
using PostInfrastructure;
using PostInfrastructure.Interfaces;
using PostService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton(new MessageClient(RabbitHutch.CreateBus("host=rabbitmq;port=5672;virtualHost=/;username=guest;password=guest")));
builder.Services.AddHostedService<MessageHandler>();
builder.Services.AddControllers();
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

app.UseAuthorization();

app.MapControllers();

app.Run();