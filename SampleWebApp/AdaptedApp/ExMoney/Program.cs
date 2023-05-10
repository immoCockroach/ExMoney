using ExMoney.Data;
using ExMoney.Services;

var builder = WebApplication.CreateBuilder(args);

//Configuration
builder.Configuration["AuthBaseAddress"] = "http://localhost:3000";
builder.Configuration["BackendBaseAddress"] = "http://localhost:3000";

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddLogging();

builder.Services.AddHttpClient();

//Add backend HttpClients
builder.Services.AddScoped<Backend>();
builder.Services.AddTransient<AuthService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}


app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
