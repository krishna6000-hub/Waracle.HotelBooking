

using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Waracle.HotelBooking.Domain.Interfaces;
using Waracle.HotelBooking.Infrastructure.DbContext;
using Waracle.HotelBooking.Services;

namespace Waracle.HotelBooking.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "HotelBookingAPI", Version = "v1" });
            });
            builder.Services.AddControllers();

            builder.Services.AddDbContextPool<AppDbContext>(options =>
            {
                //options.UseInMemoryDatabase("Developer");
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("Waracle.HotelBooking.WebAPI"));
            });

            builder.Services.AddScoped<IBookingService, BookingService>();
            builder.Services.AddScoped<IBookingRepository, BookingRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "HotelBookingAPI v1");
            });


            app.MapControllers();
            app.Run();
        }
    }
}
