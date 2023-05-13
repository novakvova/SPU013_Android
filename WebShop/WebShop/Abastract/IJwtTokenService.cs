using WebShop.Data.Entites.Identity;

namespace WebShop.Abastract
{
    public interface IJwtTokenService
    {
        Task<string> CreateToken(UserEntity user);
    }
}
