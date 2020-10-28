using EnigmatiKreations.Framework.MVVM.BaseViewModels;
using EnigmatiKreations.Framework.MVVM.BaseViewModels.Contract;
using EnigmatiKreations.Framework.Services.Navigation;
using EnigmatiKreations.Framework.Utils.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace EnigmatiKreations.Framework.Controls.Shared.Behaviors
{
    /// <summary>
    /// The class in charge of reacting to the Appearing event of a ContentPage
    /// </summary>
    public class ViewModelLifecycleBehavior : BehaviorBase<Element>
    {


        #region LoadCommand
        public static readonly BindableProperty LoadCommandProperty = BindableProperty.Create(nameof(LoadCommand), typeof(ICommand), typeof(ViewModelLifecycleBehavior), propertyChanged: (obj, old, newV) =>
        {
            var me = obj as ViewModelLifecycleBehavior;
            if (newV != null && !(newV is ICommand)) return;
            var oldLoadCommand = (ICommand)old;
            var newLoadCommand = (ICommand)newV;
            me?.LoadCommandChanged(oldLoadCommand, newLoadCommand);
        });

        private void LoadCommandChanged(ICommand oldLoadCommand, ICommand newLoadCommand)
        {

        }

        /// <summary>
        /// Obtient ou définit la commande a appeler pour le chargement de la vue
        /// </summary>
        public ICommand LoadCommand
        {
            get => (ICommand)GetValue(LoadCommandProperty);
            set => SetValue(LoadCommandProperty, value);
        }
        #endregion



        #region UnloadCommand
        public static readonly BindableProperty UnloadCommandProperty = BindableProperty.Create(nameof(UnloadCommand), typeof(ICommand), typeof(ViewModelLifecycleBehavior), propertyChanged: (obj, old, newV) =>
        {
            var me = obj as ViewModelLifecycleBehavior;
            if (newV != null && !(newV is ICommand)) return;
            var oldUnloadCommand = (ICommand)old;
            var newUnloadCommand = (ICommand)newV;
            me?.UnloadCommandChanged(oldUnloadCommand, newUnloadCommand);
        });

        private void UnloadCommandChanged(ICommand oldUnloadCommand, ICommand newUnloadCommand)
        {

        }

        /// <summary>
        /// Obtient ou définit la commande a appeler pour le déchargement de la vue
        /// </summary>
        public ICommand UnloadCommand
        {
            get => (ICommand)GetValue(UnloadCommandProperty);
            set => SetValue(UnloadCommandProperty, value);
        }
        #endregion


        /// <summary>
        /// Method attaching to the ContentPage
        /// </summary>
        /// <param name="bindable">The content page</param>
        protected override void OnAttachedTo(Element bindable)
        {
            base.OnAttachedTo(bindable);
            if (bindable.Parent == null)
            {
                bindable.PropertyChanged += Bindable_PropertyChanged;
            }
            RegisterElement(bindable);
        }
        /// <summary>
        /// Method dettaching from the ContentPage
        /// </summary>
        /// <param name="bindable">The content page</param>
        protected override void OnDetachingFrom(Element bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.PropertyChanged -= Bindable_PropertyChanged;
        }



        private void RegisterElement(Element bindable)
        {
            if (bindable == null)
                return;

            ILifecycleable viewmodel;
            viewmodel = bindable as ILifecycleable;
            if (viewmodel == null)
            {
                viewmodel = bindable.BindingContext as ILifecycleable;
            }
            if (viewmodel == null)
                return;

            if (bindable.Parent == null)
                return;

            var ancestors = ExtensionMethods.FindVisualAncestorWithAncestorList<ContentPage>(bindable, true);
            if (ancestors == null)
                return;

            Element associatedControl;
            object associetedElement;

            associatedControl = ancestors.FirstOrDefault((element) => element != null);
            Page page = ancestors.Last() as Page;
            if (page == null)
            {
                if (associatedControl == null) // we haven't got a page so we don't know where to attatch this view model
                    return;

                // Get the current page
                page = Container.Resolve<INavigationService>().GetCurrentPage();
            }

            if (associatedControl == null)
            {
                associetedElement = page;
            }
            else
            {
                associetedElement = associatedControl.BindingContext;
            }
            ICommand unloadCommand = UnloadCommand;
            ICommand loadCommand = LoadCommand;


            if (loadCommand == null)
            {
                // We don't have to specify a command for Load, take the one by default
                loadCommand = viewmodel.LoadCommand;
            }

            if (unloadCommand == null)
            {
                // Don't have to specify a command for Unload, take the one by default
                unloadCommand = viewmodel.UnloadCommand;
            }

            //si on est sur un onglet(tabcontrol), il faut notifier le tabcontrol qu'on vient d'enregistrer le viewmodel et qu'il faut peut etre appelé le load
            //si le viewmodel etait déjà enregistré on a pas besoin de le notifier car il a déjà toutes les infos pour gérer le chargement
            //if (isTab && !isRegistered)
            //{
            //    var tabControl = ancestors.FirstOrDefault((element) => element is TabControl.TabControl) as TabControl.TabControl;
            //    if (tabControl != null)
            //    {
            //        tabControl.RegisterElementForLoad(vmRegisterData);
            //    }

            //}
        }

        private void Bindable_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Parent")
            {
                RegisterElement(sender as Element);
            }
        }
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            RegisterElement(AssociatedObject);
        }




    }

}

