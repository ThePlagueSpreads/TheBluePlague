using System.Collections;
using UnityEngine;

namespace TheBluePlague.Mono;

public class DestroyAfterRealSeconds : MonoBehaviour
{
    public float seconds;
    
    private IEnumerator Start()
    {
        yield return new WaitForSecondsRealtime(seconds);
        Destroy(gameObject);
    }
}