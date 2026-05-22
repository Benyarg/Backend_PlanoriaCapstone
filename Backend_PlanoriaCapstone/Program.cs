using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PlanoriaCapstone.Bll.Interface;
using PlanoriaCapstone.Bll.Service;
using PlanoriaCapstone.Dal;
using PlanoriaCapstone.Dal.Interface;
using PlanoriaCapstone.Dal.Repository;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// ─── Database ─────────────────────────────────────────────────────────────────
builder.Environment.WebRootPath = Path.Combine(builder.Environment.ContentRootPath, "wwwroot");
Directory.CreateDirectory(builder.Environment.WebRootPath);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// ─── BLL Services ─────────────────────────────────────────────────────────────
builder.Services.AddScoped<IAuthService,     AuthService>();
builder.Services.AddScoped<IJwtService,      JwtService>();
builder.Services.AddScoped<IUsuarioService,  UsuarioService>();
builder.Services.AddScoped<IArchivoService,  ArchivoService>();
builder.Services.AddScoped<IFlashcardService,FlashcardService>();
builder.Services.AddScoped<IQuizService,     QuizService>();
builder.Services.AddScoped<IProgresoService, ProgresoService>();
builder.Services.AddScoped<IIAService,       GeminiService>();   // Required by ArchivoService (class is GeminiService)
builder.Services.AddScoped<IImageService,    ImageService>();    // Required by UsuarioService
builder.Services.AddHttpClient();                                // Required by GeminiService (IHttpClientFactory)

// ─── DAL Repositories ─────────────────────────────────────────────────────────
builder.Services.AddScoped<IUsuarioRepository,  UsuarioRepository>();
builder.Services.AddScoped<IArchivoRepository,  ArchivoRepository>();
builder.Services.AddScoped<IFlashcardRepository,FlashcardRepository>();
builder.Services.AddScoped<IQuizRepository,     QuizRepository>();
builder.Services.AddScoped<IProgresoRepository, ProgresoRepository>();

// ─── Controllers & Swagger ────────────────────────────────────────────────────
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Planoria API", Version = "v1" });

    // Allow sending JWT via Swagger UI
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name         = "Authorization",
        Type         = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme       = "bearer",
        BearerFormat = "JWT",
        In           = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description  = "Introduce el token JWT: Bearer {token}"
    });
    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id   = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// ─── JWT Authentication ───────────────────────────────────────────────────────
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer           = true,
            ValidateAudience         = true,
            ValidateLifetime         = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer              = builder.Configuration["Jwt:Issuer"],
            ValidAudience            = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey         = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });

builder.Services.AddAuthorization();

// ─── CORS (allow frontend dev servers) ───────────────────────────────────────
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});

// ─── Build app ────────────────────────────────────────────────────────────────
var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI(c =>
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Planoria API v1"));

//app.UseHttpsRedirection();
app.UseStaticFiles(); // Allow frontend to fetch uploaded files (images, pdfs)
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();