using Microsoft.AspNetCore.Mvc;
using PD421_Dashboard_WEB_API.BLL.Services;

namespace PD421_Dashboard_WEB_API.Extensions
{
    public static class ControllerBaseExtensions
    {
        public static IActionResult ToActionResult(this ControllerBase controller, ServiceResponse response)
        {
            return controller.StatusCode((int)response.HttpStatusCode, response);
        }
    }
}
