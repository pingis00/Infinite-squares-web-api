using InfiniteSquaresWebAPI.Configurations;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

// Register services
builder.Services.RegisterSwagger();
builder.Services.RegisterServices(builder.Configuration);
builder.Services.AddApiConfigurations();

var app = builder.Build();

// Configure middleware pipeline
app.UseCustomMiddleware();
app.UseCors("AllowAllOrigins");
app.UseSwagger();
app.UseSwaggerUI(x => x.SwaggerEndpoint("/swagger/v1/swagger.json", "Infinite Squares API v1"));
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
