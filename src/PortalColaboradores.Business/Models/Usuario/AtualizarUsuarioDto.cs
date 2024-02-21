using System.ComponentModel.DataAnnotations;

namespace PortalColaboradores.Business.Models.Usuario;

public class AtualizarUsuarioDto
{
    [StringLength(50, MinimumLength = 6, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres.")]
    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    public string Nome { get; set; }

    [StringLength(20, MinimumLength = 6, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres.")]
    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [RegularExpression(@"^\S*$", ErrorMessage = "O campo {0} não deve conter espaços.")]
    public string SenhaAntiga { get; set; }
    
    [StringLength(20, MinimumLength = 6, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres.")]
    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [RegularExpression(@"^\S*$", ErrorMessage = "O campo {0} não deve conter espaços.")]
    public string NovaSenha { get; set; }
    
    [Compare("NovaSenha", ErrorMessage = "Senhas não conferem.")]    
    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    public string ConfirmarNovaSenha { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    public bool Administrador { get; set; }
}