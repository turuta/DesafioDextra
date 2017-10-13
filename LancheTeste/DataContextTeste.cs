using ApiLanches.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
namespace LancheTeste
{
    public class DataContextTeste : DbContext
    {
        public DataContextTeste()
            : base("Lanches")
        {
            // Database.SetInitializer(new GraficoDbInicialise());
            // Configuration.LazyLoadingEnabled = true;
        }

        public virtual DbSet<Ingrediente> Ingredientes { get; set; }
        public virtual DbSet<Lanche> Lanches { get; set; }
    }
}
