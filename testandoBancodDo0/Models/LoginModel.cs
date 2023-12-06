using System.ComponentModel.DataAnnotations;
//adicionado a pouco tempo (model de login para validar campos).
namespace testandoBancodDo0.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Campo Nome é obrigatório")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Campo E-mail é obrigatório")]
        [EmailAddress(ErrorMessage = "E-mail inválido, digite um válido.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Campo Senha é obrigatório")]
        public string? Senha { get; set; }
    }
}
