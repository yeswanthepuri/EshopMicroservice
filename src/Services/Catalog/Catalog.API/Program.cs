


using BuildingBlocks.Exceptions.Handler;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
        policy.WithOrigins("http://localhost:4200") // Allow Angular app's origin
              .AllowAnyHeader()
              .AllowAnyMethod()
    );
});
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
builder.Services.AddMarten(opt =>
{
    opt.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();
builder.Services.AddExceptionHandler<CustomExceptionHandler>();
//Add Services to Container
var app = builder.Build();
app.UseCors("AllowAngularApp");
//Configer the HTTP request pipeline
app.MapCarter();
app.UseExceptionHandler(options =>
{

});

app.Run();
