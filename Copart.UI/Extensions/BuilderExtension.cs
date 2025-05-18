using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace Copart.UI.Extensions
{
    public static class BuilderExtension
    {
        public static void Configure(this WebAssemblyHostBuilder builder)
        {
            builder.Services.AddOidcAuthentication(options =>
            {
                builder.Configuration.Bind("Auth0", options.ProviderOptions);

                options.ProviderOptions.ResponseType = "code";
                options.ProviderOptions.AdditionalProviderParameters.Add("audience", builder.Configuration["Auth0:Audience"]!);
            });
        }
    }
}
