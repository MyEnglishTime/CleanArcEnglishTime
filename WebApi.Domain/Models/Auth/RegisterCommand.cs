using MediatR;

namespace WebApi.Domain.Models.Auth;

public class RegisterCommand: IRequest
{
    public string UserName { get; set; }
    public string Password { get; set; }
}