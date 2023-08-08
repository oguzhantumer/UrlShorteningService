using System;
using System.Web;
using UrlShorteningService.Manager.ResponseModels;
using UrlShorteningService.Manager.ResponseModels.Enums;
using UrlShorteningService.Manager.Services.Abstractions;
using UrlShorteningService.Repository.Models;
using UrlShorteningService.Repository.Repositories.Abstractions;

namespace UrlShorteningService.Manager.Services
{
    public class ShortUrlService : IShortUrlService
    {        
        private const int shortenedUrlLength = 6;
        private readonly IAlphanumericService _alphanumericService;
        private readonly IShortenedUrlRepository _shortenedUrlRepository;

        public ShortUrlService(IAlphanumericService alphanumericService, IShortenedUrlRepository shortenedUrlRepository)
        {
            _alphanumericService = alphanumericService;
            _shortenedUrlRepository = shortenedUrlRepository;
        }

        public ResponseModel CreateCustomShortenedUrl(string url, string shortenedUrl)
        {
            try
            {
                if (!ValidateUrl(url))
                {
                    return new ResponseModel(ReturnCode.ValidationError, false, $"Url: {url} is not valid");
                }
                if (!ValidateUrl(shortenedUrl))
                {
                    return new ResponseModel(ReturnCode.ValidationError, false, $"Url: {url} is not valid");
                }
                if (!Uri.TryCreate(shortenedUrl, UriKind.Absolute, out var uri))
                {
                    return new ResponseModel(ReturnCode.UnSuccess, false, $"Error occured while converting to resultUrl:{shortenedUrl}");
                }
                var shortenedUrlModel = new ShortenedUrl()
                {
                    CreatedAt = DateTime.Now,
                    OriginalUrl = url,
                    ShortUrl = shortenedUrl
                };
                if (_shortenedUrlRepository.CreateShortenedUrl(shortenedUrlModel))
                {
                    return new ResponseModel(ReturnCode.Ok, true, data: shortenedUrl);
                }
                return new ResponseModel(ReturnCode.UnSuccess, false, "Error occured while creating data on db");
            }
            catch (Exception e)
            {
                return new ResponseModel(ReturnCode.UnSuccess, false, $"Error occured {e.Message}");
            }            
        }

        public ResponseModel CreateShortenedUrl(string url)
        {
            try
            {
                if (!ValidateUrl(url))
                {
                    return new ResponseModel(ReturnCode.ValidationError, false, $"Url: {url} is not valid");
                }
                if (!Uri.TryCreate(url, UriKind.Absolute, out var uri))
                {
                    return new ResponseModel(ReturnCode.UnSuccess, false, $"Error occured while converting to uri");
                }
                var str = _alphanumericService.Create(shortenedUrlLength);
                var result = $"{uri.Scheme}://{uri.Host}/{str}";
                var shortenedUrlModel = new ShortenedUrl()
                {
                    CreatedAt = DateTime.Now,
                    OriginalUrl = url,
                    ShortUrl = result
                };
                if (_shortenedUrlRepository.CreateShortenedUrl(shortenedUrlModel))
                {
                    return new ResponseModel(ReturnCode.Ok, true, data: result);
                }
                return new ResponseModel(ReturnCode.UnSuccess, false, "Error occured while creating data on db");
            }
            catch (Exception e)
            {
                return new ResponseModel(ReturnCode.UnSuccess, false, $"Error occured {e.Message}");
            }
            
        }

        public ResponseModel GetUrl(string url)
        {
            try
            {
                url = HttpUtility.UrlDecode(url);
                if (!ValidateUrl(url))
                {
                    return new ResponseModel(ReturnCode.ValidationError, false, $"Url: {url} is not valid");
                }
                var result = _shortenedUrlRepository.GetByShortUrl(url);
                if (result != null)
                {
                    return new ResponseModel(ReturnCode.Ok, true, data: result.OriginalUrl);
                }
                return new ResponseModel(ReturnCode.NoData, true, null);
            }
            catch (Exception e)
            {
                return new ResponseModel(ReturnCode.UnSuccess, false, $"Error occured {e.Message}");
            }
            
        }

        private bool ValidateUrl(string url)
        {
            return Uri.IsWellFormedUriString(url, UriKind.Absolute);
        }
    }
}
