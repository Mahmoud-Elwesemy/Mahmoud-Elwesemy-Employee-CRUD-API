
using Employee.Core.Application;
using Employee.Core.Application.Abstraction;
using Employee.Core.Application.Mapping;
using Employee.Core.Domin.Repositories.contract;
using Employee.Core.Domin.UnitOfWork.Contract;
using Employee.Infrastructure.Presistence.Data;
using Employee.Infrastructure.Presistence.Repositories;
using Employee.Infrastructure.Presistence.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Emp.Task
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Configure Services
            // Add DB Connection
            builder.Services.AddDbContext<EmployeeContext>(options =>
            {
                options.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("con"));
            });
            builder.Services.AddScoped(typeof(IGenericRepository<,>),typeof(GenericRepository<,>));
            builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
            builder.Services.AddScoped(typeof(IServiceManager),typeof(ServiceManager));
            builder.Services.AddAutoMapper(typeof(MappingProfile));

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddOpenApi();
            // Add CORS policy
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAngularApp",policy =>
                {
                    policy.WithOrigins("http://localhost:4200")
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .SetIsOriginAllowedToAllowWildcardSubdomains()
                          .AllowCredentials(); // Allow cookies if needed
                });
            });

           
            #endregion

            var app = builder.Build();


            #region  Configure Middleware
            // Configure the HTTP request pipeline.
            if(app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwaggerUI(op => op.SwaggerEndpoint("/openapi/v1.json","v1"));//enable swagger
            }

            app.UseCors("AllowAngularApp");
            app.UseHttpsRedirection();
            app.UseAuthorization();

            app.MapControllers();
            #endregion

            app.Run();
        }
    }
}
