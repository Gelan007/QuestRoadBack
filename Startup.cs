using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using QuestRoadBack.Contex;
using QuestRoadBack.Contracts;

using QuestRoadBack.Repositories;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuestRoadBack.Utils;

namespace QuestRoadBack
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.

        public void ConfigureServices(IServiceCollection services)
        {
            //var authOptionsConfiguration = Configuration.GetSection("Auth");
            //services.Configure<AuthOption>(authOptionsConfiguration);
            services.AddControllers();
            var authOptions = Configuration.GetSection("Auth").Get<AuthOption>();


            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options =>
               {
                   options.RequireHttpsMetadata = false;
                   options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                   {
                       ValidateIssuer = true,
                       ValidIssuer = authOptions.Issuer,
                       ValidateAudience = true,
                       ValidAudience = authOptions.Audience,
                       ValidateLifetime = true,
                       IssuerSigningKey = authOptions.GetSymmetricSecurityKey(),
                       ValidateIssuerSigningKey = true,


                   };
               });

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                    });

            });

            services.AddSingleton<DapperContext>();
            //services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IHelpRepository, HelpRepository>();
            services.AddScoped<IMemberRepository, MemberRepository>();
            services.AddScoped<ITeamRepository, TeamRepository>();
            services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddScoped<IQuestRepository, QuestRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();

            services.AddScoped<IProfileRepository, ProfileRepository>();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "QuestRoadBack", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "QuestRoadBack v1"));
            }
            


            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
