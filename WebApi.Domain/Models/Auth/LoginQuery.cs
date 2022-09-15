using MediatR;

namespace WebApi.Domain.Models.Auth;

public sealed class LoginQuery: IRequest<LoginQueryResult>
{
    public string UserName { get; set; }
    public string Password { get; set; }
}

public sealed class LoginQueryResult
{
    public string AccessToken { get; set; }
}