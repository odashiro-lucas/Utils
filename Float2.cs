using System;
using System.Runtime.CompilerServices;
using System.Numerics;

namespace Utils
{
    public struct Float2
    {
        internal Vector2 _vector;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float2(float x, float y) { _vector = new Vector2(x, y); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal Float2(Vector2 v) { _vector = v; }

        public float x { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => _vector.X; [MethodImpl(MethodImplOptions.AggressiveInlining)] set => _vector.X = value; }
        public float y { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => _vector.Y; [MethodImpl(MethodImplOptions.AggressiveInlining)] set => _vector.Y = value; }

        public static readonly Float2 Zero = new Float2(Vector2.Zero);
        public static readonly Float2 One = new Float2(Vector2.One);

        public float DistanceTo(Float2 other) => System.Numerics.Vector2.Distance(this._vector, other._vector);
        public float DistanceSquaredTo(Float2 other) => System.Numerics.Vector2.DistanceSquared(this._vector, other._vector);
        public Float2 DirectionTo(Float2 other) => new Float2(System.Numerics.Vector2.Normalize(other._vector - this._vector));
        public Float2 Normalized => new Float2(System.Numerics.Vector2.Normalize(this._vector));
        public float Length => this._vector.Length();
        /// <summary>Faster than Length as it avoids the square root calculation.</summary>
        public float LengthSquared => this._vector.LengthSquared();

        // --- Random ---
        private static readonly Random _rand = new Random();

        /// <summary>Returns a random Float2 with x and y in range [-range, range].</summary>
        public static Float2 Rand(float range) => new Float2(
            (float)(_rand.NextDouble() * 2 - 1) * range,
            (float)(_rand.NextDouble() * 2 - 1) * range);

        /// <summary>Returns a random Float2 with x and y in range [min, max].</summary>
        public static Float2 Rand(float min, float max) => new Float2(
            (float)(_rand.NextDouble() * (max - min) + min),
            (float)(_rand.NextDouble() * (max - min) + min));

        /// <summary>Returns a random Float2 with x in range [xRange.x, xRange.y] and y in range [yRange.x, yRange.y].</summary>
        public static Float2 Rand(Float2 xRange, Float2 yRange) => new Float2(
            (float)(_rand.NextDouble() * (xRange.y - xRange.x) + xRange.x),
            (float)(_rand.NextDouble() * (yRange.y - yRange.x) + yRange.x));

        /// <summary>Returns a random Float2 with x in range [xMin, xMax] and y in range [yMin, yMax].</summary>
        public static Float2 Rand(float xMin, float xMax, float yMin, float yMax) => new Float2(
            (float)(_rand.NextDouble() * (xMax - xMin) + xMin),
            (float)(_rand.NextDouble() * (yMax - yMin) + yMin));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float2 operator +(Float2 a, Float2 b) => new Float2(a._vector + b._vector);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float2 operator -(Float2 a, Float2 b) => new Float2(a._vector - b._vector);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float2 operator -(Float2 a) => new Float2(-a._vector);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float2 operator *(Float2 a, Float2 b) => new Float2(a._vector * b._vector);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float2 operator /(Float2 a, Float2 b) => new Float2(a._vector / b._vector);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float2 operator *(Float2 a, float scalar) => new Float2(a._vector * scalar);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float2 operator *(float scalar, Float2 a) => new Float2(a._vector * scalar);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float2 operator /(Float2 a, float divisor) => new Float2(a._vector / divisor);

        // --- Conversions ---

        // Explicit: Float2 -> Int2 (Truncates)
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Int2(Float2 v) => new Int2((int)v.x, (int)v.y);

        // Implicit: Float2 -> Float3 (Z=0)
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Float3(Float2 v) => new Float3(v.x, v.y, 0f);

        // Explicit: Float2 -> Int3 (Truncates, Z=0)
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Int3(Float2 v) => new Int3((int)v.x, (int)v.y, 0);

        public override string ToString() => $"({x:F2}, {y:F2})";

        // ==========================================
        // GODOT SUPPORT
        // ==========================================
#if GODOT
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Godot.Vector2(Float2 v) => new Godot.Vector2(v.x, v.y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Float2(Godot.Vector2 v) => new Float2(v.X, v.Y);
#endif

        // ==========================================
        // UNITY SUPPORT
        // ==========================================
#if UNITY_5_3_OR_NEWER
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator UnityEngine.Vector2(Float2 v) => new UnityEngine.Vector2(v.x, v.y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Float2(UnityEngine.Vector2 v) => new Float2(v.x, v.y);
#endif
    }
}