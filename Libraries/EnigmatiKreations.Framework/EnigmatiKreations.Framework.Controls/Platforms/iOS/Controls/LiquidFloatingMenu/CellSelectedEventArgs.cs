// ========================================================================
// Module       : NomadMobile.iOS - Source File
// Author       : Bill Holmes
// Creation date: 2018-05-25
// ========================================================================

using System;

namespace EnigmatiKreations.Framework.Controls.iOS
{
    /// <summary>
    /// Event args for a selected cell
    /// </summary>
    public class CellSelectedEventArgs : EventArgs
    {
        /// <summary>
        /// The cell that has been selected
        /// </summary>
        public LiquidFloatingCell Cell { get; set; }

        /// <summary>
        /// This cell's index
        /// </summary>
        public int Index { get; set; }


        /// <summary>
        /// Constructs a CellSelectedEventArgs object
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="index"></param>
        public CellSelectedEventArgs(LiquidFloatingCell cell, int index)
        {
            Cell = cell;
            Index = index;
        }
    }
}
