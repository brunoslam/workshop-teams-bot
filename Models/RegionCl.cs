using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace EchoBot.Models
{
    [Table("region_cl")]
    public partial class RegionCl
    {
        public RegionCl()
        {
            ComunaCls = new HashSet<ComunaCl>();
        }

        [Key]
        [Column("id_re")]
        public int IdRe { get; set; }
        [Required]
        [Column("str_descripcion")]
        [StringLength(60)]
        public string StrDescripcion { get; set; }
        [Required]
        [Column("str_romano")]
        [StringLength(5)]
        public string StrRomano { get; set; }
        [Column("num_provincias")]
        public int NumProvincias { get; set; }
        [Column("num_comunas")]
        public int NumComunas { get; set; }

        [InverseProperty(nameof(ComunaCl.IdRegionNavigation))]
        public virtual ICollection<ComunaCl> ComunaCls { get; set; }
    }
}
