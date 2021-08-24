using Identity_API.DbContexts;
using Identity_API.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog.Fluent;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Log = Serilog.Log;

using System.IO;

using System.Security.Claims;
using System.Security.Principal;

using System.Text.RegularExpressions;


using Microsoft.AspNetCore.Diagnostics;

using Microsoft.AspNetCore.Http;

using Newtonsoft.Json;

namespace Identity_API
{
    public class Startup
    {
        private readonly IHostEnvironment _env;
        public Startup(IConfiguration configuration, IHostEnvironment env)
        {
            _env = env;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            JwtConfiguration(services);
            services.AddSession(o =>
            {
                o.IdleTimeout = TimeSpan.FromSeconds(1800);
            });

            services.Configure<GoogleConfigModel>(Configuration.GetSection(GoogleConfigModel.GoogleConfig));

            services.AddControllers();

            if (_env.IsProduction())
            {
                string connection = Environment.GetEnvironmentVariable("ConnectionString");

                services.AddDbContext<DBContextCom>(options => options.UseSqlServer(connection), ServiceLifetime.Transient);

                //Connection.testAPI = connection;
            }
            else
            {
                var data = Configuration.GetConnectionString("Development");

                services.AddDbContext<DBContextCom>(options => options.UseSqlServer(data));

                //Connection.testAPI = Configuration.GetConnectionString("Development");
            }
            services.AddHttpContextAccessor();

            services.Configure<Helper.Audience>(Configuration.GetSection("Audience"));

            //services.AddTransient<ISignUp, SignUp>();
            //services.AddTransient<IUserLogIn, UserLogIn>();
            //services.AddTransient<IGmailVerification, GmailVerification>();

            //Log.Logger = new LoggerConfiguration()
            //   .Enrich.FromLogContext()
            //   .Enrich.WithExceptionDetails()
            //   .Enrich.WithMachineName()
            //   .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elasticUri))
            //   {
            //       AutoRegisterTemplate = true,
            //   })
            //.CreateLogger();

            services.AddControllers(opts =>
            {
                if (_env.IsDevelopment())
                {
                    opts.Filters.Add<AllowAnonymousFilter>();
                }
                else
                {
                    var authenticatedUserPolicy = new AuthorizationPolicyBuilder()
                          .RequireAuthenticatedUser()
                          .Build();
                    opts.Filters.Add(new AuthorizeFilter(authenticatedUserPolicy));
                }

            });

            RegisterServices(services);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Identity_API", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "Enter the request header in the following box to add Jwt To grant authorization Token: Bearer Token",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });


                c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
            {
            new OpenApiSecurityScheme
            {
            Reference = new OpenApiReference {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
            }
            },
            new string[] { }
            }
            });

            });

            services.AddMemoryCache();
            services.AddMvc();

        }
        private void RegisterServices(IServiceCollection services)
        {
            DependencyContainer.RegisterServices(services);
        }

        private void JwtConfiguration(IServiceCollection services)
        {
            var audienceConfig = Configuration.GetSection("Audience");
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(audienceConfig["Secret"]));

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                ValidateIssuer = true,
                ValidIssuer = audienceConfig["Iss"],
                ValidateAudience = true,
                ValidAudience = audienceConfig["Aud"],
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                RequireExpirationTime = true,
            };

            //services.AddAuthentication(o =>
            //{
            //    o.DefaultAuthenticateScheme = "AuthScheme";


            //}).AddScheme("AuthScheme", x =>
            //    {
            //        x.RequireHttpsMetadata = false;
            //        x.TokenValidationParameters = tokenValidationParameters;

            //    });

        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseCors(x => x
                   .AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader());

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            //app.UseMiddleware<UserInfoMiddleware>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Identity_API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
