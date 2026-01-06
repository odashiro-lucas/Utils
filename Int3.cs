using System;
using System.Runtime.CompilerServices;

namespace Utils
{
    public record struct Int3(int X, int Y, int Z)
    {
        public static readonly Int3 Zero = new Int3(0, 0, 0);
        public static readonly Int3 One = new Int3(1, 1, 1);
        public static readonly Int3 Up = new Int3(0, 1, 0);
        public static readonly Int3 Right = new Int3(1, 0, 0);
        public static readonly Int3 Forward = new Int3(0, 0, 1);

        // --- Operators (Int3 vs Int3) ---

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int3 operator +(Int3 a, Int3 b) => new Int3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int3 operator -(Int3 a, Int3 b) => new Int3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int3 operator -(Int3 a) => new Int3(-a.X, -a.Y, -a.Z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int3 operator *(Int3 a, Int3 b) => new Int3(a.X * b.X, a.Y * b.Y, a.Z * b.Z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int3 operator /(Int3 a, Int3 b) => new Int3(a.X / b.X, a.Y / b.Y, a.Z / b.Z);

        // --- Operators (Int3 vs Scalar) ---

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int3 operator *(Int3 a, int scalar) => new Int3(a.X * scalar, a.Y * scalar, a.Z * scalar);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int3 operator /(Int3 a, int divisor) => new Int3(a.X / divisor, a.Y / divisor, a.Z / divisor);

        // --- Operators (Int3 * Float Scalar) -> Float3 ---

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float3 operator *(Int3 a, float scalar) => new Float3(a.X * scalar, a.Y * scalar, a.Z * scalar);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float3 operator *(float scalar, Int3 a) => new Float3(a.X * scalar, a.Y * scalar, a.Z * scalar);

        // --- Conversions ---

        // Implicit: Int3 -> Float3
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Float3(Int3 v) => new Float3(v.X, v.Y, v.Z);

        // Explicit: Int3 -> Int2 (Drops Z)
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Int2(Int3 v) => new Int2(v.X, v.Y);

        // Implicit: Int2 -> Int3 (Z = 0)
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Int3(Int2 v) => new Int3(v.X, v.Y, 0);

        public override string ToString() => $"({X}, {Y}, {Z})";

        // ==========================================
        // GODOT SUPPORT
        // ==========================================
#if GODOT
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Godot.Vector3I(Int3 v) => new Godot.Vector3I(v.X, v.Y, v.Z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Int3(Godot.Vector3I v) => new Int3(v.X, v.Y, v.Z);
#endif

        // ==========================================
        // UNITY SUPPORT
        // ==========================================
#if UNITY_5_3_OR_NEWER
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator UnityEngine.Vector3Int(Int3 v) => new UnityEngine.Vector3Int(v.X, v.Y, v.Z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Int3(UnityEngine.Vector3Int v) => new Int3(v.x, v.y, v.z);
#endif
    }
}