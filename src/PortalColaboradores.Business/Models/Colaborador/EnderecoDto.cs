using System.ComponentModel.DataAnnotations;
using PortalColaboradores.Business.Enum;

namespace PortalColaboradores.Business.Models.Colaborador;

public class EnderecoDto
{
    public int PessoaFisicaId { get; set; }
    
    [Required(ErrorMessage = "O campo Tipo de Endereço é obrigatório.")]
    public ETipoEndereco Tipo { get; set; }

    [Required(ErrorMessage = "O campo CEP é obrigatório.")]
    [StringLength(8, ErrorMessage = "O campo CEP deve ter exatamente 8 caracteres.")]
    public string Cep { get; set; }

    [Required(ErrorMessage = "O campo Logradouro é obrigatório.")]
    [StringLength(100, ErrorMessage = "O campo Logradouro deve ter no máximo 100 caracteres.")]
    public string Logradouro { get; set; }

    [Required(ErrorMessage = "O campo Número/Complemento é obrigatório.")]
    [StringLength(10, ErrorMessage = "O campo Número/Complemento deve ter no máximo 10 caracteres.")]
    public string NumeroComplemento { get; set; }

    [Required(ErrorMessage = "O campo Bairro é obrigatório.")]
    [StringLength(50, ErrorMessage = "O campo Bairro deve ter no máximo 50 caracteres.")]
    public string Bairro { get; set; }

    [Required(ErrorMessage = "O campo Cidade é obrigatório.")]
    [StringLength(100, ErrorMessage = "O campo Cidade deve ter no máximo 100 caracteres.")]
    public string Cidade { get; set; }
}