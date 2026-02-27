using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TestDeploy.Controllers.Data;

var builder = WebApplication.CreateBuilder(args);

// Railway port
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

// DEV = SQL Server
// PROD = PostgreSQL
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(
            builder.Configuration.GetConnectionString("DefaultConnection")));
}
else
{
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseNpgsql(
            builder.Configuration.GetConnectionString("DefaultConnection")));
}

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("AllowAll");

app.UseAuthentication();   // 🔥 thêm dòng này
app.UseAuthorization();

app.MapControllers();


// 🔥 Auto migrate database khi deploy
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
}

app.Run();