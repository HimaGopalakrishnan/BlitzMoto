using System;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Xamarin.Forms;

namespace App.Providers.Dialog.Services
{
    public class DialogService : IDialogService
    {
        public void Alert(string message, string title = "", string accept = "", string cancel = "")
        {
            if (string.IsNullOrEmpty(title))
                title = "Alert";
            if (string.IsNullOrEmpty(accept))
                accept = "Ok";
            if (string.IsNullOrEmpty(cancel))
                cancel = "Cancel";

            Device.BeginInvokeOnMainThread(() =>
            {
                UserDialogs.Instance.Alert(new AlertConfig()
                {
                    Title = title,
                    Message = message
                });
            });
        }

        public async Task AlertAsync(string message, string title = "", string accept = "", string cancel = "")
        {
            if (string.IsNullOrEmpty(title))
                title = "Alert";
            if (string.IsNullOrEmpty(accept))
                accept = "Ok";
            if (string.IsNullOrEmpty(cancel))
                cancel = "Cancel";

            await UserDialogs.Instance.AlertAsync(new AlertConfig()
            {
                Title = title,
                Message = message
            });
        }

        public async Task<bool> Confirm(string message, string title = "Alert", string accept = "Ok", string cancel = "Cancel")
        {
            var result = await Application.Current.MainPage.DisplayAlert(title, message, accept, cancel);
            return result;
        }

        public void HideLoading()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                UserDialogs.Instance.HideLoading();
            });
        }

        public void LoginRetry(string title, Action tryAgain, Action resetPassword, Action register)
        {
            var config = new ActionSheetConfig
            {
                Cancel = new ActionSheetOption("Cancel"),
                Title = title
            };

            if (tryAgain != null)
            {
                config.Add("Try Again", tryAgain);
            }

            if (resetPassword != null)
            {
                config.Add("Reset Password", resetPassword);
            }

            if (register != null)
            {
                config.Add("Register", register);
            }

            Device.BeginInvokeOnMainThread(() =>
            {
                UserDialogs.Instance.ActionSheet(config);
            });
        }

        public void ShowLoading(string message = "")
        {
            if (string.IsNullOrEmpty(message))
                message = "Loading";

            Device.BeginInvokeOnMainThread(() =>
            {
                UserDialogs.Instance.ShowLoading(message, MaskType.None);
            });
        }

        public void Toast(string message)
        {
            var toastConfig = new ToastConfig(message);
            toastConfig.SetDuration(3000);
            toastConfig.SetBackgroundColor(System.Drawing.Color.FromArgb(12, 131, 193));

            Device.BeginInvokeOnMainThread(() =>
            {
                UserDialogs.Instance.Toast(toastConfig);
            });
        }
    }
}
