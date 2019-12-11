using System.Collections.Generic;
using System.Diagnostics;

namespace ClientV1.Models
{
    class DrawingObject : Volume
    {
        public List<Volume> DrawingParts = new List<Volume>();

    }
}
