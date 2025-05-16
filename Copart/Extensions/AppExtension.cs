using Copart.Api.Hubs;

namespace Copart.Api.Extensions
{
    public static class AppExtension
    {
        public static void Configure(this WebApplication app)
        {
            app.UseCors(policy =>
              policy.AllowAnyOrigin()
             .AllowAnyMethod()
             .AllowAnyHeader()
             );
            app.MapHub<BidsHub>("/bidsHub");
        }
    }
}
