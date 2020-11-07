using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Restaurante.Models.DB
{
    public partial class ItemDestacado
    {
        public int ItemDestacadoId { get; set; }
        [Required]
        public int? RestauranteId { get; set; }
        [Required]
        public string Descripcion { get; set; }
        [Required]
        public byte[] Foto { get; set; }

        public virtual Restaurante Restaurante { get; set; }
    }
}
