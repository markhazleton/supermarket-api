using Microsoft.EntityFrameworkCore;
using Supermarket.API.Controllers.Config;
using Supermarket.API.Extensions;
using Supermarket.API.Persistence.Contexts;
using Supermarket.API.Persistence.Repositories;
using Supermarket.API.Services;
using Supermarket.Domain.Repositories;
using Supermarket.Domain.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddMemoryCache();
builder.Services.AddCustomSwagger();

builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
{
    options.InvalidModelStateResponseFactory = InvalidModelStateResponseFactory.ProduceErrorResponse;
});

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseInMemoryDatabase(builder.Configuration.GetConnectionString("memory") ?? "data-in-memory");
});

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.UseCustomSwagger();

// Seed database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        await SeedData.Seed(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred seeding the DB.");
    }
}

await app.RunAsync();
