using MinimalAPIService.BookService;
using MinimalAPIService.WeatherService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IBookService, BookService>();

var app = builder.Build();

// configure exception middleware
app.UseStatusCodePages(async statusCodeContext
    => await Results.Problem(statusCode: statusCodeContext.HttpContext.Response.StatusCode)
        .ExecuteAsync(statusCodeContext.HttpContext));

//The path that the API gateway listens for this service under.
var basePath = Environment.GetEnvironmentVariable("apiGatewayPath")?.Trim() ?? "/minimalAPIservice";
Console.WriteLine($"basepath: {basePath}");
app.UsePathBase(basePath);
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint($"{basePath}/swagger/v1/swagger.json", "Minimal API Service"));

app.UseHttpsRedirection();
app.UseDeveloperExceptionPage();

app.AddBookServiceEndpoints();
app.AddWeatherServiceEndpoints();

app.MapGet("/", () => "Hello, World!");

app.Run();
