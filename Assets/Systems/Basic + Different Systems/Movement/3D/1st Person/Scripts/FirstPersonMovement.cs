using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonMovement : MonoBehaviour
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
        MoveCharacter();
        ApplyGravity();
    }

    #endregion

    #region Components

    private CharacterController m_Controller;

    private void GetComponents()
    {
        m_Controller = GetComponent<CharacterController>();
        if (m_Controller == null)
        {
            DebugWarning("No CharacterController component found. Please add a CharacterController component to the GameObject");
        }
    }

    #endregion

    #region Movement

    public float m_Speed = 5.0f;
    private Vector2 m_MoveInput;

    public void OnMove(InputAction.CallbackContext _context)
    {
        m_MoveInput = _context.ReadValue<Vector2>();
        if (m_MoveInput.magnitude > 1)
        {
            m_MoveInput.Normalize();
        }
    }

    private void MoveCharacter()
    {
        Vector3 move = transform.forward * m_MoveInput.y + transform.right * m_MoveInput.x;
        m_Controller.Move(move * m_Speed * Time.deltaTime);
    }

    #endregion

    #region Jumping

    private bool isGrounded;
    public Transform m_GroundCheck;
    public float m_GroundCheckRadius = 0.4f;
    public LayerMask m_GroundLayer;

    public float m_Gravity = -9.81f;
    private Vector3 m_Velocity;

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
        if (_context.performed && isGrounded)
        {
            m_Velocity.y = Mathf.Sqrt(-2 * m_Gravity * 3.0f);
        }
    }

    private void CheckGroundStatus()
    {
        isGrounded = Physics.CheckSphere(m_GroundCheck.position, m_GroundCheckRadius, m_GroundLayer);
    }

    private void ApplyGravity()
    {
        if (isGrounded && m_Velocity.y < 0)
        {
            m_Velocity.y = -2f;
        }

        m_Velocity.y += m_Gravity * Time.deltaTime;
        m_Controller.Move(m_Velocity * Time.deltaTime);
    }

    #endregion

    #region Debugging

    private void DebugWarning(string _warning)
    {
        Debug.LogWarning("Warning: " + _warning);
    }

    #endregion
}
