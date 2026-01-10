using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Utils
{
    public static class Interp
    {
        /// <summary>Linear interpolation between a and b.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Lerp(float a, float b, float t) => a + (b - a) * t;

        /// <summary>Linear interpolation between a and b.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float2 Lerp(Float2 a, Float2 b, float t) => a + (b - a) * t;

        /// <summary>Linear interpolation between a and b.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float3 Lerp(Float3 a, Float3 b, float t) => a + (b - a) * t;

        /// <summary>Smooth interpolation between a and b following a cubic Bezier curve with smooth factor k.
        /// <br/> k = 0.5: Smooth S-curve. (starts slow, accelerates in the middle, slows at the end)
        /// <br/> k = 0: Linear interpolation. (same as regular lerp)
        /// <br/> k = -1: Sharp curve. (slows to a stop midway, then accelerates towards b)
        /// <br/> Values outside [-1, 0.5] result in overshooting motion. </summary>
        /// <returns>An interpolated value between a and b following curve k at t.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Smerp(float a, float b, float k, float t) => a + (b - a) * t * (1 - 2 * k * (1 - 3 * t + 2 * t * t));

        /// <summary>Smooth interpolation between a and b following a cubic Bezier curve with smooth factor k.
        /// <br/> k = 0.5: Smooth S-curve. (starts slow, accelerates in the middle, slows at the end)
        /// <br/> k = 0: Linear interpolation. (same as regular lerp)
        /// <br/> k = -1: Sharp curve. (slows to a stop midway, then accelerates towards b)
        /// <br/> Values outside [-1, 0.5] result in overshooting motion. </summary>
        /// <returns>An interpolated Float2 between a and b following curve k at t.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float2 Smerp(Float2 a, Float2 b, float k, float t) => a + (b - a) * (t * (1 - 2 * k * (1 - 3 * t + 2 * t * t)));

        /// <summary> Spline interpolation between unlimited points. Float2 and Float3 signatures also available.
        /// <br/>Flexible due to unlimited point calculation, but slow due to heap allocation. Stack versions are faster.</summary>
        /// <returns>The interpolated value at t.</returns>
        /// <param name="points">The points to interpolate between.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Serp(ReadOnlySpan<float> points, float t)
        {
            if (points.Length == 2)
                return points[0] + (points[1] - points[0]) * t; // lerp
            
            Span<float> buffer = stackalloc float[points.Length - 1];

            for (int i = 0; i < points.Length - 1; i++)
                buffer[i] = points[i] + (points[i + 1] - points[i]) * t; // lerp
            
            for (int level = points.Length - 1; level > 1; level--)
                for (int i = 0; i < level - 1; i++)
                    buffer[i] = buffer[i] + (buffer[i + 1] - buffer[i]) * t; // lerp
            
            return buffer[0];
        }

        /// <summary> Spline interpolation between unlimited Float2 points. Float and Float3 signatures also available.
        /// <br/>Flexible due to unlimited point calculation, but slow due to heap allocation. Stack versions are faster.</summary>
        /// <returns>The interpolated value at t.</returns>
        /// <param name="points">The points to interpolate between.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float2 Serp(ReadOnlySpan<Float2> points, float t)
        {
            if (points.Length == 2)
                return points[0] + (points[1] - points[0]) * t;
            
            Span<Float2> buffer = stackalloc Float2[points.Length - 1];

            for (int i = 0; i < points.Length - 1; i++)
                buffer[i] = points[i] + (points[i + 1] - points[i]) * t;
            
            for (int level = points.Length - 1; level > 1; level--)
                for (int i = 0; i < level - 1; i++)
                    buffer[i] = buffer[i] + (buffer[i + 1] - buffer[i]) * t;
            
            return buffer[0];
        }

        /// <summary> Spline interpolation between unlimited Float3 points. Float and Float2 signatures also available.
        /// <br/>Flexible due to unlimited point calculation, but slow due to heap allocation. Stack versions are faster.</summary>
        /// <returns>The interpolated value at t.</returns>
        /// <param name="points">The points to interpolate between.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float3 Serp(ReadOnlySpan<Float3> points, float t)
        {
            if (points.Length == 2)
                return points[0] + (points[1] - points[0]) * t;
            
            Span<Float3> buffer = stackalloc Float3[points.Length - 1];

            for (int i = 0; i < points.Length - 1; i++)
                buffer[i] = points[i] + (points[i + 1] - points[i]) * t;
            
            for (int level = points.Length - 1; level > 1; level--)
                for (int i = 0; i < level - 1; i++)
                    buffer[i] = buffer[i] + (buffer[i + 1] - buffer[i]) * t;
            
            return buffer[0];
        }

        /// <summary> Efficient 3-point spline interpolation (quadratic Bezier in 2nd degree Bernstein polynomial form).
        /// <br/> Fast due to no heap allocation.</summary>
        /// <returns>The interpolated float at t.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Serp(float a, float b, float c, float t) => a + (2 * (b - a) + (a - 2 * b + c) * t) * t;

        /// <summary> Efficient 3-point Float2 spline interpolation (quadratic Bezier in 2nd degree Bernstein polynomial form).
        /// <br/> Fast due to no heap allocation.</summary>
        /// <returns>The interpolated Float2 at t.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float2 Serp(Float2 a, Float2 b, Float2 c, float t) => new Float2(a.x + (2 * (b.x - a.x) + (a.x - 2 * b.x + c.x) * t) * t, a.y + (2 * (b.y - a.y) + (a.y - 2 * b.y + c.y) * t) * t);

        /// <summary> Efficient 3-point Float3 spline interpolation (quadratic Bezier in 2nd degree Bernstein polynomial form).
        /// <br/> Fast due to no heap allocation.</summary>
        /// <returns>The interpolated Float3 at t.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float3 Serp(Float3 a, Float3 b, Float3 c, float t) => new Float3(a.x + (2 * (b.x - a.x) + (a.x - 2 * b.x + c.x) * t) * t, a.y + (2 * (b.y - a.y) + (a.y - 2 * b.y + c.y) * t) * t, a.z + (2 * (b.z - a.z) + (a.z - 2 * b.z + c.z) * t) * t);

        /// <summary> Efficient 4-point spline interpolation (cubic Bezier in 3rd degree Bernstein polynomial form).
        /// <br/> Fast due to no heap allocation.</summary>
        /// <returns>The interpolated float at t.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Serp(float a, float b, float c, float d, float t) => a + (3 * (b - a) + (3 * (a - 2 * b + c) + (-a + 3 * b - 3 * c + d) * t) * t) * t;

        /// <summary> Efficient 4-point Float2 spline interpolation (cubic Bezier in 3rd degree Bernstein polynomial form).
        /// <br/> Fast due to no heap allocation.</summary>
        /// <returns>The interpolated Float2 at t.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float2 Serp(Float2 a, Float2 b, Float2 c, Float2 d, float t) => new Float2(a.x + (3 * (b.x - a.x) + (3 * (a.x - 2 * b.x + c.x) + (-a.x + 3 * b.x - 3 * c.x + d.x) * t) * t) * t, a.y + (3 * (b.y - a.y) + (3 * (a.y - 2 * b.y + c.y) + (-a.y + 3 * b.y - 3 * c.y + d.y) * t) * t) * t);

        /// <summary> Efficient 4-point Float3 spline interpolation (cubic Bezier in 3rd degree Bernstein polynomial form).
        /// <br/> Fast due to no heap allocation.</summary>
        /// <returns>The interpolated Float3 at t.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float3 Serp(Float3 a, Float3 b, Float3 c, Float3 d, float t) => new Float3(a.x + (3 * (b.x - a.x) + (3 * (a.x - 2 * b.x + c.x) + (-a.x + 3 * b.x - 3 * c.x + d.x) * t) * t) * t, a.y + (3 * (b.y - a.y) + (3 * (a.y - 2 * b.y + c.y) + (-a.y + 3 * b.y - 3 * c.y + d.y) * t) * t) * t, a.z + (3 * (b.z - a.z) + (3 * (a.z - 2 * b.z + c.z) + (-a.z + 3 * b.z - 3 * c.z + d.z) * t) * t) * t);

        /// <summary>Fast search for a target value in a 2D matrix using MemoryMarshal.</summary>
        /// <returns>The (row, col) position of the target, or (-1, -1) if not found.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int2 FindFast(int[,] matrix, int target)
        {
            if (matrix == null || matrix.Length == 0)
                return new Int2(-1, -1);

            var span = MemoryMarshal.CreateSpan(ref matrix[0, 0], matrix.Length);
            int flatIndex = span.IndexOf(target);

            if (flatIndex == -1)
                return new Int2(-1, -1);

            int width = matrix.GetLength(1);
            int row = flatIndex / width;
            int col = flatIndex % width;

            return new Int2(row, col);
        }
    }
}
