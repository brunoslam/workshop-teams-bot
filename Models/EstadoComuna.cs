using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace EchoBot.Models
{
    [Table("Estado_Comuna")]
    public partial class EstadoComuna
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("id_comuna")]
        public int IdComuna { get; set; }
        [Column("fecha_actualización", TypeName = "datetime")]
        public DateTime? FechaActualización { get; set; }
        [Column("id_fase")]
        public int IdFase { get; set; }

        [ForeignKey(nameof(IdComuna))]
        [InverseProperty(nameof(ComunaCl.EstadoComunas))]
        public virtual ComunaCl IdComunaNavigation { get; set; }
        [ForeignKey(nameof(IdFase))]
        [InverseProperty(nameof(Fase.EstadoComunas))]
        public virtual Fase IdFaseNavigation { get; set; }
    }
}
