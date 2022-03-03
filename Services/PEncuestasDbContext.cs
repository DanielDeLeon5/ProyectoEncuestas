using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Encuestas.Entities;

namespace Encuestas.Services
{
    public class PEncuestasDbContext : DbContext 
    {
        public PEncuestasDbContext()
        {
        }

        public PEncuestasDbContext(DbContextOptions options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseSqlServer("A FALLBACK CONNECTION STRING");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Encuesta> Encuesta { get; set; }
        public DbSet<CampoEncuesta> CampoEncuestas { get; set; }
        public DbSet<ValorCampo> ValorCampo { get; set; }
        public object CampoEncuesta { get; internal set; }
    }
}
