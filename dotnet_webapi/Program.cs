using Serilog;
using Serilog.Events;
using Serilog.Filters;

Log.Logger = new LoggerConfiguration()
//.Filter.ByExcluding("SourceContext = 'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware'")
.Enrich.FromLogContext()
.MinimumLevel.Information()
.MinimumLevel.Override("Default", LogEventLevel.Fatal)
.MinimumLevel.Override("Microsoft", LogEventLevel.Fatal)
.MinimumLevel.Override("System", LogEventLevel.Fatal)
.WriteTo.Logger(wt => wt
    .Enrich.FromLogContext()
    .Filter.ByIncludingOnly(Matching.WithProperty("type1"))
    .WriteTo.File(
        "./log/type1-.txt",
        rollingInterval: RollingInterval.Day
    )
)
.WriteTo.Logger(wt => wt
    .Enrich.FromLogContext()
    .Filter.ByIncludingOnly(Matching.WithProperty("type2"))
    .WriteTo.File(
        "./log/type2-.txt",
        rollingInterval: RollingInterval.Day
    )
)
.WriteTo.File(
    "./log/log-.txt",
    rollingInterval: RollingInterval.Day
)
.CreateLogger();

Log.Information("Ah, there you are!");

var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddHttpContextAccessor();
//builder.Services.AddLogging(loggingBuilder =>
//{
//    //loggingBuilder.ClearProviders();
//    loggingBuilder.AddSerilog();
//});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // app.UseSwagger();
    // app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
