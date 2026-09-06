using Microsoft.EntityFrameworkCore;
using Project.Application.Services;
using Project.Application.Services.Funcionario;
using Project.Domain.Interfaces;
using Project.Infrastructure.Data;
using Project.Infrastructure.Repository;
using Project.Infrastructure.UnitOfWork;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Reposity
builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddScoped<IFuncionarioRepository, FuncionarioRepository>();

// Services
builder.Services.AddScoped<IPedidoService, PedidoService>();
builder.Services.AddScoped<IFuncionarioService, FuncionarioService>();

// Unit Of Work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

Console.WriteLine(connectionString);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.MapControllers();

app.Run();