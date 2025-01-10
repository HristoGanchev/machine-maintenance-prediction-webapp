var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.MaintenancePrediction_ApiService>("apiservice");

builder.AddProject<Projects.MaintenancePrediction_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();
