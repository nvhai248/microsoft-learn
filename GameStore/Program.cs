using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using GameStore.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add services
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddSignalR();
builder.Services.AddAuthorization();

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

app.UseHttpsRedirection();
app.UseStaticFiles();

// Custom middleware
app.Use(async (context, next) =>
{
    context.Response.Headers.Append("x-my-custom-header", "My custom value");
    await next.Invoke();
});

app.UseRouting();
app.UseAuthorization();

// API endpoints
app.MapControllers();

// Razor Pages endpoints
app.MapRazorPages();

app.Run();
