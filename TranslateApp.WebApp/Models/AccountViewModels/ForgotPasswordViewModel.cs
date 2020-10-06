using System.ComponentModel.DataAnnotations;

namespace TranslateApp.WebApp.Models.AccountViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
