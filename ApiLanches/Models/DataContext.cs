using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ApiLanches.Models
{
    public class DataContext: DbContext
    {
        public DataContext()
            : base("Lanches")
        {
            // Database.SetInitializer(new GraficoDbInicialise());
            // Configuration.LazyLoadingEnabled = true;
        }

        public virtual DbSet<Ingrediente> Ingredientes { get; set; }
        public virtual DbSet<Lanche> Lanches { get; set; }

    }
}