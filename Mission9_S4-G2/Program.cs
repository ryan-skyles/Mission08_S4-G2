using Microsoft.EntityFrameworkCore;
using Mission8_S4_G2.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<QuadrantsContext>(options =>
{
    options.UseSqlite(builder.Configuration["ConnectionStrings:DatabaseConnection"]);
});

//builder.Services.AddScoped<ITasksRepository, EFTasksRepository>();

var app = builder.Build();

// Ensure database is created and seed categories
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<QuadrantsContext>();
    db.Database.EnsureCreated();

    if (!db.Categories.Any())
    {
        db.Categories.AddRange(
            new Category { CategoryName = "Home" },
            new Category { CategoryName = "School" },
            new Category { CategoryName = "Work" },
            new Category { CategoryName = "Church" }
        );

        db.SaveChanges();
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // ...
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
