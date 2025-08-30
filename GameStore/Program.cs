using GameStore.Dtos;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddControllersWithViews();
builder.Services.AddAuthorization();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "GameStore API",
        Version = "v1"
    });
});

var app = builder.Build();

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
}

app.UseStaticFiles();

app.Use(async (context, next) =>
{
    context.Response.Headers.Append("x-my-custom-header", "My custom valuee");
    await next.Invoke();
});

app.MapControllers();

app.Run();
