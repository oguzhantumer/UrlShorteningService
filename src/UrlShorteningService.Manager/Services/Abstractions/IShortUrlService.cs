using UrlShorteningService.Manager.ResponseModels;

namespace UrlShorteningService.Manager.Services.Abstractions
{
    public interface IShortUrlService
    {
        public ResponseModel GetUrl(string url);
        public ResponseModel CreateShortenedUrl(string url);
        public ResponseModel CreateCustomShortenedUrl(string url, string shortenedUrl);
    }
}
