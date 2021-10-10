using System;

namespace App.Features.Menu.Pages
{
    public class MenuViewFlyoutMenuItem
    {
        public MenuViewFlyoutMenuItem()
        {
            TargetType = typeof(MenuViewFlyoutMenuItem);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}