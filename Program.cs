using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Stardeck.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddMvcCore();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add cookie Authenticationas a default politic
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "";
        options.AccessDeniedPath = "";
    });
//Add cors  
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            //Allow exchange betwen proxy, real server and web browser
            policy.WithOrigins("https://localhost:44069", "https://localhost:7212", "https://localhost:5056", "https://localhost:44478").AllowCredentials();
            policy.WithExposedHeaders("*");
            policy.AllowAnyHeader();
            policy.AllowAnyMethod();
        });
});

builder.Services.AddHttpContextAccessor();

var connectionString = builder.Configuration.GetConnectionString("PosgreSQLConnection");
builder.Services.AddDbContext<StardeckContext>(options =>
options.UseNpgsql(connectionString));


var app = builder.Build();
app.UseSwaggerUI();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseSwagger(x => x.SerializeAsV2 = true);
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();
app.UseCookiePolicy(new CookiePolicyOptions());

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
