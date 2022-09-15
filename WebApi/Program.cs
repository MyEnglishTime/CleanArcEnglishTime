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

builder.Services.AddIdentityCore<UserEntity>(config =>
    {
        config.Password.RequireDigit = false;
        config.Password.RequireLowercase = false;
        config.Password.RequireNonAlphanumeric = false;
        config.Password.RequireUppercase = false;
        config.Password.RequiredLength = 6;
        config.Password.RequiredUniqueChars = 0;
    })
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

builder.Services.AddDbContext<EnglishTimeContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

var servicesAssembly = AppDomain.CurrentDomain.GetAssemblies().Single(x=> x.GetName().Name == $"{nameof(WebApi)}.{nameof(WebApi.Services)}");
builder.Services.AddMediatR(servicesAssembly);
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

builder.Services.AddValidatorsFromAssembly(servicesAssembly);
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