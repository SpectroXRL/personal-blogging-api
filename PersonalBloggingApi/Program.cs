using Microsoft.EntityFrameworkCore;
using PersonalBloggingApi.Data;
using PersonalBloggingApi.Endpoints;
using PersonalBloggingApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ArticlesDbContext>(opt =>
opt.UseNpgsql(builder.Configuration.GetConnectionString("ArticlesDbContext")));

builder.Services.AddTransient<IArticlesRepository, EFArticlesRepository>();

var app = builder.Build();

app.RegisterArticlesEndpoints();

app.Run();
