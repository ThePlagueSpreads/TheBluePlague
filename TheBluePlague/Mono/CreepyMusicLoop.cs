using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TheBluePlague.Mono;

public class CreepyMusicLoop : MonoBehaviour
{
    private float _realTimePlayAgain;

    private void Update()
    {
        if (Time.realtimeSinceStartup > _realTimePlayAgain)
        {
            Play();
            _realTimePlayAgain = Time.realtimeSinceStartup + 60;
        }
    }

    private void Play()
    {
        var player = new GameObject("Nothing at all").AddComponent<AudioSource>();
        player.clip =
            Plugin.Bundle.LoadAsset<AudioClip>("ambiance" + Random.Range(1, 4));
        player.Play();
        player.gameObject.AddComponent<DestroyAfterRealSeconds>().seconds = 80;
    }
}