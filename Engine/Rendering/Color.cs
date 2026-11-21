using System.Numerics;

namespace Engine.Rendering;

public readonly record struct Color(byte R, byte G, byte B, byte A = 255)
{
    public static Color Black => new(0, 0, 0);

    public static Color White => new(255, 255, 255);

    public static Color Green => new(0, 228, 48);

    public static Color Red => new(230, 41, 55);

    public static Color Blue => new(0, 121, 241);
}
