using SB_Controller.Components;
using Core;
using Blazored.LocalStorage;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddHttpClient("ApiDebug", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiDebug:BaseUrl"] ?? "http://localhost:5062");
});

builder.Services.AddSingleton(_ => new HttpClient
{//Lavet af chatten, men samme type kode som vi benytter i program.cs ved backend. Kunne ikke finde dokumentation i vores lektioner.
    BaseAddress = new Uri("http://localhost:5046/")
});

builder.Services.AddBlazoredLocalStorage(); //local storage

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
