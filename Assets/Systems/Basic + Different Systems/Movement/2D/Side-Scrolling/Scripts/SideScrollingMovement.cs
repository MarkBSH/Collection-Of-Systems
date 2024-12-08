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

    public void OnMove(InputAction.CallbackContext _context)
    {
        m_MoveInput = _context.ReadValue<Vector2>();
    }

    private void MoveCharacter()
    {
        m_RB.linearVelocity = new Vector2(m_MoveInput.x * m_Speed, m_RB.linearVelocity.y);
    }

    #endregion

    #region Jumping

    public float m_JumpForce = 10f;
    private Transform m_GroundCheck;
    public LayerMask m_GroundLayer;
    private bool isGrounded;
    private float m_GroundCheckRadius = 0.2f;

    private void GetGroundCheck()
    {
        m_GroundCheck = transform.Find("GroundCheck");
        if (m_GroundCheck == null)
        {
            DebugWarning("No GroundCheck object found. Please create an empty GameObject and name it 'GroundCheck'");
        }
    }

    public void OnJump(InputAction.CallbackContext _context)
    {
        if (_context.performed && isGrounded && !isCrouching)
        {
            m_RB.linearVelocity = new Vector2(m_RB.linearVelocity.x, m_JumpForce);
        }
    }

    private void CheckGroundStatus()
    {
        isGrounded = Physics.CheckSphere(m_GroundCheck.position, m_GroundCheckRadius, m_GroundLayer);
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
