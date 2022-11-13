using WebApi.Persistence;

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

builder.Services.AddSingleton<ICoasterRepository,CoasterRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseOpenApi();
    app.UseSwaggerUi3();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();