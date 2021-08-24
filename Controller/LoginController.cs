using Identity_API.DTO;
using Identity_API.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity_API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginRepository _IRepository;

        public LoginController(ILoginRepository IRepository)
        {
            _IRepository = IRepository;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("LoginwithJWTGenerate")]
        public async Task<IActionResult> LoginwithJWTGenerate(LoginDTO login)
        {
            try
            {
                //_logger.LogInformation($"sme Token Generate by User {user.UserName.ToString()}! : {DateTime.UtcNow}");
                return Ok(await _IRepository.LoginwithJWTGenerate(login.PhoneNo, login.Password));
            }
            catch (Exception )
            {
                throw;
            }
        }
    }
}
