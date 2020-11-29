using HermesModels.Interfaces;

namespace HermesLogic.Base.Validator
{
    public interface IValidatorFactory
    {
        IApplicationValidator<T> Get<T>(T model) where T : IValidationObject;
    }
}