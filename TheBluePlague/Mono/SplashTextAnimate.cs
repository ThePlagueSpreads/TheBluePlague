using TMPro;
using UnityEngine;

namespace TheBluePlague.Mono;

public class SplashTextAnimate : MonoBehaviour
{
    public float variation = 0.1f;
    public float cycleSpeed = 0.15f;

    private float _startingScale;

    public static SplashTextAnimate Main;

    private void Start()
    {
        Main = this;
        _startingScale = transform.localScale.x;
    }

    public void RandomizeText()
    {
        GetComponent<TextMeshProUGUI>().text = SplashTextManager.GetRandomSplashText();
    }

    private void Update()
    {
        transform.localScale = Vector3.one * (_startingScale + Mathf.PingPong(Time.time * cycleSpeed, variation));
    }
}