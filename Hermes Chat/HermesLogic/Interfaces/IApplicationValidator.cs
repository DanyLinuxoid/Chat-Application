using FluentValidation.Results;

namespace HermesLogic.Interfaces
{
    public interface IApplicationValidator<in T>
    {
        ValidationResult Validate(T model);
    }
}