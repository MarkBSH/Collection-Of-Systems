using UnityEngine;
using UnityEngine.AI;

public class MouseInputMovement : MonoBehaviour
{
    private NavMeshAgent m_agent;
    private NavMeshPath m_path;
    private Vector3 m_target;
    private Camera m_mainCamera;
    public float m_speed = 5f;

    private void Start()
    {
        m_agent = GetComponent<NavMeshAgent>();
        m_agent.speed = m_speed; // Set the agent's speed
        m_path = new NavMeshPath();
        m_mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray _ray = m_mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(_ray, out RaycastHit _hit))
            {
                NavMesh.CalculatePath(transform.position, _hit.point, NavMesh.AllAreas, m_path);

                if (m_path.status == NavMeshPathStatus.PathComplete)
                {
                    m_target = _hit.point;
                    m_agent.SetDestination(m_target);
                }
                else
                {
                    Debug.Log("Path is incomplete");
                }
            }
        }
    }
}
