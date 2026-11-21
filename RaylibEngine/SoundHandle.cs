using Engine;
using Engine.Audio;
using Raylib_cs;

namespace RaylibEngine;

internal sealed record SoundHandle(Sound Value) : ISound;
