using Common.Models;
using System;

namespace Common.TBSEventArgs
{
    public class SelectedUnitChangedEventArgs : EventArgs
    {
        public Unit BaseUnit { get; set; }
    }
}
