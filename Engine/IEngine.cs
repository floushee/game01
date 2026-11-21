using Engine.Audio;
using Engine.Input;
using Engine.Rendering;
using Engine.Window;

namespace Engine;

public interface IEngine
{
    IWindow Window { get; }

    IInput Input { get; }

    IRenderer Renderer { get; }

    IAudio Audio { get; }
}
