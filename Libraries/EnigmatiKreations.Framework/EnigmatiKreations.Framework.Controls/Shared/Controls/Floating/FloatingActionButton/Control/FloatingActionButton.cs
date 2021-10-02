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
    /// A highly customizable floating action button with different sizes <see cref="FloatingActionButtonSize"/> and a detail text that appears when long pressed
    /// </summary>
    public class FloatingActionButton : ContentView
    {

        #region Private Fields
        private readonly uint _rotationDurationInMiliseconds = 450;
        private readonly Easing _rotationAnimationEasing = Easing.SpringIn;
        #endregion

        #region Template Parts

        #region Button
        private const string PART_Button = "PART_Button";
        private Frame ButtonPart;
        #endregion

        #region Icon
        private const string PART_Path = "PART_Icon";
        private Path IconPath;
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
            me?.SetButtonContentDependingOnSize();
        });

        private void SetButtonContentDependingOnSize()
        {
            if (!AreButtonAndContentCorrect()) return;
            (ButtonPart.Content as Label).Text = Size == FloatingActionButtonSize.Extended ? Detail : string.Empty;
        }

        private bool AreButtonAndContentCorrect()
        {
            if (ButtonPart is null) return false;
            if (ButtonPart.Content is not Label) return false;
            return true;
        }

        public string Detail
        {
            get => (string)GetValue(DetailProperty);
            set => SetValue(DetailProperty, value);
        }
        #endregion

        #region NormalStateColor
        public static readonly BindableProperty NormalStateColorProperty = BindableProperty.Create(nameof(NormalStateColor), typeof(Color), typeof(FloatingActionButton), propertyChanged: (obj, old, newV) =>
        {
            var me = obj as FloatingActionButton;
            if (newV != null && !(newV is Color)) return;
            var oldColorNormal = (Color)old;
            var newColorNormal = (Color)newV;
        });

        public Color NormalStateColor
        {
            get => (Color)GetValue(NormalStateColorProperty);
            set => SetValue(NormalStateColorProperty, value);
        }
        #endregion

        #region PressedStateColor
        public static readonly BindableProperty PressedStateColorProperty = BindableProperty.Create(nameof(PressedStateColor), typeof(Color), typeof(FloatingActionButton), propertyChanged: (obj, old, newV) =>
        {
            var me = obj as FloatingActionButton;
            if (newV != null && !(newV is Color)) return;
            var oldColorPressed = (Color)old;
            var newColorPressed = (Color)newV;
        });


        public Color PressedStateColor
        {
            get => (Color)GetValue(PressedStateColorProperty);
            set => SetValue(PressedStateColorProperty, value);
        }
        #endregion

        #region RippleStateColor
        public static readonly BindableProperty RippleStateColorProperty = BindableProperty.Create(nameof(RippleStateColor), typeof(Color), typeof(FloatingActionButton), propertyChanged: (obj, old, newV) =>
        {
            var me = obj as FloatingActionButton;
            if (newV != null && !(newV is Color)) return;
            var oldColorRipple = (Color)old;
            var newColorRipple = (Color)newV;
        });


        /// <summary>
        /// The color to use for this <see cref="FloatingActionMenu"/> for the ripple created after the user pressed the button
        /// </summary>
        public Color RippleStateColor
        {
            get => (Color)GetValue(RippleStateColorProperty);
            set => SetValue(RippleStateColorProperty, value);
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

        public ICommand ClickedCommand
        {
            get => (ICommand)GetValue(ClickedCommandProperty);
            set => SetValue(ClickedCommandProperty, value);
        }

        private bool CanExecuteClick(object arg)
        {
            if (ClickedCommand == null) return true;
            return ClickedCommand.CanExecute(arg);
        }

        private void RaiseClickedCommand()
        {
            if (ClickedCommand == null) return;
            if (ClickedCommand.CanExecute(this))
            {
                ClickedCommand.Execute(this);
            }
        }
        #endregion

        #region Size
        public static readonly BindableProperty SizeProperty = BindableProperty.Create(nameof(Size), typeof(FloatingActionButtonSize), typeof(FloatingActionButton), propertyChanged: (obj, old, newV) =>
        {
            var me = obj as FloatingActionButton;
            if (newV != null && !(newV is FloatingActionButtonSize)) return;
            me.ApplyCurrentSize();
            me.SetButtonContentDependingOnSize();
        });

        public FloatingActionButtonSize Size
        {
            get => (FloatingActionButtonSize)GetValue(SizeProperty);
            set => SetValue(SizeProperty, value);
        }
        #endregion

        #endregion

        #region Commands

        #region LongPressCommand
        public ICommand LongPressCommand { get; set; }
        private bool CanExecuteLongPress(object arg)
        {
            if (ClickedCommand == null) return false;
            return ClickedCommand.CanExecute(arg);
        }
        private async Task OnLongPress()
        {
            if (string.IsNullOrEmpty(Detail)) return;
            await MakeDetailAppearAndDisappear();
        }

        #endregion

        #region ButtonClickedCommand

        public ICommand ButtonClickedCommand { get; set; }

        private async Task OnButtonClicked()
        {
            RaiseClickedEvent();
            RaiseClickedCommand();
            await ApplyFullRotationToIcon();
        }

        #endregion

        #endregion

        #region Constructor

        public FloatingActionButton()
        {
            LongPressCommand = new AsyncCommand(OnLongPress, CanExecuteLongPress);
            ButtonClickedCommand = new AsyncCommand(OnButtonClicked, CanExecuteClick);

            // call to OnPropertyChanged because the constructor gets called after OnApplyTemplate
            OnPropertyChanged(nameof(LongPressCommand));
            OnPropertyChanged(nameof(ButtonClickedCommand));
        }

        protected override void OnApplyTemplate()
        {
            // somehow, this is called before the constructor
            base.OnApplyTemplate();

            GetTemplateChilds();

            ApplyCurrentSize();
        }

        private void GetTemplateChilds()
        {
            IconPath = GetTemplateChild(PART_Path) as Path;
            ButtonPart = GetTemplateChild(PART_Button) as Frame;
            DetailFrame = GetTemplateChild(PART_DetailFrame) as Frame;
        }

        #endregion

        #region Animation Methods

        private async Task ApplyFullRotationToIcon()
        {
            await MakeIconRotate180Degrees();
            await ResetIconRotationToZero();
        }


        private async Task MakeIconRotate180Degrees()
        {
            await IconPath.RotateTo(180, _rotationDurationInMiliseconds, _rotationAnimationEasing);
        }

        private async Task ResetIconRotationToZero()
        {
            await IconPath.RotateTo(0, 0);
        }


        private async Task MakeDetailAppearAndDisappear()
        {
            if (Size == FloatingActionButtonSize.Extended) return;
            await MakeDetailAppear();
            await MakeDetailDisappear();
        }

        private async Task MakeDetailAppear()
        {
            await MakeDetailVeryLittle();

            await MakeDetailNormalSize();
        }

        private async Task MakeDetailDisappear()
        {
            await Task.Delay(2000)
                .ContinueWith((t) =>
                {
                    DetailFrame.FadeTo(0, 100);
                    DetailFrame.ScaleTo(0, 100);
                }
            );
        }

        private async Task MakeDetailNormalSize()
        {
            DetailFrame.IsVisible = true;

            await Task.WhenAll(
                DetailFrame.ScaleTo(1, 100),
                DetailFrame.FadeTo(1, 100)
            );
        }

        private async Task MakeDetailVeryLittle()
        {
            await Task.WhenAll(
                DetailFrame.ScaleTo(0.1, 10),
                DetailFrame.FadeTo(0.1, 10)
            );
        }


        #endregion

        #region Utility Methods

        private void ApplyCurrentSize()
        {
            // FAB containers come in three sizes:
            // 1. Default(56 x 56dp) - the default size of this class
            // 2. Mini(40 x 40dp) - 5/7ths smaller than default
            // 3. Extended
            switch (Size)
            {
                case FloatingActionButtonSize.Normal:
                    MakeNormalFABSize();
                    break;

                case FloatingActionButtonSize.Mini:
                    MakeMiniFABSize();
                    break;
                case FloatingActionButtonSize.Extended:
                    MakeExtendedFABSize();
                    break;
            }
        }

        private void MakeNormalFABSize()
        {
            ButtonPart.CornerRadius = 100;
            ButtonPart.HeightRequest = 56;
            ButtonPart.WidthRequest = 56;
        }

        private void MakeMiniFABSize()
        {
            ButtonPart.HeightRequest = 40;
            ButtonPart.WidthRequest = 40;
            ButtonPart.CornerRadius = 100;
        }

        private void MakeExtendedFABSize()
        {
            AbsoluteLayout.SetLayoutFlags(ButtonPart, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutBounds(ButtonPart, new Xamarin.Forms.Rectangle(0.5, 0.5, 0.35, 0.1));
            AbsoluteLayout.SetLayoutBounds(IconPath, new Xamarin.Forms.Rectangle(0.38, 0.5, 16, 16));
            ButtonPart.Padding = new Thickness(left:30, 0, 0, 0);
            ButtonPart.CornerRadius = 80;
        }

        #endregion

        #region Events
        public event EventHandler<EventArgs> Clicked;
        public void RaiseClickedEvent()
        {
            Clicked?.Invoke(this, new FABMenuIndexChangedArgs(nameof(Clicked), this));
        }

        #endregion
    }
}
