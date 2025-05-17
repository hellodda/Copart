using Copart.Api.Extensions;
using Copart.UI;
using Copart.UI.Extensions;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices();
builder.Services.AddProjectClients(new ("https://localhost:7043"));

builder.Configure();

await builder.Build().RunAsync();
