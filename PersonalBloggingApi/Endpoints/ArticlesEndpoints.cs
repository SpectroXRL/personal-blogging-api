using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PersonalBloggingApi.Models;
using System.Diagnostics.Metrics;

namespace PersonalBloggingApi.Endpoints
{
    public static class ArticlesEndpoints
    {
        static List<Article> articles = new List<Article> { };
        public static void RegisterArticlesEndpoints(this WebApplication app)
        {
            //Endpoint Group
            var articles = app.MapGroup("/articles");

            //Endpoints
            articles.MapGet("/", GetAllArticles);

            articles.MapGet("/{id}", GetArticleById)
                .WithName("GetArticleById");

            articles.MapPost("/", CreateArticle);

            articles.MapPut("/{id}", UpdateArticleById);

            articles.MapDelete("/{id}", DeleteArticle);
        }

        //GetAllArticles
        static Results<BadRequest, Ok<List<Article>>> GetAllArticles(DateOnly? before, DateOnly? after) 
        {
            if (before != null && after != null)
            {
                if (before.Value.CompareTo(after.Value) < 0 || after.Value.CompareTo(before.Value) > 0)
                {
                    return TypedResults.BadRequest();
                }
                return TypedResults.Ok(articles.FindAll(article => article.CreatedAt.CompareTo(before) < 0 && article.CreatedAt.CompareTo(after) > 0));
            }

            if (after != null)
            {
                return TypedResults.Ok(articles.FindAll(article => article.CreatedAt.CompareTo(after) > 0));
            }

            if (before != null)
            {
                return TypedResults.Ok(articles.FindAll(article => article.CreatedAt.CompareTo(before) < 0));
            }

            return TypedResults.Ok(articles);
        }

        //GetArticleById
        static Results<Ok<Article>, NotFound> GetArticleById(int id)
        {
            var article = articles.Find(article => article.Id == id);

            if (article != null)
            {
                return TypedResults.Ok(article);
            }

            return TypedResults.NotFound();
        }

        //CreateArticle
        static IResult CreateArticle ([FromBody] Article article) 
        {
            article.Id = articles.Count != 0 ? articles.Max(article => article.Id) + 1 : 0;
            article.CreatedAt = article.LastEdited = DateOnly.FromDateTime(DateTime.Now);
            articles.Add(article);
            return TypedResults.CreatedAtRoute(article, "GetArticleById", new { id = article.Id});
        }

        //UpdateArticleById
        static Results<NoContent, NotFound> UpdateArticleById(int id, [FromBody] Article updatedArticle)
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
        }

        //DeleteArticle
        static Results<NoContent, NotFound> DeleteArticle(int id)
        {
            Article? article = articles.Find(article => article.Id == id);
            if (article != null)
            {
                articles.Remove(article);
                return TypedResults.NoContent();
            }
            return TypedResults.NotFound();
        }

    }
}
