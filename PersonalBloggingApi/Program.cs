using Microsoft.AspNetCore.Mvc;
using PersonalBloggingApi.Models;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

List<Article> articles = new List<Article>{};

app.MapGet("/articles", () => articles);

app.MapGet("/articles/{id}", (int id) => articles.Find(article => article.Id == id));

app.MapPost("/articles", ([FromBody]Article article) => {
        article.Id = articles.Count != 0 ? articles.Max(article => article.Id) + 1 : 0;
        article.CreatedAt = article.LastEdited = DateOnly.FromDateTime(DateTime.Now);
        articles.Add(article);
    });

app.MapPut("/articles/{id}", (int id, [FromBody]Article updatedArticle) =>
{
    Article? existingArticle = articles.Find(article => article.Id == id);
    if (existingArticle != null)
    {
        existingArticle.Title = updatedArticle.Title;
        existingArticle.Body = updatedArticle.Body;
        existingArticle.Author = updatedArticle.Author;
        existingArticle.LastEdited = DateOnly.FromDateTime(DateTime.Now);
    }
    else
    {

    }
});

app.MapDelete("/articles/{id}", (int id) =>
{
    Article? article = articles.Find(article => article.Id == id);
    if(article != null)
    {
        articles.Remove(article);
    }
});

app.Run();
