using Copart.BLL.Services.VehicleService;
using Copart.Data;
using Copart.Data.Repositories;
using Copart.Domain.BaseRepositories;
using Microsoft.EntityFrameworkCore;

namespace Copart.Api.Extensions
{
    public static class BuilderExtension
    {
        public static void Configure(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IVehicleService, VehicleService>();
            builder.Services.AddSignalR();
            builder.Services.AddDbContext<CopartDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("Sql")));
        }
    }
}
