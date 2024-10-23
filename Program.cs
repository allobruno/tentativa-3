var builder = WebApplication.CreateBuilder(args);

// Adiciona o servi�o de sess�o
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Tempo de expira��o da sess�o
    options.Cookie.HttpOnly = true; // Aumenta a seguran�a dos cookies
    options.Cookie.IsEssential = true; // Faz com que o cookie seja essencial
});

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Habilita o uso de sess�es
app.UseSession();  // <-- Adicione essa linha para habilitar a sess�o

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
