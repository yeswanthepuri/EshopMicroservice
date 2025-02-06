using Ordering.API;
using Ordering.Application;
using Ordering.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
//Add services to the container
builder.Services
    .AddApplicationService()
    .AddInfrastructureService(builder.Configuration)
    .AddApiService();
/*-------------
Infrastructute -- EF Core
Application -- mediatR
API - Carter , Heathh Check

builder.Services
    .AddApplicationService()
    .AddInfrastructureService(builder.Configuration)
    .AddWebServices();
/--------------*/

var app = builder.Build();
//configer the HTTP request Pipeline

app.UseApiServices();
app.Run();
