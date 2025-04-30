using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NotesTakerApp.Core.Repositories;
using NotesTakerApp.Core.Services;
using NotesTakerApp.Infrastructure.Data;
using NotesTakerApp.Infrastructure.Repositories;
using NotesTakerApp.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<UserSqlServerDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("NotesTakerAppSqlServerContext")));
// builder.Services.AddDbContext<UsersIdentityDb>(options =>
//     options.UseSqlServer(builder.Configuration.GetConnectionString("NotesTakerIdentityServerContext")));
builder.Services.AddDbContext<NotePostgresDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("NotesTakerAppPostgreSqlServerContext")));
builder.Services.AddScoped<IHttpLogRepository, HttpLogMsSqlRepository>();
builder.Services.AddScoped<IUserRepository, UserMSSqlRepository>();
builder.Services.AddScoped<INoteRepository, NotePostgreSqlRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<INoteService, NoteService>();
// builder.Services.AddDbContext<UsersIdentityDb>(options =>
// {
//     var connectionString = builder.Configuration.GetConnectionString("IdentityDb");
//     options.UseSqlServer(connectionString);
// });

// builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => {})
//     .AddEntityFrameworkStores<UsersIdentityDb>();

builder.Services.AddDataProtection();

builder.Services.AddAuthentication(defaultScheme: CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(
        authenticationScheme: CookieAuthenticationDefaults.AuthenticationScheme,
        configureOptions: options =>
        {
            options.LoginPath = "/Identity/Login";
        });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("MyPolicy", policy =>
    {
        policy.RequireRole("Admin")
            .RequireClaim(ClaimTypes.Name, "Admin")
            .RequireRole("User");
    });
});

var app = builder.Build();

var serviceScope = app.Services.CreateScope();

// await roleManager.CreateAsync(new IdentityRole {Name = "Admin"});
// await roleManager.CreateAsync(new IdentityRole {Name = "User"});

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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

