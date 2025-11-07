using System.Net;

namespace PD421_Dashboard_WEB_API.BLL.Services
{
    public class ServiceResponse
    {
        public required string Message { get; set; }
        public bool IsSuccess { get; set; } = true;
        public object? Data { get; set; } = null;
        public HttpStatusCode HttpStatusCode { get; set; } = HttpStatusCode.OK;
    }
}
