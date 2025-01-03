

using Discount.Grpc.Data;
using Discount.Grpc.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
SQLitePCL.Batteries_V2.Init();
// Add services to the container.
builder.Services.AddGrpc();
var consrint = builder.Configuration.GetConnectionString("Database");
builder.Services.AddDbContext<DiscountContext>(opt =>
opt.UseSqlite(consrint)
);
var app = builder.Build();
app.UseMigration();
// Configure the HTTP request pipeline.
app.MapGrpcService<DiscountService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
