using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Sampaio.Logging;
using Sampaio.Shared.Constants;
using Sampaio.Shared.Notifications;
using Sampaio.Shared.Results;

namespace Sampaio.Api.Config.ActionFilters
{
    public class ApiExceptionAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            
            var notification = new Notification(CommonMessages.InternalServerError);
            
            var notifications =
                context.HttpContext.RequestServices.GetService(typeof(IDomainNotification)) as IDomainNotification;
            
            Logging4NetFactory.Logger.Error(context.Exception);

            if (notifications == null)
            {
                context.Result = new ObjectResult(EnvelopResult.Fail(new[]{ notification}))
                {
                    StatusCode = 500
                };
                return;
            }
            
            if (!notifications.HasNotifications())
            {
                notifications.Handle(notification);
            }
            
            context.Result = new ObjectResult(EnvelopResult.Fail(notifications.Values))
            {
                StatusCode = 500
            };
        }
    }
}
