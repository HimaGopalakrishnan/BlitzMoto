using System;
namespace App.Providers.Validation.Rules
{
    public class IsValidDateRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            if (value == null)
            {
                return false;
            }

            var isValid = DateTime.TryParse(value.ToString(), out DateTime date);

            if (!isValid)
            {
                return false;
            }
            return date != null;
        }
    }
}