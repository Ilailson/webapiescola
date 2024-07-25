using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.Data;
using Newtonsoft.Json;
using AutoMapper;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

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
             var serverVersion = new MySqlServerVersion(new Version(8, 0, 21));

             services.AddDbContext<SmartContext>(options =>
                options.UseMySql(
                    Configuration.GetConnectionString("MysqlConnection"),
                    serverVersion
            ));


            services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });



            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); //automapper
            //toda vez que utilizar o IRepository... Inserindo repository
            services.AddScoped<IRepository, Repository>(); //injeção dependencia

            services.AddVersionedApiExplorer(options =>{
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            })
            .AddApiVersioning(options =>{
                options.DefaultApiVersion = new ApiVersion(1,0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
            });


            var apiProviderDescription = services.BuildServiceProvider().GetService<IApiVersionDescriptionProvider>();

            services.AddSwaggerGen(options => 
            {
                foreach (var description in apiProviderDescription.ApiVersionDescriptions)
                {
                    options.SwaggerDoc(
                    description.GroupName,
                    new Microsoft.OpenApi.Models.OpenApiInfo()
                    {
                        Title = "SmartSchoolAPI",
                        Version = description.ApiVersion.ToString(),
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
                    });
                }

                

                //exibir xml... documentação
                var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);
                options.IncludeXmlComments(xmlCommentsFullPath);
            });
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
                               IApplicationBuilder app, 
                               IWebHostEnvironment env,
                               IApiVersionDescriptionProvider apiProviderDescription )
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

                foreach (var description in apiProviderDescription.ApiVersionDescriptions)
                {
                     options.SwaggerEndpoint(
                        $"/swagger/{description.GroupName}/swagger.json", 
                        description.GroupName.ToUpperInvariant()
                    );
                }

               
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