using Randomizer.Framework.Models;
using Randomizer.Framework.ViewModels.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Randomizer.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class ListEditionPage : ContentPage
    {
        private ListEditionPageViewModel VM;
        public ListEditionPage()
        {
            InitializeComponent();
            
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            if (VM != null) VM.PropertyChanged -= VM_PropertyChanged;
            if (VM?.ListVM != null) VM.ListVM.ItemAdded -= ListVM_ItemAdded;
            VM = BindingContext as ListEditionPageViewModel;
            if (VM == null) return;
            VM.PropertyChanged += VM_PropertyChanged;
        }

        private void VM_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            
            switch(e.PropertyName)
            {
                case nameof(VM.ListVM):
                    if (VM.ListVM == null) return;
                    VM.ListVM.ItemAdded += ListVM_ItemAdded;
                    break;
            }
        }

        private void ListVM_ItemAdded(object sender, System.EventArgs e)
        {
            ItemEntry.Text = string.Empty;
            ItemEntry.Focus();
            ItemsList.ScrollTo(VM.ListVM.ItemsVM.Count);
        }
    }
}