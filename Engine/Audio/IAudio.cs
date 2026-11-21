namespace Engine.Audio;

public interface IAudio
{
    void InitAudioDevice();
    
    void CloseAudioDevice();
    
    IMusic LoadMusicStream(string path);
    
    ISound LoadSound(string path);
    
    void UnloadMusicStream(IMusic music);
    
    void UnloadSound(ISound sound);

    void PlayMusicStream(IMusic music);
    
    void UpdateMusicStream(IMusic music);
    
    void PlaySound(ISound sound);
}
