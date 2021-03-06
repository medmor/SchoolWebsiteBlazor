using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SchoolWebsite;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<SchoolWebsite.App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddLocalization();

var host = builder.Build();
await host.SetDefaultCulture();
await host.RunAsync();
