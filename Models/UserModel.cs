using System.ComponentModel.DataAnnotations;

namespace TaskSchedule.Models {
  public class UserModel {

    [Required]
    [Display (Name = "Nome")]
    public string Name { get; set; }

    [Required]
    [EmailAddress]
    [Display (Name = "Email")]
    public string Email { get; set; }

    [StringLength (100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
    [DataType (DataType.Password)]
    [Required]
    [Display (Name = "Senha")]
    public string Password { get; set; }

    [DataType (DataType.Password)]
    [Required]
    [Display (Name = "Confirme sua senha")]
    [Compare ("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }

    [Display (Name = "Lembrete de senha...")]

    public string PasswordTip { get; set; }

    [Display (Name = "Telefone")]
    public string PhoneNumber { get; set; }

    public bool EmailConfirmado { get; set; }

    [Display (Name = "Função")]
    public int RoleID { get; set; }
  }
}
