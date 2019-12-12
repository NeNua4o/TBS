namespace ClientV1.Models.Shaders
{
    public interface IShaderL
    {
        int Mx4Model { get; set; }
        int Mx4View { get; set; }
        int Mx4ModelRotate { get; set; }
        int Vec3LightPos { get; set; }
        int Vec3LightCol { get; set; }
    }
}
