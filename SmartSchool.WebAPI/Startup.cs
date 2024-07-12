using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.Data;
using Newtonsoft.Json;
using AutoMapper;
using System.Reflection;

namespace SmartSchool.WebAPI
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
            services.AddDbContext<SmartContext>(
                context => context.UseSqlite(Configuration.GetConnectionString("default"))
            );

            services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });



            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); //automapper
            //toda vez que utilizar o IRepository... Inserindo repository
            services.AddScoped<IRepository, Repository>(); //injeção dependencia

            services.AddSwaggerGen(options => 
            {
                options.SwaggerDoc(
                    "smartschoolapi",
                    new Microsoft.OpenApi.Models.OpenApiInfo()
                    {
                        Title = "SmartSchoolAPI",
                        Version = "1.0",
                        TermsOfService = new Uri("https://siga.iec.gov.br"),
                        Description = "A descrição da WebAPI do SmartSchool",
                        License = new Microsoft.OpenApi.Models.OpenApiLicense
                        {
                            Name = "SmartSchool License",
                            Url = new Uri("https://siga.iec.gov.br")
                        },
                        Contact = new Microsoft.OpenApi.Models.OpenApiContact
                        {
                            Name = "Ilailson Rocha",
                            Email = "ilailson@hotmail.com",
                            Url = new Uri("https://programadamente.com")
                        }
                    }
                );

                //exibir xml... documentação
                var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);
                options.IncludeXmlComments(xmlCommentsFullPath);
            });
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseSwagger()
               .UseSwaggerUI(options => 
               {
                options.SwaggerEndpoint("/swagger/smartschoolapi/swagger.json", "smartschoolapi");
                options.RoutePrefix="";
               });

            // app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}