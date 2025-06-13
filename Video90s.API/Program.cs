using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Video90s.API.Data;
using Video90s.API.Services;

var builder = WebApplication.CreateBuilder(args);

// 1. Configurar DbContext con resiliencia a errores transitorios
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions => sqlOptions.EnableRetryOnFailure()
    )
);

// 2. Registrar tu servicio de autenticación
builder.Services.AddScoped<IAuthService, AuthService>();

// 3. Añadir controladores
builder.Services.AddControllers();

// 4. Configurar Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 5. Configurar JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer           = true,
            ValidateAudience         = true,
            ValidateLifetime         = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer              = builder.Configuration["Jwt:Issuer"],
            ValidAudience            = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey         = new SymmetricSecurityKey(
                                            Encoding.UTF8.GetBytes(
                                                builder.Configuration["Jwt:Key"]
                                            )
                                        ),
            ClockSkew = TimeSpan.Zero
        };
    });

var app = builder.Build();

// 6. Auto-aplicar migraciones al arrancar
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
}

// 7. Middleware
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Video90s API V1");
    c.RoutePrefix = "swagger"; // <- de nuevo en /swagger
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
