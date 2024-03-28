using DocSumRepository;
using DocSumServices;
using Microsoft.Azure.Cosmos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IDocSumService, DocSumService>();
builder.Services.AddScoped<IDocSumRepo,DocSumRepo>();


var configuration1 = builder.Configuration;

// Add services to the container.
builder.Services.AddSingleton<CosmosClient>((provider) =>
{
    var endpointUri = configuration1["CosmosDbSettings:EndpointUri"];
    var primaryKey = configuration1["CosmosDbSettings:PrimaryKey"];
    var databaseName = configuration1["CosmosDbSettings:DatabaseName"];
    var cosmosClientOptions = new CosmosClientOptions
    {
        ApplicationName = databaseName
    };
    var loggerFactory = LoggerFactory.Create(builder =>
    {
        builder.AddConsole();
    });
    var cosmosClient = new CosmosClient(endpointUri, primaryKey, cosmosClientOptions);
    cosmosClient.ClientOptions.ConnectionMode = ConnectionMode.Direct;
    return cosmosClient;
});






var app = builder.Build();



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
