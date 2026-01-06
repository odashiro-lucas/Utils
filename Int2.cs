using System;
using System.Runtime.CompilerServices;

namespace Utils
{
    public record struct Int2(int X, int Y)
    {
        public static readonly Int2 Zero = new Int2(0, 0);
        public static readonly Int2 One = new Int2(1, 1);
        public static readonly Int2 Up = new Int2(0, 1);
        public static readonly Int2 Right = new Int2(1, 0);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int2 operator +(Int2 a, Int2 b) => new Int2(a.X + b.X, a.Y + b.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int2 operator -(Int2 a, Int2 b) => new Int2(a.X - b.X, a.Y - b.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int2 operator -(Int2 a) => new Int2(-a.X, -a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int2 operator *(Int2 a, Int2 b) => new Int2(a.X * b.X, a.Y * b.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int2 operator /(Int2 a, Int2 b) => new Int2(a.X / b.X, a.Y / b.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int2 operator *(Int2 a, int scalar) => new Int2(a.X * scalar, a.Y * scalar);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int2 operator /(Int2 a, int divisor) => new Int2(a.X / divisor, a.Y / divisor);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float2 operator *(Int2 a, float scalar) => new Float2(a.X * scalar, a.Y * scalar);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float2 operator *(float scalar, Int2 a) => new Float2(a.X * scalar, a.Y * scalar);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float2 operator /(Int2 a, float divisor) => new Float2(a.X / divisor, a.Y / divisor);

        // --- Conversions ---

        // Implicit: Int2 -> Float2
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Float2(Int2 v) => new Float2(v.X, v.Y);

        // Implicit: Int2 -> Int3 (Z=0)
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Int3(Int2 v) => new Int3(v.X, v.Y, 0);

        // Implicit: Int2 -> Float3 (Z=0)
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Float3(Int2 v) => new Float3(v.X, v.Y, 0f);

        public override string ToString() => $"({X}, {Y})";
        
        // ==========================================
        // GODOT SUPPORT
        // ==========================================
#if GODOT
        // Godot uses Vector2I for integers
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Godot.Vector2I(Int2 v) => new Godot.Vector2I(v.X, v.Y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Int2(Godot.Vector2I v) => new Int2(v.X, v.Y);
#endif

        // ==========================================
        // UNITY SUPPORT
        // ==========================================
#if UNITY_5_3_OR_NEWER
        // Unity uses Vector2Int
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator UnityEngine.Vector2Int(Int2 v) => new UnityEngine.Vector2Int(v.X, v.Y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Int2(UnityEngine.Vector2Int v) => new Int2(v.x, v.y);
#endif
    }
}