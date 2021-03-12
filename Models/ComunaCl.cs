using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace EchoBot.Models
{
    [Table("comuna_cl")]
    public partial class ComunaCl
    {
        public ComunaCl()
        {
            EstadoComunas = new HashSet<EstadoComuna>();
        }

        [Key]
        [Column("id_comuna")]
        public int IdComuna { get; set; }
        [Column("id_pr")]
        public int IdPr { get; set; }
        [Column("str_descripcion")]
        [StringLength(30)]
        public string StrDescripcion { get; set; }
        [Column("id_region")]
        public int? IdRegion { get; set; }

        [ForeignKey(nameof(IdPr))]
        [InverseProperty(nameof(ProvinciaCl.ComunaCls))]
        public virtual ProvinciaCl IdPrNavigation { get; set; }
        [ForeignKey(nameof(IdRegion))]
        [InverseProperty(nameof(RegionCl.ComunaCls))]
        public virtual RegionCl IdRegionNavigation { get; set; }
        [InverseProperty(nameof(EstadoComuna.IdComunaNavigation))]
        public virtual ICollection<EstadoComuna> EstadoComunas { get; set; }
    }
}
