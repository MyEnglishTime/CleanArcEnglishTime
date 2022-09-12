using MediatR;
using WebApi.Domain.Abstractions;
using WebApi.Domain.Entities;
using WebApi.Domain.Models;

namespace WebApi.Services.Commands;

public sealed class CreateWordCommandHandler : IRequestHandler<CreateWordCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;
    public CreateWordCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CreateWordCommand request, CancellationToken cancellationToken)
    {
        var word = new WordEntity {
            TextEn = "one",
            TextRu = "two"
        };
        var a = await _unitOfWork.Words.FetchAllAsync();
        await _unitOfWork.Words.CreateAsync(word);
        await _unitOfWork.SaveChangesAsync();
        return word.Id;
    }
}