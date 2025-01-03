var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres").WithDataVolume(isReadOnly: false).WithPgAdmin();

var postgresdb = postgres.AddDatabase("postgresdb");

builder.AddProject<Projects.ConstantReminder_Api>("constantreminderapi").WithExternalHttpEndpoints().WithReference(postgresdb).WaitFor(postgresdb);

await builder.Build().RunAsync();
