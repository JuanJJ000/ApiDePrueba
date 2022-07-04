using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entidades
{
    public class PanaderoViewModel
    {

        public int Id { get; set; }
        [Required]
        [StringLength (30)]
        public string Nombre { get; set; }
        [Required]
        [StringLength(30)]
        public string Apellido { get; set; }
        [Required]
        public int Edad { get; set; }
        [Required]
        [StringLength (30)]
        public string Carnet { get; set; }

    }
}
