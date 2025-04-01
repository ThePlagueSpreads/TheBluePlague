using System;
using UnityEngine;

namespace TheBluePlague.Mono;

public class MakeShitBlue : MonoBehaviour
{
    private float _timeUpdateAgain;
    
    private void Update()
    {
        if (Time.time < _timeUpdateAgain)
        {
            return;
        }
        
        _timeUpdateAgain = Time.time + 0.3f;
        Blueify();
    }

    private void Blueify()
    {
        MakeBlue(Player.main.gameObject);
        if (CrashedShipExploder.main != null)
        {
            MakeBlue(CrashedShipExploder.main.transform.root.gameObject);
        }

        var identifiers = UniqueIdentifier.identifiers;
        foreach (var identifier in identifiers.Values)
        {
            if (identifier == null)
                continue;
            MakeBlue(identifier.gameObject);
        }
        
        BlueifyTerrain();
    }

    private static void BlueifyTerrain()
    {
        var levels = LargeWorld.main.streamer.streamerV2.clipmapStreamer.levels;

        foreach (var level in levels)
        {
            foreach (var cell in level.cells)
            {
                if (cell.chunk == null) continue;

                IVoxelandChunk2 voxelandChunk = cell.chunk;
                
                foreach (var layer in voxelandChunk.hiRenders)
                {
                    if (layer == null) continue;
                    MakeBlue(layer.gameObject);
                }
            }
        }
    }

    private static void MakeBlue(GameObject obj)
    {
        try
        {
            var renderers = obj.GetComponentsInChildren<Renderer>();
            foreach (var renderer in renderers)
            {
                var materials = renderer.materials;
                foreach (var material in materials)
                {
                    if (!material)
                        continue;
                    material.color = new Color(0.1f, 0.1f, 2f);
                    material.SetColor(ShaderPropertyID._SpecColor, new Color(0.1f, 0.1f, 1f));
                    material.SetColor(ShaderPropertyID._GlowColor, new Color(0.1f, 0.1f, 2f));
                }

                renderer.materials = materials;
            }
        }
        catch (Exception e)
        {
            Plugin.Logger.LogError("Failed to make " + obj + " blue!!! " + e);
        }
    }
}