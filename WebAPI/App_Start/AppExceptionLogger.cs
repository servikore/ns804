using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;

namespace WebAPI.App_Start
{
    public class AppExceptionLogger : ExceptionLogger
    {
        public override Task LogAsync(ExceptionLoggerContext context, CancellationToken cancellationToken)
        {
            //DO App wide exception loggin.
            return Task.CompletedTask;
        }
    }
}