using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using NovelReader.UI.Common.Services.IdGeneratorService;
using NovelReader.UI.Common.Services.LocalStorageService;
using NovelReader.UI.Common.Services.RequestService;
using NovelReader.UI.Common.Services.ToastService;
using NovelReader.UI.Common.Shared;
using NovelReader.UI.Common.ViewModels;
using NovelReader.WebClient;
using NovelReader.WebClient.Services.LocalStorageService;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7124/api/") });
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddScoped<IRequestService, RequestService>();
builder.Services.AddScoped<IStorageService, LocalStorageService>();
builder.Services.AddTransient<IdGeneratorService>();
builder.Services.AddSingleton<IToastService, ToastService>();

builder.Services.AddScoped<LoginViewModel>();

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();

await builder.Build().RunAsync();
