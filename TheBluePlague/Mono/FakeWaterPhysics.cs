using UnityEngine;

namespace TheBluePlague.Mono;

public class FakeWaterPhysics : MonoBehaviour
{
    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.AddExplosionForce(0.1f, new Vector3(0, 0, 0), 50, 0.1f, ForceMode.VelocityChange);
    }

    private void FixedUpdate()
    {
        if (transform.position.y < Ocean.GetOceanLevel())
        {
            _rb.AddForce(Vector3.up * 5, ForceMode.Acceleration);
            _rb.drag = 1f;
            _rb.angularDrag = 0.5f;
        }
        else
        {
            _rb.AddForce(Vector3.down * 9.8f, ForceMode.Acceleration);
            _rb.drag = 0;
            _rb.angularDrag = 0.05f;
        }
    }
}