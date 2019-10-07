using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inmobiliaria_002.Models;

namespace Inmobiliaria_002.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Inmobiliaria_002.Models.Propietario> propietarios { get; set; }
        public DbSet<Inmobiliaria_002.Models.Inquilino> inquilino { get; set; }
        public DbSet<Inmobiliaria_002.Models.Inmueble> inmueble { get; set; }
        public DbSet<Inmobiliaria_002.Models.Pago> pago { get; set; }
        public DbSet<Inmobiliaria_002.Models.Alquiler> alquiler { get; set; }


    }
}
