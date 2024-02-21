using System.ComponentModel.DataAnnotations;

namespace PortalColaboradores.Business.Models.Usuario;

public class CadastroDto
{
    [Key]
    [StringLength(20, MinimumLength = 6, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres.")]
    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [RegularExpression(@"^\S*$", ErrorMessage = "O campo {0} não deve conter espaços.")]
    public string Login { get; set; }

    [StringLength(50, MinimumLength = 6, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres.")]
    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    public string Nome { get; set; }

    [StringLength(20, MinimumLength = 6, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres.")]
    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [RegularExpression(@"^\S*$", ErrorMessage = "O campo {0} não deve conter espaços.")]
    public string Senha { get; set; }
    
    [Compare("Senha", ErrorMessage = "Senhas não conferem.")]    
    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    public string ConfirmarSenha { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    public bool Administrador { get; set; }
}