using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Restaurante.Models.DB
{
    public partial class Restaurante
    {
        public Restaurante()
        {
            ItemDestacado = new HashSet<ItemDestacado>();
            RestauranteVotaciones = new HashSet<RestauranteVotaciones>();
        }

        public int RestauranteId { get; set; }
        [Required]
        [Display(Name = "Nombre Restaurante")]
        public string NombreRestaurante { get; set; }
        [Required]
        public byte[] Logo { get; set; }
        [Required]
        [Display(Name = "Hora Apertura")]
        public string HoraApertura { get; set; }
        [Required]
        [Display(Name = "Horra de Cierre")]
        public string HoraCierre { get; set; }
        public int? Votos { get; set; }

        public virtual ICollection<ItemDestacado> ItemDestacado { get; set; }
        public virtual ICollection<RestauranteVotaciones> RestauranteVotaciones { get; set; }
    }
}
