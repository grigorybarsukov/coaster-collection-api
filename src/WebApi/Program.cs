using WebApi;
using WebApi.Persistence;
using WebApi.Providers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddSwaggerDocument(config =>
{
    config.PostProcess = document =>
    {
        document.Info.Version = "1.0.0";
        document.Info.Title = "Coaster API";
        document.Info.Description = "An web API to explore coasters collection";
        document.Info.Contact = new NSwag.OpenApiContact
        {
            Name = "Grigorii Barsukov",
            Email = string.Empty,
            Url = "https://myBeerCollection.com"
        };
        document.Info.License = new NSwag.OpenApiLicense
        {
            Name = "MIT",
            Url = "https://example.com/license"
        };
    };
});

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();
builder.Services.AddOptions();
builder.Services.Configure<ServiceConfiguration>(configuration);

builder.Services.AddSingleton<ICoasterRepository, CoasterRepository>();
builder.Services.AddSingleton<IImageProvider, ImageProvider>();
builder.Services.AddCors(o => o.AddDefaultPolicy(cb => cb
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowAnyOrigin()
    .SetPreflightMaxAge(TimeSpan.FromMinutes(10))));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseOpenApi();
    app.UseSwaggerUi3();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();