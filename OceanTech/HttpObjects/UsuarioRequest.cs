using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OceanTech.HttpObjects
{
    public class UsuarioRequest : RequestBase
    {
        [Required(ErrorMessage ="Nome é obrigatório")]      
        public string Nome { get; set; }

        
        [Required(ErrorMessage = "Email é obrigatório")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Senha é obrigatória")]
        public string Senha { get; set; }
    }
}
