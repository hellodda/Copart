using Copart.BLL.Services.LotService;
using Copart.BLL.Services.VehicleService;
using Copart.Data;
using Copart.Data.Repositories;
using Copart.Domain.BaseRepositories;
using Microsoft.EntityFrameworkCore;
using Copart.BLL.Profiles;
using Copart.BLL.Services.BidService;
using Copart.BLL.Services.BidderService;
using FluentValidation;
using Copart.BLL.Validators;
using Copart.BLL.Services.SearchService;
using Copart.BLL.Services.BackgroundJob;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Copart.Api.Extensions
{
    public static class BuilderExtension
    {
        public static void Configure(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IVehicleService, VehicleService>();
            builder.Services.AddScoped<ILotService, LotService>();
            builder.Services.AddScoped<IBidService, BidService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ISearchService, SearchService>();
            builder.Services.AddSignalR();
            builder.Services.AddSingleton<IBackgroundJobQueue, BackgroundJobQueue>();
            builder.Services.AddHostedService<BackgroundJobService>();
            builder.Services.AddValidatorsFromAssemblyContaining<BidValidator>(ServiceLifetime.Transient);
            builder.Services.AddAutoMapper(typeof(LotProfile));


            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = builder.Configuration["Auth0:Authority"];
                options.Audience = builder.Configuration["Auth0:Audience"];
            });

            builder.Services.AddDbContext<CopartDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("Sql")));
        }
    }
}
