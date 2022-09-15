using FluentValidation;
using Microsoft.AspNetCore.Identity;
using WebApi.Domain.Entities;
using WebApi.Domain.Models.Auth;

namespace WebApi.Services.Validators.Auth;

public sealed class RegisterCommandValidator: AbstractValidator<RegisterCommand>
{
    const int MIN_PASSWORD_LENGTH = 6;
        
    private readonly UserManager<UserEntity> _userManager;
    
    public RegisterCommandValidator(UserManager<UserEntity> userManager)
    {
        _userManager = userManager;
        
        RuleFor(x => x.UserName).MustAsync((x, _token) => IsUserNameAvailableAsync(x)).WithMessage("User name is already taken.");
        RuleFor(x => x.Password).MinimumLength(MIN_PASSWORD_LENGTH);
    }

    private async Task<bool> IsUserNameAvailableAsync(string userName)
    {
        var user = await _userManager.FindByNameAsync(userName);
        return user is null;
    }
}