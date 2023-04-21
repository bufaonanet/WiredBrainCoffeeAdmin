using Microsoft.EntityFrameworkCore;
using WiredBrainCoffeeAdmin.Data;
using WiredBrainCoffeeAdmin.Data.Repositories;
using WiredBrainCoffeeAdmin.HttpServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<WiredContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("WiredBrain")));

builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddHttpClient();

builder.Services.AddHttpClient<ITicketService, TicketService>(options =>
{
    options.BaseAddress = new Uri("https://wiredbraincoffeeadmin.azurewebsites.net/");
});

var app = builder.Build();

//Migrations
await EnsureDbCreated(app.Services, app.Logger);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");    
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();

async Task EnsureDbCreated(IServiceProvider services, ILogger logger)
{
    using var db = services.CreateScope()
        .ServiceProvider.GetRequiredService<WiredContext>();
    await db.Database.MigrateAsync();
}