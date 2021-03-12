using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace EchoBot.Models
{
    [Table("provincia_cl")]
    public partial class ProvinciaCl
    {
        public ProvinciaCl()
        {
            ComunaCls = new HashSet<ComunaCl>();
        }

        [Key]
        [Column("id_pr")]
        public int IdPr { get; set; }
        [Column("id_re")]
        public int IdRe { get; set; }
        [Required]
        [Column("str_descripcion")]
        [StringLength(30)]
        public string StrDescripcion { get; set; }
        [Column("num_comunas")]
        public int NumComunas { get; set; }

        [InverseProperty(nameof(ComunaCl.IdPrNavigation))]
        public virtual ICollection<ComunaCl> ComunaCls { get; set; }
    }
}
