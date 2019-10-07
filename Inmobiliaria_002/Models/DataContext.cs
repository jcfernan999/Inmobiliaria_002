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
        public DbSet<Inmobiliaria_002.Models.Propietario> Propietarios { get; set; }
        public DbSet<Inmobiliaria_002.Models.Inquilino> Inquilino { get; set; }
        public DbSet<Inmobiliaria_002.Models.Garante> Garante { get; set; }
        public DbSet<Inmobiliaria_002.Models.Inmueble> Inmueble { get; set; }
        
    }
}
