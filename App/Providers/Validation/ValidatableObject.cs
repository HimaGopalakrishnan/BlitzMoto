using System.Collections.Generic;
using System.Linq;
using App.Providers.Navigation.Base;
using App.Providers.Validation.Rules;

namespace App.Providers.Validation
{
    public class ValidatableObject<T> : ObservableObject, IValidity
    {
        readonly List<IValidationRule<T>> _validations;
        T _value;
        bool _isValid;

        public List<IValidationRule<T>> Validations => _validations;

        List<string> _errors;
        public List<string> Errors
        {
            get => _errors;
            set => SetProperty(ref _errors, value);
        }

        public T Value
        {
            get => _value;
            set => SetProperty(ref _value, value);
        }

        public bool IsValid
        {
            get => _isValid;
            set => SetProperty(ref _isValid, value);
        }

        public ValidatableObject()
        {
            _isValid = true;
            _errors = new List<string>();
            _validations = new List<IValidationRule<T>>();
        }

        public ValidatableObject(T value)
        {
            _isValid = true;
            _errors = new List<string>();
            _validations = new List<IValidationRule<T>>();
            Value = value;
        }

        public bool Validate()
        {
            Errors = new List<string>();

            IEnumerable<string> errors = _validations.Where(v => !v.Check(Value))
                .Select(v => v.ValidationMessage);

            Errors = errors.ToList();
            IsValid = !Errors.Any();

            return this.IsValid;
        }

        public void Clear()
        {
            Value = default(T);
            Errors.Clear();
        }
    }
}
