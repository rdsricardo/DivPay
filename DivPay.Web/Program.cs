using DivPay.Web.HttpClients;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.LoginPath = "/Usuario/Login";
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddHttpClient<DividaApiClient>(client =>
{
    client.BaseAddress = new System.Uri(builder.Configuration.GetValue<string>("AppSettings:ApiUrl"));
});

builder.Services.AddHttpClient<ClienteApiClient>(client =>
{
    client.BaseAddress = new System.Uri(builder.Configuration.GetValue<string>("AppSettings:ApiUrl"));
});

builder.Services.AddHttpClient<UsuarioApiClient>(client =>
{
    client.BaseAddress = new System.Uri(builder.Configuration.GetValue<string>("AppSettings:ApiUrl"));
});

builder.Services.AddHttpClient<AuthApiClient>(client =>
{
    client.BaseAddress = new System.Uri(builder.Configuration.GetValue<string>("AppSettings:ApiAuthUrl"));
});

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
