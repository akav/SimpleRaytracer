using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRaytracer
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Vec2
    {
        public float X;
        public float Y;
        public Vec2(float x, float y)
        {
            X = x;
            Y = y;
        }

        public static Vec2 Zero => new Vec2(0, 0);

        public static Vec2 One => new Vec2(1, 1);

        public static Vec2 UnitX => new Vec2(1, 0);

        public static Vec2 UnitY => new Vec2(0, 1);

        public static Vec2 operator +(Vec2 a, Vec2 b) => new Vec2(a.X + b.X, a.Y + b.Y);

        public static Vec2 operator -(Vec2 a, Vec2 b) => new Vec2(a.X - b.X, a.Y - b.Y);

        public static Vec2 operator *(Vec2 a, Vec2 b) => new Vec2(a.X * b.X, a.Y * b.Y);

        public static Vec2 operator *(Vec2 a, float s) => new Vec2(a.X * s, a.Y * s);

        public static Vec2 operator /(Vec2 a, float s) => new Vec2(a.X / s, a.Y / s);
    }
}
