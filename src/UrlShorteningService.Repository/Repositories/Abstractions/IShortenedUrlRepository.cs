using UrlShorteningService.Repository.Models;

namespace UrlShorteningService.Repository.Repositories.Abstractions
{
    public interface IShortenedUrlRepository
    {
        public ShortenedUrl GetByShortUrl(string url);
        public bool CreateShortenedUrl(ShortenedUrl data);
    }
}
