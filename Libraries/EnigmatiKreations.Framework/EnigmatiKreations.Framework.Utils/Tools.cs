using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace EnigmatiKreations.Framework.Utils
{
    public static class Tools
    {
        public static object TryFindResource(string ressourceId)
        {
            try
            {
                Application.Current.Resources.TryGetValue(ressourceId, out object value);
                if (value != null) return value;

                foreach (var dico in Application.Current.Resources.MergedDictionaries)
                {
                    dico.TryGetValue(ressourceId, out value);
                    if (value != null) return value;
                }

                System.Diagnostics.Debug.WriteLine($"Resource {ressourceId} not found in the application's dictionaries. Returning NULL.");
                return null;
            }
            catch (KeyNotFoundException e)
            {
                System.Diagnostics.Debug.WriteLine($"Resource {ressourceId} not found in the application's dictionaries. Returning NULL. {e.Data} {e.StackTrace}");
                return null;
            }
        }
    }
}
