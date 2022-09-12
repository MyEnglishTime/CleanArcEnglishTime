using MediatR;

namespace WebApi.Domain.Models;

public sealed class FetchWordsQuery : IRequest<List<WordModel>>
{
}