using System;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.Models;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;


namespace Infrastructure.Services
{
    public class UserService : IUserService
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;
        private readonly ITokenService _tokenService;

        public UserService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IOptions<JWT> jwt,
       

        ApplicationDbContext context, ITokenService tokenService)
        {
            _tokenService = tokenService;
            
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
           

        }

    public async Task<ServiceResponse<LogInResponse>> LogInAsync(LogInModel model)
    {
        ServiceResponse<LogInResponse> response = new ServiceResponse<LogInResponse>();
        

        var userWithEmail = await _userManager.FindByEmailAsync(model.Email);
        if(userWithEmail.Email != model.Email)
        {
            response.Success = false;
            response.Message = "no user with this email ";
            return response;
        }
        if (userWithEmail == null)
        {
            response.Success = false;
            response.Message = "no user with this email ";
            return response;
        }
        var res = new LogInResponse();
        res.UserName = userWithEmail.UserName;
        res.Token = await _tokenService.CreateToken(userWithEmail);
        if (userWithEmail != null)
        {
            var roles = await _userManager.GetRolesAsync(userWithEmail);
            var passCheck = await _userManager.CheckPasswordAsync(userWithEmail, model.Password);
            if(!passCheck)
            {
                response.Message ="wrong password";
                response.Success=false;
            }
            if(passCheck)
            {
                 response.Data = res;
                 response.Success=true;
                 response.Message="log in done";
            }
           

        }
        return response;




    }

    public async Task<ServiceResponse<string>> RegisterAsync(RegisterModel model)
    {
        ServiceResponse<string> response = new ServiceResponse<string>();
        ApplicationUser user = new ApplicationUser
        {
            UserName = model.Username,
            Email = model.Email,
            FirstName = model.FirstName,
            LastName = model.LastName,
            SecurityStamp = Guid.NewGuid().ToString(),
        };
        var userWithSameEmail = await _userManager.FindByEmailAsync(model.Email);
        if (userWithSameEmail != null)
        {
            response.Success = false;
            response.Message = "User already exists.";
            return response;
        }
        if (userWithSameEmail == null)
        {
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, DefaulUserRole.default_role.ToString());
                response.Data = user.UserName;
                response.Message = "user registration done! ";
            }
            return response;


            // return $"User Registered with username {user.UserName}";
        }
        else
        {
            throw new Exception($"Email {user.Email } is already registered.");
            //return $"Email {user.Email } is already registered.";
        }
    }
}
}