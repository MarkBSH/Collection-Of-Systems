using UnityEngine;
using UnityEngine.AI;

public class MouseInputMovement : MonoBehaviour
{
    #region Unity Methods

    private void Start()
    {
        GetComponents();
        SetSpeed();
        NewPath();
    }

    private void Update()
    {
        MouseInputCheck();
    }

    #endregion

    #region Components

    private NavMeshAgent m_agent;

    private void GetComponents()
    {
        m_agent = GetComponent<NavMeshAgent>();
        if (m_agent == null)
        {
            DebugWarning("No NavMeshAgent component found. Please add a NavMeshAgent component to the GameObject");
        }
    }

    #endregion

    #region Mouse Input

    private void MouseInputCheck()
    {
        if (Input.GetMouseButton(0))
        {
            if (CheckRay())
            {
                if (CalculatePath())
                {
                    SetDestination(m_Hit);
                }
            }
        }
    }

    #endregion

    #region Raycasting

    private RaycastHit m_Hit;

    private bool CheckRay()
    {
        Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(_ray, out m_Hit))
        {
            return true;
        }
        DebugWarning("Raycast did not hit anything");
        return false;
    }

    #endregion

    #region Movement

    public float m_speed = 5f;

    private void SetSpeed()
    {
        m_agent.speed = m_speed;
    }

    #endregion

    #region Pathfinding

    private NavMeshPath m_path;
    private Vector3 m_target;

    private void NewPath()
    {
        m_path = new NavMeshPath();
    }

    private bool CalculatePath()
    {
        NavMesh.CalculatePath(transform.position, m_target, NavMesh.AllAreas, m_path);
        DebugWarning("Path is incomplete");
        return m_path.status == NavMeshPathStatus.PathComplete;
    }

    private void SetDestination(RaycastHit _hit)
    {
        m_target = _hit.point;
        m_agent.SetDestination(m_target);
    }

    #endregion

    #region Debugging

    private void DebugWarning(string _message)
    {
        Debug.LogWarning("Warning: " + _message);
    }

    #endregion
}
