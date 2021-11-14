using Xamarin.Forms;

namespace App.Controls
{
    public partial class ValidatorControl : ContentView
    {
        #region Constructor

        public ValidatorControl()
        {
            InitializeComponent();
        }

        #endregion

        #region Bindable Properties

        public static readonly BindableProperty TextProperty = BindableProperty.Create(propertyName: "Text",
                                                                                        returnType: typeof(string),
                                                                                        declaringType: typeof(ValidatorControl),
                                                                                        defaultValue: "",
                                                                                        defaultBindingMode: BindingMode.TwoWay,
                                                                                        propertyChanged: TextPropertyChanged);

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        private static void TextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (ValidatorControl)bindable;
            control.entry.Text = newValue?.ToString();
        }

        public static readonly BindableProperty ErrorTextProperty = BindableProperty.Create(propertyName: "ErrorText",
                                                                                        returnType: typeof(string),
                                                                                        declaringType: typeof(ValidatorControl),
                                                                                        defaultValue: "",
                                                                                        defaultBindingMode: BindingMode.TwoWay,
                                                                                        propertyChanged: ErrorTextPropertyChanged);
        public string ErrorText
        {
            get { return (string)GetValue(ErrorTextProperty); }
            set { SetValue(ErrorTextProperty, value); }
        }

        private static void ErrorTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (ValidatorControl)bindable;
            control.label.Text = newValue?.ToString();
        }

        public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(propertyName: "Placeholder",
                                                                                        returnType: typeof(string),
                                                                                        declaringType: typeof(ValidatorControl),
                                                                                        defaultValue: "",
                                                                                        defaultBindingMode: BindingMode.TwoWay,
                                                                                        propertyChanged: PlaceholderPropertyChanged);
        public string Placeholder
        {
            get { return (string)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }

        private static void PlaceholderPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (ValidatorControl)bindable;
            control.entry.Placeholder = newValue?.ToString();
        }

        public static readonly BindableProperty IsPasswordProperty = BindableProperty.Create(propertyName: "IsPassword",
                                                                                        returnType: typeof(bool),
                                                                                        declaringType: typeof(ValidatorControl),
                                                                                        defaultValue: false,
                                                                                        defaultBindingMode: BindingMode.TwoWay,
                                                                                        propertyChanged: IsPasswordPropertyChanged);
        public bool IsPassword
        {
            get { return (bool)GetValue(IsPasswordProperty); }
            set { SetValue(IsPasswordProperty, value); }
        }

        private static void IsPasswordPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (ValidatorControl)bindable;
            control.entry.IsPassword = (bool)newValue;
        }

        public static readonly BindableProperty MaxLengthProperty = BindableProperty.Create(propertyName: "MaxLength",
                                                                                        returnType: typeof(int),
                                                                                        declaringType: typeof(ValidatorControl),
                                                                                        defaultValue: 100,
                                                                                        defaultBindingMode: BindingMode.TwoWay,
                                                                                        propertyChanged: MaxLengthPropertyChanged);
        public int MaxLength
        {
            get { return (int)GetValue(MaxLengthProperty); }
            set { SetValue(MaxLengthProperty, value); }
        }

        private static void MaxLengthPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (ValidatorControl)bindable;
            control.entry.MaxLength = (int)newValue;
        }

        #endregion
    }
}
