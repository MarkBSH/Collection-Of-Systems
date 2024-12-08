using UnityEngine;

public class RotateShowcase : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 0.6f;
    private bool isRotating = true;

    private void FixedUpdate()
    {
        if (isRotating)
        {
            transform.Rotate(Vector3.up, rotationSpeed);
        }
    }

    public void StartRotation()
    {
        isRotating = true;
    }

    public void StopRotation()
    {
        isRotating = false;
    }
}
