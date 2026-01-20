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

        // --- Component-wise Math ---
        public static Int3 Clamp(Int3 value, Int3 min, Int3 max) => new Int3(Math.Clamp(value.x, min.x, max.x), Math.Clamp(value.y, min.y, max.y), Math.Clamp(value.z, min.z, max.z));
        public static Int3 Abs(Int3 value) => new Int3(Math.Abs(value.x), Math.Abs(value.y), Math.Abs(value.z));

        // --- Random ---
        private static readonly Random _rand = new Random();

        /// <summary>Returns a random Int3 with x, y, and z in range [-range, range].</summary>
        public static Int3 Rand(int range) => new Int3(
            _rand.Next(-range, range + 1),
            _rand.Next(-range, range + 1),
            _rand.Next(-range, range + 1));

        /// <summary>Returns a random Int3 with x, y, and z in range [min, max].</summary>
        public static Int3 Rand(int min, int max) => new Int3(
            _rand.Next(min, max + 1),
            _rand.Next(min, max + 1),
            _rand.Next(min, max + 1));

        /// <summary>Returns a random Int3 with x in [xRange.x, xRange.y], y in [yRange.x, yRange.y], z in [zRange.x, zRange.y].</summary>
        public static Int3 Rand(Int2 xRange, Int2 yRange, Int2 zRange) => new Int3(
            _rand.Next(xRange.x, xRange.y + 1),
            _rand.Next(yRange.x, yRange.y + 1),
            _rand.Next(zRange.x, zRange.y + 1));

        /// <summary>Returns a random Int3 with x in [xMin, xMax], y in [yMin, yMax], z in [zMin, zMax].</summary>
        public static Int3 Rand(int xMin, int xMax, int yMin, int yMax, int zMin, int zMax) => new Int3(
            _rand.Next(xMin, xMax + 1),
            _rand.Next(yMin, yMax + 1),
            _rand.Next(zMin, zMax + 1));

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