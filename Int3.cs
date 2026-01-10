using System;
using System.Runtime.CompilerServices;

namespace Utils
{
    public record struct Int3(int x, int y, int z)
    {
        public static readonly Int3 Zero = new Int3(0, 0, 0);
        public static readonly Int3 One = new Int3(1, 1, 1);
        public static readonly Int3 Up = new Int3(0, 1, 0);
        public static readonly Int3 Right = new Int3(1, 0, 0);
        public static readonly Int3 Forward = new Int3(0, 0, 1);

        public int DistanceTo(Int3 other) => (int)MathF.Sqrt(DistanceSquaredTo(other));
        public int DistanceSquaredTo(Int3 other) => (x - other.x) * (x - other.x) + (y - other.y) * (y - other.y) + (z - other.z) * (z - other.z);
        public Int3 DirectionTo(Int3 other) => (Int3)((Float3)other - (Float3)this).Normalized;
        public Int3 Normalized => (Int3)((Float3)this).Normalized;
        public int Length => (int)MathF.Sqrt(LengthSquared);
        /// <summary>Faster than Length as it avoids the square root calculation.</summary>
        public int LengthSquared => x * x + y * y + z * z;

        // --- Operators (Int3 vs Int3) ---

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int3 operator +(Int3 a, Int3 b) => new Int3(a.x + b.x, a.y + b.y, a.z + b.z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int3 operator -(Int3 a, Int3 b) => new Int3(a.x - b.x, a.y - b.y, a.z - b.z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int3 operator -(Int3 a) => new Int3(-a.x, -a.y, -a.z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int3 operator *(Int3 a, Int3 b) => new Int3(a.x * b.x, a.y * b.y, a.z * b.z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int3 operator /(Int3 a, Int3 b) => new Int3(a.x / b.x, a.y / b.y, a.z / b.z);

        // --- Operators (Int3 vs Scalar) ---

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int3 operator *(Int3 a, int scalar) => new Int3(a.x * scalar, a.y * scalar, a.z * scalar);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int3 operator /(Int3 a, int divisor) => new Int3(a.x / divisor, a.y / divisor, a.z / divisor);

        // --- Operators (Int3 * Float Scalar) -> Float3 ---

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float3 operator *(Int3 a, float scalar) => new Float3(a.x * scalar, a.y * scalar, a.z * scalar);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float3 operator *(float scalar, Int3 a) => new Float3(a.x * scalar, a.y * scalar, a.z * scalar);

        // --- Conversions ---

        // Implicit: Int3 -> Float3
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Float3(Int3 v) => new Float3(v.x, v.y, v.z);

        // Explicit: Int3 -> Int2 (Drops Z)
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Int2(Int3 v) => new Int2(v.x, v.y);

        // Implicit: Int2 -> Int3 (Z = 0)
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Int3(Int2 v) => new Int3(v.x, v.y, 0);

        public override string ToString() => $"({x}, {y}, {z})";

        // ==========================================
        // GODOT SUPPORT
        // ==========================================
#if GODOT
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Godot.Vector3I(Int3 v) => new Godot.Vector3I(v.x, v.y, v.z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Int3(Godot.Vector3I v) => new Int3(v.X, v.Y, v.Z);
#endif

        // ==========================================
        // UNITY SUPPORT
        // ==========================================
#if UNITY_5_3_OR_NEWER
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator UnityEngine.Vector3Int(Int3 v) => new UnityEngine.Vector3Int(v.x, v.y, v.z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Int3(UnityEngine.Vector3Int v) => new Int3(v.x, v.y, v.z);
#endif
    }
}