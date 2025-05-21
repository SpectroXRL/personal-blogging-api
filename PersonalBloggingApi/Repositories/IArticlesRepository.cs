using PersonalBloggingApi.Models;

namespace PersonalBloggingApi.Repositories
{
    public interface IArticlesRepository
    {
        void Create(Article article);
        void Delete(int id);
        Article? Get(int id);
        List<Article> GetAll(DateOnly? before, DateOnly? after);
        void Update(Article updatedArticle);
    }
}