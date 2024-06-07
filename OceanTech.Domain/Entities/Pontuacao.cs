using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OceanTech.Domain.Entities
{
    public class Pontuacao
    {
        [Key]
        [Column("pc_int_id")]
        public int Id { get; set; }


        [Column("pc_int_pontuacaoMensal")]
        public int PontuacaoMensal { get; set; }

        [Column("us_int_id")]
        public int UsuarioId { get; set; }

        [JsonIgnore]
        [ForeignKey("UsuarioId")]
        public Usuario? Usuario { get; set; }
    }
}