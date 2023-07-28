
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SistemaDeTarefas.Data;
using SistemaDeTarefas.Repositorios;
using SistemaDeTarefas.Repositorios.Interfaces;

namespace SistemaDeTarefas
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

            //Install-Pakage Npgsql.EntityFrameworkCore.PostgreSQL
            builder.Services.AddEntityFrameworkNpgsql()
                .AddDbContext<SistemaTarefasDBContex>(
                    //options => options.UseSqlServer(builder.Configuration.GetConnectionString("DataBase")) Utilizar esse trecho se quiser conectar com o SQLServer                    
                    options => options.UseNpgsql(builder.Configuration.GetConnectionString("DataBase"))
                );



            builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();


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

            app.Run();
        }
    }
}