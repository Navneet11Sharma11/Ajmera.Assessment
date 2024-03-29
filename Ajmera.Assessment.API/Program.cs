using Ajmera.Assessment.DL.Repositories;
using Ajmera.Assessment.DL;
using Microsoft.EntityFrameworkCore;
using Ajmera.Assessment.DL.Models;
using Ajmera.Assessment.API.CustomMiddlewares;
using Ajmera.Assessment.BL.Services;
using FluentValidation.AspNetCore;
using System.Reflection;
using Ajmera.Assessment.Shared.Common;
using Ajmera.Assessment.API.Logging;

namespace Ajmera.Assessment.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Add services to the container.
            builder.Services.AddAutoMapper(Assembly.Load(ConstantMessages.BusinessAssemblyName));
            builder.Services.AddControllers().AddFluentValidation(fv =>
            {
                fv.RegisterValidatorsFromAssembly(Assembly.Load(ConstantMessages.SharedAssemblyName));
            });

            builder.Services.AddDbContext<AjmeraContext>(options =>
                options.UseSqlServer((builder.Configuration.GetSection("DBConnectionString").Value)
            ),ServiceLifetime.Singleton);
            // DAL
            builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            // BL
            builder.Services.AddTransient<IBookService, BookService>();
            builder.Services.AddSingleton<EnableRequestBufferingMiddleware>();
            builder.Services.AddTransient<IErrorLog, ErrorLog>();
            builder.Services.AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.Run();
        }
    }
}