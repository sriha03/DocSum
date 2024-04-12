using DocSumRepository;
using DocSumServices;
using Microsoft.Azure.Cosmos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();/*
builder.Services.AddScoped<IDocSumService, DocSumService>();*/
builder.Services.AddScoped<IDocSumRepo,DocSumRepo>();


var configuration1 = builder.Configuration;

// Add services to the container.
builder.Services.AddScoped<IDocSumService>((provider) =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    var textAnalyticsEndpoint = configuration["TextAnalytics:Endpoint"];
    var textAnalyticsKey = configuration["TextAnalytics:Key"];
    var docSumRepo = provider.GetRequiredService<IDocSumRepo>();
    return new DocSumService(docSumRepo, textAnalyticsEndpoint, textAnalyticsKey);
});


builder.Services.AddScoped<IDocSumService>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();

    // Get the configuration values for text analytics
    var textAnalyticsEndpoint = configuration["TextAnalytics:Endpoint"];
    var textAnalyticsKey = configuration["TextAnalytics:Key"];

    // Resolve the IDocSumRepo dependency
    var docSumRepo = provider.GetRequiredService<IDocSumRepo>();

    // Create and return the DocSumService instance with resolved dependencies
    return new DocSumService(docSumRepo, textAnalyticsEndpoint, textAnalyticsKey);
});




var app = builder.Build();
// Configure CORS
app.UseCors(builder =>
{
    builder.WithOrigins("https://localhost:5173") // Update with your frontend origin URL
           .AllowAnyHeader()
           .AllowAnyMethod();
});






// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
