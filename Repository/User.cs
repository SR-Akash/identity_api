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

        public async Task<MessageHelper> CreateUser(List<DTO.User> cretae)
        {
            try
            {
                var userList = new List<Models.TblUser>();

                foreach(var itm in cretae)
                {
                    var check = _context.TblUsers.Where(x => x.StrPhone == itm.PhoneNo
                    || x.StrEmail == itm.EmailAddress && x.IsActive == true).FirstOrDefault();

                    if (check != null)
                        throw new Exception("User Already Exist");

                    var data = new Models.TblUser
                    {
                        StrUserName = itm.UserName,
                        StrPassword = itm.Password,
                        StrEmail = itm.EmailAddress,
                        StrPhone = itm.PhoneNo,
                        IsActive = true,
                        IsMasterUser = itm.isMasterUser,
                        DteInsertTime = DateTime.Now
                    };

                    userList.Add(data);
                }


                await _context.TblUsers.AddRangeAsync(userList);
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

        public async Task<List<DTO.User>> GetUserList()
        {
            return await Task.FromResult((from a in _context.TblUsers
                                          where a.IsActive == true
                                          select new DTO.User
                                          {
                                              UserId = a.IntUserId,
                                              UserName = a.StrUserName,
                                              PhoneNo = a.StrPhone,
                                              EmailAddress = a.StrEmail,
                                              Password = a.StrPassword,
                                              isMasterUser = a.IsMasterUser
                                          }).ToList());
        }
    }
}
