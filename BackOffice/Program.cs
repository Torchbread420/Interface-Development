using DataAccessLayer;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// We gebruiken voor nu even een SQLite voor de database,
// omdat deze eenvoudig lokaal te gebruiken is en geen extra configuratie nodig heeft.
builder.Services.AddDbContext<MatrixIncDbContext>();
builder.Services.AddControllersWithViews();

// We registreren de repositories in de DI container
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IPartRepository, PartRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Zorg ervoor dat de database is aangemaakt en gevuld met testdata
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<MatrixIncDbContext>();
    MatrixIncDbInitializer.Initialize(context);
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();