﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inmobiliaria_002.Models
{
     
    public interface IRepositorioInmueble : IRepositorio<Inmueble>
	{

        //IList<Inmueble> BuscarPorAlquileres(int InmuebleId);
    }
}
