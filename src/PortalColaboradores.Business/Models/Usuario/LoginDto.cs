using System.ComponentModel.DataAnnotations;

namespace PortalColaboradores.Business.Models.Usuario;

public class LoginDto
{ 
    [Key]
    [StringLength(20, MinimumLength = 6, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres.")]
    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [RegularExpression(@"^\S*$", ErrorMessage = "O campo {0} não deve conter espaços.")]
    public string Login { get; set; }

    [StringLength(20, MinimumLength = 6, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres.")]
    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [RegularExpression(@"^\S*$", ErrorMessage = "O campo {0} não deve conter espaços.")]
    public string Senha { get; set; }
}