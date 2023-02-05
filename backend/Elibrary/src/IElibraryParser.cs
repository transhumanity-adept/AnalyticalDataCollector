using Elibrary.Contracts;

namespace Elibrary;

public interface IElibraryParser
{
    Task<ParseResponse<Article>> GetArticleByIdAsync(int articleId);
}