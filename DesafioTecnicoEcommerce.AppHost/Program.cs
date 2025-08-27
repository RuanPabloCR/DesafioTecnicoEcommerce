using Aspire.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

var postgresPassword = builder.AddParameter("postgres-password", () => "senhasegura");

var postgres = builder.AddPostgres("postgres")
    .WithPassword(postgresPassword)
    .WithImage("postgres:17")
    .WithDataVolume(isReadOnly: false)
    .WithContainerName("ecommerce-postgres");

var postgresdb1 = postgres.AddDatabase("ecommerceAuth");

var postgresdb2 = postgres.AddDatabase("ecommerceEstoque");

var rabbitMqService = builder.AddRabbitMQ("rabbitmq")
    .WithManagementPlugin()
    .WithImage("rabbitmq:3-management");

var apiService = builder.AddProject<Projects.DesafioTecnicoEcommerce_ApiGateway>("apiGateway")
    .WithHttpEndpoint(name: "api-http", port: 8080)
    .WithReference(postgresdb1)
    .WithReference(postgresdb2)
    .WaitFor(postgresdb1)
    .WaitFor(postgresdb2);

var msVendas = builder.AddProject<Projects.MsVendas>("msvendas")
    .WithReference(rabbitMqService)
    .WithHttpEndpoint(name: "vendas-http",port: 6060)
    .WaitFor(rabbitMqService);

var msEstoque = builder.AddProject<Projects.MSEstoque>("msestoque")
    .WithReference(rabbitMqService)
    .WithReference(postgresdb2)
    .WithHttpEndpoint(name: "estoque-http", port: 7070);

builder.Build().Run();
