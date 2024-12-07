using UnityEngine;

public class RotateShowcase : MonoBehaviour
{
    private float m_RotationSpeed = 0.6f;

    private void FixedUpdate()
    {
        transform.Rotate(Vector3.up, m_RotationSpeed);
    }
}
