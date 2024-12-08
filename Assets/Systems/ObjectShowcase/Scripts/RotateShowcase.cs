using UnityEngine;

public class RotateShowcase : MonoBehaviour
{
    #region Unity Methods

    private void FixedUpdate()
    {
        RotateObject();
    }

    #endregion

    #region Components



    #endregion

    #region Rotation

    public float m_RotationSpeed = 0.6f;
    private bool isRotating = true;

    private void RotateObject()
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

    #endregion

    #region Debugging

    private void DebugWarning(string _warning)
    {
        Debug.LogWarning("Warning: " + _warning);
    }

    #endregion
}
