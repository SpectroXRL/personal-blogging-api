using Microsoft.EntityFrameworkCore;
using PersonalBloggingApi.Data;
using PersonalBloggingApi.Models;

namespace PersonalBloggingApi.Repositories
{
    public class EFArticlesRepository : IArticlesRepository
    {
        private readonly ArticlesDbContext _context;
        public EFArticlesRepository(ArticlesDbContext context)
        {
            _context = context;
        }

        public List<Article> GetAll(DateOnly? before, DateOnly? after)
        {
            if (before != null && after != null)
            {
                return _context.Articles.ToList().FindAll(article => article.CreatedAt.CompareTo(before) < 0 && article.CreatedAt.CompareTo(after) > 0);
            }

            if (after != null)
            {
                return _context.Articles.ToList().FindAll(article => article.CreatedAt.CompareTo(after) > 0);
            }

            if (before != null)
            {
                return _context.Articles.ToList().FindAll(article => article.CreatedAt.CompareTo(before) < 0);
            }

            return _context.Articles.ToList();
        }

        public Article? Get(int id)
        {
            return _context.Articles.FirstOrDefault(article => article.Id == id);
        }

        public void Create(Article article)
        {
            article.CreatedAt = article.LastEdited = DateOnly.FromDateTime(DateTime.Now);
            _context.Articles.Add(article);
            _context.SaveChanges();
        }

        public void Update(Article updatedArticle)
        {
            _context.Articles.Update(updatedArticle);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            _context.Articles.Where(article => article.Id == id).ExecuteDelete();
            _context.SaveChanges();
        }

        public int Count()
        {
            return _context.Articles.Count();
        }
    }
}
