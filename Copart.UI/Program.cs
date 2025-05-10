using Copart.UI;
using Copart.UI.Apis.LotApi;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices();
builder.Services.AddHttpClient<ILotApi, LotApi>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7043");
});
await builder.Build().RunAsync();
