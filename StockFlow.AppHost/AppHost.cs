var builder = DistributedApplication.CreateBuilder(args);

var api = builder.AddProject<Projects.API>("api", launchProfileName: "http")
    .WithHttpHealthCheck("/health");

builder.AddViteApp("frontend", "../frontend")
    .WithPnpm()
    .WithReference(api)
    .WithEnvironment("API_HTTP", api.GetEndpoint("http"))
    .WithExternalHttpEndpoints()
    .WaitFor(api);

builder.Build().Run();
