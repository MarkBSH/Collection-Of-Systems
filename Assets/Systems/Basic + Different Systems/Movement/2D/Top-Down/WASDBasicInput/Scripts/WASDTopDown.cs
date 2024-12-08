using UnityEngine;
using UnityEngine.InputSystem;

public class WASDTopDown : MonoBehaviour
{
    private Rigidbody m_rb;
    public float speed = 5f;
    private Vector2 m_move;

    private void Start()
    {
        m_rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        m_rb.linearVelocity = new Vector3(m_move.x, 0, m_move.y) * speed;
    }

    public void OnMove(InputAction.CallbackContext _context)
    {
        m_move = _context.ReadValue<Vector2>();
    }
}
