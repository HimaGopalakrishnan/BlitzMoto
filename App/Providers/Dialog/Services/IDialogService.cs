using System.Threading.Tasks;

namespace App.Providers.Dialog.Services
{
    public interface IDialogService
    {
        void Alert(string message, string title = "", string accept = "", string cancel = "");
        Task AlertAsync(string message, string title = "", string accept = "", string cancel = "");
        Task<bool> Confirm(string message, string title = "Alert", string accept = "Ok", string cancel = "Cancel");
        void Toast(string message);
        void ShowLoading(string message = "");
        void HideLoading();
    }
}
