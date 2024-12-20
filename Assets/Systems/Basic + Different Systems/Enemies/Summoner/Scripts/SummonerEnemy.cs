using UnityEngine;

public class SummonerEnemy : EnemyBase
{
    #region Unity Methods

    protected override void Update()
    {
        base.Update();
        SummonEnemy();
    }

    #endregion

    #region Components



    #endregion

    #region Movement

    [SerializeField] private float m_MoveRange;

    private void GoToRandomPoint()
    {
        if (m_NavMeshAgent.remainingDistance < 0.5f)
        {
            Vector3 randomDirection = Random.insideUnitSphere * m_MoveRange;
            randomDirection += transform.position;
            if (CalculatePath())
            {
                GameObject tempTarget = new GameObject("TempTarget");
                tempTarget.transform.position = randomDirection;
                SetTarget(tempTarget.transform);
                Destroy(tempTarget);
            }
        }
    }

    #endregion

    #region Summoning

    [SerializeField] private float m_SummonDistance;
    [SerializeField] private float m_SummonRadius;

    protected override void CanSeePlayer()
    {

    }

    private void SummonEnemy()
    {
        Vector3 summonPosition = transform.position + transform.forward * m_SummonDistance;
        if (!Physics.CheckSphere(summonPosition, m_SummonRadius))
        {
            m_CanSeeTarget = true;
            GameObject summonPoint = new("SummonPoint");
            summonPoint.transform.position = summonPosition;
            SetAttackPoint(summonPoint.transform);
            Destroy(summonPoint);
        }
        else
        {
            m_CanSeeTarget = false;
        }
    }

    #endregion
}
