using MediatR;

namespace WebApi.Domain.Models.Auth;

public sealed class LoginQuery: IRequest<string>
{
    public string UserName { get; set; }
    public string Password { get; set; }
}