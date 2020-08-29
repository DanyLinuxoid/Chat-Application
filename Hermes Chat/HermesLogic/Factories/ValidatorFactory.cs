using HermesLogic.Interfaces;
using HermesLogic.Validators;
using HermesModels.Chat;
using HermesModels.Interfaces;
using HermesModels.MVC;
using System;
using System.Collections.Generic;

namespace HermesLogic.Factories
{
    public class ValidatorFactory : IApplicationValidatorFactory
    {
        private readonly IUserManager _userManager;
        private readonly Dictionary<Type, Type> _objectValidators;

        public ValidatorFactory(IUserManager userManager)
        {
            _userManager = userManager;
            _objectValidators = new Dictionary<Type, Type>();
            SetupValidators();
        }

        public IApplicationValidator<T> Get<T>(T model) where T : IValidationObject
        {
            _objectValidators.TryGetValue(typeof(T), out Type validator);
            return (IApplicationValidator<T>)Activator.CreateInstance(validator, _userManager);
        }

        private void SetupValidators()
        {
            _objectValidators.Add(typeof(LoginModel), typeof(LoginValidator));
            _objectValidators.Add(typeof(RegistrationModel), typeof(RegistrationValidator));
            _objectValidators.Add(typeof(MessageModel), typeof(MessageValidator));
        }
    }
}