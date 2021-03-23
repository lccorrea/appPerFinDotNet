//using System.Runtime.Intrinsics.Arm.Arm64;
//using System.Buffers;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using AutoMapper;
//using Microsoft.AspNetCore.HttpsPolicy;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Logging;
using System.IO;
using System;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using appPerfinAPI.Data;

namespace appPerfinAPI
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
            services.AddDbContext<DataContext>(
                context => { 
                    context.UseSqlite(Configuration.GetConnectionString("Default"));
                }
            );
            services.AddControllers();//.AddNewtonsoftJson(
                    //opt => opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
                    // -- instalar via nuget [Parar loop infinito no select no primeiro nivel];
                    
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<IRepository, Repository>();
            services.AddSwaggerGen( options => {
                options.SwaggerDoc("appPerFinAPI", new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Title = "appPerFinAPI",
                    Version = "1.0.0"
                });

                var XMLSwaggerName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var XMLSwaggerFullPath = Path.Combine(AppContext.BaseDirectory, XMLSwaggerName);
                options.IncludeXmlComments(XMLSwaggerFullPath);
            }

            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseSwagger()
                .UseSwaggerUI( options => 
                {
                    options.SwaggerEndpoint("/swagger/appPerFinAPI/swagger.json", "appperfinapi");
                    options.RoutePrefix = "";
                });

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
