using AutoMapper;
using Domain.DTOs;
using TwitterCloneCompulsory.Business_Entities;
using TwitterCloneCompulsory.Interfaces;
using TwitterCloneCompulsory.Models;
using TwitterCloneCompulsory.Repo;

var builder = WebApplication.CreateBuilder(args);

var mapperConfig = new MapperConfiguration(cfg =>
{
    cfg.CreateMap<ExtendedLoginDto, Login>()
       .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Username))
       .ForMember(dest => dest.PasswordHash, opt => opt.Ignore()); 

    cfg.CreateMap<ExtendedLoginDto, UserDto>()
       .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
       .ForMember(dest => dest.Firstname, opt => opt.MapFrom(src => src.Firstname))
       .ForMember(dest => dest.Lastname, opt => opt.MapFrom(src => src.Lastname))
       .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.Age));
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
// Add services to the container.

builder.Services.AddDbContext<AuthenticationContext>();
builder.Services.AddScoped<IAuthRepo, AuthRepo>();
builder.Services.AddScoped<IValidationService, ValidationService>();

builder.Services.AddHttpClient();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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