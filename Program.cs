using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using FP._059_NAGASystems_Prod3.Data;
using System.Diagnostics; // Importante para utilizar Process

var builder = WebApplication.CreateBuilder(args);

// Agrega el contexto de la base de datos
builder.Services.AddDbContext<FP_059_NAGASystems_Prod3Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("FP_059_NAGASystems_Prod3Context") ?? throw new InvalidOperationException("Connection string 'FP_059_NAGASystems_Prod3Context' not found.")));

// Agrega servicios al contenedor.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configura el pipeline de solicitudes HTTP.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // El valor predeterminado de HSTS es de 30 días. Puedes cambiar esto para escenarios de producción, consulta https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Lanzar el navegador automáticamente al iniciar la aplicación


    Process.Start(new ProcessStartInfo("cmd", $"/c start http://localhost:{builder.Configuration["ApplicationPort"]}") { CreateNoWindow = true });

app.Run();