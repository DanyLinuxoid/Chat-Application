using HermesLogic.Interfaces;
using HermesModels.Interfaces;

namespace HermesLogic.Base
{
    public class BaseLogic
    {
        protected readonly ICommonDependencies _commonDependencies;

        public BaseLogic(ICommonDependencies commonDependencies)
        {
            _commonDependencies = commonDependencies;
        }

        public bool Validate<T>(T model) where T : IValidationObject
        {
            return  _commonDependencies.ApplicationValidatorFactory.Get(model).Validate(model).IsValid;
        }
    }
}