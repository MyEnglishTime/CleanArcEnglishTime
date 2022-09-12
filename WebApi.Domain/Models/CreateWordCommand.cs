using MediatR;

namespace WebApi.Domain.Models;

public sealed class CreateWordCommand: IRequest<int>
{
    public string TextRu { get; set; }
    public string TextEn { get; set; }
}