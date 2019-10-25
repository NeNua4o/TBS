namespace Common.Models
{
    public class ECube : Cube
    {
        public ECube[] Dirs = new ECube[6];
        public Axial Axial;
        public ECube(Cube cube, bool generateDirs = false) : base(cube)
        {
            if (generateDirs)
                GenerateDirs();
        }
        public ECube(int q, int r) : base(q, r)
        {
            Axial.Q = q;
            Axial.R = r;
        }
        public void GenerateDirs()
        {
            Dirs[0] = new ECube(Axial.Q + 1, Axial.R + 0);
            Dirs[1] = new ECube(Axial.Q + 1, Axial.R - 1);
            Dirs[2] = new ECube(Axial.Q + 0, Axial.R - 1);
            Dirs[3] = new ECube(Axial.Q - 1, Axial.R + 0);
            Dirs[4] = new ECube(Axial.Q - 1, Axial.R + 1);
            Dirs[5] = new ECube(Axial.Q + 0, Axial.R + 1);
        }

        public void Scale(int direction, int distance)
        {
            for (int i = 0; i < distance; i++)
            {
                Axial = Dirs[direction].Axial;
                GenerateDirs();
            }
        }
    }
}
