using System;
using System.Collections.Generic;
using System.Text;

namespace EnigmatiKreations.Framework.MVVM.Navigation
{
	public class NavigationOptions
	{
		public NavigationOptions(bool modal = false, bool closeFlyout = true, bool animated = true)
		{
			CloseFlyout = closeFlyout;
			Modal = modal;
			Animated = animated;
		}

		public bool CloseFlyout { get; }

		public bool Modal { get; }

		public bool Animated { get; set; }

		public static NavigationOptions Default()
		{
			return new NavigationOptions();
		}
	}

	public static class DictionaryExtensions
	{
		public static string AsQueryString(this Dictionary<string, string> args)
		{
			string resultQuery = null;

			if (args != null && args.Count > 0)
			{
				List<string> argList = new List<string>();
				resultQuery = "?";
				foreach (var arg in args)
				{
					argList.Add(arg.Key + "=" + arg.Value);
				}
				resultQuery = resultQuery + string.Join("&", argList);
			}

			return resultQuery;
		}
	}
}
