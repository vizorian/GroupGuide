using Group_Guide.Auth;
using Group_Guide.Auth.Model;
using Group_Guide.Data;
using Group_Guide.Data.Dtos.Auth;
using Group_Guide.Data.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Group_Guide
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });


            services.AddIdentity<GroupGuideUser, IdentityRole>()
                .AddEntityFrameworkStores<GroupGuideContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters.ValidAudience = _configuration["JWT:ValidAudience"];
                    options.TokenValidationParameters.ValidIssuer = _configuration["JWT:ValidIssuer"];
                    options.TokenValidationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(PolicyNames.SameUser, policy => policy.Requirements.Add(new SameUserRequirement()));
                options.AddPolicy(PolicyNames.UserBelongs, policy => policy.Requirements.Add(new UserBelongsRequirement()));
            });

            services.AddDbContext<GroupGuideContext>();
            services.AddAutoMapper(typeof(Startup));
            services.AddControllers();
            services.AddTransient<IGamesRepository, GamesRepository>();
            services.AddTransient<ICampaignsRepository, CampaignsRepository>();
            services.AddTransient<ISessionsRepository, SessionsRepository>();
            services.AddTransient<ITopicsRepository, TopicsRepository>();
            services.AddTransient<IPostsRepository, PostsRepository>();
            services.AddTransient<ITokenManager, TokenManager>();
            services.AddTransient<DatabaseSeeder, DatabaseSeeder>();

            services.AddTransient<IAuthorizationHandler, MainAuthorizationHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
