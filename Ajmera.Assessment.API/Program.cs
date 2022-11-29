using Ajmera.Assessment.DL.Repositories;
using Ajmera.Assessment.DL;
using Microsoft.EntityFrameworkCore;
using Ajmera.Assessment.DL.Entities;
using Ajmera.Assessment.API.CustomMiddlewares;
using Ajmera.Assessment.BL.Services;
using FluentValidation.AspNetCore;
using System.Reflection;
using Ajmera.Assessment.Shared.Common;

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
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddControllers().AddFluentValidation(fv =>
            {
                fv.RegisterValidatorsFromAssembly(Assembly.Load(ConstantMessages.SharedAssemblyName));
            });

            builder.Services.AddDbContext<BookDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetSection("MicroService:Settings:DBConnectionString").Value)
            );
            // DAL
            builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            // BL
            builder.Services.AddTransient<IBookService, BookService>();
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