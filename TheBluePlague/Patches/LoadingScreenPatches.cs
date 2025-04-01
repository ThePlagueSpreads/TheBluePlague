using UnityEngine.UI;
using HarmonyLib;
using UnityEngine;

namespace TheBluePlague.Patches;

[HarmonyPatch(typeof(uGUI_SceneLoading))]
public static class LoadingScreenPatches
{
    [HarmonyPatch(nameof(uGUI_SceneLoading.SetProgress))]
    [HarmonyPostfix]
    public static void NothingPatch(uGUI_SceneLoading __instance)
    {
        if (!NothingManager.NothingActive)
            return;
        foreach (var image in __instance.GetComponentsInChildren<Image>())
        {
            image.color = Color.black;
        }
    }
}