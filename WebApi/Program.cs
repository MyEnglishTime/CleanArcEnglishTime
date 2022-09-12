using System.Reflection;
using MediatR;
using WebApi.Domain.Models;
using WebApi.Services.Commands;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//TODO: investigate add assembly without specify type
builder.Services.AddMediatR(Assembly.GetExecutingAssembly(), typeof(CreateWordCommand).Assembly, typeof(CreateWordCommandHandler).Assembly);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();