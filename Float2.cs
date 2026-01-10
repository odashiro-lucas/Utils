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

        public float X { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => _vector.X; [MethodImpl(MethodImplOptions.AggressiveInlining)] set => _vector.X = value; }
        public float Y { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => _vector.Y; [MethodImpl(MethodImplOptions.AggressiveInlining)] set => _vector.Y = value; }

        public static readonly Float2 Zero = new Float2(Vector2.Zero);
        public static readonly Float2 One = new Float2(Vector2.One);

        public float DistanceTo(Float2 other) => System.Numerics.Vector2.Distance(this._vector, other._vector);
        public float DistanceSquaredTo(Float2 other) => System.Numerics.Vector2.DistanceSquared(this._vector, other._vector);
        public Float2 DirectionTo(Float2 other) => new Float2(System.Numerics.Vector2.Normalize(other._vector - this._vector));

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
        public static explicit operator Int2(Float2 v) => new Int2((int)v.X, (int)v.Y);

        // Implicit: Float2 -> Float3 (Z=0)
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Float3(Float2 v) => new Float3(v.X, v.Y, 0f);

        // Explicit: Float2 -> Int3 (Truncates, Z=0)
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Int3(Float2 v) => new Int3((int)v.X, (int)v.Y, 0);

        public override string ToString() => $"({X:F2}, {Y:F2})";

        // ==========================================
        // GODOT SUPPORT
        // ==========================================
#if GODOT
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Godot.Vector2(Float2 v) => new Godot.Vector2(v.X, v.Y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Float2(Godot.Vector2 v) => new Float2(v.X, v.Y);
#endif

        // ==========================================
        // UNITY SUPPORT
        // ==========================================
#if UNITY_5_3_OR_NEWER
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator UnityEngine.Vector2(Float2 v) => new UnityEngine.Vector2(v.X, v.Y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Float2(UnityEngine.Vector2 v) => new Float2(v.x, v.y);
#endif
    }
}