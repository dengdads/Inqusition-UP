using Blazored.LocalStorage;
using Microsoft.AspNetCore.Hosting;
using System.Reflection.PortableExecutable;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

string fileName = "test.json";
StreamReader file = File.OpenText(fileName);
JsonTextReader reader = new JsonTextReader(file);
JObject jsonObject = (JObject)JToken.ReadFrom(reader);
string address = jsonObject["address"].ToString() + "/";
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddBlazoredLocalStorage(config =>
        config.JsonSerializerOptions.WriteIndented = true
    );
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(address) });
builder.Services.AddMasaBlazor(builder =>
{
    builder.ConfigureTheme(theme =>
    {
        theme.Themes.Light.Primary = "#4318FF";
        theme.Themes.Light.Accent = "#4318FF";
    });
}).AddI18nForServer("wwwroot/i18n");
var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();