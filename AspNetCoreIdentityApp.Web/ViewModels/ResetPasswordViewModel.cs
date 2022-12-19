using System.ComponentModel.DataAnnotations;

namespace AspNetCoreIdentityApp.Web.ViewModels
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "Şifre alanı boş bırakılamaz.")]
        [Display(Name = "Yeni Şifre:")]
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Girmiş olduğunuz şifreler eşleşmemektedir.")]
        [Required(ErrorMessage = "Şifre tekrar alanı boş bırakılamaz.")]
        [Display(Name = "Yeni Şifre Tekrar:")]
        public string PasswordConfirm { get; set; }
    }
}
