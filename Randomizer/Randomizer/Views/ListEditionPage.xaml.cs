using Randomizer.Framework.Models;
using Randomizer.Framework.ViewModels.ListEditionPage;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Randomizer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListEditionPage : ContentPage
    {
        public ListEditionPage()
        {
            InitializeComponent();
            BindingContext = new ListEditionPageViewModel<string>(new RandomizerList<string>());
        }
    }
}