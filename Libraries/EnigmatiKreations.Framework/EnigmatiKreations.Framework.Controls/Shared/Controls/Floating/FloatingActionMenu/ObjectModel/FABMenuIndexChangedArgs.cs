using System;
using System.Collections.Generic;
using System.Text;

namespace EnigmatiKreations.Framework.Controls.Floating
{

    /// <summary>
    /// Class for managing the view event arguments for our SelectIndexChanged event in the FAB Menu class
    /// </summary>
    /// <see cref="FABMenu"/>
    public class FABMenuIndexChangedArgs : EventArgs
    {
        /// <summary>
        /// The name of the event that has been triggered
        /// </summary>
        public readonly string EventName;

        /// <summary>
        /// The index of the FAB selected in the FAB Menu
        /// </summary>
        public readonly int EventIndex;

        public readonly string EventDescription;

        public readonly object CastObject;

        /// <summary>
        /// True if the menu was toggled to be opened, false otherwise
        /// </summary>
        public readonly bool IsMenuOpen;

        public FABMenuIndexChangedArgs(string eventName)
        {
            EventName = eventName;
        }

        public FABMenuIndexChangedArgs(string eventName, bool isMenuOpenNow) : this(eventName)
        {
            IsMenuOpen = isMenuOpenNow;
        }

        public FABMenuIndexChangedArgs(string eventName, int eventIndex) : this(eventName)
        {
            EventIndex = eventIndex;
        }

        public FABMenuIndexChangedArgs(string eventName, object castObject) : this(eventName)
        {
            CastObject = castObject;
        }

        public FABMenuIndexChangedArgs(string eventName, int eventIndex, string desc)
        {
            EventName = eventName;
            EventIndex = eventIndex;
            EventDescription = desc;
        }
    }

}
