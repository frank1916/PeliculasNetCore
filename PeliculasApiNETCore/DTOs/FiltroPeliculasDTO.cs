using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasApiNETCore.DTOs
{
    public class FiltroPeliculasDTO
    {
        public int pagina { get; set; } = 1;
        public int cantidadRegistrosPorPagina { get; set; } = 10;
        public PaginacionDTO paginacion
        {
            get { return new PaginacionDTO() { pagina = pagina, CantidadRegistrosPorPagina = cantidadRegistrosPorPagina }; } 
        }
        public string titulo { get; set; }
        public int generoId { get; set; }
        public bool enCines { get; set; }
        public bool proximosEstrenos { get; set; }
        public string campoOrdenar  { get; set; }
        public bool ordenAscendente { get; set; } = true;
    }

}
