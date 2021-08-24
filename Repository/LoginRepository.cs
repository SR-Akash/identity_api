using Identity_API.DbContexts;
using Identity_API.DTO;
using Identity_API.Helper;
using Identity_API.IRepository;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Identity_API.Repository
{
    public class LoginRepository : ILoginRepository
    {
        private readonly DBContextCom _context;
        private IOptions<Audience> _settings;
        public LoginRepository(DBContextCom context, IOptions<Audience> setting)
        {
            _context = context;
           this._settings = setting;
        }

        public async Task<UserLoginDTO> LoginwithJWTGenerate(string phoneNo, string password)
        {
            //phoneNo = phoneNo.Length == 14 ? phoneNo : "+88" + phoneNo;

            //if (phoneNo.Length != 14)
            //{
            //    throw new Exception("Phone No is Invalid");
            //}

            var findPhone = _context.TblUsers.Where(x => x.StrPhone == phoneNo && x.IsActive == true).FirstOrDefault();

            if (findPhone == null)
            {
                throw new Exception("You entired an invalid phone No :" + phoneNo);
            }

            var findPass = _context.TblUsers.Where(x => x.StrPhone == phoneNo && x.IsActive == true).Select(a => a.StrPassword).FirstOrDefault();

            if (findPass.ToLower().Trim() != password.ToLower().Trim())
            {
                throw new Exception("You entired wrong password");
            }

            var data = _context.TblUsers.Where(x => x.StrPhone == phoneNo
            && x.StrPassword == password && x.IsActive == true).FirstOrDefault();

            UserLoginDTO entity = new UserLoginDTO();

            var token = await GenerateToken();

            entity.UserId = data.IntUserId;
            entity.UserName = data.StrUserName;
            entity.Password = data.StrPassword;
            entity.EmailAddress = data.StrEmail;
            entity.PhoneNo = data.StrPhone;
            entity.isMasterUser = data.IsMasterUser;
            entity.auth = token;

            return entity;


        }
        public async Task<AuthDTO> GenerateToken()
        {
            var now = DateTime.UtcNow;
            try
            {
                var claims = new Claim[]
               {

                    new Claim(ClaimTypes.Role,"test"),
                    new Claim("enroll",AesOperation.EncryptString("b14ca5898a4e4133bbce2ea2315a1916","test")),
                    new Claim("terantId",AesOperation.EncryptString("b14ca5898a4e4133bbce2ea2315a1916", "Test")),
                    new Claim(JwtRegisteredClaimNames.Sub, "test"),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, now.ToUniversalTime().ToString(), ClaimValueTypes.Integer64)
                };

                //_env.IsProduction()? Configuration.GetSection("REACT_APP_SECRET_VALUE").Value.Trim():

                var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_settings.Value.Secret));

                var jwt = new JwtSecurityToken(
                    issuer: _settings.Value.Iss,
                    audience: _settings.Value.Aud,
                    claims: claims,
                    //notBefore: now,
                    //expires: now.Add(TimeSpan.FromMinutes(60 * 24 * 1)), //expires after 1day
                    expires: now.Add(TimeSpan.FromMinutes(60 * 60 * 24 * 1)), //expires after 1day
                    signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
                );
                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);


                return new AuthDTO
                {
                    Success = true,
                    Token = encodedJwt,
                    //RefreshToken = encodedJwt,
                    //expires_in = (int)TimeSpan.FromMinutes(60).TotalSeconds,
                    expires_in = (int)TimeSpan.FromHours(24).TotalHours,
                    //ActionTime = TimeZones.GetCurrentDatTime()//actionTime.ToString("yyyy-MM-dd hh:mm:ss.ff")
                };

            }
            catch (Exception)
            {
                throw new Exception("data not match");
            }
        }
    }
}
