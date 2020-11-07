using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Restaurante.Models.DB
{
    public partial class RestauranteVotaciones
    {
        public int VotoId { get; set; }
        public DateTime? FechaVoto { get; set; }
        [Required]
        public int? RestauranteId { get; set; }
        [Required]
        public short? Rating { get; set; }

        public virtual Restaurante Restaurante { get; set; }
    }
}
