using OpenTK;
using System.Drawing;

namespace ClientV1.Models
{
    public class Plane:Volume
    {
        public Plane(Rectangle rect, float pitch, Vector3 color)
        {
            Vertices = new Vector3[]
            {
                new Vector3(),
            };
        }
    }
}
