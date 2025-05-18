using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NotesTakerApp.Core.Models;
using NotesTakerApp.Core.Repositories;
using NotesTakerApp.Infrastructure.Data;
using NotesTakerApp.Infrastructure.Repositories;
using NotesTakerApp.Presentation.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<UsersIdentityDb>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("NotesTakerAppSqlServerContext");
    options.UseSqlServer(connectionString);
});

builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
})
.AddEntityFrameworkStores<UsersIdentityDb>()
.AddDefaultTokenProviders();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<INoteRepository, NoteSqlRepository>(); // If still using PostgreSQL for notes



builder.Services.AddScoped<EmailSender>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Identity/Login";
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("MyPolicy", policy =>
    {
        policy.RequireRole("Admin", "User");
        policy.RequireClaim(ClaimTypes.Name);
    });
});

builder.Services.AddSignalR(options => options.EnableDetailedErrors = true);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    string[] roles = ["Admin", "User"];
    foreach (var role in roles)
    {
        if (await roleManager.FindByNameAsync(role) == null)
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}

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

app.MapHub<NotesHub>("/notesHub");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
