using System.Threading.Tasks;

namespace App.Providers.Navigation.Base
{
    public class ViewModelBase : ObservableObject
    {
        #region Constructor

        public ViewModelBase()
        {
        }

        #endregion

        #region Virtual Methods

        public virtual Task InitializeAsync(object navigationData)
        {
            return Task.FromResult(false);
        }

        #endregion
    }
}
