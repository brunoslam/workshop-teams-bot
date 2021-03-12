using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace EchoBot.Models
{
    public partial class Fase
    {
        public Fase()
        {
            EstadoComunas = new HashSet<EstadoComuna>();
        }

        [Key]
        [Column("codigo")]
        public int Codigo { get; set; }
        [Column("nombre")]
        [StringLength(50)]
        public string Nombre { get; set; }

        [InverseProperty(nameof(EstadoComuna.IdFaseNavigation))]
        public virtual ICollection<EstadoComuna> EstadoComunas { get; set; }
    }
}
