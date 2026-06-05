using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace FormulariosUAF.Validation;

/// <summary>
/// Valida que un bool sea true (checkbox marcado).
/// Genera data-val-mustbetrue para jQuery Unobtrusive Validation.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class MustBeTrueAttribute : ValidationAttribute, IClientModelValidator
{
    public MustBeTrueAttribute() : base("Este campo es obligatorio.") { }

    protected override ValidationResult? IsValid(object? value, ValidationContext context)
    {
        if (value is bool b && b)
            return ValidationResult.Success;

        return new ValidationResult(ErrorMessage);
    }

    public void AddValidation(ClientModelValidationContext context)
    {
        context.Attributes["data-val"] = "true";
        context.Attributes["data-val-mustbetrue"] = ErrorMessage ?? "Este campo es obligatorio.";
    }
}
