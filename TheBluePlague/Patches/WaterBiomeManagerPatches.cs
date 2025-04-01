using HarmonyLib;
using UnityEngine;

namespace TheBluePlague.Patches;

[HarmonyPatch(typeof(WaterBiomeManager))]
public static class WaterBiomeManagerPatches
{
    [HarmonyPatch(nameof(WaterBiomeManager.Start))]
    [HarmonyPostfix]
    public static void StartPostfix(WaterBiomeManager __instance)
    {
        if (!NothingManager.NothingActive)
            return;
        var newSettings = new WaterscapeVolume.Settings()
        {
            absorption = new Vector3(8, 5, 3),
            scattering = 0.075f,
            scatteringColor = new Color(0.2f, 0.3f, 0.5f),
            murkiness = 0.8f,
            startDistance = 15f,
            sunlightScale = 0.3f,
            ambientScale = 0.5f,
            emissiveScale = 0f,
            temperature = 15f
        };
        foreach (var biome in __instance.biomeSettings)
        {
            biome.settings = newSettings;
        }
    }
}