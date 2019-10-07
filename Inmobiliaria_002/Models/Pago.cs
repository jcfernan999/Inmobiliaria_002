using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inmobiliaria_002.Models
{
	public class Pago
    {
		public int PagoId { get; set; }
		[Required]
		public string Numero { get; set; }
		[Required]
		public DateTime Fecha { get; set; }
		[Required]
		public string Importe { get; set; }

        [Display(Name = "Cuota")]
        public int AlquilerId { get; set; }
        [ForeignKey("AlquilerId")]
        public Alquiler miAlquiler { get; set; }



    }
}
