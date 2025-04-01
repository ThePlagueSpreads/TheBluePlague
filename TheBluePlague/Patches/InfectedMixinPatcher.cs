using System.Collections;
using HarmonyLib;

namespace TheBluePlague.Patches;

[HarmonyPatch(typeof(InfectedMixin))]
public static class InfectedMixinPatcher
{
    [HarmonyPatch(nameof(InfectedMixin.UpdateInfectionShading))]
    [HarmonyPostfix]
    public static void UpdateInfectionShadingPostfix(InfectedMixin __instance)
    {
        if (!BluePlagueManager.IsBluePlagueActive) return;

        __instance.StartCoroutine(LateColorApply(__instance));
    }

    private static IEnumerator LateColorApply(InfectedMixin instance)
    {
        yield return null;
        if (instance == null) yield break;
        var renderers = instance.renderers;
        if (renderers == null) yield break;
        foreach (var renderer in renderers)
        {
            var materials = renderer.materials;
            for (var i = 0; i < renderer.materials.Length; i++)
            {
                var material = materials[i];
                material.SetTexture(ShaderPropertyID._InfectionAlbedomap, BluePlagueManager.BluePlagueTexture);
                materials[i] = material;
            }

            renderer.materials = materials;
        }
    }
}