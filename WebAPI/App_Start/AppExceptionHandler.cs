using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;

namespace WebAPI.App_Start
{
    public class AppExceptionHandler : ExceptionHandler
    {
        public override Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
        {
            var response = context.Request.CreateResponse(HttpStatusCode.InternalServerError,
            new
            {
                Message = "An unexpected error occured."
            });

            context.Result = new ResponseMessageResult(response);

            return Task.CompletedTask;
            
        }
    }
}