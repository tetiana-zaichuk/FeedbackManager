using AutoMapper;
using FeedbackManager.BusinessLayer.Interfaces;
using FeedbackManager.BusinessLayer.Services;
using FeedbackManager.DataAccessLayer;
using FeedbackManager.DataAccessLayer.Data;
using FeedbackManager.DataAccessLayer.Entities;
using FeedbackManager.DataAccessLayer.Interfaces;
using FeedbackManager.Shared.Dtos;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FeedbackManager
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<ISurveyService, SurveyService>();
            services.AddTransient<IQuestionService, QuestionService>();
            services.AddTransient<ISurveyQuestionsService, SurveyQuestionsService>();


            services.AddDbContext<FeedbackManagerDbContext>(options =>
                options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=FeedbackManagerDb;Trusted_Connection=True"));
            //Configuration.GetConnectionString(",b => b.MigrationsAssembly(Configuration["MigrationsAssembly"])

            InitializeAutomapper(services);
        }

        public virtual IServiceCollection InitializeAutomapper(IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Survey, SurveyDto>();
                cfg.CreateMap<SurveyDto, Survey>();
                cfg.CreateMap<Question, QuestionDto>();
                cfg.CreateMap<QuestionDto, Question>();
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            return services;
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
