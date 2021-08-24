using Identity_API.IRepository;
using Identity_API.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity_API
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<IUser, User>();
            services.AddTransient<ILoginRepository, LoginRepository>();

        }

    }
}
