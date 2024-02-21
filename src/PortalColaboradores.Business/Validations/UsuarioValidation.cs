using FluentValidation;
using PortalColaboradores.Business.Entities;

namespace PortalColaboradores.Business.Validations;

public class UsuarioValidation : AbstractValidator<Usuario>
{
    public UsuarioValidation()
    {
        RuleFor(x => x.UserName)
            .NotNull()
            .NotEmpty()
            .WithMessage("O campo {PropertyName} é obrigatório.")
            .Must(s => !string.IsNullOrWhiteSpace(s))
            .WithMessage("O campo {PropertyName} não pode conter espaços em branco.")
            .Length(6, 20)
            .WithMessage("O campo {PropertyName} deve conter entre 6 e 20 caracteres");

        RuleFor(x => x.Nome)
            .NotNull()
            .NotEmpty()
            .WithMessage("O campo {PropertyName} é obrigatório.")
            .Must(s => !string.IsNullOrWhiteSpace(s))
            .WithMessage("O campo {PropertyName} não pode conter espaços em branco.")
            .Length(6, 50)
            .WithMessage("O campo {PropertyName} deve conter entre 6 e 50 caracteres");

        RuleFor(x => x.Administrador)
            .NotNull()
            .NotEmpty()
            .WithMessage("O campo {PropertyName} é obrigatório.");
    }
}