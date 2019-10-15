using System;
using TBS;

namespace Common.TBSEventArgs
{
    public class SelectedUnitChangedEventArgs : EventArgs
    {
        public BaseUnit BaseUnit { get; set; }
    }
}
