using CareerGuidancePlatform.Data;
using CareerGuidancePlatform.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://localhost:5037");

// 🔗 Setting connection to MySQL
var connectionString = "server=127.0.0.1;port=3306;database=career_guidance;user=root;password=;";
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// ⚙️ Các dịch vụ MVC và Session
builder.Services.AddControllers();        // ✅ Cho Web API
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddAuthorization();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoadmapService, RoadmapService>();
builder.Services.AddScoped<IProgressService, ProgressService>();
builder.Services.AddScoped<IPhaseSubstepService, PhaseSubstepService>();
builder.Services.AddScoped<IResumeService, ResumeService>();



var app = builder.Build();


// 2. Áp migration & seed dữ liệu có điều kiện
using(var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    // Áp migration
    db.Database.Migrate();

    // Thư mục chứa các file seed
    var seedFolder = Path.Combine(app.Environment.ContentRootPath, "Data", "Seed");

    if (Directory.Exists(seedFolder))
    {
        var sqlFiles = Directory.GetFiles(seedFolder, "*.sql")
                                .OrderBy(path => path);

        foreach(var file in sqlFiles)
        {
            var fileName = Path.GetFileName(file);            // e.g. "Users.sql"
            bool shouldSeed = fileName switch
            {
                "users.sql" => !db.Users.Any(),
                "companies.sql" => !db.Companies.Any(),
                "messages.sql" => !db.Messages.Any(),
                "phase_substeps.sql" => !db.Messages.Any(),
                "roadmapsteps.sql" => !db.RoadmapSteps.Any(),
                "jobs.sql" =>  !db.Jobs.Any(),
                _ => false
            };

            if (!shouldSeed)
                continue;   // đã có data → bỏ qua

            var sql = File.ReadAllText(file);
            db.Database.ExecuteSqlRaw(sql);
        }
    }
}

// 📁 Cho phép dùng file tĩnh (nếu có frontend Razor, HTML, JS,...)
app.UseStaticFiles();

// ❌ Tắt HTTPS redirect nếu không cấu hình HTTPS
// app.UseHttpsRedirection(); // COMMENT để tránh lỗi cảnh báo

app.UseRouting();

app.UseSession();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors("AllowAll");



// ✅ Cho API Controller
app.MapControllers();

// ✅ Cho MVC Controller (nếu có view)
app.MapDefaultControllerRoute();

app.Run();
