using System.Reflection;
using System.Text;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebApi.Database.Common;
using WebApi.Database.Repositories;
using WebApi.Domain.Abstractions;
using WebApi.Domain.Abstractions.Repositories;
using WebApi.Domain.Entities;
using WebApi.Domain.Models;
using WebApi.Framework;
using WebApi.Framework.Jwt;
using WebApi.Services.Commands;
using WebApi.Services.Mappers;
using WebApi.Services.Queries.Auth;
using WebApi.Services.Validators;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(WordsMapperProfile));

builder.Services.AddIdentityCore<UserEntity>()
    .AddEntityFrameworkStores<EnglishTimeContext>()
    .AddSignInManager<SignInManager<UserEntity>>();
builder.Services.AddSingleton<ISystemClock, SystemClock>();
builder.Services.AddScoped<IJwtGenerator, JwtGenerator>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(
        opt =>
        {
            opt.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtToken"])),
                ValidateAudience = false,
                ValidateIssuer = false,
            };
        });	

//TODO: add repositories dynamically
builder.Services.AddTransient<IWordRepository, WordRepository>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddDbContext<EnglishTimeContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));

//TODO: investigate add assembly without specify type
builder.Services.AddMediatR(Assembly.GetExecutingAssembly(), typeof(CreateWordCommand).Assembly, typeof(CreateWordCommandHandler).Assembly, typeof(ValidationBehavior<,>).Assembly, typeof(LoginQueryHandler).Assembly);

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();