using System.Numerics;

namespace SimpleRaytracer
{
    public struct Ray
    {
        public Vec3 Origin { get; set; }
        public Vec3 Direction { get; set; }

        public Ray(Vec3 origin, Vec3 direction)
        {
            Origin = origin;
            Direction = direction;
        }
    }
}
