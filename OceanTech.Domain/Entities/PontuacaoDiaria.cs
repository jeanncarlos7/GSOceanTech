using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OceanTech.Domain.Entities
{
    public class PontuacaoDiaria
    {
        [Key]
        [Column("pd_int_id")]
        public int Id { get; set; }

        [Column("pd_int_valor")]
        public int Valor { get; set; }


        [Column("gd_int_id")]
        public int GameDiarioId { get; set; }

        [JsonIgnore]
        [ForeignKey("GameDiarioId")]
        public GameDiario? GameDiario { get; set; }
    }
}
