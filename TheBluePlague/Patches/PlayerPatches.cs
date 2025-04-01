using System.Collections;
using HarmonyLib;
using TheBluePlague.Mono;
using UnityEngine;

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

        if (NothingManager.NothingActive)
        {
            new GameObject("Silence").AddComponent<Silence>();
            new GameObject("CreepyMusic").AddComponent<CreepyMusicLoop>();
            new GameObject("Grayer").AddComponent<IncreaseGrayscale>();

            UWE.CoroutineHost.StartCoroutine(SpawnLifepods());
        }
    }

    private static IEnumerator SpawnLifepods()
    {
        yield return new WaitUntil(() => EscapePod.main != null);
        var lifepod = EscapePod.main;
        for (int i = 0; i < 20; i++)
        {
            Object.Instantiate(lifepod.gameObject);
        }
    }
}