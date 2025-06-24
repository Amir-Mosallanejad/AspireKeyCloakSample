var builder = DistributedApplication.CreateBuilder(args);

IResourceBuilder<KeycloakResource> keycloak = builder
    .AddKeycloak("keycloak", 8080)
    .WithOtlpExporter()
    .WithDataVolume("AspireKeyCloakSample");

builder.AddProject<Projects.AspireKeyCloakSample_Api>("api")
    .WithExternalHttpEndpoints()
    .WithReference(keycloak);

builder.Build().Run();