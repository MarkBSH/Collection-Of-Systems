using UnityEngine;

public class RotateShowcase : MonoBehaviour
{
    [SerializeField] private float m_RotationSpeed = 0.6f;
    private bool isRotating = true;

    private void FixedUpdate()
    {
        if (isRotating)
        {
            transform.Rotate(Vector3.up, m_RotationSpeed, Space.Self);
        }
    }

    public void ToggleRotationState()
    {
        isRotating = !isRotating;
    }
}
