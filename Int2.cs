using System;
using System.Runtime.CompilerServices;

namespace Utils
{
    public record struct Int2(int x, int y)
    {
        public static readonly Int2 Zero = new Int2(0, 0);
        public static readonly Int2 One = new Int2(1, 1);
        public static readonly Int2 Up = new Int2(0, 1);
        public static readonly Int2 Right = new Int2(1, 0);

        public int DistanceTo(Int2 other) => (int)MathF.Sqrt(DistanceSquaredTo(other));
        public int DistanceSquaredTo(Int2 other) => (x - other.x) * (x - other.x) + (y - other.y) * (y - other.y);
        public Int2 DirectionTo(Int2 other) => (Int2)((Float2)other - (Float2)this).Normalized;
        public Int2 Normalized => (Int2)((Float2)this).Normalized;
        public int Length => (int)MathF.Sqrt(LengthSquared);
        /// <summary>Faster than Length as it avoids the square root calculation.</summary>
        public int LengthSquared => x * x + y * y;

        // --- Random ---
        private static readonly Random _rand = new Random();

        /// <summary>Returns a random Int2 with x and y in range [-range, range].</summary>
        public static Int2 Rand(int range) => new Int2(
            _rand.Next(-range, range + 1),
            _rand.Next(-range, range + 1));

        /// <summary>Returns a random Int2 with x and y in range [min, max].</summary>
        public static Int2 Rand(int min, int max) => new Int2(
            _rand.Next(min, max + 1),
            _rand.Next(min, max + 1));

        /// <summary>Returns a random Int2 with x in range [xRange.x, xRange.y] and y in range [yRange.x, yRange.y].</summary>
        public static Int2 Rand(Int2 xRange, Int2 yRange) => new Int2(
            _rand.Next(xRange.x, xRange.y + 1),
            _rand.Next(yRange.x, yRange.y + 1));

        /// <summary>Returns a random Int2 with x in range [xMin, xMax] and y in range [yMin, yMax].</summary>
        public static Int2 Rand(int xMin, int xMax, int yMin, int yMax) => new Int2(
            _rand.Next(xMin, xMax + 1),
            _rand.Next(yMin, yMax + 1));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int2 operator +(Int2 a, Int2 b) => new Int2(a.x + b.x, a.y + b.y);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int2 operator -(Int2 a, Int2 b) => new Int2(a.x - b.x, a.y - b.y);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int2 operator -(Int2 a) => new Int2(-a.x, -a.y);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int2 operator *(Int2 a, Int2 b) => new Int2(a.x * b.x, a.y * b.y);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int2 operator /(Int2 a, Int2 b) => new Int2(a.x / b.x, a.y / b.y);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int2 operator *(Int2 a, int scalar) => new Int2(a.x * scalar, a.y * scalar);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int2 operator /(Int2 a, int divisor) => new Int2(a.x / divisor, a.y / divisor);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float2 operator *(Int2 a, float scalar) => new Float2(a.x * scalar, a.y * scalar);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float2 operator *(float scalar, Int2 a) => new Float2(a.x * scalar, a.y * scalar);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float2 operator /(Int2 a, float divisor) => new Float2(a.x / divisor, a.y / divisor);

        // --- Conversions ---

        // Implicit: Int2 -> Float2
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Float2(Int2 v) => new Float2(v.x, v.y);

        // Implicit: Int2 -> Int3 (Z=0)
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Int3(Int2 v) => new Int3(v.x, v.y, 0);

        // Implicit: Int2 -> Float3 (Z=0)
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Float3(Int2 v) => new Float3(v.x, v.y, 0f);

        public override string ToString() => $"({x}, {y})";
        
        // ==========================================
        // GODOT SUPPORT
        // ==========================================
#if GODOT
        // Godot uses Vector2I for integers
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Godot.Vector2I(Int2 v) => new Godot.Vector2I(v.x, v.y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Int2(Godot.Vector2I v) => new Int2(v.X, v.Y);
#endif

        // ==========================================
        // UNITY SUPPORT
        // ==========================================
#if UNITY_5_3_OR_NEWER
        // Unity uses Vector2Int
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator UnityEngine.Vector2Int(Int2 v) => new UnityEngine.Vector2Int(v.x, v.y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Int2(UnityEngine.Vector2Int v) => new Int2(v.x, v.y);
#endif
    }
}