using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Restaurante.Models.DB
{
    public partial class Usuario
    {
        public int UsuarioId { get; set; }
        public string Nombre { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string Correo { get; set; }
        [Required]
        public string Password { get; set; }
        public int Rol { get; set; }
    }
}
