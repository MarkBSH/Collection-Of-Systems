using UnityEngine;
using UnityEngine.InputSystem;

public class WASDGridBasedMovement : MonoBehaviour
{
    #region Unity Methods

    private void Start()
    {
        SetStartPosition();
    }

    private void Update()
    {
        MoveCharacter();
    }

    #endregion

    #region Components



    #endregion

    #region Movement

    enum Direction
    {
        None,
        forward,
        back,
        Left,
        Right
    }
    private Direction m_Direction;
    private Vector3 m_TargetPosition;

    public float m_MoveTime = 0.5f;
    private float m_MoveTimer;

    private void SetStartPosition()
    {
        m_TargetPosition = transform.position;
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
        else
        {
            DebugWarning("Character is already moving");
        }
    }

    private void MoveCharacter()
    {
        if (m_Direction != Direction.None)
        {
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

    #endregion

    #region Debugging

    private void DebugWarning(string _warning)
    {
        Debug.LogWarning("Warning: " + _warning);
    }

    #endregion
}
