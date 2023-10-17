using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back.CoNexionDB
{
    public class AplicacionDBcontext : DbContext
    {
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Cuenta_bancaria> Cuenta_Bancarias { get; set; }
        public DbSet<Tarjeta_de_credito> Tarjeta_De_Creditos { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server = LAPTOP-7U6PB0A0; database = EntidadFinanciera; trusted_connection = true; Encrypt = False");
        }
    }
}
