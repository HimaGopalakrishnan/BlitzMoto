namespace App.Providers.Validation.Rules
{
    public class IsValidNumericRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            if (value == null)
            {
                return false;
            }

            var isValid = decimal.TryParse(value.ToString(), out decimal numericValue);

            if (!isValid)
            {
                return false;
            }
            return numericValue > 0;
        }
    }
}
