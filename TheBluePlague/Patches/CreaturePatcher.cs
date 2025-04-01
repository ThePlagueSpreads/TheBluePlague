using HarmonyLib;
using UnityEngine;

namespace TheBluePlague.Patches;

[HarmonyPatch(typeof(Creature))]
public static class CreaturePatcher
{
    [HarmonyPatch(nameof(Creature.Start))]
    [HarmonyPostfix]
    public static void StartPostfix(Creature __instance)
    {
        Object.Destroy(__instance.gameObject);
    }
}