using Engine.Audio;
using Raylib_cs;

namespace RaylibEngine.Audio;

internal sealed class RaylibAudio : IAudio
{
    public void InitAudioDevice() => Raylib.InitAudioDevice();

    public void CloseAudioDevice() => Raylib.CloseAudioDevice();

    public IMusic LoadMusicStream(string path) => new MusicHandle(Raylib.LoadMusicStream(path));

    public ISound LoadSound(string path) => new SoundHandle(Raylib.LoadSound(path));

    public void PlayMusicStream(IMusic music) => Raylib.PlayMusicStream(((MusicHandle)music).Value);

    public void UpdateMusicStream(IMusic music) => Raylib.UpdateMusicStream(((MusicHandle)music).Value);

    public void PlaySound(ISound sound) => Raylib.PlaySound(((SoundHandle)sound).Value);

    public void UnloadMusicStream(IMusic music) => Raylib.UnloadMusicStream(((MusicHandle)music).Value);

    public void UnloadSound(ISound sound) => Raylib.UnloadSound(((SoundHandle)sound).Value);
}
