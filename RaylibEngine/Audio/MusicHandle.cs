using Engine.Audio;
using Raylib_cs;

namespace RaylibEngine.Audio;

internal sealed record MusicHandle(Music Value) : IMusic;
