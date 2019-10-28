using Common.Extensions;

namespace Common.Models
{
    public class VCube : Cube
    {
        public VCube[] Dirs = new VCube[6];
        public Axial Axial = new Axial();
        public VCube(Cube cube, bool generateDirs = false) : base(cube)
        {
            Axial = cube.ToAxial();
            if (generateDirs)
                GenerateDirs();
        }
        public VCube(int q, int r) : base(q, r)
        {
            Axial.Q = q;
            Axial.R = r;
        }
        public void GenerateDirs()
        {
            Dirs[0] = new VCube(Axial.Q + 1, Axial.R + 0);
            Dirs[1] = new VCube(Axial.Q + 1, Axial.R - 1);
            Dirs[2] = new VCube(Axial.Q + 0, Axial.R - 1);
            Dirs[3] = new VCube(Axial.Q - 1, Axial.R + 0);
            Dirs[4] = new VCube(Axial.Q - 1, Axial.R + 1);
            Dirs[5] = new VCube(Axial.Q + 0, Axial.R + 1);
        }

        public void Scale(int direction, int distance)
        {
            for (int i = 0; i < distance; i++)
            {
                Axial = Dirs[direction].Axial;
                var cube = Axial.ToCube(); X = cube.X; Y = cube.Y; Z = cube.Z;
                GenerateDirs();
            }
        }
    }
}
