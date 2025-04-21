using Microsoft.EntityFrameworkCore;
using NotesTakerApp.Core.Repositories;
using NotesTakerApp.Core.Services;
using NotesTakerApp.Infrastructure.Data;
using NotesTakerApp.Infrastructure.Repositories;
using NotesTakerApp.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<NotesTakerSqlServerDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("NotesTakerAppSqlServerContext")));
builder.Services.AddDbContext<NotesTakerNoteDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("NotesTakerAppPostgreSqlServerContext")));
builder.Services.AddScoped<IUserRepository, UserMSSqlRepository>();
builder.Services.AddScoped<INoteRepository, NotePostgreSqlRepository>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

