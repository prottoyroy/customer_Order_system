using System.Threading.Tasks;
using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CustomerController : BaseApiController
    {
        private readonly IUserService _userService;
        public CustomerController(IUserService userService)
        {
            _userService = userService;

        }
        [HttpPost("register")]
        public async Task<ActionResult> RegisterAsync(RegisterModel model)
        {

           ServiceResponse<string> response = await _userService.RegisterAsync(model);
           if(!response.Success)
           return BadRequest(response);
           return Ok(response);
        }
         [HttpPost("log-in")]
        public async Task<ActionResult> LogInAsync(LogInModel model)
        {

           ServiceResponse<LogInResponse> response = await _userService.LogInAsync(model);
           if(!response.Success)
           return BadRequest(response);
           return Ok(response);
        }
    }
}