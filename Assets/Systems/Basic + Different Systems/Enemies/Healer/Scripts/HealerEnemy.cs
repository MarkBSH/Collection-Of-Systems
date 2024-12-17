using System.Collections;
using UnityEngine;

public class HealerEnemy : EnemyBase
{
    #region Unity Methods

    protected override void Start()
    {
        base.Start();
        StartCoroutine(ChangeEnemyToHeal());
    }

    protected override void Update()
    {
        base.Update();
    }

    #endregion

    #region Components



    #endregion

    #region Healing

    private GameObject _LowestHealthEnemy = null;
    private float _LowestHealth = float.MaxValue;

    private IEnumerator ChangeEnemyToHeal()
    {
        GameObject[] _Enemies = GameObject.FindGameObjectsWithTag("Enemy");


        foreach (GameObject enemy in _Enemies)
        {
            EnemyBase _EnemyBase = enemy.GetComponent<EnemyBase>();
            if (_EnemyBase != null && _EnemyBase.m_CurrentHealth < _LowestHealth)
            {
                _LowestHealth = _EnemyBase.m_CurrentHealth;
                _LowestHealthEnemy = enemy;
            }
        }

        if (_LowestHealthEnemy != null)
        {
            SetTarget(_LowestHealthEnemy);
            SetAttackPoint(_LowestHealthEnemy);
        }

        yield return new WaitForSeconds(1f);

        StartCoroutine(ChangeEnemyToHeal());
    }

    #endregion
}
