using conversor_moedas.domain.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace conversor_moedas.api.Filters
{
    public class AfterHandlerActionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            HandleResult(context);

            base.OnActionExecuted(context);
        }

        private void HandleResult(ActionExecutedContext context)
        {
            if (context.Result is not ObjectResult objectResult)
                return;

            var isGenericType = objectResult.Value.GetType().IsGenericType &&
                                objectResult.Value.GetType().GetGenericTypeDefinition() == typeof(Result<>);

            if (isGenericType)
            {
                dynamic dynamicResult = objectResult.Value;
                HandleGenericResult(context, dynamicResult);
            }
            else
            {
                context.Result = new NoContentResult();
            }
        }

        private static void HandleGenericResult<TResult>(ActionExecutedContext context, Result<TResult> result)
        {
            context.Result = context.HttpContext.Request.Method == HttpMethod.Post.Method
                ? new CreatedResult(string.Empty, result.Value)
                : new OkObjectResult(result.Value);
        }
    }
}
