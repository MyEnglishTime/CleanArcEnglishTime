using AutoMapper;
using MediatR;
using WebApi.Domain.Abstractions;
using WebApi.Domain.Entities;
using WebApi.Domain.Models;

namespace WebApi.Services.Commands;

public sealed class CreateWordCommandHandler : IRequestHandler<CreateWordCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public CreateWordCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateWordCommand request, CancellationToken cancellationToken)
    {
        var word = _mapper.Map<WordEntity>(request);
        await _unitOfWork.Words.CreateAsync(word);
        await _unitOfWork.SaveChangesAsync();
        return word.Id;
    }
}