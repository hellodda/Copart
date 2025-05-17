using Copart.BLL.Services.BackgroundJob;

namespace Copart.Api.Extensions
{
    public static class AppJobsExtension
    {
        public static void ConfigureBackgroundJobs(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var queue = scope.ServiceProvider.GetRequiredService<IBackgroundJobQueue>();

                queue.EnqueueJob(async token =>
                {
                    
                });
            }
        }
    }
}
