using System.ComponentModel.DataAnnotations;

namespace AspNetCoreIdentityApp.Web.ViewModels
{
    public class PasswordChangeViewModel
    {
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Eski şifre alanı boş bırakılamaz.")]
        [Display(Name = "Eski Şifre: ")]
        [MinLength(6, ErrorMessage = "Şifreniz en az 6 karekter olabilir.")]
        public string PasswordOld { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Yeni şifre alanı boş bırakılamaz.")]
        [Display(Name = "Yeni Şifre: ")]
        [MinLength(6, ErrorMessage = "Şifreniz en az 6 karekter olabilir.")]
        public string PasswordNew { get; set; }

        [DataType(DataType.Password)]
        [Compare(nameof(PasswordNew), ErrorMessage = "Girmiş olduğunuz şifreler eşleşmemektedir.")]
        [Required(ErrorMessage = "Şifre tekrar alanı boş bırakılamaz.")]
        [Display(Name = "Şifre Tekrar: ")]
        [MinLength(6, ErrorMessage = "Şifreniz en az 6 karekter olabilir.")]
        public string PasswordNewConfirm { get; set; }
    }
}
