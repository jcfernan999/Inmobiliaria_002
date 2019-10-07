using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inmobiliaria_002.Models
{
	public interface IRepositorioPropietario : IRepositorio<Propietario>
	{
		Propietario ObtenerPorEmail(string email);
	}
}
