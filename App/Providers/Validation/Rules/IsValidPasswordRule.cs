using System.Text.RegularExpressions;

namespace App.Providers.Validation.Rules
{
    public class IsValidPasswordRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }
        public Regex RegexExpression { get; set; } = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$");

        public bool Check(T value)
        {
            return (RegexExpression.IsMatch($"{value}"));
        }
    }
}
