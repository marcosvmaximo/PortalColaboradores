using System.ComponentModel.DataAnnotations;
using PortalColaboradores.Business.Enum;
using PortalColaboradores.Business.Validations;

namespace PortalColaboradores.Business.Models.Colaborador;

public class ColaboradorCommand
{
    [Required(ErrorMessage = "O campo Nome é obrigatório.")]
    [StringLength(50, MinimumLength = 6, ErrorMessage = "O campo Nome deve ter no minimo 6 e no máximo 50 caracteres.")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "O campo Data de Nascimento é obrigatório.")]
    [DataType(DataType.Date, ErrorMessage = "Formato de data inválido.")]
    public DateTime DataNascimento { get; set; }

    [Required(ErrorMessage = "O campo CPF é obrigatório.")]
    [RegularExpression(@"^\d{11}$", ErrorMessage = "O campo CPF deve conter exatamente 11 dígitos.")]
    [Cpf]
    public string Cpf { get; set; }

    [Required(ErrorMessage = "O campo RG é obrigatório.")]
    [StringLength(10, ErrorMessage = "O campo RG deve ter no máximo 10 caracteres.")]
    public string Rg { get; set; }

    [Required(ErrorMessage = "O campo Matrícula é obrigatório.")]
    [RegularExpression(@"^\d+$", ErrorMessage = "O campo Matrícula deve conter somente números.")]
    public string Matricula { get; set; }

    [Required(ErrorMessage = "O campo Tipo de Colaborador é obrigatório.")]
    public ETipoColaborador Tipo { get; set; }

    [DataType(DataType.Date, ErrorMessage = "Formato de data inválido.")]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
    public DateTime? DataAdmissao { get; set; }

    [Range(0.01, 9999.99, ErrorMessage = "O campo Valor de Contribuição deve ser maior ou igual a R$0,01 e menor que R$10.000,00.")]
    public decimal? ValorContribuicao { get; set; }
}