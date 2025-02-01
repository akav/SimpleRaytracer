using System.Numerics;

namespace SimpleRaytracer
{
    public struct Material
    {
        public Vec3 Albedo { get; set; } = Vec3.Zero;
        public Vec3 Emission { get; set; } = Vec3.Zero;
        public float Smoothness { get; set; } = 0;

        public Material(Vec3 albedo)
        {
            Albedo = albedo;
            Emission = Vec3.Zero;
        }

        public Material(Vec3 albedo, float smoothness)
        {
            Albedo = albedo;
            Smoothness = smoothness;
        }

        public Material(Vec3 albedo, Vec3 emission)
        {
            Albedo = albedo;
            Emission = emission;
        }
    }
}
