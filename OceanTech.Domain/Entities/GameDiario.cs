using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using OceanTech.Domain.Entities;

namespace OceanTech.Domain.Entities
{
    public class GameDiario
    {
        [Key]
        [Column("gd_int_id")]
        public int Id { get; set; }
        
        [Column("gd_int_Jogou")]
        public bool Jogou { get; set; }

        [Column("gd_dat_DataJogou")]
        public DateTime dataJogo { get; set; }

        [Column("us_int_id")]
        public int? UsuarioId { get; set; }

        [JsonIgnore]
        [ForeignKey("UsuarioId")]
        public Usuario? Usuario { get; set; }


        [JsonIgnore]
        public ICollection<PontuacaoDiaria> PontuacaoDiaria { get; set; }
    }
}
