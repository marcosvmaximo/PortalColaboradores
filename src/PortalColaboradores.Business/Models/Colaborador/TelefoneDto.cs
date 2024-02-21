using System.ComponentModel.DataAnnotations;
using PortalColaboradores.Business.Enum;

namespace PortalColaboradores.Business.Models.Colaborador;

public class TelefoneDto
{
    public int PessoaFisicaId { get; set; }

    [Required(ErrorMessage = "O campo Tipo de Telefone é obrigatório.")]
    public ETipoTelefone Tipo { get; set; }

    [Required(ErrorMessage = "O campo Número de Telefone é obrigatório.")]
    [RegularExpression(@"^\d{0,20}$", ErrorMessage = "O campo Número de Telefone deve conter somente números e no máximo 20 caracteres.")]
    public string Numero { get; set; }
}