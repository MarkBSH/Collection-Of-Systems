using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonMovement : MonoBehaviour
{
    private GameObject m_Camera;
    public float m_speed = 5.0f;
    private Vector2 m_MoveInput;
    public float gravity = -9.81f;
    public CharacterController controller;
    private bool isGrounded;
    public Transform m_groundCheck;
    public float m_groundCheckRadius = 0.4f;
    public LayerMask m_groundLayer;

    private Vector3 velocity;

    private void Start()
    {
        m_Camera = Camera.main.gameObject;
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        // Check if the player is grounded
        CheckGroundStatus();

        // Apply gravity if not grounded
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Small value to keep the player grounded
        }

        // Move the player
        Vector3 cameraForward = m_Camera.transform.forward;
        cameraForward.y = 0; // Ignore the y component of the camera's forward vector
        cameraForward.Normalize(); // Normalize to maintain consistent movement speed

        Vector3 move = cameraForward * m_MoveInput.y + transform.right * m_MoveInput.x;
        controller.Move(move * m_speed * Time.deltaTime);

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    public void OnMove(InputAction.CallbackContext _context)
    {
        m_MoveInput = _context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext _context)
    {
        if (_context.performed && isGrounded)
        {
            velocity.y = Mathf.Sqrt(-2 * gravity * 3.0f);
        }
    }

    private void CheckGroundStatus()
    {
        isGrounded = Physics.CheckSphere(m_groundCheck.position, m_groundCheckRadius, m_groundLayer);
    }
}
