using System.ComponentModel.DataAnnotations;

namespace PortalColaboradores.Business.Validations;
public static class ValidadorCPF
{
    public static bool ValidarCPF(string cpf)
    {
        cpf = RemoverCaracteresNaoNumericos(cpf);

        if (string.IsNullOrEmpty(cpf) || cpf.Length != 11)
            return false;

        int[] digitos = new int[11];
        for (int i = 0; i < 11; i++)
        {
            digitos[i] = int.Parse(cpf[i].ToString());
        }

        int soma1 = 0;
        int soma2 = 0;

        for (int i = 0; i < 9; i++)
        {
            soma1 += digitos[i] * (10 - i);
            soma2 += digitos[i] * (11 - i);
        }

        int resto = (soma1 * 10) % 11;
        int digito1 = (resto == 10 || resto == 11) ? 0 : resto;

        resto = (soma2 + (digito1 * 2)) * 10 % 11;
        int digito2 = (resto == 10 || resto == 11) ? 0 : resto;

        return digito1 == digitos[9] && digito2 == digitos[10];
    }

    private static string RemoverCaracteresNaoNumericos(string input)
    {
        return string.IsNullOrEmpty(input) ? string.Empty : new string(Array.FindAll(input.ToCharArray(), c => char.IsDigit(c)));
    }
}

public class CpfAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        string cpf = value.ToString().Replace(".", "").Replace("-", ""); // Remove caracteres de formatação

        if (cpf.Length != 11 || !ValidadorCPF.ValidarCPF(cpf))
        {
            return new ValidationResult("CPF inválido");
        }

        return ValidationResult.Success;
    }
}