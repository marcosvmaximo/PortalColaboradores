using FluentValidation;
using PortalColaboradores.Business.Entities;

namespace PortalColaboradores.Business.Validations;

public class TelefoneValidation : AbstractValidator<Telefone>
{
    public TelefoneValidation()
    {
        RuleFor(x => x.PessoaFisica)
            .NotEmpty().WithMessage("O campo Pessoa Física é obrigatório.");

        RuleFor(x => x.Numero)
            .NotEmpty().WithMessage("O campo Número do Telefone é obrigatório.")
            .Matches(@"^\d+$").WithMessage("O campo Número do Telefone deve conter apenas números.")
            .MaximumLength(20).WithMessage("O campo Número do Telefone deve ter no máximo 20 caracteres.");

        RuleFor(x => x.Tipo)
            .NotEmpty().WithMessage("O campo Tipo de Telefone é obrigatório.")
            .IsInEnum().WithMessage("O campo Tipo de Telefone deve ser uma opção");
    }
}