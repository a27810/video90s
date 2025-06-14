using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Video90s.API.Data;
using Video90s.API.Services;

var builder = WebApplication.CreateBuilder(args);

// --- 1) DbContext con resiliencia ---
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions => sqlOptions.EnableRetryOnFailure()
    )
);

// --- 2) AuthService, Controllers, Swagger, JWT (igual que antes) ---
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => { /* ... tu configuración ... */ });

// --- 3) HttpClient para ExternalService ---
builder.Services
       .AddHttpClient<IExternalService, ExternalService>(client =>
       {
           client.BaseAddress = new Uri("https://api.tvmaze.com/");
       });

var app = builder.Build();

// Auto-migrate (igual que antes)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
}

// Middleware (igual que antes)
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Video90s API V1");
    c.RoutePrefix = "swagger";
});
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
