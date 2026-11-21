using Engine;
using Engine.Audio;
using Engine.Input;
using Engine.Rendering;
using Engine.Window;

using RaylibEngine.Audio;
using RaylibEngine.Input;
using RaylibEngine.Rendering;
using RaylibEngine.Window;

namespace RaylibEngine;

public sealed class RaylibEngine : IEngine
{
    public IWindow Window { get; } = new RaylibWindow();

    public IInput Input { get; } = new RaylibInput();

    public IRenderer Renderer { get; } = new RaylibRenderer();

    public IAudio Audio { get; } = new RaylibAudio();
}
