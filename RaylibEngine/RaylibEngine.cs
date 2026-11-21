using Engine.Rendering;
using System.Numerics;
using Engine;
using Engine.Audio;
using Engine.Input;
using Raylib_cs;
using Color = Engine.Rendering.Color;

namespace RaylibEngine;

public sealed class RaylibEngine : IEngine
{
    private static Raylib_cs.Color Convert(Color c) => new(c.R, c.G, c.B, c.A);

    public void InitWindow(int width, int height, string title) => Raylib.InitWindow(width, height, title);

    public void CloseWindow() => Raylib.CloseWindow();

    public void SetTargetFPS(int fps) => Raylib.SetTargetFPS(fps);

    public void HideCursor() => Raylib.HideCursor();

    public bool WindowShouldClose() => Raylib.WindowShouldClose();

    public float GetFrameTime() => Raylib.GetFrameTime();

    public bool IsKeyDown(Key key) => Raylib.IsKeyDown(Enum.Parse<KeyboardKey>(key.ToString()));

    public bool IsKeyPressed(Key key) => Raylib.IsKeyPressed(Enum.Parse<KeyboardKey>(key.ToString()));

    public bool IsMouseButtonPressed(Engine.Input.MouseButton button) => Raylib.IsMouseButtonPressed(Enum.Parse<Raylib_cs.MouseButton>(button.ToString()));

    public Vector2 GetMousePosition() => Raylib.GetMousePosition();

    public ITexture LoadTexture(string path) => new Texture(Raylib.LoadTexture(path));

    public void UnloadTexture(ITexture texture) => Raylib.UnloadTexture(((Texture)texture).Value);

    public void BeginDrawing() => Raylib.BeginDrawing();

    public void EndDrawing() => Raylib.EndDrawing();

    public void ClearBackground(Color color) => Raylib.ClearBackground(Convert(color));

    public void DrawRectangle(int x, int y, int width, int height, Color color) => Raylib.DrawRectangle(x, y, width, height, Convert(color));

    public void DrawCircleV(Vector2 position, float radius, Color color) => Raylib.DrawCircleV(position, radius, Convert(color));

    public void DrawText(string text, int x, int y, int size, Color color) => Raylib.DrawText(text, x, y, size, Convert(color));

    public void DrawTexture(ITexture texture, int x, int y, Color color) => Raylib.DrawTexture(((Texture)texture).Value, x, y, Convert(color));

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
