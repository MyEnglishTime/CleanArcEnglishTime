using WebApi.Domain.Entities;

namespace WebApi.Domain.Abstractions;

public interface IJwtGenerator
{
    string CreateToken(UserEntity user);
}