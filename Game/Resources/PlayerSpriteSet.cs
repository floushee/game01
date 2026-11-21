using Engine.Rendering;

namespace Game.Resources;

internal sealed record PlayerSpriteSet(ITexture Idle, ITexture WalkA, ITexture WalkB, ITexture Rise, ITexture Fall);
