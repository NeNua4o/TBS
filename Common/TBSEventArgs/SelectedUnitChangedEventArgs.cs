using Common.Models;
using System;

namespace Common.TBSEventArgs
{
    public class SelectedUnitChangedEventArgs : EventArgs
    {
        public BaseUnit BaseUnit { get; set; }
    }
}
