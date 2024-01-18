
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using practise_JWT.Data;

namespace practise_JWT
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connect = builder.Configuration.GetConnectionString("defaultconnection");

            // Add services to the container.
            builder.Services.AddDbContext<MyContext>(option => option.UseSqlServer(connect));
            builder.Services.AddControllers();
            builder.Services.AddSwaggerGen();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddIdentity<Applicationuser, IdentityRole>().AddEntityFrameworkStores<MyContext>();
            builder.Services.AddAuthentication(option => { 
            option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

            
            }).AddJwtBearer(option => {
                option.SaveToken = true;
                option.RequireHttpsMetadata = true;
            
            
            });
           
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}