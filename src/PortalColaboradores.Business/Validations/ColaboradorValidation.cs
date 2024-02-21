using FluentValidation;
using PortalColaboradores.Business.Entities;

namespace PortalColaboradores.Business.Validations;

public class ColaboradorValidation : AbstractValidator<Colaborador>
{
    public ColaboradorValidation()
    {
        RuleFor(x => x.Cpf)
            .NotEmpty().WithMessage("O campo CPF é obrigatório.")
            .Matches(@"^\d{11}$").WithMessage("O CPF deve conter exatamente 11 dígitos.")
            .Must(ValidadorCPF.ValidarCPF).WithMessage("O CPF informado é inválido.");

        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("O campo Nome é obrigatório.")
            .MinimumLength(6).WithMessage("O campo Nome deve ter no mínimo 6 caracteres.")
            .MaximumLength(50).WithMessage("O campo Nome deve ter no máximo 50 caracteres.");

        RuleFor(x => x.DataNascimento)
            .NotEmpty().WithMessage("O campo Data de Nascimento é obrigatório.")
            .Must(data => data <= DateTime.Now.AddYears(-18)).WithMessage("É necessário ser maior de 18 anos para se cadastrar.");

        RuleFor(x => x.Rg)
            .NotEmpty().WithMessage("O campo RG é obrigatório.")
            .MaximumLength(10).WithMessage("O campo RG deve ter no máximo 10 caracteres.")
            .Must(rg => !rg.Contains(" ")).WithMessage("O campo RG não pode conter espaços.");
    
        RuleFor(x => x.Tipo)
            .NotEmpty().WithMessage("O campo Tipo de Colaborador é obrigatório.")
            .IsInEnum().WithMessage("O campo Tipo de Colaborador deve ser uma opção válida.");

        RuleFor(x => x.Matricula)
            .NotEmpty().WithMessage("O campo Matrícula é obrigatório.");
            // .GreaterThan(0).WithMessage("O campo Matrícula deve ser maior que zero.");

        RuleFor(x => x.DataAdmissao)
            .Must((entity, dataAdmissao) => dataAdmissao == null || dataAdmissao > entity.DataNascimento).WithMessage("A Data de Admissão deve ser posterior à Data de Nascimento.")
            .When(x => x.DataAdmissao != null);

        RuleFor(x => x.DataAdmissao)
            .Must((entity, dataAdmissao) => dataAdmissao == null || dataAdmissao <= DateTime.Now.AddDays(30)).WithMessage("A Data de Admissão não pode ser superior a 30 dias em relação à data atual.")
            .When(x => x.DataAdmissao != null && x.DataAdmissao > DateTime.Now);

        RuleFor(x => x.ValorContribuicao)
            .Must((entity, valor) => valor == null || (valor >= 0.01m && valor < 10000)).WithMessage("O Valor da Contribuição Sindical deve ser maior ou igual a R$0,01 e menor que R$10.000,00.")
            .When(x => x.ValorContribuicao != null);

        RuleFor(x => x.ValorContribuicao)
            .Must((entity, valor) => valor == null || valor > 0).WithMessage("O Valor da Contribuição Sindical deve ser maior que zero ou nulo.")
            .When(x => x.ValorContribuicao != null);

        RuleFor(x => x.ValorContribuicao)
            .ScalePrecision(2, 10).WithMessage("O Valor da Contribuição Sindical deve ter no máximo duas casas decimais.")
            .When(x => x.ValorContribuicao != null);
    }
}