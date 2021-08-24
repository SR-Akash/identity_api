using Identity_API.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity_API.IRepository
{
    public interface ILoginRepository
    {
        public Task<UserLoginDTO> LoginwithJWTGenerate (string phoneNo, string password);
    }
}
