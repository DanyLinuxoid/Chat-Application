using HermesModels.Interfaces;

namespace HermesLogic.Interfaces
{
    public interface IApplicationValidatorFactory
    {
        IApplicationValidator<T> Get<T>(T model) where T : IValidationObject;
    }
}