namespace Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PATRIMONIO")]
    public partial class PATRIMONIO
    {
        public int patrimonioId { get; set; }

        [Required]
        [StringLength(50)]
        public string nome { get; set; }

        [Required]
        [StringLength(50)]
        public string descricao { get; set; }

        public int nTombo { get; set; }

        public int marcaId { get; set; }

        public virtual MARCA MARCA { get; set; }
    }
}
