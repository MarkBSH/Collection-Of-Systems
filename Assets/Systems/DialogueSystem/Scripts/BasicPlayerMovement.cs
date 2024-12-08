using UnityEngine;
using UnityEngine.InputSystem;

public class BasicPlayerMovement : MonoBehaviour
{
    public static float m_Speed = 5f;
    private Rigidbody m_Rigidbody;
    private Vector3 m_Input;

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    public void OnMove(InputAction.CallbackContext _context)
    {
        Vector2 input = _context.ReadValue<Vector2>();
        m_Input = new Vector3(input.x, 0, input.y);
    }

    private void FixedUpdate()
    {
        Vector3 _movement = m_Input * (Time.fixedDeltaTime * m_Speed);
        m_Rigidbody.MovePosition(transform.position + _movement);
    }
}
