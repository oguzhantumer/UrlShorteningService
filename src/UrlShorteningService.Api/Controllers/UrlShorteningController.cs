using Microsoft.AspNetCore.Mvc;
using UrlShorteningService.Manager.RequestModels;
using UrlShorteningService.Manager.ResponseModels;
using UrlShorteningService.Manager.Services.Abstractions;

namespace UrlShorteningService.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UrlShorteningController : Controller
    {
        private readonly IShortUrlService _shortUrlService;
        public UrlShorteningController(IShortUrlService shortUrlService)
        {
            _shortUrlService = shortUrlService;
        }

        [HttpGet("url/{url}")]
        public ActionResult<ResponseModel> Get(string url)
        {
            var result = _shortUrlService.GetUrl(url);
            return result.Status ? Ok(result) : BadRequest(result);
        }

        [HttpPost]
        [Route("Create")]
        public ActionResult<ResponseModel> CreateShortenedUrl(CreateShortUrlRequestModel request)
        {
            var result = _shortUrlService.CreateShortenedUrl(request.Url);
            return result.Status ? Ok(result) : BadRequest(result);
        }

        [HttpPost]
        [Route("Create/Custom")]
        public ActionResult<ResponseModel> CreateCustomShortenedUrl(CreateCustomShortUrlRequestModel request)
        {
            var result = _shortUrlService.CreateCustomShortenedUrl(request.Url, request.ResultUrl);
            return result.Status ? Ok(result) : BadRequest(result);
        }
    }
}
