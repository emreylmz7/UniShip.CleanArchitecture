var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.UniShip_WebAPI>("uniship-webapi");

builder.Build().Run();
