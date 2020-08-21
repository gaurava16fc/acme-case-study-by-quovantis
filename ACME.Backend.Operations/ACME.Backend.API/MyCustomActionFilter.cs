using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace ACME.Backend.API
{
    // THIS IS TO TEST SOMETHING...

    public class MyCustomActionFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            throw new NotImplementedException();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            throw new NotImplementedException();
        }
    }
}
