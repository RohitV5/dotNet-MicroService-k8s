using Microsoft.EntityFrameworkCore;
using PlatformService.Data;
using PlatformService.Extensions;
using PlatformService.SyncDataServices.Http;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDatabaseService(builder.Configuration, builder.Environment);
builder.Services.AddScoped<IPlatformRepo, PlatformRepo>();
builder.Services.AddHttpClient<ICommandDataClient, HttpCommandDataClient>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
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

//Seeding DB
PrepDb.PrepPopulation(app, app.Environment.IsProduction());

// This is causing issue inside kubernetest
// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
