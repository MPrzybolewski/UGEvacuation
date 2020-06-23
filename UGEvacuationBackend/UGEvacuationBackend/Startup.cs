using System.Text;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using UGEvacuationBLL.Services;
using UGEvacuationBLL.Services.Interfaces;
using UGEvacuationCommon.Models;
using UGEvacuationDAL;
using UGEvacuationDAL.Repositories;
using UGEvacuationDAL.Repositories.Interfaces;
using Swashbuckle.AspNetCore.Swagger;
using UGEvacuationDAL.Entities;

namespace UGEvacuationBackend
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
            FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile("key.json"),
            });
            
            var appSettings = new AppSettings();
            Configuration.Bind(appSettings);
            
            services.Configure<IAppSettings>(Configuration);
            services.AddSingleton<IAppSettings>(appSettings);
            
            // services.AddDbContext<UGEvacuationContext>(options =>
            //     options.UseSqlServer(Configuration.GetConnectionString("UGEvacuationContext")));
            
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<INotificationsService, NotificationService>();
            services.AddScoped<IRegistrationService, RegistrationService>();
            services.AddScoped<IEvacuationService, EvacuationService>();
            services.AddScoped<IEvacuationNodeService, EvacuationNodeService>();
            
            services.AddScoped<IAdminUserRepository, AdminUserRepository>();
            services.AddScoped<IAppUserRepository, AppUserRepository>();
            services.AddScoped<IEvacuationRepository, EvacuationRepository>();
            services.AddScoped<IEvacuationNodeRepository, EvacuationNodeRepository>();
            services.AddScoped<IEvacuationNodeAppUserRepository, EvacuationNodeAppUserRepository>();
            
            services.AddControllers();
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });
           
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)    
                .AddJwtBearer(options =>    
                {    
                    options.TokenValidationParameters = new TokenValidationParameters    
                    {    
                        ValidateIssuer = true,    
                        ValidateAudience = true,    
                        ValidateLifetime = true,    
                        ValidateIssuerSigningKey = true,    
                        ValidIssuer = Configuration["Jwt:Issuer"],    
                        ValidAudience = Configuration["Jwt:Issuer"],    
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))    
                    };    
                });    
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {   app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
            
            app.UseAuthentication();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
         
        }
    }
}
