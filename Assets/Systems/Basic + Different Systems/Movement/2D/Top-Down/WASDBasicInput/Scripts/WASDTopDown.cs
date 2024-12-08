using UnityEngine;
using UnityEngine.InputSystem;

public class WASDTopDown : MonoBehaviour
{
    private Rigidbody m_RB;
    public float m_Speed = 5f;
    private Vector2 m_MoveInput;

    private void Start()
    {
        m_RB = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        m_RB.linearVelocity = m_MoveInput * m_Speed;

    }

    public void OnMove(InputAction.CallbackContext context)
    {
        m_MoveInput = context.ReadValue<Vector2>();
    }
}
