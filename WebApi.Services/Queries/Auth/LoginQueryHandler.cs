using System.Net;
using MediatR;
using Microsoft.AspNetCore.Identity;
using WebApi.Domain.Abstractions;
using WebApi.Domain.Entities;
using WebApi.Domain.Models.Auth;

namespace WebApi.Services.Queries.Auth;

public sealed class LoginQueryHandler : IRequestHandler<LoginQuery, LoginQueryResult>
{
    private readonly UserManager<UserEntity> _userManager;
    private readonly SignInManager<UserEntity> _signInManager;
    private readonly IJwtGenerator _jwtGenerator;
    
    public LoginQueryHandler(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager, IJwtGenerator jwtGenerator)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _jwtGenerator = jwtGenerator;
    }

    public async Task<LoginQueryResult> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        //todo in validator
        var user = await _userManager.FindByNameAsync(query.UserName);
        if (user == null)
        {
            throw new Exception(HttpStatusCode.Unauthorized.ToString());
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, query.Password, false);

        if (result.Succeeded)
        {
            return new LoginQueryResult { AccessToken = _jwtGenerator.CreateToken(user) };
        }
        
        throw new Exception(HttpStatusCode.Unauthorized.ToString());
    }
}