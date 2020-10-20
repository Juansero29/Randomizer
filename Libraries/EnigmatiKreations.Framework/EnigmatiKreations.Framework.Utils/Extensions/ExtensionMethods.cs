using log4net.Core;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EnigmatiKreations.Framework.Utils.Extensions
{
    public static class ExtensionMethods
    {
#pragma warning disable RECS0165 // Asynchronous methods should return a Task instead of void
        public static async void FireAndForgetSafeAsync(this Task task, IErrorHandler handler = null)
#pragma warning restore RECS0165 // Asynchronous methods should return a Task instead of void
        {
            try
            {
                await task;
            }
            catch (Exception ex)
            {
                handler?.Error(ex.Message);
            }
        }



        #region Recherche de control graphiques
        public static T GetTemplateChild<T>(this Element parent, string name) where T : Element
        {
            if (parent == null)
                return null;

            T templateChild;

            foreach (var child in FindVisualChildren<Element>(parent, false))
            {
                templateChild = GetTemplateChild<T>(child, name);
                if (templateChild != null)
                    return templateChild;
            }

            try
            {
                templateChild = parent.FindByName<T>(name);
            }
            catch (InvalidOperationException)
            {
                templateChild = null;
            }

            return templateChild;
        }


        public static IEnumerable<T> FindVisualChildren<T>(this Element element, bool recursive = true) where T : Element
        {
            if (element != null && element is Layout)
            {
                var childrenProperty = element.GetType().GetProperty("InternalChildren", BindingFlags.Instance | BindingFlags.NonPublic);
                if (childrenProperty != null)
                {
                    var children = (IEnumerable<Element>)childrenProperty.GetValue(element);
                    foreach (var child in children)
                    {
                        if (child != null && child is T)
                        {
                            yield return (T)child;
                        }
                        if (recursive)
                        {
                            foreach (T childOfChild in FindVisualChildren<T>(child))
                            {
                                yield return childOfChild;
                            }
                        }
                    }
                }
            }

        }


        /// <summary>
        /// Recherche le premier parent du type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static T FindVisualAncestor<T>(this Element parent) where T : Element
        {
            if (parent == null)
                return null;
            while (parent != null)
            {
                if (parent is T)
                    return (T)parent;
                parent = parent.Parent;
            }
            return null;
        }

        /// <summary>
        /// Cherche le parent de type T.
        /// Null si pas trouvé.
        /// Si trouvé renvoi la liste de tous les ancestres. Le dernier de la liste correpondant a celui cherché
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static List<Element> FindVisualAncestorWithAncestorList<T>(this Element parent) where T : Element
        {
            return FindVisualAncestorWithAncestorList<T>(parent, false);
        }


        /// <summary>
        /// Cherche le parent de type T.
        /// Null si pas trouvé et giveAncestorFinded == false. sinon on retourne les ancetres trouvé quoi qu'il arrive
        /// Si trouvé renvoi la liste de tous les ancestres. Le dernier de la liste correpondant a celui cherché
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static List<Element> FindVisualAncestorWithAncestorList<T>(this Element parent, bool giveAncestorFinded) where T : Element
        {
            if (parent == null)
                return null;
            var ancestors = new List<Element>();
            while (parent != null)
            {
                ancestors.Add(parent);
                if (parent is T)
                    return ancestors;
                parent = parent.Parent;
            }
            if (giveAncestorFinded)
                return ancestors;
            return null;
        }
        #endregion
    }
}
