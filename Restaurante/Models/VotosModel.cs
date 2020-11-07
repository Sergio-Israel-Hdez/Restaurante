using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurante.Models
{
    public class VotosModel
    {
        public int RestauranteId { get; set; }
        public int Nota { get; set; }
        public int CantidadVoto { get; set; }
    }
}
