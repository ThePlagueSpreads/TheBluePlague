using UnityEngine;
using UnityStandardAssets.ImageEffects;

namespace TheBluePlague.Mono;

public class IncreaseGrayscale : MonoBehaviour
{
    private float _grayscaleValue;

    private void Update()
    {
        _grayscaleValue += Time.deltaTime * 0.02f;
        var gray = MainCamera.camera.GetComponent<Grayscale>();
        gray.effectAmount = _grayscaleValue;
        gray.enabled = true;
    }
}