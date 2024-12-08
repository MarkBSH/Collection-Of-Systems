using UnityEngine;
using UnityEngine.InputSystem;

enum Direction
{
    None,
    forward,
    back,
    Left,
    Right
}

public class WASDGridBasedMovement : MonoBehaviour
{
    private Direction m_Direction;
    public float m_MoveTime = 0.5f;
    private float m_MoveTimer;
    private Vector3 m_TargetPosition;

    private void Start()
    {
        m_TargetPosition = transform.position;
    }

    private void Update()
    {
        Debug.Log(m_MoveTimer / m_MoveTime);
        if (m_Direction != Direction.None)
        {
            Vector3 moveDirection = Vector3.zero;
            switch (m_Direction)
            {
                case Direction.forward:
                    moveDirection = Vector3.forward;
                    break;
                case Direction.back:
                    moveDirection = Vector3.back;
                    break;
                case Direction.Left:
                    moveDirection = Vector3.left;
                    break;
                case Direction.Right:
                    moveDirection = Vector3.right;
                    break;
            }
            m_MoveTimer += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, m_TargetPosition, m_MoveTimer / m_MoveTime);
            if (m_MoveTimer >= m_MoveTime)
            {
                m_MoveTimer = 0;
                m_Direction = Direction.None;
                transform.position = m_TargetPosition;
            }
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (m_Direction == Direction.None)
        {
            Vector2 m_MoveInput = context.ReadValue<Vector2>();
            if (m_MoveInput.x > 0 && Mathf.Abs(m_MoveInput.x) > Mathf.Abs(m_MoveInput.y))
            {
                m_Direction = Direction.Right;
                m_TargetPosition += Vector3.right;
            }
            else if (m_MoveInput.x < 0 && Mathf.Abs(m_MoveInput.x) > Mathf.Abs(m_MoveInput.y))
            {
                m_Direction = Direction.Left;
                m_TargetPosition += Vector3.left;
            }
            else if (m_MoveInput.y > 0 && Mathf.Abs(m_MoveInput.y) > Mathf.Abs(m_MoveInput.x))
            {
                m_Direction = Direction.forward;
                m_TargetPosition += Vector3.forward;
            }
            else if (m_MoveInput.y < 0 && Mathf.Abs(m_MoveInput.y) > Mathf.Abs(m_MoveInput.x))
            {
                m_Direction = Direction.back;
                m_TargetPosition += Vector3.back;
            }
        }
    }
}
