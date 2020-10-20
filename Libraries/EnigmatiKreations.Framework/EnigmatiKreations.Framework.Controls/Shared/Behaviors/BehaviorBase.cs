using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EnigmatiKreations.Framework.Controls.Shared.Behaviors
{
    /// <summary>
    /// Base class for generalized user-defined behaviors responding to arbitrary conditions or events
    /// </summary>
    /// <typeparam name="T">The object for which you want to control the behavior</typeparam>
    public class BehaviorBase<T> : Behavior<T> where T : BindableObject
    {
        /// <summary>
        /// The object to which behaviors we want to react to
        /// </summary>
        public T AssociatedObject { get; private set; }

        /// <summary>
        /// Attaches to the bindable object
        /// </summary>
        /// <remarks>
        /// Subscribe to events of the <see cref="AssociatedObject"/> on this method
        /// </remarks>
        /// <param name="bindable">The bindable object</param>
        protected override void OnAttachedTo(T bindable)
        {
            base.OnAttachedTo(bindable);
            AssociatedObject = bindable;

            if (bindable.BindingContext != null)
            {
                BindingContext = bindable.BindingContext;
            }

            bindable.BindingContextChanged += OnBindingContextChanged;
        }

        /// <summary>
        /// Detaches from the bindable object
        /// </summary>
        /// <remarks>
        /// Unsubscribe from any events of the <see cref="AssociatedObject"/> on this method
        /// </remarks>
        /// <param name="bindable">The bindable object</param>
        protected override void OnDetachingFrom(T bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.BindingContextChanged -= OnBindingContextChanged;
            AssociatedObject = null;
        }


        void OnBindingContextChanged(object sender, EventArgs e)
        {
            OnBindingContextChanged();
        }

        /// <summary>
        /// Override this method to execute an action when the binding context of the <see cref="AssociatedObject"/> changes
        /// </summary>
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            BindingContext = AssociatedObject.BindingContext;
        }
    }
}
