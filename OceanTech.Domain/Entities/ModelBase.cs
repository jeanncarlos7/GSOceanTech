using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OceanTech.Domain.Entities
{
    public abstract class ModelBase
    {
        [Key]
        [Column("ativo")]
        public bool Ativo { get; set; }
    }
}
