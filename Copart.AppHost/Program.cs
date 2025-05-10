var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Copart_Api>("copart-api");

builder.Build().Run();
