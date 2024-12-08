using UnityEngine;
using UnityEngine.InputSystem;

public class WASDTopDown : MonoBehaviour
{
    #region Unity Methods

    private void Start()
    {
        GetComponents();
    }

    private void FixedUpdate()
    {
        MoveCharacter();
    }

    #endregion

    #region Components

    private Rigidbody m_RB;

    private void GetComponents()
    {
        m_RB = GetComponent<Rigidbody>();
        if (m_RB == null)
        {
            DebugWarning("No Rigidbody component found. Please add a Rigidbody component to the GameObject");
        }
    }

    #endregion

    #region Movement

    public float m_Speed = 5f;
    private Vector2 m_MoveInput;

    public void OnMove(InputAction.CallbackContext context)
    {
        m_MoveInput = context.ReadValue<Vector2>();
    }

    private void MoveCharacter()
    {
        m_RB.linearVelocity = m_MoveInput * m_Speed;
    }

    #endregion

    #region Debugging

    private void DebugWarning(string _warning)
    {
        Debug.LogWarning("Warning: " + _warning);
    }

    #endregion
}
