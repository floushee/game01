using Engine.Rendering;
using Raylib_cs;

namespace RaylibEngine.Rendering;

internal sealed record Texture(Texture2D Value) : ITexture
{
    public int Width => Value.Width;

    public int Height => Value.Height;
}
