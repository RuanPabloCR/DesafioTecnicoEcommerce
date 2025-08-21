var builder = DistributedApplication.CreateBuilder(args);


var apiService = builder.AddProject<Projects.DesafioTecnicoEcommerce_ApiGateway>("apiservice");

builder.AddProject<Projects.MsVendas>("msvendas").WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();
