using Microsoft.EntityFrameworkCore;
using Sistema_Gestion_Tickets.Data;
using Sistema_Gestion_Tickets.Repositories.Implementations;
using Sistema_Gestion_Tickets.Repositories.Interfaces;
using Sistema_Gestion_Tickets.Services.Implementations;
using Sistema_Gestion_Tickets.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<SAEContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("AppConnection"))); //dbcontext para conectar con la base de datos

builder.Services.AddScoped<IAdministradorRepository, AdministradorRepository>();
builder.Services.AddScoped<IEstacionamientoRepository, EstacionamientoRepository>();
builder.Services.AddScoped<ITicketRepository, TicketRepository>();
builder.Services.AddScoped<IVehicle, VehicleService>();
builder.Services.AddScoped<IAdmin, AdminService>();



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
