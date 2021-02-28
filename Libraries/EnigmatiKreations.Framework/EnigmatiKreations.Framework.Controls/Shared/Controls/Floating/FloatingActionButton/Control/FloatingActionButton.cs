using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Shapes;

namespace EnigmatiKreations.Framework.Controls.Floating
{
    /// <summary>
    /// Class representing a Floating Action Button
    /// </summary>

    public class FloatingActionButton : ContentView
    {
        #region Template Parts

        #region Button
        private const string PART_Button = "PART_Button";
        private Frame ButtonPart;
        #endregion

        #region Path
        private const string PART_Path = "PART_Path";
        private Path PathPart;
        #endregion



        #region Frame
        private const string PART_DetailFrame = "PART_DetailFrame";
        private Frame DetailFrame;
        #endregion

        #endregion

        #region Bindable Properties
        #region Detail
        public static readonly BindableProperty DetailProperty = BindableProperty.Create(nameof(Detail), typeof(string), typeof(FloatingActionButton), propertyChanged: (obj, old, newV) =>
        {
            var me = obj as FloatingActionButton;
            if (newV != null && !(newV is string)) return;
            var oldDetail = (string)old;
            var newDetail = (string)newV;
            me?.DetailChanged(oldDetail, newDetail);
        });

        private void DetailChanged(string oldDetail, string newDetail)
        {
            if (ButtonPart == null) // || ButtonPart.Content == null) return;
                return;
            ((Label)ButtonPart.Content).Text = Size == FloatingActionButtonSize.Extended ? newDetail : string.Empty;
            // if (ButtonPart.Content is not Label label) return;
            // label.Text = newDetail;
        }

        /// <summary>
        /// The detail label for this <see cref="FloatingActionButton"/>
        /// </summary>
        public string Detail
        {
            get => (string)GetValue(DetailProperty);
            set => SetValue(DetailProperty, value);
        }
        #endregion

        #region ColorNormal
        public static readonly BindableProperty ColorNormalProperty = BindableProperty.Create(nameof(ColorNormal), typeof(Color), typeof(FloatingActionButton), propertyChanged: (obj, old, newV) =>
        {
            var me = obj as FloatingActionButton;
            if (newV != null && !(newV is Color)) return;
            var oldColorNormal = (Color)old;
            var newColorNormal = (Color)newV;
            me?.ColorNormalChanged(oldColorNormal, newColorNormal);
        });

        private void ColorNormalChanged(Color oldColorNormal, Color newColorNormal)
        {

        }

        /// <summary>
        /// The color when this <see cref="FloatingActionButton"/> is in a normal state
        /// </summary>
        public Color ColorNormal
        {
            get => (Color)GetValue(ColorNormalProperty);
            set => SetValue(ColorNormalProperty, value);
        }
        #endregion

        #region ImageName
        public static readonly BindableProperty ImageNameProperty = BindableProperty.Create(nameof(ImageName), typeof(string), typeof(FloatingActionButton), propertyChanged: (obj, old, newV) =>
        {
            var me = obj as FloatingActionButton;
            if (newV != null && !(newV is string)) return;
            var oldImageName = (string)old;
            var newImageName = (string)newV;
            me?.ImageNameChanged(oldImageName, newImageName);
        });

        private void ImageNameChanged(string oldImageName, string newImageName)
        {

        }

        /// <summary>
        /// The image name to use and put inside of this <see cref="FloatingActionButton"/>
        /// </summary>
        public string ImageName
        {
            get => (string)GetValue(ImageNameProperty);
            set => SetValue(ImageNameProperty, value);
        }
        #endregion

        #region ColorPressed
        public static readonly BindableProperty ColorPressedProperty = BindableProperty.Create(nameof(ColorPressed), typeof(Color), typeof(FloatingActionButton), propertyChanged: (obj, old, newV) =>
        {
            var me = obj as FloatingActionButton;
            if (newV != null && !(newV is Color)) return;
            var oldColorPressed = (Color)old;
            var newColorPressed = (Color)newV;
            me?.ColorPressedChanged(oldColorPressed, newColorPressed);
        });

