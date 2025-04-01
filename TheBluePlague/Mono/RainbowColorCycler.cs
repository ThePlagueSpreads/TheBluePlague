using UnityEngine.Serialization;

namespace TheBluePlague.Mono;

using UnityEngine;
using UnityEngine.UI;

public class RainbowColorCycler : MonoBehaviour
{
    public Graphic[] graphics;
    public float cycleSpeed = 1f;

    private float _hue;
    
    void Update()
    {
        _hue += Time.deltaTime * cycleSpeed;

        if (_hue > 1f)
        {
            _hue = 0f;
        }

        var rainbowColor = Color.HSVToRGB(_hue, 1f, 1f);

        foreach (var graphic in graphics)
        {
            graphic.color = rainbowColor;
        }
    }
}