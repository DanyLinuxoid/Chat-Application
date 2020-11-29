using FluentValidation.Results;

namespace HermesLogic.Base.Validator
{
    public interface IApplicationValidator<in T>
    {
        ValidationResult Validate(T model);
    }
}