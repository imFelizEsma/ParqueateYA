using Microsoft.EntityFrameworkCore;
using Sistema_Gestion_Tickets.Data;
using Sistema_Gestion_Tickets.Repositories.Implementations;
using Sistema_Gestion_Tickets.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<SAEContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("AppConnection"))); //dbcontext para conectar con la base de datos

builder.Services.AddScoped<IAdministradorRepository, AdministradorRepository>();
builder.Services.AddScoped<IEstacionamientoRepository, EstacionamientoRepository>();
builder.Services.AddScoped<ITicketRepository, TicketRepository>();



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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
