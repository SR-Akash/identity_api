using Identity_API.DbContexts;
using Identity_API.Helper;
using Identity_API.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity_API.Repository
{
    public class User : IUser
    {
        private readonly DBContextCom _context;
        public User(DBContextCom context)
        {
            _context = context;
        }

        public async Task<MessageHelper> CreateUser(DTO.User cretae)
        {
            try
            {
                var check = _context.TblUsers.Where(x => x.StrPhone == cretae.PhoneNo
                || x.StrEmail == cretae.EmailId && x.IsActive == true).FirstOrDefault();

                if (check != null)
                    throw new Exception("User Already Exist");

                var data = new Models.TblUser
                {
                    StrUserName = cretae.UserName,
                    StrPassword = cretae.Password,
                    StrEmail = cretae.EmailId,
                    StrPhone = cretae.PhoneNo,
                    IsActive = true,
                    IsMasterUser = cretae.isMasterUser,
                    DteInsertTime = DateTime.Now
                };

                await _context.TblUsers.AddAsync(data);
                await _context.SaveChangesAsync();

                return new MessageHelper
                {
                    Message = "Create Successfully",
                    statuscode = 200
                };
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
