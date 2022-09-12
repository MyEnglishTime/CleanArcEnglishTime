using MediatR;
using WebApi.Domain.Models;

namespace WebApi.Services.Queries;

public sealed class FetchWordQueryHandler : IRequestHandler<FetchWordsQuery, List<WordModel>>
{
    public Task<List<WordModel>> Handle(FetchWordsQuery query, CancellationToken cancellationToken)
        => Task.FromResult(new List<WordModel>() { new WordModel() { TextEn = "En", TextRu = "Ru" }});
}