using MediatR;
using Microsoft.AspNetCore.Identity;
using WebApi.Domain.Entities;
using WebApi.Domain.Models.Auth;

namespace WebApi.Services.Commands.Auth;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand>
{
    private readonly UserManager<UserEntity> _userManager;
    
    public RegisterCommandHandler(UserManager<UserEntity> userManager)
    {
        _userManager = userManager;
    }
    
    public async Task<Unit> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        await _userManager.CreateAsync(new UserEntity()
        {
            UserName = request.UserName,
            FirstName = "",
            LastName = ""
        }, request.Password);
        
        return Unit.Value;
    }
}