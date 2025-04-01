using UnityEngine;

namespace TheBluePlague.Mono;

public class MadeInHeaven : MonoBehaviour
{
    public float speed = 15;
    
    private void Update()
    {
        uSkyManager.main.Timeline += Time.deltaTime * speed;
    }
}