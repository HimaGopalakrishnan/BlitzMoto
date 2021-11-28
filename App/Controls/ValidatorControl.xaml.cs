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
                                                                                        defaultBindingMode: BindingMode.TwoWay);

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly BindableProperty ErrorTextProperty = BindableProperty.Create(propertyName: "ErrorText",
                                                                                        returnType: typeof(string),
                                                                                        declaringType: typeof(ValidatorControl),
                                                                                        defaultValue: "",
                                                                                        defaultBindingMode: BindingMode.TwoWay);
        public string ErrorText
        {
            get { return (string)GetValue(ErrorTextProperty); }
            set { SetValue(ErrorTextProperty, value); }
        }

        public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(propertyName: "Placeholder",
                                                                                        returnType: typeof(string),
                                                                                        declaringType: typeof(ValidatorControl),
                                                                                        defaultValue: "",
                                                                                        defaultBindingMode: BindingMode.TwoWay);
        public string Placeholder
        {
            get { return (string)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }

        public static readonly BindableProperty IsPasswordProperty = BindableProperty.Create(propertyName: "IsPassword",
                                                                                        returnType: typeof(bool),
                                                                                        declaringType: typeof(ValidatorControl),
                                                                                        defaultValue: false,
                                                                                        defaultBindingMode: BindingMode.TwoWay);
        public bool IsPassword
        {
            get { return (bool)GetValue(IsPasswordProperty); }
            set { SetValue(IsPasswordProperty, value); }
        }

        public static readonly BindableProperty MaxLengthProperty = BindableProperty.Create(propertyName: "MaxLength",
                                                                                        returnType: typeof(int),
                                                                                        declaringType: typeof(ValidatorControl),
                                                                                        defaultValue: 100,
                                                                                        defaultBindingMode: BindingMode.TwoWay);
        public int MaxLength
        {
            get { return (int)GetValue(MaxLengthProperty); }
            set { SetValue(MaxLengthProperty, value); }
        }

        public static readonly BindableProperty KeyboardProperty = BindableProperty.Create(propertyName: "Keyboard",
                                                                                        returnType: typeof(Keyboard),
                                                                                        declaringType: typeof(ValidatorControl),
                                                                                        defaultValue: Keyboard.Default,
                                                                                        defaultBindingMode: BindingMode.TwoWay);
        public Keyboard Keyboard
        {
            get { return (Keyboard)GetValue(KeyboardProperty); }
            set { SetValue(KeyboardProperty, value); }
        }

        #endregion
    }
}
