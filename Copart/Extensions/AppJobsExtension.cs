using Copart.BLL.Services.BackgroundJob;
using Copart.BLL.Services.LotService;

namespace Copart.Api.Extensions
{
    public static class AppJobsExtension
    {
        public static void ConfigureBackgroundJobs(this WebApplication app)
        {
            var queue = app.Services.GetRequiredService<IBackgroundJobQueue>();

            //jobs \/ \/ \/ \/ \/ \/ \/ \/

            queue.EnqueueJob(async token =>
            {
                using var scope = app.Services.CreateScope();
                var lotService = scope.ServiceProvider.GetRequiredService<ILotService>();
                await lotService.ChangeAllLotsStatus(token);
            });

            //jobs /\ /\ /\ /\ /\ /\ /\ /\
        }
    }
}
