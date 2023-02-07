using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using reCAPTCHA.AspNetCore;
using WebApplication3.Model;
using WebApplication3.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<AuthDbContext>();
builder.Services.AddIdentity<CustomUser, IdentityRole>(Options =>
{ 
    Options.User.RequireUniqueEmail = true;
    Options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);
    Options.Lockout.MaxFailedAccessAttempts = 3;

}).AddEntityFrameworkStores<AuthDbContext>();


builder.Services.AddRecaptcha(builder.Configuration.GetSection("ReCaptcha"));
builder.Services.AddSingleton<HttpContextAccessor, HttpContextAccessor>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
/*builder.Services.AddSession(option =>
{
    option.Cookie.HttpOnly = true;
    option.Cookie.IsEssential = true;
});*/

builder.Services.AddAuthentication("MyAuthCookie").AddCookie("MyAuthCookie",options =>
{
    options.Cookie.Name = "MyAuthCookie";
    options.AccessDeniedPath = "/Error/NotFound?errorCode={0}";
    options.LoginPath = "/Login";
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Login";
    options.SlidingExpiration = true;
    options.LogoutPath = "/Error/Timeout";
    options.Events = new CookieAuthenticationEvents
    {
        OnRedirectToAccessDenied = Context =>
        {
            Context.Response.StatusCode = 401;
            Context.Response.Redirect("/Error/Timeout");
            return Task.CompletedTask;
        }
    };

    options.AccessDeniedPath = "/Error";
    options.ExpireTimeSpan = TimeSpan.FromSeconds(60);
});

builder.Services.Configure<GoogleCaptchacs>(builder.Configuration.GetSection("ReCaptcha"));
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<PasswordService>();
builder.Services.AddScoped<LogginService>();


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
app.UseCookiePolicy();

app.UseStatusCodePagesWithRedirects("/Errors/NotFound?errorCode={0}");

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
