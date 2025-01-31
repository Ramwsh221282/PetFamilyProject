using Microsoft.EntityFrameworkCore;
using PetFamily.Api.Extensions;
using PetFamily.Api.Middleware;
using PetFamily.Infrastructure;
using PetFamily.Infrastructure.DependencyInjection;
using PetFamily.UseCases.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.AddLogging();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddPostgresDbServices();
builder.Services.AddUseCasesServices();
builder.Services.AddControllers();
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    await using var scope = app.Services.CreateAsyncScope();
    ApplicationDbContext context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await context.Database.EnsureCreatedAsync();
    await context.Database.MigrateAsync();
}
app.UseExceptionMiddleWare();
app.MapControllers();
app.UseHttpsRedirection();
app.UseHttpLogging();
app.Run();
