using System.Text.RegularExpressions;

namespace App.Providers.Validation.Rules
{
    public class IsValidContactNumberRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }
        public Regex RegexExpression { get; set; } = new Regex(@"((\+*)((0[ -]*)*|((91 )*))((\d{12})+|(\d{10})+))|\d{5}([- ]*)\d{6}");

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
            return RegexExpression.IsMatch(numericValue.ToString());
        }
    }
}
