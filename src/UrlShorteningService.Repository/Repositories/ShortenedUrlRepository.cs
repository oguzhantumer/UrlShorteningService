using System.Linq;
using UrlShorteningService.Repository.Models;
using UrlShorteningService.Repository.Repositories.Abstractions;

namespace UrlShorteningService.Repository.Repositories
{
    public class ShortenedUrlRepository : IShortenedUrlRepository
    {
        public bool CreateShortenedUrl(ShortenedUrl data)
        {
            using (var db = new ShortenedUrlContext())
            {
                var existingItem = db.ShortenedUrls.FirstOrDefault(x => x.OriginalUrl.Equals(data.OriginalUrl));
                if (existingItem != null)
                {
                    existingItem.ShortUrl = data.ShortUrl;
                }
                else
                {
                    db.ShortenedUrls.Add(data);
                }
                return db.SaveChanges() > 0;
            }
        }

        public ShortenedUrl GetByShortUrl(string url)
        {
            using (var db = new ShortenedUrlContext())
            {
                var result = db.ShortenedUrls.FirstOrDefault(urls => urls.ShortUrl.Equals(url));
                return result;
            }
        }
    }
}
