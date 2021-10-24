using AutoMapper;
using FluentValidation.AspNetCore;
using LearnIt.Courses.Data;
using LearnIt.Courses.Data.Repositories;
using LearnIt.Courses.Domain.Configuration;
using LearnIt.Courses.Domain.Models;
using LearnIt.Courses.Domain.Services;
using LearnIt.Courses.WebHost.Filters;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace LearnIt.Courses.WebHost
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
            services.AddDbContext<CoursesDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<ICourseRepository, CourseRepository>();

            var mapperConfig = new MapperConfiguration(mc => mc.AddProfile(new MappingProfile()));

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services
                .AddControllers(opt=>opt.Filters.Add(typeof(ValidatorActionFilter)))
                .AddFluentValidation(fv => {
                    fv.RegisterValidatorsFromAssemblyContaining<CourseRequestDtoValidator>();
                    fv.DisableDataAnnotationsValidation = true;
                });

            services.AddFluentValidationRulesToSwagger();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "LearnIt.Courses.WebHost", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, CoursesDbContext dataContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "LearnIt.Courses.WebHost v1"));
            }
            dataContext.Database.Migrate();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}