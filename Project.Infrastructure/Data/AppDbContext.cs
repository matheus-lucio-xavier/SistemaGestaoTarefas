using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.Domain.Entities;

namespace Project.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<FuncionarioModel> Funcionarios { get; set; }
        public DbSet<PedidoModel> Pedidos { get; set; }
        public DbSet<TarefaModel> Tarefas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TarefaModel>()
                .HasOne(t => t.Funcionario)
                .WithMany(f => f.Tarefas)
                .HasForeignKey(t => t.FuncionarioId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(true);
            modelBuilder.Entity<TarefaModel>()
                .HasOne(t => t.Pedido)
                .WithOne()
                .HasForeignKey<TarefaModel>(t => t.PedidoId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(true);
        }
    }
}