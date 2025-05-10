using Copart.BLL.Services.LotService;
using Copart.BLL.Services.VehicleService;
using Copart.Data;
using Copart.Data.Repositories;
using Copart.Domain.BaseRepositories;
using Microsoft.EntityFrameworkCore;
using Copart.BLL.Profiles;

namespace Copart.Api.Extensions
{
    public static class BuilderExtension
    {
        public static void Configure(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
            builder.Services.AddScoped<ILotRepository, LotRepository>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IVehicleService, VehicleService>();
            builder.Services.AddScoped<ILotService, LotService>();
            builder.Services.AddSignalR();
            builder.Services.AddAutoMapper(typeof(LotProfile));
            builder.Services.AddDbContext<CopartDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("Sql")));
        }
    }
}
