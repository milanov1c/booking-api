using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Booking.API;
using Booking.API.Middleware;
using Booking.DataAccess;
using Booking.DataAccess.Seeding;
using Booking.Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
var settings = new AppSettings();
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Configuration.Bind(settings);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Booking API",
        Version = "v1",
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Unesite: Bearer {vaš JWT token}",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
    });

    options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        { new OpenApiSecuritySchemeReference("Bearer", document),new List<string>()},
    });
});

const string AngularClientPolicy = "AngularClient";
builder.Services.AddCors(options =>
{
    options.AddPolicy(AngularClientPolicy, policy =>
    {
        policy.WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});



var seedSettings = new SeedSettings();
builder.Configuration.GetSection("Seed").Bind(seedSettings);
settings.Seed = seedSettings;

builder.Services.SetupApplication(settings);
builder.Services.AddAuthentication(options =>
{
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(cfg =>
{
    cfg.RequireHttpsMetadata = false;
    cfg.SaveToken = true;
    cfg.IncludeErrorDetails = true;
    cfg.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = settings.JwtSettings.Issuer,
        ValidateIssuer = true,
        ValidAudience = "Any",
        ValidateAudience = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.JwtSettings.SecretKey)),
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,
        RoleClaimType = ClaimTypes.Role
    };
    cfg.Events.OnTokenValidated = context =>
    {
        /*var dbContext = context.HttpContext.RequestServices.GetRequiredService<BookingDbContext>();
        var token = context.HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1];
        var tokenObj = new JwtSecurityTokenHandler().ReadJwtToken(token);

        var tokenId = tokenObj.Claims.FirstOrDefault(x => x.Type == "TokenId").Value;

        AuthToken dbToken = dbContext.AuthTokens.FirstOrDefault(x => x.TokenId == tokenId);

        if(dbToken == null || dbToken.InvalidatedAt.HasValue)
        {
            context.Fail("Unauthorized");
        }*/
        
        return Task.CompletedTask;
    };

});



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<BookingDbContext>();
    var seeder = scope.ServiceProvider.GetRequiredService<DatabaseSeeder>();
    var seedOptions = scope.ServiceProvider.GetRequiredService<SeedSettings>();
    var logger = scope.ServiceProvider.GetRequiredService<ILoggerFactory>()
        .CreateLogger("DatabaseSeed");

    SeedBootstrapper.Run(context, seeder, seedOptions, logger);
}

app.UseCors(AngularClientPolicy);

app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// GlobalExceptionHandlingMiddleware mora biti PRE ApiKeyAuthorizationMiddleware
// da bi hvatao izuzetke koje ovaj drugi baci (inace probijaju direktno do
// Kestrel-a kao goli 500 bez logovanja i bez JSON tela).
app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.UseMiddleware<ApiKeyAuthorizationMiddleware>();

app.MapControllers();

app.Run();

