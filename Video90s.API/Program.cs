using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Video90s.API.Data;
using Video90s.API.Services;

var builder = WebApplication.CreateBuilder(args);

// 1) Configuramos nuestra base de datos con paciencia de santo
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions => sqlOptions.EnableRetryOnFailure()
    )
);

// 2) Montamos el servicio de autenticación y lo demás de siempre:
//    - IAuthService: nuestro guardián de tokens
//    - Controladores: para que ASP.NET escuche peticiones
//    - Swagger: para que podamos jugar con la API en el navegador
//    - JWT: para que nadie indeseado se cuele sin invitación
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => { /* ... tu configuración ... */ });

// 3) Preparamos un HttpClient apañado para hablar con TVmaze
builder.Services
       .AddHttpClient<IExternalService, ExternalService>(client =>
       {
           client.BaseAddress = new Uri("https://api.tvmaze.com/");
       });

var app = builder.Build();

// 4) Auto-migraciones al arrancar, estilo “funciona o muere intentándolo”
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
}

// Middleware (igual que antes) --> swagger en su lugar
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
