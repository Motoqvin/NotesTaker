using Back.Data;
using Back.Models;
using Back.Repositories;
using Back.Repositories.Base;
using Back.Services.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Back.Services;
using Back.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.InitAspnetIdentity(builder.Configuration);
builder.Services.InitAuth(builder.Configuration);
builder.Services.InitSwagger();
builder.Services.InitCors();


builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddDbContext<UserSqlServerDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("NotesTakerAppSqlServerContext")));
builder.Services.AddDbContext<NotePostgresDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("NotesTakerAppPostgreSqlServerContext")));
builder.Services.AddScoped<IHttpLogRepository, HttpLogMsSqlRepository>();
builder.Services.AddScoped<IUserRepository, UserMSSqlRepository>();
builder.Services.AddScoped<INoteRepository, NotePostgreSqlRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<INoteService, NoteService>();

builder.Services.AddScoped<EmailSender>();
builder.Services.AddIdentity<User, IdentityRole>(options => {})
    .AddEntityFrameworkStores<UsersIdentityDb>();

builder.Services.AddDataProtection();

builder.Services.AddSignalR(options =>
{
    options.EnableDetailedErrors = true;
});



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

var scope = app.Services.CreateScope();
var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
await roleManager.CreateAsync(new IdentityRole(UserRoleDefaults.User));
await roleManager.CreateAsync(new IdentityRole(UserRoleDefaults.Admin));

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.MapControllers();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors("BlazorApp");

app.Run();
