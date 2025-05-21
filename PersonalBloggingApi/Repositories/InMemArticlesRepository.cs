using PersonalBloggingApi.Models;
using System.Diagnostics.Metrics;

namespace PersonalBloggingApi.Repositories
{
    public class InMemArticlesRepository : IArticlesRepository
    {
        List<Article> articles = new List<Article> { };

        public List<Article> GetAll(DateOnly? before, DateOnly? after)
        {
            if (before != null && after != null)
            {
                return articles.FindAll(article => article.CreatedAt.CompareTo(before) < 0 && article.CreatedAt.CompareTo(after) > 0);
            }

            if (after != null)
            {
                return articles.FindAll(article => article.CreatedAt.CompareTo(after) > 0);
            }

            if (before != null)
            {
                return articles.FindAll(article => article.CreatedAt.CompareTo(before) < 0);
            }

            return articles;
        }

        public Article? Get(int id)
        {
            return articles.Find(article => article.Id == id);
        }

        public void Create(Article article)
        {
            article.Id = articles.Count != 0 ? articles.Max(article => article.Id) + 1 : 0;
            article.CreatedAt = article.LastEdited = DateOnly.FromDateTime(DateTime.Now);
            articles.Add(article);
        }

        public void Update(Article updatedArticle)
        {
            int index = articles.FindIndex(article => article.Id == updatedArticle.Id);
            articles[index] = updatedArticle;
        }

        public void Delete(int id)
        {
            int index = articles.FindIndex(article => id == article.Id);
            articles.RemoveAt(index);
        }
    }
}
