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

    private NavMeshAgent m_Agent;

    private void GetComponents()
    {
        m_Agent = GetComponent<NavMeshAgent>();
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
        return false;
    }

    #endregion

    #region Movement

    public float m_Speed = 5f;

    private void SetSpeed()
    {
        m_Agent.speed = m_Speed;
    }

    #endregion

    #region Pathfinding

    private NavMeshPath m_Path;
    private Vector3 m_Target;

    private void NewPath()
    {
        m_Path = new NavMeshPath();
    }

    private bool CalculatePath()
    {
        NavMesh.CalculatePath(transform.position, m_Target, NavMesh.AllAreas, m_Path);
        return m_Path.status == NavMeshPathStatus.PathComplete;
    }

    private void SetDestination(RaycastHit _hit)
    {
        m_Target = _hit.point;
        m_Agent.SetDestination(m_Target);
    }

    #endregion
}
