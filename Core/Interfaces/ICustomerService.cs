using System.Threading.Tasks;
using Core.Models;

namespace Core.Interfaces
{
    public interface ICustomerService
    {
        Task<ServiceResponse<string>> RegisterAsync(RegisterModel model);
        Task<ServiceResponse<LogInResponse>> LogInAsync(LogInModel model);
        


    }
}