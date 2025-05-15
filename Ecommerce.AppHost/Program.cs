var builder = DistributedApplication.CreateBuilder(args);

var productApi = builder.AddProject<Projects.Ecommerce_ProductService>("apiservice-product");
var orderApi = builder.AddProject<Projects.Ecommerce_OrderService>("apiservice-order");

builder.AddProject<Projects.Ecommerce_Web>("webfronted")
    .WithReference(productApi)
    .WithReference(orderApi);

builder.Build().Run();
