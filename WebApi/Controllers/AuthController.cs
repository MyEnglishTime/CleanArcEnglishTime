using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Domain.Models.Auth;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("login")]
    public Task<string> LoginAsync(LoginQuery query)
        => _mediator.Send(query);
    
    [HttpPost("register")]
    public Task RegisterAsync(RegisterCommand command)
        => _mediator.Send(command);
}