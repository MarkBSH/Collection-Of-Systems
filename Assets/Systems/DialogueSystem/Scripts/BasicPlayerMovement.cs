using UnityEngine;
using UnityEngine.InputSystem;

public class BasicPlayerMovement : MonoBehaviour
{
    public static float m_Speed = 5f;
    private Rigidbody m_Rigidbody;
    private Vector2 m_Input;

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    public void OnMove(InputAction.CallbackContext _context)
    {
        m_Input = _context.ReadValue<Vector2>();
    }

    private void Update()
    {
        Vector3 _movement = new Vector3(m_Input.x, 0, m_Input.y) * (Time.deltaTime * m_Speed);
        m_Rigidbody.MovePosition(transform.position + _movement);
    }
}
