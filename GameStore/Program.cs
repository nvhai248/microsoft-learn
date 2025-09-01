using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using GameStore.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add services
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddSignalR();
// Load AppConstant from configuration
AppConstant.Init(builder.Configuration);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
{
    options.LoginPath = "/Index";
    options.AccessDeniedPath = "/Denied";
})
.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = AppConstant.Jwt.Issuer,
        ValidAudience = AppConstant.Jwt.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(AppConstant.Jwt.SecretKey))
    };
});

// ✅ Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularDev", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Get connection string
builder.Services.AddDbContext<GameStoreContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add service swagger to 
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "GameStore API",
        Version = "v1"
    });
});

var app = builder.Build();

// Error handling & Swagger
if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "GameStore API v1");
    });
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

// Enable CORS middleware
app.UseCors("AllowAngularDev");

app.UseHttpsRedirection();
app.UseStaticFiles();

// Custom middleware
app.Use(async (context, next) =>
{
    context.Response.Headers.Append("x-my-custom-header", "My custom value");
    await next.Invoke();
});

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// API endpoints
app.MapControllers();

// Razor Pages endpoints
app.MapRazorPages();

app.Run();
