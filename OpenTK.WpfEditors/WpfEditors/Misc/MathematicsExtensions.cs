using System;
using System.Runtime.CompilerServices;

namespace OpenTK.WpfEditors;

public static partial class MathematicsExtensions
{
    [CLSCompliant(false)][MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Mathematics.Vector2 Abs(this Mathematics.Vector2 value)
    {
        return new Mathematics.Vector2(MathF.Abs(value.X), MathF.Abs(value.Y));
    }
}

public static partial class MathematicsExtensions
{
    [CLSCompliant(false)]
    public static readonly Mathematics.Matrix4 LeftHandedIdentity = new(1f, 0.0f, 0.0f, 0.0f, 0.0f, 1f, 0.0f, 0.0f, 0.0f, 0.0f, -1f, 0.0f, 0.0f, 0.0f, 0.0f, 1f);


    [CLSCompliant(false)][MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Mathematics.Matrix4 PerspectiveFieldOfView(float fovy,
                                                             float aspect,
                                                             float depthNear,
                                                             float depthFar)
    {
        float top    = depthNear * MathF.Tan(0.5f * fovy);
        float bottom = -top;
        return PerspectiveOffCenter(bottom * aspect, top * aspect, bottom, top, depthNear, depthFar);
    }

    [CLSCompliant(false)][MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Mathematics.Matrix4 PerspectiveOffCenter(float left,
                                                           float right,
                                                           float bottom,
                                                           float top,
                                                           float depthNear,
                                                           float depthFar)
    {
        float rightMleft         = right - left;
        float topMbottom         = top - bottom;
        float depthFarMdepthNear = depthFar - depthNear;

        return new Mathematics.Matrix4(2.0f * depthNear / rightMleft,
                                       0.0f,
                                       0.0f,
                                       0.0f,
                                       0.0f,
                                       2.0f * depthNear / topMbottom,
                                       0.0f,
                                       0.0f,
                                       (right + left) / rightMleft,
                                       (top + bottom) / topMbottom,
                                       -(depthFar + depthNear) / depthFarMdepthNear,
                                       -1f,
                                       0.0f,
                                       0.0f,
                                       -(2.0f * depthFar * depthNear) / depthFarMdepthNear,
                                       0f);
    }

    [CLSCompliant(false)][MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Mathematics.Matrix4 Orthographic(float width,
                                                   float height,
                                                   float depthNear,
                                                   float depthFar)
    {
        float num1 = width / 2.0f;
        float num2 = height / 2.0f;

        return OrthographicOffCenter(-num1, num1, -num2, num2, depthNear, depthFar);
    }

    [CLSCompliant(false)][MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Mathematics.Matrix4 OrthographicOffCenter(float left,
                                                            float right,
                                                            float bottom,
                                                            float top,
                                                            float depthNear,
                                                            float depthFar)
    {
        float num1 = 1.0f / (right - left);
        float num2 = 1.0f / (top - bottom);
        float num3 = 1.0f / (depthFar - depthNear);

        return Mathematics.Matrix4.Identity with
        {
            M11 = 2f * num1,
            M23 = 2f * num2,
            M34 = -2f * num3,
            M41 = -(right + left) * num1,
            M42 = -(top + bottom) * num2,
            M43 = -(depthFar + depthNear) * num3
        };
    }

    [CLSCompliant(false)][MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Mathematics.Matrix4 LookAt(Mathematics.Vector3 eye,
                                             Mathematics.Vector3 target,
                                             Mathematics.Vector3 up)
    {
        Mathematics.Vector3 vector3_1 = Mathematics.Vector3.Normalize(eye - target);
        Mathematics.Vector3 right     = Mathematics.Vector3.Normalize(Mathematics.Vector3.Cross(up,        vector3_1));
        Mathematics.Vector3 vector3_2 = Mathematics.Vector3.Normalize(Mathematics.Vector3.Cross(vector3_1, right));

        return Mathematics.Matrix4.Identity with
        {
            M11 = right.X,
            M12 = vector3_2.X,
            M13 = vector3_1.X,
            M14 = 0.0f,
            M21 = right.Y,
            M22 = vector3_2.Y,
            M23 = vector3_1.Y,
            M24 = 0.0f,
            M31 = right.Z,
            M32 = vector3_2.Z,
            M33 = vector3_1.Z,
            M34 = 0.0f,
            M41 = -(right.X * eye.X + right.Y * eye.Y + right.Z * eye.Z),
            M42 = -(vector3_2.X * eye.X + vector3_2.Y * eye.Y + vector3_2.Z * eye.Z),
            M43 = -(vector3_1.X * eye.X + vector3_1.Y * eye.Y + vector3_1.Z * eye.Z),
            M44 = 1f
        };
    }
}

public static partial class MathematicsExtensions
{
    [CLSCompliant(false)][MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static float ToDegrees(this float radians)
    {
        const float convertion = 57.295779513082320876798154814105f;
        return radians * convertion;
    }

    [CLSCompliant(false)][MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static float ToRadians(this float degrees)
    {
        const float convertion = 0.01745329251994329576923690768489f;
        return degrees * convertion;
    }

    [CLSCompliant(false)][MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool IsZero(this float value)
    {
        return MathF.Abs(value) < float.Epsilon;
    }

    [CLSCompliant(false)][MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool IsOne(this float value)
    {
        return MathF.Abs(value - 1f) < float.Epsilon;
    }

    [CLSCompliant(false)][MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static double ToDegrees(this double radians)
    {
        const double convertion = 57.295779513082320876798154814105;
        return radians * convertion;
    }

    [CLSCompliant(false)][MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static double ToRadians(this double degrees)
    {
        const double convertion = 0.01745329251994329576923690768489;
        return degrees * convertion;
    }

    [CLSCompliant(false)][MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool IsZero(this double value)
    {
        return Math.Abs(value) < double.Epsilon;
    }

    [CLSCompliant(false)][MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool IsOne(this double value)
    {
        return Math.Abs(value - 1.0) < double.Epsilon;
    }


    [CLSCompliant(false)][MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static float Min(float[] vals)
    {
        float min = float.MaxValue;

        for(uint i = 0; i < vals.Length; i++)
        {
            if(min > vals[i])
            {
                min = vals[i];
            }
        }

        return min;
    }

    [CLSCompliant(false)][MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static float Max(float[] vals)
    {
        float min = float.MinValue;

        for(uint i = 0; i < vals.Length; i++)
        {
            if(min < vals[i])
            {
                min = vals[i];
            }
        }

        return min;
    }


    [CLSCompliant(false)][MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static float RoundDown(float value,
                                  float multiple = 1f)
    {
        if(multiple == 0f)
        {
            return value;
        }

        return MathF.Floor(value / multiple) * multiple;
    }

    [CLSCompliant(false)][MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static System.Numerics.Vector2 RoundDown(System.Numerics.Vector2 value,
                                                    System.Numerics.Vector2 multiple)
    {
        if(multiple == System.Numerics.Vector2.Zero)
        {
            return value;
        }

        return new System.Numerics.Vector2(MathF.Floor(value.X / multiple.X) * multiple.X, MathF.Floor(value.Y / multiple.Y) * multiple.Y);
    }

    [CLSCompliant(false)][MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static float RoundUp(float value,
                                float multiple = 1f)
    {
        if(multiple == 0f)
        {
            return value;
        }

        return MathF.Ceiling(value / multiple) * multiple;
    }

    [CLSCompliant(false)][MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static System.Numerics.Vector2 RoundUp(System.Numerics.Vector2 value,
                                                  System.Numerics.Vector2 multiple)
    {
        if(multiple == System.Numerics.Vector2.Zero)
        {
            return value;
        }

        return new System.Numerics.Vector2(MathF.Ceiling(value.X / multiple.X) * multiple.X, MathF.Ceiling(value.Y / multiple.Y) * multiple.Y);
    }

    [CLSCompliant(false)][MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static int AlignUp(int val,
                              int align)
    {
        return (val + align - 1) / align * align;
    }

    [CLSCompliant(false)][MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static uint AlignUp(uint val,
                               uint align)
    {
        return (val + align - 1) / align * align;
    }
}

public static partial class MathematicsExtensions
{
    [CLSCompliant(false)][MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static System.Numerics.Vector2 Transform(this System.Numerics.Vector2   v,
                                                    ref  System.Numerics.Matrix3x2 matrix)
    {
        return System.Numerics.Vector2.Transform(v, matrix);
    }

    [CLSCompliant(false)][MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static System.Numerics.Vector3 TransformToVector3(this System.Numerics.Vector2   v,
                                                             ref  System.Numerics.Matrix3x2 matrix,
                                                             float                          z)
    {
        System.Numerics.Vector2 result = v.Transform(ref matrix);
        return new System.Numerics.Vector3(result.X, result.Y, z);
    }
}

public static partial class MathematicsExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    private static void MinMaxRgb(out float min,
                                  out float max,
                                  float     r,
                                  float     g,
                                  float     b)
    {
        if(r > g)
        {
            max = r;
            min = g;
        }
        else
        {
            max = g;
            min = r;
        }

        if(b > max)
        {
            max = b;
        }
        else if(b < min)
        {
            min = b;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    internal static float GetBrightness(this Mathematics.Color4 color)
    {
        MinMaxRgb(out float min, out float max, color.R, color.G, color.B);

        return (max + min) / (byte.MaxValue * 2f);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    internal static float GetHue(this Mathematics.Color4 color)
    {
        if(MathF.Abs(color.R - color.G) < float.Epsilon && MathF.Abs(color.G - color.B) < float.Epsilon)
        {
            return 0f;
        }

        MinMaxRgb(out float min, out float max, color.R, color.G, color.B);

        float delta = max - min;
        float hue;

        if(MathF.Abs(color.R - max) < float.Epsilon)
        {
            hue = (color.G - color.B) / delta;
        }
        else if(MathF.Abs(color.G - max) < float.Epsilon)
        {
            hue = (color.B - color.R) / delta + 2f;
        }
        else
        {
            hue = (color.R - color.G) / delta + 4f;
        }

        hue *= 60f;
        if(hue < 0f)
        {
            hue += 360f;
        }

        return hue;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    internal static float GetSaturation(this Mathematics.Color4 color)
    {
        if(MathF.Abs(color.R - color.G) < float.Epsilon && MathF.Abs(color.G - color.B) < float.Epsilon)
        {
            return 0f;
        }

        MinMaxRgb(out float min, out float max, color.R, color.G, color.B);

        float div = max + min;

        if(div > 1f)
        {
            div = 2f - max - min;
        }

        return (max - min) / div;
    }
}

public static partial class MathematicsExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    internal static Mathematics.Vector4 ToVector4(this Mathematics.Color4 color)
    {
        return new Mathematics.Vector4(color.R, color.G, color.B, color.A);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    internal static uint ToRgba(this Mathematics.Color4 color)
    {
        return ((uint)(color.R * byte.MaxValue) << 24) | ((uint)(color.G * byte.MaxValue) << 16) | ((uint)(color.B * byte.MaxValue) << 8) | (uint)(color.A * byte.MaxValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    internal static uint ToAbgr(this Mathematics.Color4 color)
    {
        return ((uint)(color.A * byte.MaxValue) << 24) | ((uint)(color.B * byte.MaxValue) << 16) | ((uint)(color.G * byte.MaxValue) << 8) | (uint)(color.R * byte.MaxValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    internal static uint ToRgba(this System.Drawing.Color color)
    {
        return (uint)(color.R << 24) | (uint)(color.G << 16) | (uint)(color.B << 8) | color.A;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    internal static uint ToAbgr(this System.Drawing.Color color)
    {
        return ((uint)color.A << 24) | ((uint)color.B << 16) | ((uint)color.G << 8) | color.R;
    }
}

#region Single Value

public static partial class MathematicsExtensions
{
    [CLSCompliant(false)][MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static System.Drawing.Color ToSystemNumerics(this Mathematics.Color4 value)
    {
        return System.Drawing.Color.FromArgb((int)(value.A * byte.MaxValue), (int)(value.R * byte.MaxValue), (int)(value.G * byte.MaxValue), (int)(value.B * byte.MaxValue));
    }


    [CLSCompliant(false)][MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static System.Windows.Media.Color ToSystemWindowsMedia(this Mathematics.Color4 value)
    {
        return System.Windows.Media.Color.FromArgb((byte)(value.A * byte.MaxValue), (byte)(value.R * byte.MaxValue), (byte)(value.G * byte.MaxValue), (byte)(value.B * byte.MaxValue));
    }
    

    [CLSCompliant(false)][MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Mathematics.Color4 ToOpenTK(this System.Drawing.Color value)
    {
        return new Mathematics.Color4(value.R, value.G, value.B, value.A);
    }

    [CLSCompliant(false)][MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Mathematics.Color4 ToOpenTK(this System.Windows.Media.Color value)
    {
        return new Mathematics.Color4(value.R, value.G, value.B, value.A);
    }
}

public static partial class MathematicsExtensions
{
    [CLSCompliant(false)][MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static System.Drawing.Color? ToSystemNumerics(this Mathematics.Color4? value)
    {
        if(value.HasValue)
        {
            return System.Drawing.Color.FromArgb((int)(value.Value.A * byte.MaxValue), (int)(value.Value.R * byte.MaxValue), (int)(value.Value.G * byte.MaxValue), (int)(value.Value.B * byte.MaxValue));
        }

        return System.Drawing.Color.Transparent;
    }

    [CLSCompliant(false)][MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Mathematics.Color4? ToOpenTK(this System.Drawing.Color? value)
    {
        if(value.HasValue)
        {
            return System.Drawing.Color.FromArgb(value.Value.ToArgb());
        }

        return new Mathematics.Color4(0, 0, 0, 0);
    }
}

public static partial class MathematicsExtensions
{
    [CLSCompliant(false)][MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Half ToSystemNumerics(this OpenTK.Mathematics.Half value)
    {
        unsafe
        {
            return *(Half*)Unsafe.AsPointer(ref value);
        }
    }

    [CLSCompliant(false)][MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static System.Numerics.Vector2 ToSystemNumerics(this Mathematics.Vector2 value)
    {
        unsafe
        {
            return *(System.Numerics.Vector2*)Unsafe.AsPointer(ref value);
        }
    }

    [CLSCompliant(false)][MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static System.Numerics.Vector3 ToSystemNumerics(this Mathematics.Vector3 value)
    {
        unsafe
        {
            return *(System.Numerics.Vector3*)Unsafe.AsPointer(ref value);
        }
    }

    [CLSCompliant(false)][MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static System.Numerics.Vector4 ToSystemNumerics(this Mathematics.Vector4 value)
    {
        unsafe
        {
            return *(System.Numerics.Vector4*)Unsafe.AsPointer(ref value);
        }
    }


    [CLSCompliant(false)][MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static System.Numerics.Matrix3x2 ToSystemNumerics(this Mathematics.Matrix3x2 value)
    {
        unsafe
        {
            return *(System.Numerics.Matrix3x2*)Unsafe.AsPointer(ref value);
        }
    }

    [CLSCompliant(false)][MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static System.Numerics.Matrix4x4 ToSystemNumerics(this Mathematics.Matrix4 value)
    {
        unsafe
        {
            return *(System.Numerics.Matrix4x4*)Unsafe.AsPointer(ref value);
        }
    }

    [CLSCompliant(false)][MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static System.Numerics.Quaternion ToSystemNumerics(this Mathematics.Quaternion value)
    {
        unsafe
        {
            return *(System.Numerics.Quaternion*)Unsafe.AsPointer(ref value);
        }
    }
}

public static partial class MathematicsExtensions
{
    [CLSCompliant(false)][MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static OpenTK.Mathematics.Half ToOpenTK(this Half value)
    {
        unsafe
        {
            return *(OpenTK.Mathematics.Half*)Unsafe.AsPointer(ref value);
        }
    }

    [CLSCompliant(false)][MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Mathematics.Vector2 ToOpenTK(this System.Numerics.Vector2 value)
    {
        unsafe
        {
            return *(Mathematics.Vector2*)Unsafe.AsPointer(ref value);
        }
    }

    [CLSCompliant(false)][MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Mathematics.Vector3 ToOpenTK(this System.Numerics.Vector3 value)
    {
        unsafe
        {
            return *(Mathematics.Vector3*)Unsafe.AsPointer(ref value);
        }
    }

    [CLSCompliant(false)][MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Mathematics.Vector4 ToOpenTK(this System.Numerics.Vector4 value)
    {
        unsafe
        {
            return *(Mathematics.Vector4*)Unsafe.AsPointer(ref value);
        }
    }


    [CLSCompliant(false)][MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Mathematics.Matrix3x2 ToOpenTK(this System.Numerics.Matrix3x2 value)
    {
        unsafe
        {
            return *(Mathematics.Matrix3x2*)Unsafe.AsPointer(ref value);
        }
    }

    [CLSCompliant(false)][MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Mathematics.Matrix4 ToOpenTK(this System.Numerics.Matrix4x4 value)
    {
        unsafe
        {
            return *(Mathematics.Matrix4*)Unsafe.AsPointer(ref value);
        }
    }

    [CLSCompliant(false)][MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Mathematics.Quaternion ToOpenTK(this System.Numerics.Quaternion value)
    {
        unsafe
        {
            return *(Mathematics.Quaternion*)Unsafe.AsPointer(ref value);
        }
    }
}

public static partial class MathematicsExtensions
{
    [CLSCompliant(false)][MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Half? ToSystemNumerics(this OpenTK.Mathematics.Half? value)
    {
        unsafe
        {
            return *(Half?*)Unsafe.AsPointer(ref value);
        }
    }

    [CLSCompliant(false)][MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static System.Numerics.Vector2? ToSystemNumerics(this Mathematics.Vector2? value)
    {
        unsafe
        {
            return *(System.Numerics.Vector2?*)Unsafe.AsPointer(ref value);
        }
    }

    [CLSCompliant(false)][MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static System.Numerics.Vector3? ToSystemNumerics(this Mathematics.Vector3? value)
    {
        unsafe
        {
            return *(System.Numerics.Vector3?*)Unsafe.AsPointer(ref value);
        }
    }

    [CLSCompliant(false)][MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static System.Numerics.Vector4? ToSystemNumerics(this Mathematics.Vector4? value)
    {
        unsafe
        {
            return *(System.Numerics.Vector4?*)Unsafe.AsPointer(ref value);
        }
    }


    [CLSCompliant(false)][MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static System.Numerics.Matrix3x2? ToSystemNumerics(this Mathematics.Matrix3x2? value)
    {
        unsafe
        {
            return *(System.Numerics.Matrix3x2?*)Unsafe.AsPointer(ref value);
        }
    }

    [CLSCompliant(false)][MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static System.Numerics.Matrix4x4? ToSystemNumerics(this Mathematics.Matrix4? value)
    {
        unsafe
        {
            return *(System.Numerics.Matrix4x4?*)Unsafe.AsPointer(ref value);
        }
    }

    [CLSCompliant(false)][MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static System.Numerics.Quaternion? ToSystemNumerics(this Mathematics.Quaternion? value)
    {
        unsafe
        {
            return *(System.Numerics.Quaternion?*)Unsafe.AsPointer(ref value);
        }
    }
}

public static partial class MathematicsExtensions
{
    [CLSCompliant(false)][MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static OpenTK.Mathematics.Half? ToOpenTK(this Half? value)
    {
        unsafe
        {
            return *(OpenTK.Mathematics.Half?*)Unsafe.AsPointer(ref value);
        }
    }

    [CLSCompliant(false)][MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Mathematics.Vector2? ToOpenTK(this System.Numerics.Vector2? value)
    {
        unsafe
        {
            return *(Mathematics.Vector2?*)Unsafe.AsPointer(ref value);
        }
    }

    [CLSCompliant(false)][MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Mathematics.Vector3? ToOpenTK(this System.Numerics.Vector3? value)
    {
        unsafe
        {
            return *(Mathematics.Vector3?*)Unsafe.AsPointer(ref value);
        }
    }

    [CLSCompliant(false)][MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Mathematics.Vector4? ToOpenTK(this System.Numerics.Vector4? value)
    {
        unsafe
        {
            return *(Mathematics.Vector4?*)Unsafe.AsPointer(ref value);
        }
    }


    [CLSCompliant(false)][MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Mathematics.Matrix3x2? ToOpenTK(this System.Numerics.Matrix3x2? value)
    {
        unsafe
        {
            return *(Mathematics.Matrix3x2?*)Unsafe.AsPointer(ref value);
        }
    }

    [CLSCompliant(false)][MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Mathematics.Matrix4? ToOpenTK(this System.Numerics.Matrix4x4? value)
    {
        unsafe
        {
            return *(Mathematics.Matrix4?*)Unsafe.AsPointer(ref value);
        }
    }

    [CLSCompliant(false)][MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Mathematics.Quaternion? ToOpenTK(this System.Numerics.Quaternion? value)
    {
        unsafe
        {
            return *(Mathematics.Quaternion?*)Unsafe.AsPointer(ref value);
        }
    }
}

#endregion

#region Array Value

public static partial class MathematicsExtensions
{
    [CLSCompliant(false)][MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static System.Drawing.Color[] ToSystemNumerics(this Mathematics.Color4[] value)
    {
        //System.Buffer.MemoryCopy(pArray, Unsafe.AsPointer(ref Unsafe.As<RawArrayData>(array).Data), byteSize, byteSize);

        System.Drawing.Color[] newValue = new System.Drawing.Color[value.Length];

        for(int i = 0; i < value.Length; i++)
        {
            newValue[i] = value[i].ToSystemNumerics();
        }

        return newValue;
    }

    [CLSCompliant(false)][MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Mathematics.Color4[] ToOpenTK(this System.Drawing.Color[] value)
    {
        Mathematics.Color4[] newValue = new Mathematics.Color4[value.Length];

        for(int i = 0; i < value.Length; i++)
        {
            newValue[i] = value[i].ToOpenTK();
        }

        return newValue;
    }
}

//public static partial class MathematicsExtensions
//{
//    [CLSCompliant(false)][MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
//    public static System.Drawing.Color[]? ToSystemNumerics(this OpenTK.Mathematics.Color4[]? value)
//    {
//        if (value is not null)
//        {
//            System.Drawing.Color[] newValue = new System.Drawing.Color[value.Length];

//            for (int i = 0; i < value.Length; i++)
//            {
//                newValue[i] = value[i].ToSystemNumerics();
//            }

//            return newValue;
//        }

//        return Array.Empty<System.Drawing.Color>();
//    }

//    [CLSCompliant(false)][MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
//    public static OpenTK.Mathematics.Color4[]? ToOpenTK(this System.Drawing.Color[]? value)
//    {
//        if (value is not null)
//        {
//            OpenTK.Mathematics.Color4[] newValue = new OpenTK.Mathematics.Color4[value.Length];

//            for (int i = 0; i < value.Length; i++)
//            {
//                newValue[i] = value[i].ToOpenTK();
//            }

//            return newValue;
//        }

//        return Array.Empty<OpenTK.Mathematics.Color4>();
//    }
//}

public static partial class MathematicsExtensions
{
    [CLSCompliant(false)][MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Half[] ToSystemNumerics(this OpenTK.Mathematics.Half[] value)
    {
        ref Half[] newvalue = ref Unsafe.As<OpenTK.Mathematics.Half[], Half[]>(ref value);
        return newvalue;
    }

    [CLSCompliant(false)][MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static System.Numerics.Vector2[] ToSystemNumerics(this Mathematics.Vector2[] value)
    {
        ref System.Numerics.Vector2[] newvalue = ref Unsafe.As<Mathematics.Vector2[], System.Numerics.Vector2[]>(ref value);
        return newvalue;
    }

    [CLSCompliant(false)][MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static System.Numerics.Vector3[] ToSystemNumerics(this Mathematics.Vector3[] value)
    {
        ref System.Numerics.Vector3[] newvalue = ref Unsafe.As<Mathematics.Vector3[], System.Numerics.Vector3[]>(ref value);
        return newvalue;
    }

    [CLSCompliant(false)][MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static System.Numerics.Vector4[] ToSystemNumerics(this Mathematics.Vector4[] value)
    {
        ref System.Numerics.Vector4[] newvalue = ref Unsafe.As<Mathematics.Vector4[], System.Numerics.Vector4[]>(ref value);
        return newvalue;
    }


    [CLSCompliant(false)][MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static System.Numerics.Matrix3x2[] ToSystemNumerics(this Mathematics.Matrix3x2[] value)
    {
        ref System.Numerics.Matrix3x2[] newvalue = ref Unsafe.As<Mathematics.Matrix3x2[], System.Numerics.Matrix3x2[]>(ref value);
        return newvalue;
    }

    [CLSCompliant(false)][MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static System.Numerics.Matrix4x4[] ToSystemNumerics(this Mathematics.Matrix4[] value)
    {
        ref System.Numerics.Matrix4x4[] newvalue = ref Unsafe.As<Mathematics.Matrix4[], System.Numerics.Matrix4x4[]>(ref value);
        return newvalue;
    }
    
    [CLSCompliant(false)][MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static System.Numerics.Quaternion[] ToSystemNumerics(this Mathematics.Quaternion[] value)
    {
        ref System.Numerics.Quaternion[] newvalue = ref Unsafe.As<Mathematics.Quaternion[], System.Numerics.Quaternion[]>(ref value);
        return newvalue;
    }
}

public static partial class MathematicsExtensions
{
    [CLSCompliant(false)][MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static OpenTK.Mathematics.Half[] ToOpenTK(this Half[] value)
    {
        ref OpenTK.Mathematics.Half[] newvalue = ref Unsafe.As<Half[], OpenTK.Mathematics.Half[]>(ref value);
        return newvalue;
    }
    
    [CLSCompliant(false)][MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Mathematics.Vector2[] ToOpenTK(this System.Numerics.Vector2[] value)
    {
        ref Mathematics.Vector2[] newvalue = ref Unsafe.As<System.Numerics.Vector2[], Mathematics.Vector2[]>(ref value);
        return newvalue;
    }
    
    [CLSCompliant(false)][MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Mathematics.Vector3[] ToOpenTK(this System.Numerics.Vector3[] value)
    {
        ref Mathematics.Vector3[] newvalue = ref Unsafe.As<System.Numerics.Vector3[], Mathematics.Vector3[]>(ref value);
        return newvalue;
    }
    
    [CLSCompliant(false)][MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Mathematics.Vector4[] ToOpenTK(this System.Numerics.Vector4[] value)
    {
        ref Mathematics.Vector4[] newvalue = ref Unsafe.As<System.Numerics.Vector4[], Mathematics.Vector4[]>(ref value);
        return newvalue;
    }
    
    [CLSCompliant(false)][MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Mathematics.Matrix3x2[] ToOpenTK(this System.Numerics.Matrix3x2[] value)
    {
        ref Mathematics.Matrix3x2[] newvalue = ref Unsafe.As<System.Numerics.Matrix3x2[], Mathematics.Matrix3x2[]>(ref value);
        return newvalue;
    }
    
    [CLSCompliant(false)][MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Mathematics.Matrix4[] ToOpenTK(this System.Numerics.Matrix4x4[] value)
    {
        ref Mathematics.Matrix4[] newvalue = ref Unsafe.As<System.Numerics.Matrix4x4[], Mathematics.Matrix4[]>(ref value);
        return newvalue;
    }
    
    [CLSCompliant(false)][MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Mathematics.Quaternion[] ToOpenTK(this System.Numerics.Quaternion[] value)
    {
        ref Mathematics.Quaternion[] newvalue = ref Unsafe.As<System.Numerics.Quaternion[], Mathematics.Quaternion[]>(ref value);
        return newvalue;
    }
}

//public static partial class MathematicsExtensions
//{
//    [CLSCompliant(false)][MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
//    public static System.Half[]? ToSystemNumerics(this OpenTK.Mathematics.Half[]? value)
//    {
//        unsafe
//        {
//            return *(System.Half[]?*)Unsafe.AsPointer(ref value);
//        }
//    }

//    [CLSCompliant(false)][MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
//    public static System.Numerics.Vector2[]? ToSystemNumerics(this OpenTK.Mathematics.Vector2[]? value)
//    {
//        unsafe
//        {
//            return *(System.Numerics.Vector2[]?*)Unsafe.AsPointer(ref value);
//        }
//    }

//    [CLSCompliant(false)][MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
//    public static System.Numerics.Vector3[]? ToSystemNumerics(this OpenTK.Mathematics.Vector3[]? value)
//    {
//        unsafe
//        {
//            return *(System.Numerics.Vector3[]?*)Unsafe.AsPointer(ref value);
//        }
//    }

//    [CLSCompliant(false)][MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
//    public static System.Numerics.Vector4[]? ToSystemNumerics(this OpenTK.Mathematics.Vector4[]? value)
//    {
//        unsafe
//        {
//            return *(System.Numerics.Vector4[]?*)Unsafe.AsPointer(ref value);
//        }
//    }


//    [CLSCompliant(false)][MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
//    public static System.Numerics.Matrix3x2[]? ToSystemNumerics(this OpenTK.Mathematics.Matrix3x2[]? value)
//    {
//        unsafe
//        {
//            return *(System.Numerics.Matrix3x2[]?*)Unsafe.AsPointer(ref value);
//        }
//    }

//    [CLSCompliant(false)][MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
//    public static System.Numerics.Matrix4x4[]? ToSystemNumerics(this OpenTK.Mathematics.Matrix4[]? value)
//    {
//        unsafe
//        {
//            return *(System.Numerics.Matrix4x4[]?*)Unsafe.AsPointer(ref value);
//        }
//    }

//    [CLSCompliant(false)][MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
//    public static System.Numerics.Quaternion[]? ToSystemNumerics(this OpenTK.Mathematics.Quaternion[]? value)
//    {
//        unsafe
//        {
//            return *(System.Numerics.Quaternion[]?*)Unsafe.AsPointer(ref value);
//        }
//    }
//}

//public static partial class MathematicsExtensions
//{
//    [CLSCompliant(false)][MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
//    public static OpenTK.Mathematics.Half[]? ToOpenTK(this System.Half[]? value)
//    {
//        unsafe
//        {
//            return *(OpenTK.Mathematics.Half[]?*)Unsafe.AsPointer(ref value);
//        }
//    }

//    [CLSCompliant(false)][MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
//    public static OpenTK.Mathematics.Vector2[]? ToOpenTK(this System.Numerics.Vector2[]? value)
//    {
//        unsafe
//        {
//            return *(OpenTK.Mathematics.Vector2[]?*)Unsafe.AsPointer(ref value);
//        }
//    }

//    [CLSCompliant(false)][MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
//    public static OpenTK.Mathematics.Vector3[]? ToOpenTK(this System.Numerics.Vector3[]? value)
//    {
//        unsafe
//        {
//            return *(OpenTK.Mathematics.Vector3[]?*)Unsafe.AsPointer(ref value);
//        }
//    }

//    [CLSCompliant(false)][MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
//    public static OpenTK.Mathematics.Vector4[]? ToOpenTK(this System.Numerics.Vector4[]? value)
//    {
//        unsafe
//        {
//            return *(OpenTK.Mathematics.Vector4[]?*)Unsafe.AsPointer(ref value);
//        }
//    }


//    [CLSCompliant(false)][MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
//    public static OpenTK.Mathematics.Matrix3x2[]? ToOpenTK(this System.Numerics.Matrix3x2[]? value)
//    {
//        unsafe
//        {
//            return *(OpenTK.Mathematics.Matrix3x2[]?*)Unsafe.AsPointer(ref value);
//        }
//    }

//    [CLSCompliant(false)][MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
//    public static OpenTK.Mathematics.Matrix4[]? ToOpenTK(this System.Numerics.Matrix4x4[]? value)
//    {
//        unsafe
//        {
//            return *(OpenTK.Mathematics.Matrix4[]?*)Unsafe.AsPointer(ref value);
//        }
//    }

//    [CLSCompliant(false)][MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
//    public static OpenTK.Mathematics.Quaternion[]? ToOpenTK(this System.Numerics.Quaternion[]? value)
//    {
//        unsafe
//        {
//            return *(OpenTK.Mathematics.Quaternion[]?*)Unsafe.AsPointer(ref value);
//        }
//    }
//}

#endregion
