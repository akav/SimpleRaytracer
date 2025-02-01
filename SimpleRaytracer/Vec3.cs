using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRaytracer
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Vec3
    {
        public float X;
        public float Y;
        public float Z;

        public Vec3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static Vec3 Zero => new Vec3(0, 0, 0);

        public static Vec3 One => new Vec3(1, 1, 1);

        public static Vec3 UnitX => new Vec3(1, 0, 0);

        public static Vec3 UnitY => new Vec3(0, 1, 0);

        public static Vec3 UnitZ => new Vec3(0, 0, 1);


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec3 operator +(Vec3 a, Vec3 b) => new Vec3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec3 operator -(Vec3 a, Vec3 b) => new Vec3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec3 operator *(Vec3 a, Vec3 b) => new Vec3(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec3 operator *(Vec3 a, float s) => new Vec3(a.X * s, a.Y * s, a.Z * s);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec3 operator /(Vec3 a, float s) => new Vec3(a.X / s, a.Y / s, a.Z / s);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec3 operator *(float s, Vec3 a) => new Vec3(a.X * s, a.Y * s, a.Z * s);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec3 operator /(Vec3 a, Vec3 b) => new Vec3(a.X / b.X, a.Y / b.Y, a.Z / b.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec3 operator /(float s, Vec3 a) => new Vec3(s / a.X, s / a.Y, s / a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec3 operator -(Vec3 a) => new Vec3(-a.X, -a.Y, -a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Vec3 a, Vec3 b) => a.X == b.X && a.Y == b.Y && a.Z == b.Z;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Vec3 a, Vec3 b) => a.X != b.X || a.Y != b.Y || a.Z != b.Z;

        public override bool Equals(object? obj) => obj is Vec3 v && this == v;

        public override int GetHashCode() => X.GetHashCode() ^ Y.GetHashCode() ^ Z.GetHashCode();

        public override string ToString() => $"({X}, {Y}, {Z})";

        public float LengthSquared => X * X + Y * Y + Z * Z;

        public float Length() => MathF.Sqrt(LengthSquared);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Dot(Vec3 a, Vec3 b) => a.X * b.X + a.Y * b.Y + a.Z * b.Z;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec3 Cross(Vec3 a, Vec3 b) => new Vec3(a.Y * b.Z - a.Z * b.Y, a.Z * b.X - a.X * b.Z, a.X * b.Y - a.Y * b.X);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec3 Normalize(Vec3 a)
        {
            float invLen = 1.0f / MathF.Sqrt(a.X * a.X + a.Y * a.Y + a.Z * a.Z);
            return a * invLen;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec3 Clamp(Vec3 a, float min, float max)
        {
            return new Vec3(Math.Clamp(a.X, min, max), Math.Clamp(a.Y, min, max), Math.Clamp(a.Z, min, max));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec3 Reflect(Vec3 v, Vec3 n)
        {
            return v - 2 * Dot(v, n) * n;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec3 Refract(Vec3 v, Vec3 n, float niOverNt)
        {
            float dt = Dot(v, n);
            float discriminant = 1.0f - niOverNt * niOverNt * (1 - dt * dt);
            if (discriminant > 0)
                return niOverNt * (v - n * dt) - n * MathF.Sqrt(discriminant);
            else
                return Vec3.Zero;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec3 Lerp(Vec3 a, Vec3 b, float t)
        {
            return a * (1 - t) + b * t;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec3 Schlick(float cosine, float refIdx)
        {
            float r0 = (1 - refIdx) / (1 + refIdx);
            r0 = r0 * r0;
            return Lerp(new Vec3(r0, r0, r0), new Vec3(1, 1, 1), MathF.Pow(1 - cosine, 5));
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec3 Sqrt(Vec3 a)
        {
            return new Vec3(MathF.Sqrt(a.X), MathF.Sqrt(a.Y), MathF.Sqrt(a.Z));
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec3 Pow(Vec3 a, float b)
        {
            return new Vec3(MathF.Pow(a.X, b), MathF.Pow(a.Y, b), MathF.Pow(a.Z, b));
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec3 Exp(Vec3 a)
        {
            return new Vec3(MathF.Exp(a.X), MathF.Exp(a.Y), MathF.Exp(a.Z));
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec3 Abs(Vec3 a)
        {
            return new Vec3(MathF.Abs(a.X), MathF.Abs(a.Y), MathF.Abs(a.Z));
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec3 Sin(Vec3 a)
        {
            return new Vec3(MathF.Sin(a.X), MathF.Sin(a.Y), MathF.Sin(a.Z));
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec3 Cos(Vec3 a)
        {
            return new Vec3(MathF.Cos(a.X), MathF.Cos(a.Y), MathF.Cos(a.Z));
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec3 Tan(Vec3 a)
        {
            return new Vec3(MathF.Tan(a.X), MathF.Tan(a.Y), MathF.Tan(a.Z));
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec3 Min(Vec3 a, Vec3 b)
        {
            return new Vec3(Math.Min(a.X, b.X), Math.Min(a.Y, b.Y), Math.Min(a.Z, b.Z));
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec3 Max(Vec3 a, Vec3 b)
        {
            return new Vec3(Math.Max(a.X, b.X), Math.Max(a.Y, b.Y), Math.Max(a.Z, b.Z));
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec3 Floor(Vec3 a)
        {
            return new Vec3(MathF.Floor(a.X), MathF.Floor(a.Y), MathF.Floor(a.Z));
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec3 Ceil(Vec3 a)
        {
            return new Vec3(MathF.Ceiling(a.X), MathF.Ceiling(a.Y), MathF.Ceiling(a.Z));
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec3 Fract(Vec3 a)
        {
            return a - Floor(a);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec3 Sign(Vec3 a)
        {
            return new Vec3(Math.Sign(a.X), Math.Sign(a.Y), Math.Sign(a.Z));
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec3 Step(Vec3 edge, Vec3 x)
        {
            return new Vec3(x.X < edge.X ? 0 : 1, x.Y < edge.Y ? 0 : 1, x.Z < edge.Z ? 0 : 1);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec3 Mix(Vec3 a, Vec3 b, float t)
        {
            return a * (1 - t) + b * t;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec3 Mod(Vec3 a, float b)
        {
            return new Vec3(a.X % b, a.Y % b, a.Z % b);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec3 Mod(Vec3 a, Vec3 b)
        {
            return new Vec3(a.X % b.X, a.Y % b.Y, a.Z % b.Z);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec3 Frac(Vec3 a)
        {
            return a - Floor(a);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec3 Abs(Vec3 a, float b)
        {
            return new Vec3(Math.Abs(a.X), Math.Abs(a.Y), Math.Abs(a.Z));
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec3 Abs(Vec3 a, Vec3 b)
        {
            return new Vec3(Math.Abs(a.X), Math.Abs(a.Y), Math.Abs(a.Z));
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec3 Saturate(Vec3 a)
        {
            return new Vec3(Math.Clamp(a.X, 0, 1), Math.Clamp(a.Y, 0, 1), Math.Clamp(a.Z, 0, 1));
        }

        internal static float RandomInUnitDisk(Random random)
        {
            Vec3 p;

            do
            {
                p = 2.0f * new Vec3((float)random.NextDouble(), (float)random.NextDouble(), 0) - new Vec3(1, 1, 0);
            } while (Dot(p, p) >= 1.0f);

            return p.X;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec3 Random(Random random)
        {
            return new Vec3((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble());
        }        
    }
}
