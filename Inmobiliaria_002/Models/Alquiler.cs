using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations.Schema;


namespace Inmobiliaria_002.Models
{
    public class Alquiler
    {
        public int AlquilerId { get; set; }

        [Required]
        public DateTime FechaAlta { get; set; }

        [Required]
        public DateTime FechaBaja { get; set; }

        [Required]
        public string Monto { get; set; }

        public string Descripcion { get; set; }

        [Display(Name = "Propiedad")]
        public int InmuebleId { get; set; }
        [ForeignKey("InmuebleId")]
        public Inmueble miInmueble { get; set; }

        [Display(Name = "Inquilino")]
        public int InquilinoId { get; set; }
        [ForeignKey("InquilinoId")]
        public Inquilino miInqulino { get; set; }

    }
}


 