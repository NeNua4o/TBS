using Common.Models;
using System;

namespace Common.TBSEventArgs
{
    public class SelectedActionChangedEventArgs:EventArgs
    {
        public Act Action { get; set; }
    }
}
