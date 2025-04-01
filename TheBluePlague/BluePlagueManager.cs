using UnityEngine;

namespace TheBluePlague;

public static class BluePlagueManager
{
    public static bool IsBluePlagueActive { get; set; }

    public static Texture2D BluePlagueTexture { get; } = Plugin.Bundle.LoadAsset<Texture2D>("blueplagueinfection");
}