using System.Collections;
using UnityEngine;

public class HealerEnemy : EnemyBase
{
    #region Unity Methods

    protected override void Start()
    {
        StartCoroutine(ChangeEnemyToHeal());
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    #endregion

    #region Components



    #endregion

    #region Healing

    private GameObject m_LowestHealthEnemy = null;
    private float m_LowestHealth = float.MaxValue;

    private IEnumerator ChangeEnemyToHeal()
    {
        GameObject[] _Enemies = GameObject.FindGameObjectsWithTag("Enemy");

        for (int i = 0; i < _Enemies.Length; i++)
        {
            EnemyBase _EnemyBase = _Enemies[i].GetComponent<EnemyBase>();
            if (_EnemyBase != null && _EnemyBase.m_CurrentHealth < m_LowestHealth)
            {
                m_LowestHealth = _EnemyBase.m_CurrentHealth;
                m_LowestHealthEnemy = _Enemies[i];
            }
        }

        if (m_LowestHealthEnemy != null)
        {
            SetTarget(m_LowestHealthEnemy);
            SetAttackPoint(m_LowestHealthEnemy);
        }

        yield return new WaitForSeconds(1f);

        m_LowestHealth = float.MaxValue;

        StartCoroutine(ChangeEnemyToHeal());
    }

    #endregion
}
