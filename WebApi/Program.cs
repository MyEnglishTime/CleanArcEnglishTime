using System.Reflection;
using FluentValidation;
using MediatR;
using WebApi.Domain.Models;
using WebApi.Framework;
using WebApi.Services.Commands;
using WebApi.Services.Validators;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//TODO: investigate add assembly without specify type
builder.Services.AddMediatR(Assembly.GetExecutingAssembly(), typeof(CreateWordCommand).Assembly, typeof(CreateWordCommandHandler).Assembly, typeof(ValidationBehavior<,>).Assembly);

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddValidatorsFromAssembly(typeof(CreateWordCommandValidator).Assembly);
builder.Services.AddTransient<ExceptionHandlingMiddleware>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();