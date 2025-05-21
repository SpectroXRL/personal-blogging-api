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
            return _context.Articles.ToList();
        }

        public Article? Get(int id)
        {
            return _context.Articles.FirstOrDefault(article => article.Id == id);
        }

        public void Create(Article article)
        {
            _context.Articles.Add(article);
        }

        public void Update(Article updatedArticle)
        {
            _context.Articles.Update(updatedArticle);
        }

        public void Delete(int id)
        {
            _context.Articles.Where(article => article.Id == id).ExecuteDelete();
        }
    }
}
