using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PersonalBloggingApi.Models;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

List<Article> articles = new List<Article>{};

app.MapGet("/articles", () => TypedResults.Ok(articles));

app.MapGet("/articles/{id}", Results<Ok<Article>, NotFound> (int id) => 
{
    var article = articles.Find(article => article.Id == id);

    if(article != null)
    {
        return TypedResults.Ok(article);
    }

    return TypedResults.NotFound();


})
    .WithName("GetArticleById");

app.MapPost("/articles", ([FromBody]Article article) => {
        article.Id = articles.Count != 0 ? articles.Max(article => article.Id) + 1 : 0;
        article.CreatedAt = article.LastEdited = DateOnly.FromDateTime(DateTime.Now);
        articles.Add(article);
        return TypedResults.CreatedAtRoute(article, "GetArticleById", new { id = article.Id});
    });

app.MapPut("/articles/{id}", Results<NoContent, NotFound> (int id, [FromBody]Article updatedArticle) =>
{
    Article? existingArticle = articles.Find(article => article.Id == id);
    if (existingArticle != null)
    {
        existingArticle.Title = updatedArticle.Title;
        existingArticle.Body = updatedArticle.Body;
        existingArticle.Author = updatedArticle.Author;
        existingArticle.LastEdited = DateOnly.FromDateTime(DateTime.Now);
        return TypedResults.NoContent();
    }
    else
    {
        return TypedResults.NotFound();
    }
});

app.MapDelete("/articles/{id}", Results<NoContent, NotFound> (int id) =>
{
    Article? article = articles.Find(article => article.Id == id);
    if(article != null)
    {
        articles.Remove(article);
        return TypedResults.NoContent();
    }
    return TypedResults.NotFound();
});

app.Run();
