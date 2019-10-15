using System;
using TBS;

namespace Common.TBSEventArgs
{
    public class SelectedActionChangedEventArgs:EventArgs
    {
        public Act Action { get; set; }
    }
}
