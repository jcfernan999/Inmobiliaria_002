using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Inmobiliaria_002.Models
{
    public class Inmueble
    {
        [Display(Name = "Código")]
        public int inmuebleId { get; set; }
        [Required]
        public string Direccion { get; set; }
        [Required]
        public int Ambientes { get; set; }
        [Required]
        public int Superficie { get; set; }
        public string Latitud { get; set; }
        public string Longitud { get; set; }
        [Display(Name = "Dueño")]
        public int PropietarioId { get; set; }
        [ForeignKey("PropietarioId")]
        public Propietario Duenio { get; set; }
        public Boolean EstaPublicado { get; set; }
        public Boolean EstaHabilitado { get; set; }
    }
}
