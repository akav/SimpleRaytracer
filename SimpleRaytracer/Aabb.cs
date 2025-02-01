using ILGPU.Algorithms;
using System.Numerics;

namespace SimpleRaytracer
{
    public struct Aabb
    {
        public Vec3 Min { get; set; }
        public Vec3 Max { get; set; }

        public Aabb(Vec3 min, Vec3 max)
        {
            Min = min;
            Max = max;
        }

        public bool TestAabb(Ray ray, out float dist)
        {
            Vec3 dirfrac = Vec3.One / ray.Direction;

            float t1 = (Min.X - ray.Origin.X) * dirfrac.X;
            float t2 = (Max.X - ray.Origin.X) * dirfrac.X;
            float t3 = (Min.Y - ray.Origin.Y) * dirfrac.Y;
            float t4 = (Max.Y - ray.Origin.Y) * dirfrac.Y;
            float t5 = (Min.Z - ray.Origin.Z) * dirfrac.Z;
            float t6 = (Max.Z - ray.Origin.Z) * dirfrac.Z;

            float tmin = XMath.Max(XMath.Max(XMath.Min(t1, t2), XMath.Min(t3, t4)), XMath.Min(t5, t6));
            float tmax = XMath.Min(XMath.Min(XMath.Max(t1, t2), XMath.Max(t3, t4)), XMath.Max(t5, t6));

            if (tmin > tmax || tmax < 0)
            {
                dist = float.PositiveInfinity;
                return false;
            }

            dist = tmin;

            return true;
        }
    }
}