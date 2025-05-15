
using Ecommerce.OrderService.Data;
using Ecommerce.OrderService.Kafka;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.OrderService
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

            // Register Kafka producer
            builder.Services.AddScoped<IKafkaProducer, KafkaProducer>();

            builder.Services.AddDbContext<OrderDbContext>(options =>
                options.UseSqlServer("Data Source=.\\sqlexpress; Initial Catalog=EcommerceOrder; Integrated Security=True; TrustServerCertificate=True"));

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
