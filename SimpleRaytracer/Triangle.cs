using System.Numerics;

namespace SimpleRaytracer
{
    public struct Triangle
    {
        private const float Epsilon = float.Epsilon;
        public Vec3 v0;
        public Vec3 v1;
        public Vec3 v2;
        public Vec3 n0 = Vec3.Zero;
        public Vec3 n1 = Vec3.Zero;
        public Vec3 n2 = Vec3.Zero;

        public Triangle(Vec3 v0, Vec3 v1, Vec3 v2)
        {
            this.v0 = v0;
            this.v1 = v1;
            this.v2 = v2;
        }

        public Triangle(Vec3 v0, Vec3 v1, Vec3 v2, Vec3 n0, Vec3 n1, Vec3 n2) : this(v0, v1, v2)
        {
            this.n0 = n0;
            this.n1 = n1;
            this.n2 = n2;
        }

        public bool IntersectRayTriangle(Ray ray, ref Hit hit)
        {
            var ab = v1 - v0;
            var ac = v2 - v0;

            var n = Vec3.Cross(ab, ac);

            var det = Vec3.Dot(-ray.Direction, n);

            if (det <= 0.0f)
            {
                return false;
            }

            var ap = ray.Origin - v0;
            var t = Vec3.Dot(ap, n);

            if (t < Epsilon)
            {
                return false;
            }

            var e = Vec3.Cross(-ray.Direction, ap);
            var v = Vec3.Dot(v2 - v0, e);

            if (v < 0.0f || v > det)
            {
                return false;
            }

            var w = -Vec3.Dot(v1 - v0, e);

            if (w < 0.0f || v + w > det)
            {
                return false;
            }

            t /= det;

            var x = v / det;
            var y = w / det;

            //var l1 = Vec3.Lerp(n0, n2, y * 2f);
            //var l2 = Vec3.Lerp(n0, n1, x * 2f);
            //var norm = Vec3.Normalize(Vec3.Lerp(l1, l2, 0.5f));

            //hit.material = new Material(norm, 0);
            //hit.material = new Material(new Vec3(v / det, w / det, 0), 0.5f);
            hit.position = ray.Origin + t * ray.Direction;
            hit.distance = t;
            hit.normal = Vec3.Normalize(n);

            return true;
        }
    }
}
