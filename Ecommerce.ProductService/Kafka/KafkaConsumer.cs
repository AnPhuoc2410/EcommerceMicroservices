using Confluent.Kafka;
using Ecommerce.Model;
using Ecommerce.ProductService.Data;
using Newtonsoft.Json;

namespace Ecommerce.ProductService.Kafka
{
    public class KafkaConsumer(IServiceScopeFactory scopeFactory) : BackgroundService
    {
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.Run(() =>
            {
                _ = ConsumerAsync("order-topic", stoppingToken);
            }, stoppingToken);

        }

        public async Task ConsumerAsync(string topic, CancellationToken stoppingToken)
        {
            var config = new ConsumerConfig
            {
                GroupId = "order-group",
                BootstrapServers = "localhost:9092",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
            using var consumer = new ConsumerBuilder<string, string>(config).Build();
            consumer.Subscribe(topic);

            while (!stoppingToken.IsCancellationRequested)
            {
                var consumerResult = consumer.Consume(stoppingToken);

                var order = JsonConvert.DeserializeObject<OrderModel>(consumerResult.Message.Value);
                if (order != null)
                {
                    // Process the order
                    Console.WriteLine($"Order received: {order.Id}, {order.CustomerName}, {order.Quantity}, {order.ProductId}, {order.OrderDate}");
                }
                else
                {
                    Console.WriteLine("Failed to deserialize order.");
                }

                using var scope = scopeFactory.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<ProductDbContext>();

                var product = dbContext.Products.Find(order.ProductId);
                if (product != null)
                {
                    product.Quantity -= order.Quantity;
                   await dbContext.SaveChangesAsync();
                }
                else
                {
                    Console.WriteLine($"Product with ID {order.ProductId} not found.");
                }
            }
            consumer.Close();

        }
    }
}
