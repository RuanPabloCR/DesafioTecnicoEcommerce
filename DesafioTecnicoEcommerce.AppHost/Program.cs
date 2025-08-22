var builder = DistributedApplication.CreateBuilder(args);


var apiService = builder.AddProject<Projects.DesafioTecnicoEcommerce_ApiGateway>("apiservice")
    .WithExternalHttpEndpoints()
    .WithHttpEndpoint(port: 8080);

var rabbitMqService = builder.AddRabbitMQ("rabbitmq")
    .WithManagementPlugin()
    .WithImage("rabbitmq:3-management");


var msVendas = builder.AddProject<Projects.MsVendas>("msvendas")
    .WithReference(apiService)
    .WithReference(rabbitMqService)
    .WithExternalHttpEndpoints()
    .WithHttpEndpoint(port: 6060);

var msEstoque = builder.AddProject<Projects.MSEstoque>("msestoque")
    .WithReference(apiService)
    .WithReference(rabbitMqService)
    .WithExternalHttpEndpoints()
    .WithHttpEndpoint(port: 7070);

builder.Build().Run();
