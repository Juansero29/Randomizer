using System.Threading.Tasks;
using Xamarin.Forms;

namespace EnigmatiKreations.Framework.Controls
{
    /// <summary>
    /// A custom toolbar item that can be hidden
    /// </summary>
    public class ToolbarItem : Xamarin.Forms.ToolbarItem
    {
        #region Bindable Properties
        #region IsVisible
        public static readonly BindableProperty IsVisibleProperty = BindableProperty.Create(nameof(IsVisible), typeof(bool), typeof(ToolbarItem), propertyChanged: (obj, old, newV) =>
        {
            var me = obj as ToolbarItem;
            if (!(newV is bool)) return;
            var oldIsVisible = (bool)old;
            var newIsVisible = (bool)newV;
            me?.IsVisibleChanged(newIsVisible);
        });

        private void IsVisibleChanged(bool newIsVisible)
        {
            if (ToolbarParent == null) return;

            var items = ToolbarParent.ToolbarItems;

            if (newIsVisible && !items.Contains(this))
            {
                items.Add(this);
            }
            else if (!newIsVisible && items.Contains(this))
            {
                items.Remove(this);
            }
        }

        /// <summary>
        /// Wether this toolbaritem is visible or not
        /// </summary>
        public bool IsVisible
        {
            get => (bool)GetValue(IsVisibleProperty);
            set => SetValue(IsVisibleProperty, value);
        }

        #endregion
        #endregion
        
        #region Properties
        public ContentPage ToolbarParent { set; get; }
        #endregion

        #region Constructor(s)
        public ToolbarItem() : base()
        {
            this.InitVisibility();
        }

        #endregion

        #region Methods
        private async void InitVisibility()
        {
            await Task.Delay(30);
            IsVisibleChanged(IsVisible);
        }
        #endregion
    }
}