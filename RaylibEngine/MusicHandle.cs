using Engine;
using Engine.Audio;
using Raylib_cs;

namespace RaylibEngine;

internal sealed record MusicHandle(Music Value) : IMusic;
