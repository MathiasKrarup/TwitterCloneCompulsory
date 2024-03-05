var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

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

UserApplication.DependencyResolver.DependencyResolverService.RegisterServices(builder.Services);
UserInfrastructure.DependencyResolverService.DependencyResolverService.RegisterServices(builder.Services);


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();