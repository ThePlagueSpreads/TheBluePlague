using UnityEngine;

namespace TheBluePlague.Mono;

public class Silence : MonoBehaviour
{
    private void Update()
    {
        SoundSystem.SetMasterVolume(0);
        Subtitles.main.gameObject.SetActive(false);
    }
}