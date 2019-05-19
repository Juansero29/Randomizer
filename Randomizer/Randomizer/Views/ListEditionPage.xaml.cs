using Randomizer.Framework.Models;
using Randomizer.Framework.ViewModels.ListEditionPage;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Randomizer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty("IsEditMode", "editmode")]
    [QueryProperty("IsNew", "new")]
    public partial class ListEditionPage : ContentPage
    {
        public ListEditionPage()
        {
            InitializeComponent();
            BindingContext = new ListEditionPageViewModel(new RandomizerList());
        }

        public string IsNew { set => (BindingContext as ListEditionPageViewModel).IsNew = bool.Parse(value); }
        public string IsEditMode { set => (BindingContext as ListEditionPageViewModel).IsEditMode = bool.Parse(value); }
    }
}