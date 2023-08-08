namespace UrlShorteningService.Manager.ResponseModels.Enums
{
    public enum ReturnCode
    {
        /// <summary>
        /// ok
        /// </summary>
        Ok = 10,
        /// <summary>
        /// data not found
        /// </summary>
        NoData = 20,
        /// <summary>
        /// validation error
        /// </summary>
        ValidationError = 30,
        /// <summary>
        /// unsuccess operation
        /// </summary>
        UnSuccess = 40
    }
}
