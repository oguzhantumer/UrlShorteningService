using UrlShorteningService.Manager.ResponseModels.Enums;

namespace UrlShorteningService.Manager.ResponseModels
{
    public class ResponseModel
    {
        public ReturnCode StatusCode { get; private set; }
        public bool Status { get; private set; }
        public string Message { get; private set; }
        public string Data { get; private set; }

        public ResponseModel(ReturnCode statusCode, bool status, string message = null, string data = null)
        {
            StatusCode = statusCode;
            Status = status;
            Data = data;
            Message = message;
        }
    }
}
