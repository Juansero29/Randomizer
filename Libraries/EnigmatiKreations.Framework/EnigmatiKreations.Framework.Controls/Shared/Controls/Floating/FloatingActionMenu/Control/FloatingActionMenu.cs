// ========================================================================
// Module       : NomadMobile - Source File
// Author       : Juan Rodríguez
// Creation date: 2018-05-25
// ========================================================================

using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace EnigmatiKreations.Framework.Controls.Floating
{
    /// <summary>
    /// A class representing a Floating Action Menu. This is a FAB that can contain and show other Floating Action Menu
    /// </summary>
    /// <seealso cref="FAB"/>
    public class FloatingActionMenu : View
    {
        #region Delegates

        public delegate void ShowHideDelegate(bool animate = true);

        public delegate bool GetOpen();

        #endregion

        #region Bindable Properties
        /// <summary>
        /// The bindable property pointing to the children of this MenuFAB
        /// </summary>
        public static readonly BindableProperty ChildrenProperty = BindableProperty.Create(nameof(Children), typeof(ObservableCollection<FAB>), typeof(FABMenu));

        /// <summary>
        /// The bindable property for this FAB Menu's detail
        /// </summary>
        public static readonly BindableProperty DetailProperty = BindableProperty.Create(nameof(Detail), typeof(string), typeof(FABMenu), string.Empty);

        /// <summary>
        /// The bindable property for this FAB Menu's main button normal color
        /// </summary>
        public static readonly BindableProperty ColorNormalProperty = BindableProperty.Create(nameof(ColorNormal), typeof(Color), typeof(FABMenu), Color.Accent);

        /// <summary>
        /// Bindable property pointing to the image name property for this FAB
        /// </summary>
        /// <seealso cref="ImageName"/>
        public static readonly BindableProperty ImageNameProperty = BindableProperty.Create(nameof(ImageName), typeof(string), typeof(FAB), string.Empty);

        /// <summary>
        /// Bindable property pointing to the pressed color property for this FAB
        /// </summary>
        /// <seealso cref="ColorPressed"/>
        public static readonly BindableProperty ColorPressedProperty = BindableProperty.Create(nameof(ColorPressed), typeof(Color), typeof(FAB), Color.Accent);

        /// <summary>
        /// Bindable property pointing to the ripple color property for this FAB
        /// </summary>
        /// <seealso cref="ColorRipple"/>
        public static readonly BindableProperty ColorRippleProperty = BindableProperty.Create(nameof(ColorRipple), typeof(Color), typeof(FAB), Color.Accent);

        public static readonly BindableProperty IsMenuProperty = BindableProperty.Create(nameof(IsMenu), typeof(bool), typeof(FABMenu), true);

        #endregion

        #region Private Fields
        private bool _isOpened;
        #endregion

        #region Properties

        /// <summary>
        /// The FAB Menu's Children
        /// </summary>
        public ObservableCollection<FAB> Children
        {
            get => (ObservableCollection<FAB>)GetValue(ChildrenProperty);
            set => SetValue(ChildrenProperty, value);
        }

        /// <summary>
        /// A property indicating if the FAB Menu is open or not
        /// </summary>
        public bool IsOpened
        {
            get { return _isOpened; }
            set
            {
                _isOpened = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// The detail of this FAB Menu
        /// </summary>
        public string Detail
        {
            get { return (string)GetValue(DetailProperty); }
            set { SetValue(DetailProperty, value); }
        }

        /// <summary>
        /// The normal color for this FAB Menu 
        /// </summary>
        /// <remarks>
        /// The color is normal when the button isn't pressed and isn't rippling (extending)
        /// </remarks>
        public Color ColorNormal
        {
            get { return (Color)GetValue(ColorNormalProperty); }
            set { SetValue(ColorNormalProperty, value); }
        }

        /// <summary>
        /// A delegate for when the FAB shows itself
        /// </summary>
        public ShowHideDelegate Show { get; set; }

        /// <summary>
        /// A delegate for when the FAB Menu hides itself
        /// </summary>
        public ShowHideDelegate Hide { get; set; }

        /// <summary>
        /// The delegate for when the FAB Menu has to get open
        /// </summary>
        public GetOpen GetFabIsOpen { get; set; }

        /// <summary>
        /// The name of the image this FAB should print to the screen
        /// </summary>
        public string ImageName
        {
            get { return (string)GetValue(ImageNameProperty); }
            set { SetValue(ImageNameProperty, value); }
        }

        /// <summary>
        /// The color of this FAB when it is pressed (while the finger is on it)
        /// </summary>
        public Color ColorPressed
        {
            get { return (Color)GetValue(ColorPressedProperty); }
            set { SetValue(ColorPressedProperty, value); }
        }

        /// <summary>
        /// The color of this FAB when it is rippling (being animated after having pressed it)
        /// </summary>
        public Color ColorRipple
        {
            get { return (Color)GetValue(ColorRippleProperty); }
            set { SetValue(ColorRippleProperty, value); }
        }

        /// <summary>
        /// Property indicating if this is a menu or not
        /// </summary>
        public bool IsMenu
        {
            get => (bool)GetValue(IsMenuProperty);
            set => SetValue(IsMenuProperty, value);
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
            MenuButtonClicked?.Invoke(this, new FABMenuIndexChangedArgs("MenuButtonClicked", !GetFabIsOpen()));
        }


        #endregion

        #region Constructor

        public FloatingActionMenu()
        {
            Children = new ObservableCollection<FAB>();
            IsOpened = false;
        }

        #endregion
    }


    /// <summary>
    /// Enumerates the possible sizes for a FAB
    /// </summary>
    /// <seealso cref="FAB"/>
    public enum FloatingActionButtonSize
    {
        Normal = 0,
        Mini = 1
    }
}
