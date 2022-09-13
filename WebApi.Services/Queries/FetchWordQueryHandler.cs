using AutoMapper;
using MediatR;
using WebApi.Domain.Abstractions;
using WebApi.Domain.Models;

namespace WebApi.Services.Queries;

public sealed class FetchWordQueryHandler : IRequestHandler<FetchWordsQuery, List<WordModel>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public FetchWordQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<WordModel>> Handle(FetchWordsQuery query, CancellationToken cancellationToken)
    {
        var words = await _unitOfWork.Words.FetchAllAsync();
        var result = _mapper.Map<IEnumerable<WordModel>>(words);
        return result.ToList();
    }
}