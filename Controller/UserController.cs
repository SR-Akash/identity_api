using Identity_API.DTO;
using Identity_API.Helper;
using Identity_API.IRepository;
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
    public class UserController : ControllerBase
    {
        private readonly IUser _IRepository;

        public UserController(IUser IRepository)
        {
            _IRepository = IRepository;
        }


        [HttpPost]
        [Route("CreateUser")]
        public async Task<MessageHelper> CreateUser (List<User> Create)
        {
            var msg = await _IRepository.CreateUser(Create);
            return msg;

        }

    }
}
