using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Randomizer.Framework.Controls.Shared
{
    public class ToolbarItem : Xamarin.Forms.ToolbarItem
    {
        public ToolbarItem() : base()
        {
            this.InitVisibility();
        }

        private async void InitVisibility()
        {
            await Task.Delay(100);
            OnIsVisibleChanged(this, false, IsVisible);
        }

        public ContentPage ToolbarParent { set; get; }

        public bool IsVisible
        {
            get { return (bool)GetValue(IsVisibleProperty); }
            set { SetValue(IsVisibleProperty, value); }
        }

        public static BindableProperty IsVisibleProperty =
            BindableProperty.Create<ToolbarItem, bool>(o => o.IsVisible, false, propertyChanged: OnIsVisibleChanged);

        private static void OnIsVisibleChanged(BindableObject bindable, bool oldvalue, bool newvalue)
        {
            var item = bindable as ToolbarItem;

            if (item.ToolbarParent == null)
                return;

            var items = item.ToolbarParent.ToolbarItems;

            if (newvalue && !items.Contains(item))
            {
                items.Add(item);
            }
            else if (!newvalue && items.Contains(item))
            {
                items.Remove(item);
            }
        }
    }
}