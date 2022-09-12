using MediatR;
using WebApi.Domain.Models;

namespace WebApi.Services.Commands;

public sealed class CreateWordCommandHandler : IRequestHandler<CreateWordCommand, int>
{
    public CreateWordCommandHandler()
    {
    }

    public async Task<int> Handle(CreateWordCommand request, CancellationToken cancellationToken)
    {
        Console.WriteLine("Created word");
        return 999;
    }
}