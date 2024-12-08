using UnityEngine;
using UnityEngine.InputSystem;

public class SideScrollingMovement : MonoBehaviour
{
    #region Unity Methods

    private void Start()
    {
        GetComponents();
        GetGroundCheck();
    }

    private void Update()
    {
        CheckGroundStatus();
    }

    private void FixedUpdate()
    {
        MoveCharacter();
    }

    #endregion

    #region Components

    private Rigidbody m_rb;

    private void GetComponents()
    {
        m_rb = GetComponent<Rigidbody>();
        if (m_rb == null)
        {
            DebugWarning("No Rigidbody component found. Please add a Rigidbody component to the GameObject");
        }
    }

    #endregion

    #region Movement

    public float m_speed = 5f;
    private Vector2 m_move;

    public void OnMove(InputAction.CallbackContext _context)
    {
        m_move = _context.ReadValue<Vector2>();
    }

    private void MoveCharacter()
    {
        m_rb.linearVelocity = new Vector2(m_move.x * m_speed, m_rb.linearVelocity.y);
    }

    #endregion

    #region Jumping

    public float m_jumpForce = 10f;
    private Transform m_groundCheck;
    public LayerMask m_groundLayer;
    private bool isGrounded;
    private float m_groundCheckRadius = 0.2f;

    private void GetGroundCheck()
    {
        m_groundCheck = transform.Find("GroundCheck");
        if (m_groundCheck == null)
        {
            DebugWarning("No GroundCheck object found. Please create an empty GameObject and name it 'GroundCheck'");
        }
    }

    public void OnJump(InputAction.CallbackContext _context)
    {
        if (_context.performed && isGrounded && !isCrouching)
        {
            m_rb.linearVelocity = new Vector2(m_rb.linearVelocity.x, m_jumpForce);
        }
    }

    private void CheckGroundStatus()
    {
        isGrounded = Physics.CheckSphere(m_groundCheck.position, m_groundCheckRadius, m_groundLayer);
    }

    #endregion

    #region Crouching

    private bool isCrouching = false;

    public void OnCrouch(InputAction.CallbackContext _context)
    {
        if (_context.performed && isGrounded)
        {
            isCrouching = true;
            Debug.Log("Crouching");
        }
        else if (_context.canceled && isGrounded)
        {
            isCrouching = false;
            Debug.Log("Standing up");
        }
    }

    #endregion

    #region Debugging

    private void DebugWarning(string _message)
    {
        Debug.LogWarning("Warning: " + _message);
    }

    #endregion
}
