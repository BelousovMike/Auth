IDistributedApplicationBuilder builder = DistributedApplication.CreateBuilder(args);
builder.AddProject<Projects.Auth_Web>("web");
await builder.Build()
    .RunAsync()
    .ConfigureAwait(false);
