using System.Threading.Tasks;
using Core.Models;

namespace Core.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(ApplicationUser user);
    }
}