using Engine.Audio;
using Raylib_cs;

namespace RaylibEngine.Audio;

internal sealed record SoundHandle(Sound Value) : ISound;
