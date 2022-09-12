using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Domain.Models;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class WordController : ControllerBase
{
    private readonly IMediator _mediator;
    public WordController(IMediator mediator)
    {
        _mediator = mediator;
    }    
    
    [HttpPost]
    public Task<int> CreateWordAsync(CreateWordCommand command)
        => _mediator.Send(command);
    
    [HttpGet]
    public Task<List<WordModel>> Get()
        => _mediator.Send(new FetchWordsQuery());
}