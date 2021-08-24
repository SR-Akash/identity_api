using Identity_API.DTO;
using Identity_API.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity_API.IRepository
{
    public interface IUser
    {
        public Task<MessageHelper> CreateUser(List<User> cretae);
        public Task<List<User>> GetUserList();
    }
}
