using HarmonyLib;
using TheBluePlague.Mono;

namespace TheBluePlague.Patches;

[HarmonyPatch(typeof(Player))]
public static class PlayerPatches
{
    [HarmonyPatch(nameof(Player.Start))]
    [HarmonyPostfix]
    public static void StartPostfix(Player __instance)
    {
        if (BluePlagueManager.IsBluePlagueActive)
            __instance.gameObject.EnsureComponent<MakeShitBlue>();
    }
}