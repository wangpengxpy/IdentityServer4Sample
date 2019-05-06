// Copyright (c) Jeffcky <see cref="https://jeffcky.ke.qq.com/"/> All rights reserved.
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace IDS4Demo.Filters
{
    public class TranslateExceptonFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {

        }

        public override Task OnExceptionAsync(ExceptionContext context)
        {
            var exception = context.Exception;

            if (exception is ApiException apiException)
            {
                context.Result = new BadRequestObjectResult(new
                {
                    description = apiException.Description
                });
            }

            return Task.CompletedTask;
        }
    }
}
