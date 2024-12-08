using UnityEngine;
using UnityEngine.InputSystem;

public class SideScrollingMovement : MonoBehaviour
{
    public float m_speed = 5f;
    public Vector2 m_move;
    public float m_jumpForce = 10f;
    public Transform m_groundCheck;
    public LayerMask m_groundLayer;

    private Rigidbody m_rb;
    private bool isGrounded;
    private float m_groundCheckRadius = 0.2f;

    private void Start()
    {
        m_rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        isGrounded = Physics.OverlapSphere(m_groundCheck.position, m_groundCheckRadius, m_groundLayer).Length > 0;
    }

    private void FixedUpdate()
    {
        m_rb.linearVelocity = new Vector2(m_move.x * m_speed, m_rb.linearVelocity.y);
    }

    public void OnMove(InputAction.CallbackContext _context)
    {
        m_move = _context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext _context)
    {
        if (_context.performed && isGrounded)
        {
            m_rb.linearVelocity = new Vector2(m_rb.linearVelocity.x, m_jumpForce);
        }
    }

    public void OnCrouch(InputAction.CallbackContext _context)
    {
        if (_context.performed && isGrounded)
        {
            Debug.Log("Crouching");
        }
        else if (_context.canceled && isGrounded)
        {
            Debug.Log("Standing up");
        }
    }
}
