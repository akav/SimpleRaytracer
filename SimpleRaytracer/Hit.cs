using System.Numerics;

namespace SimpleRaytracer
{
    public struct Hit
    {
        public Material material;
        public Vec3 position;
        public Vec3 normal;
        public float distance;

        public Hit(Material material, Vec3 position, Vec3 normal, float distance)
        {
            this.material = material;
            this.position = position;
            this.normal = normal;
            this.distance = distance;
        }
    }
}
