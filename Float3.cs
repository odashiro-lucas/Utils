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