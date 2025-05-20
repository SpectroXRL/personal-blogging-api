using PersonalBloggingApi.Endpoints;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.RegisterArticlesEndpoints();

app.Run();
