using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace OceanTech.Domain.Entities
{
    public class Usuario : ModelBase
    {
        [Key]
        [Column("us_int_id")]
        public int Id { get; set; }

        [Column("us_string_nome")]
        public string Nome { get; set; }

        [Required]
        [Column("us_string_email")]
        public string Email { get; set; }

        [Required]
        [Column("us_string_senha")]
        public string Senha { get; set; }

        [Column("us_dat_inscricao")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy/HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime? Inscricao { get; set; }


        [JsonIgnore]
        public ICollection<GameDiario>? GameDiarios { get; set; }

        [JsonIgnore]
        public ICollection<Pontuacao>? Pontuacoes { get; set; }
    }
}
