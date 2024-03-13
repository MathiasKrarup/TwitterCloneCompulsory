using System.Text;
using AutoMapper;
using Domain;
using Domain.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using UserInfrastructure;

var builder = WebApplication.CreateBuilder(args);

var mapperConfig = new MapperConfiguration(configuration =>
{
    configuration.CreateMap<UserDto, User>()
      .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
      .ForMember(dest => dest.Firstname, opt => opt.MapFrom(src => src.Firstname))
      .ForMember(dest => dest.Lastname, opt => opt.MapFrom(src => src.Lastname))
      .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.Age))
      .ReverseMap();
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
            ValidateIssuer = false,
            ValidateAudience = false,
        };
    });

builder.Services.AddDbContext<UserDbContext>();
builder.Services.AddHttpClient();

UserApplication.DependencyResolver.DependencyResolverService.RegisterServices(builder.Services);
UserInfrastructure.DependencyResolverService.DependencyResolverService.RegisterServices(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}






app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();