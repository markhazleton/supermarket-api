using Microsoft.AspNetCore.Mvc;
using Mwh.Sample.CoreApi.Extensions;
using Mwh.Sample.CoreApi.Resources;

namespace Mwh.Sample.CoreApi.Controllers.Config
{
    public static class InvalidModelStateResponseFactory
    {
        public static IActionResult ProduceErrorResponse(ActionContext context)
        {
            var errors = context.ModelState.GetErrorMessages();
            var response = new ErrorResource(messages: errors);
            return new BadRequestObjectResult(response);
        }
    }
}