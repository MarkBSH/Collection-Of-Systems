using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : MonoBehaviour
{
    #region Unity Methods

    private void Start()
    {
        GetComponents();
        SetTarget();
        SetSpeed();
        NewPath();
        SetHealth();
    }

    private void Update()
    {
        GetDistance();
        GoToTarget();
        Attack();
    }

    #endregion

    #region Components

    private NavMeshAgent m_NavMeshAgent;
    private Animator m_Animator;

    private void GetComponents()
    {
        m_NavMeshAgent = GetComponent<NavMeshAgent>();
        if (m_NavMeshAgent == null)
        {
            DebugWarning("NavMeshAgent component is null.");
        }
        m_Animator = GetComponent<Animator>();
        if (m_Animator == null)
        {
            DebugWarning("Animator component is null.");
        }
    }

    #endregion

    #region Movement

    private GameObject m_Target;
    [SerializeField] private float m_Speed;
    [SerializeField] private float m_MaxRange;
    [SerializeField] private float m_MinRange;
    private float m_Distance;

    public void SetTarget()
    {
        m_Target = GameObject.FindGameObjectWithTag("Player");
        if (m_Target == null)
        {
            DebugWarning("Player not found.");
        }
    }

    private void SetSpeed()
    {
        m_NavMeshAgent.speed = m_Speed;
    }

    private void GetDistance()
    {
        m_Distance = Vector3.Distance(transform.position, m_Target.transform.position);
    }

    private void GoToTarget()
    {
        IsWalking();
        if (CalculatePath())
        {
            if (m_Distance > m_MaxRange)
            {
                m_NavMeshAgent.SetDestination(m_Target.transform.position);
            }
            else if (m_Distance < m_MinRange)
            {
                Vector3 directionAwayFromTarget = transform.position - m_Target.transform.position;
                Vector3 newPosition = transform.position + directionAwayFromTarget;
                m_NavMeshAgent.SetDestination(newPosition);
            }
            else
            {
                m_NavMeshAgent.SetDestination(transform.position);
            }
        }
    }

    #endregion

    #region Pathfinding

    private NavMeshPath m_Path;

    private void NewPath()
    {
        m_Path = new NavMeshPath();
    }

    private bool CalculatePath()
    {
        NavMesh.CalculatePath(transform.position, m_Target.transform.position, NavMesh.AllAreas, m_Path);
        if (m_Path.status != NavMeshPathStatus.PathComplete)
        {
            DebugWarning("Path could not be calculated");
        }
        return m_Path.status == NavMeshPathStatus.PathComplete;
    }

    #endregion

    #region Health

    [SerializeField] private float m_MaxHealth = 100f;
    private float m_CurrentHealth;

    private void SetHealth()
    {
        m_CurrentHealth = m_MaxHealth;
    }

    public void ChangeHealth(float _damage)
    {
        m_CurrentHealth -= _damage;
        if (m_CurrentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    #endregion

    #region Attack

    private GameObject m_AttackPoint;
    [SerializeField] private GameObject m_Projectile;
    [SerializeField] private float m_AttackSpeed;
    private float m_AttackTimer;

    private void AttackPoint()
    {
        m_AttackPoint = transform.Find("AttackPoint").gameObject;
        if (m_AttackPoint == null)
        {
            DebugWarning("Attack point not found.");
        }
    }

    private void Attack()
    {
        m_AttackTimer += Time.deltaTime;
        if (m_AttackTimer >= m_AttackSpeed)
        {
            if (m_Distance > m_MaxRange && m_Distance < m_MinRange)
            {
                m_AttackTimer = 0;
                Shoot();
            }
        }
    }

    private void Shoot()
    {
        IsAttacking();
        Instantiate(m_Projectile, transform.position, Quaternion.identity);
    }

    #endregion

    #region Animation

    private void IsWalking()
    {
        if (m_NavMeshAgent.velocity.magnitude > 0)
        {
            m_Animator.SetBool("IsWalking", true);
        }
        else
        {
            m_Animator.SetBool("IsWalking", false);
        }
    }

    private void IsAttacking()
    {
        m_Animator.SetTrigger("IsAttacking");
    }

    #endregion

    #region Debugging

    private void DebugWarning(string _warning)
    {
        Debug.LogWarning("Warning: " + _warning);
    }

    #endregion
}
