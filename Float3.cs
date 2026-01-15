using System;
using System.Runtime.CompilerServices;
using System.Numerics;

namespace Utils
{
    public struct Float3
    {
        internal Vector3 _vector;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float3(float x, float y, float z)
        {
            _vector = new Vector3(x, y, z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal Float3(Vector3 v)
        {
            _vector = v;
        }

        public float x
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _vector.X;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => _vector.X = value;
        }

        public float y
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _vector.Y;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => _vector.Y = value;
        }

        public float z
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _vector.Z;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => _vector.Z = value;
        }

        public static readonly Float3 Zero = new Float3(Vector3.Zero);
        public static readonly Float3 One = new Float3(Vector3.One);

        public float DistanceTo(Float3 other) => System.Numerics.Vector3.Distance(this._vector, other._vector);
        public float DistanceSquaredTo(Float3 other) => System.Numerics.Vector3.DistanceSquared(this._vector, other._vector);
        public Float3 DirectionTo(Float3 other) => new Float3(System.Numerics.Vector3.Normalize(other._vector - this._vector));
        public Float3 Normalized => new Float3(System.Numerics.Vector3.Normalize(this._vector));
        public float Length => this._vector.Length();
        /// <summary>Faster than Length as it avoids the square root calculation.</summary>
        public float LengthSquared => this._vector.LengthSquared();

        // --- Random ---
        private static readonly Random _rand = new Random();

        /// <summary>Returns a random Float3 with x, y, and z in range [-range, range].</summary>
        public static Float3 Rand(float range) => new Float3(
            (_rand.NextSingle() * 2 - 1) * range,
            (_rand.NextSingle() * 2 - 1) * range,
            (_rand.NextSingle() * 2 - 1) * range);

        /// <summary>Returns a random Float3 with x, y, and z in range [min, max].</summary>
        public static Float3 Rand(float min, float max) => new Float3(
            _rand.NextSingle() * (max - min) + min,
            _rand.NextSingle() * (max - min) + min,
            _rand.NextSingle() * (max - min) + min);

        /// <summary>Returns a random Float3 with x in [xRange.x, xRange.y], y in [yRange.x, yRange.y], z in [zRange.x, zRange.y].</summary>
        public static Float3 Rand(Float2 xRange, Float2 yRange, Float2 zRange) => new Float3(
            _rand.NextSingle() * (xRange.y - xRange.x) + xRange.x,
            _rand.NextSingle() * (yRange.y - yRange.x) + yRange.x,
            _rand.NextSingle() * (zRange.y - zRange.x) + zRange.x);

        /// <summary>Returns a random Float3 with x in [xMin, xMax], y in [yMin, yMax], z in [zMin, zMax].</summary>
        public static Float3 Rand(float xMin, float xMax, float yMin, float yMax, float zMin, float zMax) => new Float3(
            _rand.NextSingle() * (xMax - xMin) + xMin,
            _rand.NextSingle() * (yMax - yMin) + yMin,
            _rand.NextSingle() * (zMax - zMin) + zMin);

        /// <summary> Returns a random Float3 with vector length shorter or equal to [maxLength]. </summary>
        public static Float3 RandMaxLength(float maxLength)
        {
            float u = _rand.NextSingle();
            float v = _rand.NextSingle();
            float theta = 2.0f * MathF.PI * u;
            float phi = MathF.Acos(2.0f * v - 1.0f);
            
            float x = MathF.Sin(phi) * MathF.Cos(theta);
            float y = MathF.Sin(phi) * MathF.Sin(theta);
            float z = MathF.Cos(phi);

            float distance = MathF.Pow(_rand.NextSingle(), 1.0f / 3.0f) * maxLength; // cbrt for uniform distribution
            
            return new Float3(
                x * distance,
                y * distance,
                z * distance);
        }

        // --- SIMD Operators ---

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float3 operator +(Float3 a, Float3 b) => new Float3(a._vector + b._vector);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float3 operator -(Float3 a, Float3 b) => new Float3(a._vector - b._vector);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float3 operator -(Float3 a) => new Float3(-a._vector);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float3 operator *(Float3 a, Float3 b) => new Float3(a._vector * b._vector);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float3 operator /(Float3 a, Float3 b) => new Float3(a._vector / b._vector);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float3 operator *(Float3 a, float scalar) => new Float3(a._vector * scalar);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float3 operator *(float scalar, Float3 a) => new Float3(a._vector * scalar);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float3 operator /(Float3 a, float divisor) => new Float3(a._vector / divisor);

        // --- Conversions ---

        // Explicit: Float3 -> Int3 (Truncates)
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Int3(Float3 v) => new Int3((int)v.x, (int)v.y, (int)v.z);

        // Explicit: Float3 -> Float2 (Drops Z)
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Float2(Float3 v) => new Float2(v.x, v.y);

        // Implicit: Float2 -> Float3 (Z = 0)
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Float3(Float2 v) => new Float3(v.x, v.y, 0f);

        public override string ToString() => $"({x:F2}, {y:F2}, {z:F2})";

        // ==========================================
        // GODOT SUPPORT
        // ==========================================
#if GODOT
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Godot.Vector3(Float3 v) => new Godot.Vector3(v.x, v.y, v.z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Float3(Godot.Vector3 v) => new Float3(v.X, v.Y, v.Z);
#endif

        // ==========================================
        // UNITY SUPPORT
        // ==========================================
#if UNITY_5_3_OR_NEWER
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator UnityEngine.Vector3(Float3 v) => new UnityEngine.Vector3(v.x, v.y, v.z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Float3(UnityEngine.Vector3 v) => new Float3(v.x, v.y, v.z);
#endif
    }
}