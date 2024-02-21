using FluentValidation;
using PortalColaboradores.Business.Entities;

namespace PortalColaboradores.Business.Validations;

public class EnderecoValidation : AbstractValidator<Endereco>
{
    public EnderecoValidation()
    {
        RuleFor(x => x.PessoaFisica)
            .NotEmpty().WithMessage("O campo Pessoa Física é obrigatório.");

        RuleFor(x => x.Logradouro)
            .NotEmpty().WithMessage("O campo Logradouro é obrigatório.")
            .MaximumLength(100).WithMessage("O campo Logradouro deve ter no máximo 100 caracteres.");

        RuleFor(x => x.NumeroComplemento)
            .NotEmpty().WithMessage("O campo Número/Complemento é obrigatório.")
            .MaximumLength(10).WithMessage("O campo Número/Complemento deve ter no máximo 10 caracteres.");

        RuleFor(x => x.Bairro)
            .NotEmpty().WithMessage("O campo Bairro é obrigatório.")
            .MaximumLength(50).WithMessage("O campo Bairro deve ter no máximo 50 caracteres.");

        RuleFor(x => x.Cidade)
            .NotEmpty().WithMessage("O campo Cidade é obrigatório.")
            .MaximumLength(100).WithMessage("O campo Cidade deve ter no máximo 100 caracteres.");

        RuleFor(x => x.Cep)
            .NotEmpty().WithMessage("O campo CEP é obrigatório.")
            .Matches(@"^\d{8}$").WithMessage("O campo CEP deve ter exatamente 8 dígitos.");
        
        RuleFor(x => x.Tipo)
            .NotEmpty().WithMessage("O campo Tipo de Endereço é obrigatório.")
            .IsInEnum().WithMessage("O campo Tipo de Endereço deve ser uma opção válida.");
    }
}