        private void ColorPressedChanged(Color oldColorPressed, Color newColorPressed)
        {

        }

        /// <summary>
        /// The color to use when this <see cref="FloatingActionButton"/> is pressed by the user
        /// </summary>
        public Color ColorPressed
        {
            get => (Color)GetValue(ColorPressedProperty);
            set => SetValue(ColorPressedProperty, value);
        }
        #endregion

        #region ColorRipple
        public static readonly BindableProperty ColorRippleProperty = BindableProperty.Create(nameof(ColorRipple), typeof(Color), typeof(FloatingActionButton), propertyChanged: (obj, old, newV) =>
        {
            var me = obj as FloatingActionButton;
            if (newV != null && !(newV is Color)) return;
            var oldColorRipple = (Color)old;
            var newColorRipple = (Color)newV;
            me?.ColorRippleChanged(oldColorRipple, newColorRipple);
        });

        private void ColorRippleChanged(Color oldColorRipple, Color newColorRipple)
        {

        }

        /// <summary>
        /// The color to use for this <see cref="FloatinActionMenu"/> for the ripple created after the user pressed 
        /// </summary>
        public Color ColorRipple
        {
            get => (Color)GetValue(ColorRippleProperty);
            set => SetValue(ColorRippleProperty, value);
        }
        #endregion

        #region ClickedCommand
        public static readonly BindableProperty ClickedCommandProperty = BindableProperty.Create(nameof(ClickedCommand), typeof(ICommand), typeof(FloatingActionButton), propertyChanged: (obj, old, newV) =>
        {
            var me = obj as FloatingActionButton;
            if (!(newV is ICommand)) return;
            var oldClicked = (ICommand)old;
            var newClicked = (ICommand)newV;
            me?.ClickedChanged(oldClicked, newClicked);
        });

        private void ClickedChanged(ICommand _, ICommand _1)
        {

        }

        /// <summary>
        /// Command to execute when the FAB is clicked (or tapped)
        /// </summary>
        public ICommand ClickedCommand
        {
            get => (ICommand)GetValue(ClickedCommandProperty);
            set => SetValue(ClickedCommandProperty, value);
        }
        #endregion

        #region Size
        public static readonly BindableProperty SizeProperty = BindableProperty.Create(nameof(Size), typeof(FloatingActionButtonSize), typeof(FloatingActionButton), propertyChanged: (obj, old, newV) =>
        {
            var me = obj as FloatingActionButton;
            if (newV != null && !(newV is FloatingActionButtonSize)) return;
            var oldSize = (FloatingActionButtonSize)old;
            var newSize = (FloatingActionButtonSize)newV;
            me?.SizePropertyChanged(oldSize, newSize);
        });

        private void SizePropertyChanged(FloatingActionButtonSize oldSize, FloatingActionButtonSize newSize)
        {
            ApplyCurrentSize();
            DetailChanged(string.Empty, Detail);
        }

        /// <summary>
        /// The size for this <see cref="FloatingActionButton"/>
        /// </summary>
        public FloatingActionButtonSize Size
        {
            get => (FloatingActionButtonSize)GetValue(SizeProperty);
            set => SetValue(SizeProperty, value);
        }
        #endregion

        #endregion

        #region Commands
        public ICommand LongPressCommand { get; set; }
        public ICommand ButtonClickedCommand { get; set; }
        #endregion

        #region Constructor

        public FloatingActionButton()
        {
            LongPressCommand = new AsyncCommand(OnLongPress, CanExecuteLongPress);
            ButtonClickedCommand = new AsyncCommand(OnButtonClicked, CanExecuteClick);

            // Need to notify the property changed because the constructor gets called after OnApplyTemplate
            OnPropertyChanged(nameof(LongPressCommand));
            OnPropertyChanged(nameof(ButtonClickedCommand));
            ApplyCurrentSize();
        }

