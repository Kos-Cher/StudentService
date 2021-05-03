using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Student.Application.Commands;
using Student.Application.Interfaces;
using Student.Domain;
using Student.Persistence;
using Student.Service.Filters;

namespace Student.Service
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime.
        // Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddDbContext<StudentDbContext>(options =>
            options.UseSqlServer(
                Configuration.GetConnectionString("DefaultConnection"),
                sqlOptions => sqlOptions.EnableRetryOnFailure()));
            var assemblies = new List<Assembly>();

            var assembliesForScanning = new Assembly[]
              {
                    typeof(Group).GetTypeInfo().Assembly,               
                    typeof(Program).GetTypeInfo().Assembly,
                     typeof(CreateGroupCommand).GetTypeInfo().Assembly
              };

            services.AddSingleton(Configuration);
            services.AddScoped<IStudentDbContext, StudentDbContext>();


            services.AddMediatR(assembliesForScanning);
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));


            services.AddMemoryCache();


            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(CustomExceptionFilter));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddSwaggerGen(options => {
                options.SwaggerDoc("v1",new OpenApiInfo
                                        { 
                                            Title = "Student service", 
                                            Version = "v1", 
                                            Description = "Student API" }
                                        );
                var fileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var filePath = Path.Combine(AppContext.BaseDirectory, fileName);
                options.IncludeXmlComments(filePath);
            });

        }

        // This method gets called by the runtime.
        // Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {          
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();

                // Apply migrations onstartup
                using (var serviceScope = app.ApplicationServices.CreateScope())
                {
                    var context = serviceScope.ServiceProvider.GetService<StudentDbContext>();
                    context.Database.Migrate();
                    Trace.WriteLine("Database migrated successfully.");
                }
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseHttpsRedirection();

            app.UseCors("AllowSpecificOrigin");

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Student API V1");
            });
        }
    }
}
