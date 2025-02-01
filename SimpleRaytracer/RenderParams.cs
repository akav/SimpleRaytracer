using System.Numerics;

namespace SimpleRaytracer
{
    public struct RenderParams
    {
        public int ResolutionX { get; set; }
        public int ResolutionY { get; set; }
        public int Samples { get; set; }
        public int Bounces { get; set; }
        public Vec3 BottomLeft { get; }
        public float PlaneWidth { get; }
        public float PlaneHeight { get; }
        public Vec3 CameraPosition { get; }
        public Vec3 CameraRight { get; }
        public Vec3 CameraUp { get; }
        public Vec3 CameraForward { get; }
        public Vec3 Ambient { get; }
        public GpuBool SimplifiedEnabled { get; }
        public Vec3 SunDir { get; set; } = Vec3.One;
        public int CurrentSampleCount { get; set; }

        public RenderParams(int resolutionX, int resolutionY, int samples, int bounces, Scene scene, GpuBool simplifiedEnabled, int currentSampleCount)
        {
            if (scene is null)
            {
                throw new ArgumentNullException(nameof(scene));
            }

            if (scene.Camera is null)
            {
                throw new ArgumentNullException(nameof(scene));
            }

            ResolutionX = resolutionX;
            ResolutionY = resolutionY;
            Samples = samples;
            Bounces = bounces;
            BottomLeft = scene.Camera.BottomLeft;
            PlaneWidth = scene.Camera.PlaneWidth;
            PlaneHeight = scene.Camera.PlaneHeight;
            CameraPosition = scene.Camera.Position;
            CameraRight = scene.Camera.Right;
            CameraUp = scene.Camera.Up;
            CameraForward = scene.Camera.Forward;
            Ambient = scene.Ambient;
            SimplifiedEnabled = simplifiedEnabled;
            CurrentSampleCount = currentSampleCount;
        }
    }
}
