using UnityEngine;

public class FlyingEnemyModel : MonoBehaviour
{
    #region Unity Methods

    private void Start()
    {

    }

    private void Update()
    {
        MaintainHeight();
    }

    #endregion

    #region Components



    #endregion

    #region Height

    [SerializeField] private float m_Height;

    private void MaintainHeight()
    {
        Vector3 position = transform.position;
        position.y = m_Height;
        transform.position = position;
    }

    #endregion

    #region Debugging

    private void DebugWarning(string _warning)
    {
        Debug.LogWarning("Warning: " + _warning);
    }

    #endregion
}
