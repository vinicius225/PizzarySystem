using System.ComponentModel.DataAnnotations;

namespace LanchesMac.ViewModels
{
    public class LoginViewModel
    {
        [Required( ErrorMessage ="Digite o nomr de usuário")]
        public string UserName { get; set; }
        [Required( ErrorMessage ="Informe a senha")]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }

    }
}
