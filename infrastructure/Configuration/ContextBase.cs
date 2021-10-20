using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace infrastructure.Configuration
{
    public class ContextBase : DbContext
    {
        public ContextBase(DbContextOptions<ContextBase> options) : base (options)
        {
            //Database.EnsureCreated();
        }

        public DbSet<Client> Product { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(GetStringConectionConfig());
            base.OnConfiguring(optionsBuilder);
        }

        private string GetStringConectionConfig()
        {
            string strCon = @"Data Source=DESKTOP-ODE7HG1\SQLEXPRESS02;Initial Catalog=Imobiliaria;User ID=herman;Password=123;Integrated Security=True;";
            return strCon;
        }
    }
}
