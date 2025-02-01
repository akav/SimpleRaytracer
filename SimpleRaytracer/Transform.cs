using System.Numerics;

namespace SimpleRaytracer
{
    public abstract class Transform
    {
        public Vec3 Position
        {
            get => _position;
            set
            {
                _position = value;
            }
        }

        public Quaternion Rotation
        {
            get => _rotation;
            set
            {
                _rotation = value;
            }
        }

        public Vec3 Right
        {
            get => Mul(_rotation, Vec3.UnitX);
        }

        public Vec3 Up
        {
            get => Mul(_rotation, Vec3.UnitY);
        }

        public Vec3 Forward
        {
            get => Mul(_rotation, Vec3.UnitZ);
        }

        private Vec3 _position = Vec3.Zero;
        private Quaternion _rotation = Quaternion.Identity;

        // TODO: Remove
        private Vec3 Mul(Quaternion rotation, Vec3 point)
        {
            float num = rotation.X * 2f;
            float num2 = rotation.Y * 2f;
            float num3 = rotation.Z * 2f;
            float num4 = rotation.X * num;
            float num5 = rotation.Y * num2;
            float num6 = rotation.Z * num3;
            float num7 = rotation.X * num2;
            float num8 = rotation.X * num3;
            float num9 = rotation.Y * num3;
            float num10 = rotation.W * num;
            float num11 = rotation.W * num2;
            float num12 = rotation.W * num3;
            Vec3 result = default(Vec3);
            result.X = (1f - (num5 + num6)) * point.X + (num7 - num12) * point.Y + (num8 + num11) * point.Z;
            result.Y = (num7 + num12) * point.X + (1f - (num4 + num6)) * point.Y + (num9 - num10) * point.Z;
            result.Z = (num8 - num11) * point.X + (num9 + num10) * point.Y + (1f - (num4 + num5)) * point.Z;
            return result;
        }

        public Transform()
        {

        }

        public Transform(Vec3 position)
        {
            Position = position;
        }

        public Transform(Vec3 position, Quaternion rotation)
        {
            Position = position;
            Rotation = rotation;
        }
    }
}
