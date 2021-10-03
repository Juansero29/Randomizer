using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace EnigmatiKreations.Framework.Controls.Floating
{
    /// <summary>
    /// A class representing a Floating Action Menu. This is a FAB that can contain and show other Floating Action Buttons <see cref="FloatingActionButton"/>
    /// </summary>
    /// <seealso cref="FAB"/>
    public class FloatingActionMenu : View
    {
        #region Children
        public static readonly BindableProperty ChildrenProperty = BindableProperty.Create(nameof(Children), typeof(ObservableCollection<FloatingActionButton>), typeof(FloatingActionMenu), propertyChanged: (obj, old, newV) =>
        {
            var me = obj as FloatingActionMenu;
            if (newV != null && !(newV is ObservableCollection<FloatingActionButton>)) return;
            var oldChildren = (ObservableCollection<FloatingActionButton>)old;
            var newChildren = (ObservableCollection<FloatingActionButton>)newV;
            me?.ChildrenChanged(oldChildren, newChildren);
        });

        private void ChildrenChanged(ObservableCollection<FloatingActionButton> _, ObservableCollection<FloatingActionButton> a)
        {

        }

        /// <summary>
        /// The children <see cref="FloatingActionButton"/>s for this <see cref="FloatingActionMenu"/>
        /// </summary>
        public ObservableCollection<FloatingActionButton> Children
        {
            get => (ObservableCollection<FloatingActionButton>)GetValue(ChildrenProperty);
            set => SetValue(ChildrenProperty, value);
        }
        #endregion

        #region Events

        public event EventHandler<FABMenuIndexChangedArgs> SelectIndexChanged;
        public void RaiseSelectIndexChanged(int index)
        {
            SelectIndexChanged?.Invoke(this, new FABMenuIndexChangedArgs("SelectIndexChanged", index));
        }

        public event EventHandler<FABMenuIndexChangedArgs> MenuOpened;
        public void RaiseMenuOpened()
        {
            MenuOpened?.Invoke(this, new FABMenuIndexChangedArgs("MenuOpened"));
        }

        public event EventHandler<FABMenuIndexChangedArgs> MenuClosed;
        public void RaiseMenuClosed()
        {
            MenuClosed?.Invoke(this, new FABMenuIndexChangedArgs("MenuClosed"));
        }

        public event EventHandler<FABMenuIndexChangedArgs> MenuToggle;
        public void RaiseMenuToggle()
        {
            MenuToggle?.Invoke(this, new FABMenuIndexChangedArgs("MenuToggle", !IsOpened));
        }

        public event EventHandler<FABMenuIndexChangedArgs> MenuButtonClicked;
        public void RaiseMenuButtonClicked()
        {
            MenuButtonClicked?.Invoke(this, new FABMenuIndexChangedArgs("MenuButtonClicked", IsOpened));
        }
        #endregion

        #region Properties
        /// <summary>
        /// A property indicating if the FAB Menu is open or not
        /// </summary>
        private bool _isOpened;
        public bool IsOpened
        {
            get => _isOpened;
            set
            {
                _isOpened = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Constructor

        public FloatingActionMenu()
        {
            Children = new ObservableCollection<FloatingActionButton>();
            IsOpened = false;
        }

        #endregion
    }

}