        protected override void OnApplyTemplate()
        {
            // for some reason, this is called after the constructor
            base.OnApplyTemplate();
            PathPart = GetTemplateChild(PART_Path) as Path;
            ButtonPart = GetTemplateChild(PART_Button) as Frame;
            DetailFrame = GetTemplateChild(PART_DetailFrame) as Frame;
        }

        #endregion

        #region Animations
        private async Task MakeIconRotate()
        {
            var path = PathPart;
            if (path == null) return;
            uint milisecondsDuration = 450;
            await path.RotateTo(180, milisecondsDuration, Easing.SpringIn);
            await path.RotateTo(0, 0);
        }

        private async Task MakeDetailAppearAndDisappear()
        {
            if (Size == FloatingActionButtonSize.Extended) return;

            await Task.WhenAll(
                DetailFrame.ScaleTo(0.1, 10),
                DetailFrame.FadeTo(0.1, 10)
            );

            DetailFrame.IsVisible = true;

            await Task.WhenAll(
                DetailFrame.ScaleTo(1, 100),
                DetailFrame.FadeTo(1, 100)
            );

            await Task.Delay(2000)
                .ContinueWith((t) =>
                {
                    DetailFrame.FadeTo(0, 100);
                    DetailFrame.ScaleTo(0, 100);
                }
            );
        }


        #endregion

        #region Utility Methods
        private void ApplyCurrentSize()
        {
            // FAB containers come in two sizes:
            // 1. Default(56 x 56dp) - the default size of this class
            // 2. Mini(40 x 40dp) - 5/7ths smaller than default
            switch (Size)
            {
                case FloatingActionButtonSize.Normal:
                    // ButtonPart.Content = null;
                    ButtonPart.CornerRadius = 100;
                    ButtonPart.HeightRequest = 56;
                    ButtonPart.WidthRequest = 56;
                    break;

                case FloatingActionButtonSize.Mini:
                    // ButtonPart.Content = null;
                    ButtonPart.HeightRequest = 40;
                    ButtonPart.WidthRequest = 40;
                    ButtonPart.CornerRadius = 100;
                    break;
                case FloatingActionButtonSize.Extended:
                    AbsoluteLayout.SetLayoutFlags(ButtonPart, AbsoluteLayoutFlags.All);
                    AbsoluteLayout.SetLayoutBounds(ButtonPart, new Xamarin.Forms.Rectangle(0.5, 0.5, 0.35, 0.1));
                    AbsoluteLayout.SetLayoutBounds(PathPart, new Xamarin.Forms.Rectangle(0.38, 0.5, 16, 16));
                    //ButtonPart.Content = new Label() { Text = Detail, FontSize = 5 };
                    ButtonPart.CornerRadius = 80;
                    break;
            }
        }
        private bool CanExecuteClick(object arg)
        {
            if (ClickedCommand == null) return true;
            return ClickedCommand.CanExecute(arg);
        }

        private async Task OnButtonClicked()
        {
            RaiseClicked();
            await MakeIconRotate();
        }

        private bool CanExecuteLongPress(object arg)
        {
            //(koa/onboard)
            //    (Opacity_icommand)
            //    throw
            //    koa
            //    switch
            //    koalawhip
            return ClickedCommand.CanExecute(arg);
        }

        private async Task OnLongPress()
        {
            if (string.IsNullOrEmpty(Detail)) return;
            await MakeDetailAppearAndDisappear();
        }

        #endregion

        #region Events
        public event EventHandler<EventArgs> Clicked;

        public void RaiseClicked()
        {
            Clicked?.Invoke(this, new FABMenuIndexChangedArgs(nameof(Clicked), this));
            if (ClickedCommand == null) return;
            if (ClickedCommand.CanExecute(this))
            {
                ClickedCommand.Execute(this);
            }
        }
        #endregion
    }

}
