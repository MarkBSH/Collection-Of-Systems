using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : MonoBehaviour
{
    #region Unity Methods

    protected virtual void Start()
    {
        GetComponents();
        SetTarget(GameObject.FindGameObjectWithTag("Player").transform);
        SetSpeed();
        NewPath();
        SetHealth();
        SetAttackPoint(transform.Find("AttackPoint"));
    }

    protected virtual void Update()
    {
        GoToTarget();
        Attack();
        CanSeePlayer();
    }

    #endregion

    #region Components

    protected NavMeshAgent m_NavMeshAgent;
    private Animator m_Animator;

    protected virtual void GetComponents()
    {
        m_NavMeshAgent = GetComponent<NavMeshAgent>();
    }

    #endregion

    #region Movement

    [SerializeField] private Transform m_Target;
    [SerializeField] private float m_Speed;
    [SerializeField] private float m_MaxRange;
    [SerializeField] private float m_MinRange;
    private float m_Distance;

    protected virtual void SetTarget(Transform _Target)
    {
        m_Target = _Target;
    }

    private void SetSpeed()
    {
        m_NavMeshAgent.speed = m_Speed;
    }

    private void GetDistance()
    {
        m_Distance = m_NavMeshAgent.remainingDistance;
    }

    protected virtual void GoToTarget()
    {
        // IsWalking();
        if (CalculatePath())
        {
            GetDistance();

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

    protected bool CalculatePath()
    {
        NavMesh.CalculatePath(transform.position, m_Target.transform.position, NavMesh.AllAreas, m_Path);
        if (m_Path.status != NavMeshPathStatus.PathComplete)
        {
            Debug.Log(m_Path.status);
            return false;
        }
        return m_Path.status == NavMeshPathStatus.PathComplete;
    }

    #endregion

    #region Health

    [SerializeField] private float m_MaxHealth = 100f;
    public float m_CurrentHealth;

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

    [SerializeField] private Transform m_AttackPoint;
    [SerializeField] private GameObject m_Projectile;
    [SerializeField] private float m_AttackSpeed;
    private float m_AttackTimer;

    protected virtual void SetAttackPoint(Transform _attackPoint)
    {
        m_AttackPoint = _attackPoint;
    }

    protected virtual void Attack()
    {
        m_AttackTimer += Time.deltaTime;
        if (m_AttackTimer >= m_AttackSpeed && m_CanSeeTarget)
        {
            if (m_Distance < m_MaxRange && m_Distance > m_MinRange)
            {
                Shoot();
                m_AttackTimer = 0;
            }
        }
    }

    private void Shoot()
    {
        // IsAttacking();
        Instantiate(m_Projectile, transform.position, Quaternion.identity);
    }

    #endregion

    #region Line of Sight

    protected bool m_CanSeeTarget;

    protected virtual void CanSeePlayer()
    {
        Vector3 direction = m_Target.transform.position - transform.position;
        if (Physics.Raycast(transform.position, direction, out RaycastHit hit))
        {
            if (hit.collider.gameObject == m_Target)
            {
                m_CanSeeTarget = true;
            }
            else
            {
                m_CanSeeTarget = false;
            }
        }
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
}
